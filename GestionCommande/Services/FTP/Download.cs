using FluentFTP;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using WebGrease.Css;

namespace GestionCommande.Services.FTP
{
    public class Download
    {
        public byte[] DownloadFile(string chemin)
        {
            var ftpUsername = ConfigurationManager.AppSettings["FTPIdentifiant"];
            var ftpPassword = ConfigurationManager.AppSettings["FTPPassword"];

            var ftpClient = new FtpClient("ftp://ftp.cluster029.hosting.ovh.net/");
            ftpClient.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            ftpClient.Connect();

            using (var stream = new MemoryStream())
            {
                if (ftpClient.DownloadStream(stream, chemin))
                {
                    byte[] imageBytes = stream.ToArray();
                    return imageBytes;
                }
            }
            return null;
        }

       
    }
}