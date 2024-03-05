using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.UI.WebControls;
using System.Windows;

namespace GestionCommande.Services.Upload
{
    public class Upload
    {
        public string RegisterToFTP(HttpPostedFileBase fichier, string name)
        {
            try
            {
                var ftpUsername = ConfigurationManager.AppSettings["FTPIdentifiant"];
                var ftpPassword = ConfigurationManager.AppSettings["FTPPassword"];
                string cheminLocal = StockTempFileToContent(fichier, name);

                // Adresse du serveur FTP et chemin du dossier sur le serveur où vous souhaitez téléverser le fichier
                string adresseServeurFTP = "ftp://ftp.cluster029.hosting.ovh.net/";


                // TODO :  il faut renommer le nom du fichier qui servira a stocker l'image 
                //Trouver une solution pour que cette fonction s'adapte si c'est une image ou si 
                // c'est sur un document pdf /doc
                string cheminSurServeur = "GestionCommande/"; 

                // Composez le chemin complet sur le serveur FTP
                string cheminServeur = adresseServeurFTP + cheminSurServeur
                    + DateTime.Now.Date.ToString("dd-MM-yyyy") + "_" + name
                    + Path.GetExtension(fichier.FileName);

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(cheminServeur);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // Configuration des informations d'authentification
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

                // Lecture du fichier local
                using (Stream fileStream = fichier.InputStream)
                using (Stream ftpStream = request.GetRequestStream())
                {
                    // Copie du contenu du fichier local vers le serveur FTP
                    byte[] buffer = new byte[1024];
                    int bytesRead;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ftpStream.Write(buffer, 0, bytesRead);
                    }
                }
                DeleteTempFileInContent(cheminLocal);
                return cheminSurServeur
                    + DateTime.Now.Date.ToString("dd-MM-yyyy") + "_" + name
                    + Path.GetExtension(fichier.FileName);
            }
            catch (Exception ex)
            {
                //Ecrire un message de log dans un fichier le cas échéant
            }
            return null; 
        }
        // methode pour stoker provisoirement le un fichier dans le dossier content
        

        private string StockTempFileToContent (HttpPostedFileBase fichier, string name)
        {
            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Content\\Upload\\"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Content\\Upload\\");
                }
                //string extension = Path.GetExtension(file.FileName);
                string nomFichier = DateTime.Now.Date.ToString("dd-MM-yyyy") + "_" + name;
                fichier.SaveAs(HostingEnvironment.MapPath("~/Content/") + nomFichier);
                return AppDomain.CurrentDomain.BaseDirectory + "Content\\Upload\\" + nomFichier + Path.GetExtension(fichier.FileName);
            }
            catch
            {
                //Task.Run(() => new LogService().FileLogService("Erreur dans la fonction create (DIController, impossible d'ajouter un devis"));
            }
            return null;
        }
        private void DeleteTempFileInContent(string path)
        {
            if (File.Exists(Path.Combine(path)))
            {
                // If file found, delete it
                File.Delete(Path.Combine(path));
                Console.WriteLine("File deleted.");
            }
            else Console.WriteLine("File not found");
        }
    }
   
}