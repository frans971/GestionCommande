USE [master]
GO
/****** Object:  Database [GestionCommande]    Script Date: 28/07/2023 12:25:41 ******/
CREATE DATABASE [GestionCommande]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GestionCommande', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\GestionCommande.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GestionCommande_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\GestionCommande_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [GestionCommande] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GestionCommande].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GestionCommande] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GestionCommande] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GestionCommande] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GestionCommande] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GestionCommande] SET ARITHABORT OFF 
GO
ALTER DATABASE [GestionCommande] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GestionCommande] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GestionCommande] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GestionCommande] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GestionCommande] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GestionCommande] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GestionCommande] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GestionCommande] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GestionCommande] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GestionCommande] SET  ENABLE_BROKER 
GO
ALTER DATABASE [GestionCommande] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GestionCommande] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GestionCommande] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GestionCommande] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GestionCommande] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GestionCommande] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GestionCommande] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GestionCommande] SET RECOVERY FULL 
GO
ALTER DATABASE [GestionCommande] SET  MULTI_USER 
GO
ALTER DATABASE [GestionCommande] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GestionCommande] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GestionCommande] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GestionCommande] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GestionCommande] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'GestionCommande', N'ON'
GO
ALTER DATABASE [GestionCommande] SET QUERY_STORE = OFF
GO
USE [GestionCommande]
GO
/****** Object:  Table [dbo].[Adresse]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adresse](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[adresse_libelle] [varchar](150) NOT NULL,
	[complement_adresse] [varchar](150) NULL,
	[id_commune] [int] NOT NULL,
	[id_utilisateur] [int] NOT NULL,
 CONSTRAINT [PK_Adresse] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[id] [int] IDENTITY(120324,1) NOT NULL,
	[id_utilisateur] [int] NOT NULL,
	[date_crea] [datetime] NOT NULL,
	[date_modif] [datetime] NULL,
	[carte_fidelite] [int] NULL,
	[pt_fidelite] [int] NULL,
	[created_by] [int] NOT NULL,
 CONSTRAINT [PK__Client] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Commandes]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Commandes](
	[id] [int] IDENTITY(10001,1) NOT NULL,
	[date_commande] [date] NULL,
	[date_livraison] [date] NULL,
	[id_etat] [int] NOT NULL,
	[id_client] [int] NOT NULL,
	[montant] [float] NOT NULL,
	[id_auteur] [int] NULL,
 CONSTRAINT [PK__Commande] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Commune]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Commune](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codePostale] [varchar](5) NOT NULL,
	[ville] [varchar](250) NOT NULL,
	[id_region] [int] NOT NULL,
	[date_crea] [datetime] NOT NULL,
 CONSTRAINT [PK_Commune] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Disponibilite]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Disponibilite](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date_disponibilite_deb] [datetime] NULL,
	[date_dispinbilite_fin] [datetime] NULL,
	[isPermanant] [bit] NOT NULL,
	[date_crea] [datetime] NOT NULL,
 CONSTRAINT [PK__Disponib] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Disponibilite_produit]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Disponibilite_produit](
	[id] [int] IDENTITY(2000,1) NOT NULL,
	[id_disponibilite] [int] NOT NULL,
	[id_produit] [int] NOT NULL,
	[date_crea] [datetime] NOT NULL,
 CONSTRAINT [PK_Disponibilite_produit] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[employee]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employee](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_utilisateur] [int] NOT NULL,
	[id_responsable] [int] NOT NULL,
	[id_service] [int] NOT NULL,
	[date_crea] [datetime] NOT NULL,
	[created_by] [int] NOT NULL,
 CONSTRAINT [PK_employee] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Etat]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Etat](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[libelle] [varchar](20) NOT NULL,
	[date_crea] [datetime] NOT NULL,
	[created_by] [datetime] NOT NULL,
 CONSTRAINT [PK__Etat__3213E83FDF10935B] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Etat_Produit]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Etat_Produit](
	[id] [int] NOT NULL,
	[id_etat] [int] NOT NULL,
	[id_produit] [int] NOT NULL,
	[date_crea] [datetime] NOT NULL,
	[created_by] [int] NOT NULL,
 CONSTRAINT [PK__Etat_Pro] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Etat_Pro] UNIQUE NONCLUSTERED 
(
	[id_etat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fidelite]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fidelite](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nom] [varchar](70) NOT NULL,
	[evenement] [varchar](50) NULL,
	[date_deb] [datetime] NULL,
	[date_fin] [datetime] NULL,
	[limit_client_prevu] [int] NULL,
	[limit_client_reel] [int] NULL,
	[point_avantage] [int] NOT NULL,
	[point_fidelite_bonus] [int] NULL,
	[remise_achat_pourcentage] [int] NULL,
	[remise_achat_euro] [int] NULL,
	[prix_min] [float] NULL,
	[prix_max] [float] NULL,
	[age_min] [int] NULL,
	[age_max] [int] NULL,
	[created_by] [int] NOT NULL,
	[modif_by] [int] NULL,
	[date_crea] [datetime] NOT NULL,
	[specification] [varchar](max) NULL,
	[commentaire] [varchar](max) NULL,
 CONSTRAINT [PK__Fidelite] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Fidelite] UNIQUE NONCLUSTERED 
(
	[nom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[libelle_genre] [varchar](25) NOT NULL,
	[date_crea] [datetime] NOT NULL,
 CONSTRAINT [PK__Genre] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Implantation]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Implantation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nom] [varchar](max) NOT NULL,
	[adresse_id] [int] NOT NULL,
	[date_crea] [datetime] NOT NULL,
	[created_by] [int] NOT NULL,
 CONSTRAINT [PK_implantation] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Livraison]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Livraison](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_typeLivraison] [int] NOT NULL,
	[creneau_horaire] [varchar](25) NOT NULL,
	[prix] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Livraison_commande]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Livraison_commande](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_livraison] [int] NOT NULL,
	[id_commande] [int] NOT NULL,
	[id_adresse] [int] NOT NULL,
 CONSTRAINT [PK__Livraison_Commande] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produit]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produit](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[libelle_produit] [varchar](30) NOT NULL,
	[prix_unitaire] [float] NULL,
	[reduction_pourcentage] [int] NULL,
	[reduction_euro] [numeric](5, 0) NULL,
	[description] [varchar](150) NULL,
	[image] [varchar](70) NULL,
	[id_etat] [int] NOT NULL,
	[date_crea] [datetime] NOT NULL,
 CONSTRAINT [PK__Produit] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produit_Commande]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produit_Commande](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_commande] [int] NULL,
	[id_produit] [int] NOT NULL,
	[date_crea] [datetime] NOT NULL,
	[prix] [float] NOT NULL,
 CONSTRAINT [PK_Produit_Commande] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profil]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profil](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[libelle_profil] [varchar](20) NOT NULL,
	[date_crea] [datetime] NULL,
 CONSTRAINT [PK__Profil] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Region]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Region](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[libelle] [varchar](50) NOT NULL,
	[date_crea] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[service]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[service](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[libelle] [varchar](100) NOT NULL,
	[commentaire] [varchar](max) NULL,
	[date_crea] [datetime] NOT NULL,
	[created_by] [int] NOT NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TokenPasswordUser]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TokenPasswordUser](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[token] [varchar](max) NOT NULL,
	[idUtilisateur] [int] NOT NULL,
	[validDate] [datetime] NOT NULL,
	[commentaire] [varchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Type_livraison]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type_livraison](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[libelle] [varchar](60) NOT NULL,
	[date_crea] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Profil]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Profil](
	[id_user_profil] [int] IDENTITY(1,1) NOT NULL,
	[id_profil] [int] NOT NULL,
	[id_utilisateur] [int] NOT NULL,
	[date_crea] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_user_profil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utilisateur]    Script Date: 28/07/2023 12:25:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utilisateur](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_genre] [int] NOT NULL,
	[identifiant] [varchar](30) NULL,
	[nom] [varchar](30) NOT NULL,
	[prenom] [varchar](30) NOT NULL,
	[date_naissance] [date] NULL,
	[num_tel] [varchar](10) NOT NULL,
	[mail] [varchar](200) NOT NULL,
	[password] [varchar](128) NULL,
	[date_crea] [datetime] NOT NULL,
	[date_modif] [datetime] NULL,
	[id_etat] [int] NOT NULL,
 CONSTRAINT [PK__Utilisateur] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Client] ADD  CONSTRAINT [DF_Client_date_crea]  DEFAULT (getdate()) FOR [date_crea]
GO
ALTER TABLE [dbo].[Commandes] ADD  CONSTRAINT [DF_Commandes_date_commande]  DEFAULT (getdate()) FOR [date_commande]
GO
ALTER TABLE [dbo].[Disponibilite] ADD  CONSTRAINT [DF_Disponibilite_date_crea]  DEFAULT (getdate()) FOR [date_crea]
GO
ALTER TABLE [dbo].[Disponibilite_produit] ADD  CONSTRAINT [DF_Disponibilite_produit_date_crea]  DEFAULT (getdate()) FOR [date_crea]
GO
ALTER TABLE [dbo].[Etat] ADD  CONSTRAINT [DF_Etat_date_crea]  DEFAULT (getdate()) FOR [date_crea]
GO
ALTER TABLE [dbo].[Fidelite] ADD  CONSTRAINT [DF__Fidelite__date_c__17F790F9]  DEFAULT (getdate()) FOR [date_crea]
GO
ALTER TABLE [dbo].[Genre] ADD  CONSTRAINT [DF_Genre_date_crea]  DEFAULT (getdate()) FOR [date_crea]
GO
ALTER TABLE [dbo].[Produit] ADD  CONSTRAINT [DF_Produit_date_crea]  DEFAULT (getdate()) FOR [date_crea]
GO
ALTER TABLE [dbo].[Produit_Commande] ADD  CONSTRAINT [DF_Produit_Commande_date_crea]  DEFAULT (getdate()) FOR [date_crea]
GO
ALTER TABLE [dbo].[Profil] ADD  CONSTRAINT [DF_Profil_date_crea]  DEFAULT (getdate()) FOR [date_crea]
GO
ALTER TABLE [dbo].[Type_livraison] ADD  CONSTRAINT [DF_Type_livraison_date_crea]  DEFAULT (getdate()) FOR [date_crea]
GO
ALTER TABLE [dbo].[User_Profil] ADD  CONSTRAINT [DF_User_Profil_date_crea]  DEFAULT (getdate()) FOR [date_crea]
GO
ALTER TABLE [dbo].[Utilisateur] ADD  CONSTRAINT [df_date_crea]  DEFAULT (getdate()) FOR [date_crea]
GO
ALTER TABLE [dbo].[Adresse]  WITH CHECK ADD  CONSTRAINT [FK_Adresse_Commune] FOREIGN KEY([id_commune])
REFERENCES [dbo].[Commune] ([id])
GO
ALTER TABLE [dbo].[Adresse] CHECK CONSTRAINT [FK_Adresse_Commune]
GO
ALTER TABLE [dbo].[Adresse]  WITH CHECK ADD  CONSTRAINT [FK_Adresse_Utilisateur] FOREIGN KEY([id_utilisateur])
REFERENCES [dbo].[Utilisateur] ([id])
GO
ALTER TABLE [dbo].[Adresse] CHECK CONSTRAINT [FK_Adresse_Utilisateur]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Utilisateur] FOREIGN KEY([id_utilisateur])
REFERENCES [dbo].[Utilisateur] ([id])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Utilisateur]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Utilisateur_Created_by] FOREIGN KEY([created_by])
REFERENCES [dbo].[Utilisateur] ([id])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Utilisateur_Created_by]
GO
ALTER TABLE [dbo].[Commandes]  WITH CHECK ADD  CONSTRAINT [FK_Commandes_Client] FOREIGN KEY([id_client])
REFERENCES [dbo].[Client] ([id])
GO
ALTER TABLE [dbo].[Commandes] CHECK CONSTRAINT [FK_Commandes_Client]
GO
ALTER TABLE [dbo].[Commandes]  WITH CHECK ADD  CONSTRAINT [FK_Commandes_Etat] FOREIGN KEY([id_etat])
REFERENCES [dbo].[Etat] ([id])
GO
ALTER TABLE [dbo].[Commandes] CHECK CONSTRAINT [FK_Commandes_Etat]
GO
ALTER TABLE [dbo].[Commandes]  WITH CHECK ADD  CONSTRAINT [FK_Commandes_Utilisateur] FOREIGN KEY([id_auteur])
REFERENCES [dbo].[Utilisateur] ([id])
GO
ALTER TABLE [dbo].[Commandes] CHECK CONSTRAINT [FK_Commandes_Utilisateur]
GO
ALTER TABLE [dbo].[Commune]  WITH CHECK ADD  CONSTRAINT [FK_Commune_Region] FOREIGN KEY([id_region])
REFERENCES [dbo].[Region] ([id])
GO
ALTER TABLE [dbo].[Commune] CHECK CONSTRAINT [FK_Commune_Region]
GO
ALTER TABLE [dbo].[Disponibilite_produit]  WITH CHECK ADD  CONSTRAINT [FK_Disponibilite_produit_Disponibilite] FOREIGN KEY([id_disponibilite])
REFERENCES [dbo].[Disponibilite] ([id])
GO
ALTER TABLE [dbo].[Disponibilite_produit] CHECK CONSTRAINT [FK_Disponibilite_produit_Disponibilite]
GO
ALTER TABLE [dbo].[Disponibilite_produit]  WITH CHECK ADD  CONSTRAINT [FK_Disponibilite_produit_Produit] FOREIGN KEY([id_produit])
REFERENCES [dbo].[Produit] ([id])
GO
ALTER TABLE [dbo].[Disponibilite_produit] CHECK CONSTRAINT [FK_Disponibilite_produit_Produit]
GO
ALTER TABLE [dbo].[employee]  WITH CHECK ADD  CONSTRAINT [FK_employee_service] FOREIGN KEY([id_service])
REFERENCES [dbo].[service] ([id])
GO
ALTER TABLE [dbo].[employee] CHECK CONSTRAINT [FK_employee_service]
GO
ALTER TABLE [dbo].[employee]  WITH CHECK ADD  CONSTRAINT [Fk_employee_utilisateur_created_by] FOREIGN KEY([created_by])
REFERENCES [dbo].[Utilisateur] ([id])
GO
ALTER TABLE [dbo].[employee] CHECK CONSTRAINT [Fk_employee_utilisateur_created_by]
GO
ALTER TABLE [dbo].[employee]  WITH CHECK ADD  CONSTRAINT [FK_employee_utilisateur_id_responsable] FOREIGN KEY([id_responsable])
REFERENCES [dbo].[Utilisateur] ([id])
GO
ALTER TABLE [dbo].[employee] CHECK CONSTRAINT [FK_employee_utilisateur_id_responsable]
GO
ALTER TABLE [dbo].[Etat_Produit]  WITH CHECK ADD  CONSTRAINT [FK_Etat_Produit_Etat] FOREIGN KEY([id_produit])
REFERENCES [dbo].[Etat] ([id])
GO
ALTER TABLE [dbo].[Etat_Produit] CHECK CONSTRAINT [FK_Etat_Produit_Etat]
GO
ALTER TABLE [dbo].[Etat_Produit]  WITH CHECK ADD  CONSTRAINT [FK_Etat_Produit_Produit] FOREIGN KEY([id_produit])
REFERENCES [dbo].[Produit] ([id])
GO
ALTER TABLE [dbo].[Etat_Produit] CHECK CONSTRAINT [FK_Etat_Produit_Produit]
GO
ALTER TABLE [dbo].[Fidelite]  WITH CHECK ADD  CONSTRAINT [FK_Fidelite_employee] FOREIGN KEY([created_by])
REFERENCES [dbo].[employee] ([id])
GO
ALTER TABLE [dbo].[Fidelite] CHECK CONSTRAINT [FK_Fidelite_employee]
GO
ALTER TABLE [dbo].[Fidelite]  WITH CHECK ADD  CONSTRAINT [FK_Fidelite_employee1] FOREIGN KEY([modif_by])
REFERENCES [dbo].[employee] ([id])
GO
ALTER TABLE [dbo].[Fidelite] CHECK CONSTRAINT [FK_Fidelite_employee1]
GO
ALTER TABLE [dbo].[Implantation]  WITH CHECK ADD  CONSTRAINT [FK_Implantation_adresse] FOREIGN KEY([adresse_id])
REFERENCES [dbo].[Adresse] ([id])
GO
ALTER TABLE [dbo].[Implantation] CHECK CONSTRAINT [FK_Implantation_adresse]
GO
ALTER TABLE [dbo].[Implantation]  WITH CHECK ADD  CONSTRAINT [FK_Implantation_utilisateur] FOREIGN KEY([created_by])
REFERENCES [dbo].[Utilisateur] ([id])
GO
ALTER TABLE [dbo].[Implantation] CHECK CONSTRAINT [FK_Implantation_utilisateur]
GO
ALTER TABLE [dbo].[Livraison]  WITH CHECK ADD  CONSTRAINT [FK__Livraison__Type_Livraison] FOREIGN KEY([id_typeLivraison])
REFERENCES [dbo].[Type_livraison] ([id])
GO
ALTER TABLE [dbo].[Livraison] CHECK CONSTRAINT [FK__Livraison__Type_Livraison]
GO
ALTER TABLE [dbo].[Livraison_commande]  WITH CHECK ADD  CONSTRAINT [FK_Livraison_commande_Commandes] FOREIGN KEY([id_commande])
REFERENCES [dbo].[Commandes] ([id])
GO
ALTER TABLE [dbo].[Livraison_commande] CHECK CONSTRAINT [FK_Livraison_commande_Commandes]
GO
ALTER TABLE [dbo].[Livraison_commande]  WITH CHECK ADD  CONSTRAINT [FK_Livraison_commande_Livraison] FOREIGN KEY([id_livraison])
REFERENCES [dbo].[Livraison] ([id])
GO
ALTER TABLE [dbo].[Livraison_commande] CHECK CONSTRAINT [FK_Livraison_commande_Livraison]
GO
ALTER TABLE [dbo].[Produit]  WITH CHECK ADD  CONSTRAINT [FK_Produit_Etat] FOREIGN KEY([id_etat])
REFERENCES [dbo].[Etat] ([id])
GO
ALTER TABLE [dbo].[Produit] CHECK CONSTRAINT [FK_Produit_Etat]
GO
ALTER TABLE [dbo].[Produit_Commande]  WITH CHECK ADD  CONSTRAINT [FK_Produit_Commande_Commandes] FOREIGN KEY([id_commande])
REFERENCES [dbo].[Commandes] ([id])
GO
ALTER TABLE [dbo].[Produit_Commande] CHECK CONSTRAINT [FK_Produit_Commande_Commandes]
GO
ALTER TABLE [dbo].[Produit_Commande]  WITH CHECK ADD  CONSTRAINT [FK_Produit_Commande_Produit] FOREIGN KEY([id_produit])
REFERENCES [dbo].[Produit] ([id])
GO
ALTER TABLE [dbo].[Produit_Commande] CHECK CONSTRAINT [FK_Produit_Commande_Produit]
GO
ALTER TABLE [dbo].[service]  WITH CHECK ADD  CONSTRAINT [FK_service_utilisateur] FOREIGN KEY([created_by])
REFERENCES [dbo].[Utilisateur] ([id])
GO
ALTER TABLE [dbo].[service] CHECK CONSTRAINT [FK_service_utilisateur]
GO
ALTER TABLE [dbo].[TokenPasswordUser]  WITH CHECK ADD  CONSTRAINT [FK__TokenPass_Utilisateur] FOREIGN KEY([idUtilisateur])
REFERENCES [dbo].[Utilisateur] ([id])
GO
ALTER TABLE [dbo].[TokenPasswordUser] CHECK CONSTRAINT [FK__TokenPass_Utilisateur]
GO
ALTER TABLE [dbo].[User_Profil]  WITH CHECK ADD  CONSTRAINT [FK__User_Profil_Utilisateur] FOREIGN KEY([id_utilisateur])
REFERENCES [dbo].[Utilisateur] ([id])
GO
ALTER TABLE [dbo].[User_Profil] CHECK CONSTRAINT [FK__User_Profil_Utilisateur]
GO
ALTER TABLE [dbo].[User_Profil]  WITH CHECK ADD  CONSTRAINT [FK_User_Profil_Profil] FOREIGN KEY([id_profil])
REFERENCES [dbo].[Profil] ([id])
GO
ALTER TABLE [dbo].[User_Profil] CHECK CONSTRAINT [FK_User_Profil_Profil]
GO
ALTER TABLE [dbo].[Utilisateur]  WITH CHECK ADD  CONSTRAINT [FK__Utilisateu__etat] FOREIGN KEY([id_etat])
REFERENCES [dbo].[Etat] ([id])
GO
ALTER TABLE [dbo].[Utilisateur] CHECK CONSTRAINT [FK__Utilisateu__etat]
GO
ALTER TABLE [dbo].[Utilisateur]  WITH CHECK ADD  CONSTRAINT [FK_Utilisateur_Genre] FOREIGN KEY([id_genre])
REFERENCES [dbo].[Genre] ([id])
GO
ALTER TABLE [dbo].[Utilisateur] CHECK CONSTRAINT [FK_Utilisateur_Genre]
GO
USE [master]
GO
ALTER DATABASE [GestionCommande] SET  READ_WRITE 
GO
