USE [master]
GO
/****** Object:  Database [agprogramlama]    Script Date: 27.12.2024 17:32:00 ******/
CREATE DATABASE [agprogramlama]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'agprogramlama', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\agprogramlama.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'agprogramlama_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\agprogramlama_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [agprogramlama] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [agprogramlama].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [agprogramlama] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [agprogramlama] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [agprogramlama] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [agprogramlama] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [agprogramlama] SET ARITHABORT OFF 
GO
ALTER DATABASE [agprogramlama] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [agprogramlama] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [agprogramlama] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [agprogramlama] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [agprogramlama] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [agprogramlama] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [agprogramlama] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [agprogramlama] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [agprogramlama] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [agprogramlama] SET  DISABLE_BROKER 
GO
ALTER DATABASE [agprogramlama] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [agprogramlama] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [agprogramlama] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [agprogramlama] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [agprogramlama] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [agprogramlama] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [agprogramlama] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [agprogramlama] SET RECOVERY FULL 
GO
ALTER DATABASE [agprogramlama] SET  MULTI_USER 
GO
ALTER DATABASE [agprogramlama] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [agprogramlama] SET DB_CHAINING OFF 
GO
ALTER DATABASE [agprogramlama] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [agprogramlama] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [agprogramlama] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [agprogramlama] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'agprogramlama', N'ON'
GO
ALTER DATABASE [agprogramlama] SET QUERY_STORE = OFF
GO
USE [agprogramlama]
GO
/****** Object:  User [agprogramlama]    Script Date: 27.12.2024 17:32:00 ******/
CREATE USER [agprogramlama] FOR LOGIN [agprogramlama] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [agprogramlama]
GO
/****** Object:  Table [dbo].[messages]    Script Date: 27.12.2024 17:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[messages](
	[id] [tinyint] IDENTITY(1,1) NOT NULL,
	[sender_id] [tinyint] NOT NULL,
	[receiver_id] [tinyint] NOT NULL,
	[message] [nvarchar](300) NOT NULL,
	[sent_at] [datetime] NOT NULL,
	[isDelivered] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 27.12.2024 17:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [tinyint] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](64) NOT NULL,
	[isOnline] [tinyint] NOT NULL,
	[ipAddress] [varchar](15) NULL,
	[port] [varchar](6) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[messages] ON 

INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (80, 2, 3, N'IZZHA0B48cUXbokpbAXpWg==', CAST(N'2024-12-25T02:05:35.413' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (81, 2, 1, N'T2+Jd4lDH5meQ1EIItomAw==', CAST(N'2024-12-25T02:05:42.110' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (82, 2, 1, N'T2+Jd4lDH5meQ1EIItomAw==', CAST(N'2024-12-25T02:06:11.917' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (83, 2, 1, N'oLO9nW9/qJpSoMxzZV+u1Q==', CAST(N'2024-12-25T02:06:29.310' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (84, 1, 2, N'I6owUO5yrlLP66ybM9oeTw==', CAST(N'2024-12-25T02:06:42.293' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (85, 1, 2, N'I6owUO5yrlLP66ybM9oeTw==', CAST(N'2024-12-25T02:06:45.567' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (86, 1, 2, N'I6owUO5yrlLP66ybM9oeTw==', CAST(N'2024-12-25T02:06:46.323' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (87, 1, 2, N'I6owUO5yrlLP66ybM9oeTw==', CAST(N'2024-12-25T02:06:46.700' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (88, 1, 2, N'I6owUO5yrlLP66ybM9oeTw==', CAST(N'2024-12-25T02:06:46.927' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (89, 1, 2, N'I6owUO5yrlLP66ybM9oeTw==', CAST(N'2024-12-25T02:06:47.130' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (90, 1, 2, N'I6owUO5yrlLP66ybM9oeTw==', CAST(N'2024-12-25T02:06:47.317' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (91, 1, 2, N'I6owUO5yrlLP66ybM9oeTw==', CAST(N'2024-12-25T02:06:47.497' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (92, 1, 2, N'I6owUO5yrlLP66ybM9oeTw==', CAST(N'2024-12-25T02:06:47.677' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (93, 1, 2, N'I6owUO5yrlLP66ybM9oeTw==', CAST(N'2024-12-25T02:06:47.850' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (94, 1, 2, N'I6owUO5yrlLP66ybM9oeTw==', CAST(N'2024-12-25T02:06:48.107' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (95, 1, 2, N'I6owUO5yrlLP66ybM9oeTw==', CAST(N'2024-12-25T02:06:48.280' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (97, 1, 2, N'j+UvIymVoUoXr2dKODgX+w==', CAST(N'2024-12-25T02:10:09.947' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (101, 1, 2, N'grUEynatHucbpSF9CbVQtg==', CAST(N'2024-12-25T02:22:58.633' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (102, 2, 1, N'+UDmAowviU+Bkjj/iwKHOw==', CAST(N'2024-12-25T02:23:12.213' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (105, 1, 2, N'xmb3GtbBGL3PR4SPDp5Miw==', CAST(N'2024-12-25T02:23:22.267' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (107, 3, 1, N'fBW5nLNjtwQBDCyp4He5+5RXSIoXShl2DL+wWnRvASc=', CAST(N'2024-12-25T02:23:51.340' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (109, 3, 2, N'fBW5nLNjtwQBDCyp4He5+5RXSIoXShl2DL+wWnRvASc=', CAST(N'2024-12-25T02:23:52.317' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (110, 3, 2, N'fBW5nLNjtwQBDCyp4He5+5RXSIoXShl2DL+wWnRvASc=', CAST(N'2024-12-25T02:23:52.320' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (111, 2, 1, N'FH20tvbt+FNo2RYLNCgfSLuN5lVMLDrepwwiu2C/qik=', CAST(N'2024-12-25T02:24:13.377' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (113, 2, 3, N'FH20tvbt+FNo2RYLNCgfSLuN5lVMLDrepwwiu2C/qik=', CAST(N'2024-12-25T02:24:14.233' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (114, 2, 3, N'FH20tvbt+FNo2RYLNCgfSLuN5lVMLDrepwwiu2C/qik=', CAST(N'2024-12-25T02:24:14.240' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (96, 2, 1, N'IZZHA0B48cUXbokpbAXpWg==', CAST(N'2024-12-25T02:09:57.107' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (98, 2, 1, N'VYov3h11SFR+CIiUHynq+HFyfXRXiOHgyNbmExFa6y4=', CAST(N'2024-12-25T02:10:17.913' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (99, 1, 2, N'zokp98sCPZtDBujT9NttZw==', CAST(N'2024-12-25T02:22:26.307' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (100, 1, 2, N'grUEynatHucbpSF9CbVQtg==', CAST(N'2024-12-25T02:22:58.603' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (103, 2, 1, N'+UDmAowviU+Bkjj/iwKHOw==', CAST(N'2024-12-25T02:23:12.230' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (104, 1, 2, N'xmb3GtbBGL3PR4SPDp5Miw==', CAST(N'2024-12-25T02:23:22.253' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (106, 1, 3, N'xmb3GtbBGL3PR4SPDp5Miw==', CAST(N'2024-12-25T02:23:23.247' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (108, 3, 1, N'fBW5nLNjtwQBDCyp4He5+5RXSIoXShl2DL+wWnRvASc=', CAST(N'2024-12-25T02:23:51.350' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (112, 2, 1, N'FH20tvbt+FNo2RYLNCgfSLuN5lVMLDrepwwiu2C/qik=', CAST(N'2024-12-25T02:24:13.383' AS DateTime), 1)
INSERT [dbo].[messages] ([id], [sender_id], [receiver_id], [message], [sent_at], [isDelivered]) VALUES (115, 1, 2, N'JB3Yii4pI/bmBBCJbOpfsenivlZeAd4YMQo2LzREYQw=', CAST(N'2024-12-25T02:36:12.663' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[messages] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([id], [username], [password], [isOnline], [ipAddress], [port]) VALUES (1, N'myakn55', N'4fc82b26aecb47d2868c4efbe3581732a3e7cbcc6c2efb32062c08170a05eeb8', 0, N'192.168.56.1', N'5000')
INSERT [dbo].[users] ([id], [username], [password], [isOnline], [ipAddress], [port]) VALUES (2, N'esra', N'02d20bbd7e394ad5999a4cebabac9619732c343a4cac99470c03e23ba2bdc2bc', 0, N'192.168.56.1', N'5200')
INSERT [dbo].[users] ([id], [username], [password], [isOnline], [ipAddress], [port]) VALUES (3, N'deneme', N'4fc82b26aecb47d2868c4efbe3581732a3e7cbcc6c2efb32062c08170a05eeb8', 0, N'192.168.56.1', N'5100')
INSERT [dbo].[users] ([id], [username], [password], [isOnline], [ipAddress], [port]) VALUES (4, N'portdeneme', N'4fc82b26aecb47d2868c4efbe3581732a3e7cbcc6c2efb32062c08170a05eeb8', 0, N'192.168.56.1', N'4900')
SET IDENTITY_INSERT [dbo].[users] OFF
GO
USE [master]
GO
ALTER DATABASE [agprogramlama] SET  READ_WRITE 
GO
