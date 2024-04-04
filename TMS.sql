USE [master]
GO
/****** Object:  Database [TMSDB]    Script Date: 4/4/2024 5:04:58 PM ******/
CREATE DATABASE [TMSDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TMSDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQL2022\MSSQL\DATA\TMSDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TMSDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQL2022\MSSQL\DATA\TMSDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [TMSDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TMSDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TMSDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TMSDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TMSDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TMSDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TMSDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TMSDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TMSDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TMSDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TMSDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TMSDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TMSDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TMSDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TMSDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TMSDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TMSDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TMSDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TMSDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TMSDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TMSDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TMSDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TMSDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TMSDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TMSDB] SET RECOVERY FULL 
GO
ALTER DATABASE [TMSDB] SET  MULTI_USER 
GO
ALTER DATABASE [TMSDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TMSDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TMSDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TMSDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TMSDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TMSDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TMSDB', N'ON'
GO
ALTER DATABASE [TMSDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [TMSDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [TMSDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 4/4/2024 5:04:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 4/4/2024 5:04:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 4/4/2024 5:04:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 4/4/2024 5:04:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 4/4/2024 5:04:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 4/4/2024 5:04:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 4/4/2024 5:04:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[Department] [int] NOT NULL,
	[PhotoPath] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 4/4/2024 5:04:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 4/4/2024 5:04:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[TaskId] [int] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 4/4/2024 5:04:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[DueDate] [datetime2](7) NOT NULL,
	[Priority] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[AssignedByUserId] [nvarchar](450) NOT NULL,
	[AssignedToUserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Team]    Script Date: 4/4/2024 5:04:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTeam]    Script Date: 4/4/2024 5:04:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTeam](
	[UserId] [nvarchar](450) NOT NULL,
	[TeamId] [int] NOT NULL,
 CONSTRAINT [PK_UserTeam] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[TeamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240325124628_InitialMigration', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240325161736_Updates', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240329132810_InitializedIdentity', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240401140828_ExtendedUsers', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240401191603_TaskComment', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240402110107_UpdatedTasks', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240403183016_TeamsAndUser', N'8.0.3')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'090d4ade-f1bf-40fc-b258-283733b2d796', N'User', N'USER', N'0f0657e8-075c-4c63-a6fd-56d44da3362e')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'97d8487c-f488-4464-be8b-2d6c9dfa2d34', N'Lead', N'LEAD', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'e59c0a57-3e70-47de-b45f-8fe2d3898828', N'Admin', N'ADMIN', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'64611f86-2224-48fe-87df-e379dc05f2d1', N'090d4ade-f1bf-40fc-b258-283733b2d796')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7f69f826-d6a6-45ee-9565-1f4d3d786bb0', N'97d8487c-f488-4464-be8b-2d6c9dfa2d34')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'113c88c9-f72d-42d0-8502-342523aca1aa', N'e59c0a57-3e70-47de-b45f-8fe2d3898828')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Department], [PhotoPath], [Name]) VALUES (N'113c88c9-f72d-42d0-8502-342523aca1aa', N'admin@gmail.com', N'ADMIN@GMAIL.COM', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEIrT6w6XSP6ttqyDucEJOV2ZA2EeFyrNCGIWgcTHp0ojFEUuX8gDwYLi6dLT91q+fA==', N'MFXZLS5LVNC3HMGZR2UPOAF62GUDWECF', N'6b1133f2-7114-4cdb-9277-74cdfca22326', NULL, 0, 0, NULL, 1, 0, 2, N'c20bfd16-2b48-4763-8c13-ace43694685a_Man-Employee.png', N'Admin')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Department], [PhotoPath], [Name]) VALUES (N'64611f86-2224-48fe-87df-e379dc05f2d1', N'user@gmail.com', N'USER@GMAIL.COM', N'user@gmail.com', N'USER@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEGmhtXvFK0skjWTj8oKlbjWOFHDnj/gH7R7gn2rg7WsBypG3DjDVENSWIOBkUQQLIA==', N'OSCJVQ77OTKSNFU6EXMTH4P5BXTIQ3JI', N'cca243fd-4956-464f-814c-81a819c4da0a', NULL, 0, 0, NULL, 1, 0, 0, N'ee08f931-39ed-40c4-bdbe-9ed135317ed4_noimage.png', N'User')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Department], [PhotoPath], [Name]) VALUES (N'7f69f826-d6a6-45ee-9565-1f4d3d786bb0', N'lead@gmail.com', N'LEAD@GMAIL.COM', N'lead@gmail.com', N'LEAD@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEG3lWXUjcKYzN5pYUp11TKjPTRrsTJoDopKnYJrtCxRoXkasYA33itsNQE7HTS5F6Q==', N'UDME4A4SCRLMGHUNPFUSYUWLQ4MPZOVJ', N'f66918dc-ea69-4c3d-89a1-d9409379a791', NULL, 0, 0, NULL, 1, 0, 0, N'872bdc45-dbbd-4edb-9c58-3f9499b92b04_Woman-Employee.png', N'Lead')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Department], [PhotoPath], [Name]) VALUES (N'cc28724a-d10e-4d9e-8971-48a95136389a', N'a@gmail.com', N'A@GMAIL.COM', N'a@gmail.com', N'A@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEOgmDlZXi9lt3wqwU3r/Lejf6UkD09CRmafKsoKBptCsw6wKiF3LGapCF5bdjwuLEA==', N'SWJKOSXIPGQKU6ENPEJH5Z3R2LOL5TO4', N'08d7a571-e205-4906-bc2e-1f2c6b3e6085', NULL, 0, 0, NULL, 1, 0, 0, N'e5ecd61c-58ba-4eb8-be1a-8207fa9103b0_noimage.png', N'A')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Department], [PhotoPath], [Name]) VALUES (N'fd59b960-bafd-48a8-987b-58a824dc02f7', N'b@gmail.com', N'B@GMAIL.COM', N'b@gmail.com', N'B@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEPreK0n3491llpAFJOYwvTMn1fNhZR81N9DZOjWgij8FoB8j9c48vMU5Li9AhUU49w==', N'W34LXVSHWNXOIAU7JK5F2CAMV2LKZPRT', N'343f4e52-7700-4bd2-95cc-f7c5eea36817', NULL, 0, 0, NULL, 1, 0, 0, N'841f210a-051e-4056-b982-90ce953a232f_noimage.png', N'B')
GO
SET IDENTITY_INSERT [dbo].[Comment] ON 

INSERT [dbo].[Comment] ([Id], [Text], [Date], [TaskId], [UserId]) VALUES (1, N'Comment1', CAST(N'2024-04-02T13:35:36.6297899' AS DateTime2), 1, N'113c88c9-f72d-42d0-8502-342523aca1aa')
INSERT [dbo].[Comment] ([Id], [Text], [Date], [TaskId], [UserId]) VALUES (2, N'Comment2', CAST(N'2024-04-02T13:36:14.4762231' AS DateTime2), 1, N'113c88c9-f72d-42d0-8502-342523aca1aa')
INSERT [dbo].[Comment] ([Id], [Text], [Date], [TaskId], [UserId]) VALUES (16, N'another comment in task 4', CAST(N'2024-04-03T18:35:08.6079783' AS DateTime2), 4, N'113c88c9-f72d-42d0-8502-342523aca1aa')
INSERT [dbo].[Comment] ([Id], [Text], [Date], [TaskId], [UserId]) VALUES (17, N'test', CAST(N'2024-04-03T18:35:24.5639969' AS DateTime2), 2, N'113c88c9-f72d-42d0-8502-342523aca1aa')
INSERT [dbo].[Comment] ([Id], [Text], [Date], [TaskId], [UserId]) VALUES (18, N'test2', CAST(N'2024-04-03T18:35:24.5686750' AS DateTime2), 2, N'113c88c9-f72d-42d0-8502-342523aca1aa')
INSERT [dbo].[Comment] ([Id], [Text], [Date], [TaskId], [UserId]) VALUES (19, N'test 3', CAST(N'2024-04-03T18:35:24.5722560' AS DateTime2), 2, N'113c88c9-f72d-42d0-8502-342523aca1aa')
SET IDENTITY_INSERT [dbo].[Comment] OFF
GO
SET IDENTITY_INSERT [dbo].[Task] ON 

INSERT [dbo].[Task] ([Id], [Title], [Description], [StartDate], [DueDate], [Priority], [Status], [AssignedByUserId], [AssignedToUserId]) VALUES (1, N'Test Title', N'Test Description', CAST(N'2024-04-02T14:34:00.0000000' AS DateTime2), CAST(N'2024-04-02T16:34:00.0000000' AS DateTime2), 1, 1, N'113c88c9-f72d-42d0-8502-342523aca1aa', N'64611f86-2224-48fe-87df-e379dc05f2d1')
INSERT [dbo].[Task] ([Id], [Title], [Description], [StartDate], [DueDate], [Priority], [Status], [AssignedByUserId], [AssignedToUserId]) VALUES (2, N'Title1', N'des test', CAST(N'2024-04-02T16:11:00.0000000' AS DateTime2), CAST(N'2024-04-02T17:11:00.0000000' AS DateTime2), 0, 0, N'113c88c9-f72d-42d0-8502-342523aca1aa', N'113c88c9-f72d-42d0-8502-342523aca1aa')
INSERT [dbo].[Task] ([Id], [Title], [Description], [StartDate], [DueDate], [Priority], [Status], [AssignedByUserId], [AssignedToUserId]) VALUES (3, N'Test2', N'Test Des', CAST(N'2024-04-02T18:39:00.0000000' AS DateTime2), CAST(N'2024-04-02T19:39:00.0000000' AS DateTime2), 1, 2, N'64611f86-2224-48fe-87df-e379dc05f2d1', N'113c88c9-f72d-42d0-8502-342523aca1aa')
INSERT [dbo].[Task] ([Id], [Title], [Description], [StartDate], [DueDate], [Priority], [Status], [AssignedByUserId], [AssignedToUserId]) VALUES (4, N'Title3', N'des test 3', CAST(N'2024-04-02T16:11:00.0000000' AS DateTime2), CAST(N'2024-04-02T17:11:00.0000000' AS DateTime2), 3, 1, N'113c88c9-f72d-42d0-8502-342523aca1aa', N'113c88c9-f72d-42d0-8502-342523aca1aa')
INSERT [dbo].[Task] ([Id], [Title], [Description], [StartDate], [DueDate], [Priority], [Status], [AssignedByUserId], [AssignedToUserId]) VALUES (9, N'Task for A', N'test Des', CAST(N'2024-04-04T18:23:00.0000000' AS DateTime2), CAST(N'2024-04-04T19:23:00.0000000' AS DateTime2), 0, 2, N'113c88c9-f72d-42d0-8502-342523aca1aa', N'cc28724a-d10e-4d9e-8971-48a95136389a')
SET IDENTITY_INSERT [dbo].[Task] OFF
GO
SET IDENTITY_INSERT [dbo].[Team] ON 

INSERT [dbo].[Team] ([Id], [Name]) VALUES (1, N'TeamA')
INSERT [dbo].[Team] ([Id], [Name]) VALUES (3, N'TeamB')
SET IDENTITY_INSERT [dbo].[Team] OFF
GO
INSERT [dbo].[UserTeam] ([UserId], [TeamId]) VALUES (N'64611f86-2224-48fe-87df-e379dc05f2d1', 1)
INSERT [dbo].[UserTeam] ([UserId], [TeamId]) VALUES (N'7f69f826-d6a6-45ee-9565-1f4d3d786bb0', 1)
INSERT [dbo].[UserTeam] ([UserId], [TeamId]) VALUES (N'cc28724a-d10e-4d9e-8971-48a95136389a', 1)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 4/4/2024 5:04:58 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 4/4/2024 5:04:58 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 4/4/2024 5:04:58 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 4/4/2024 5:04:58 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 4/4/2024 5:04:58 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 4/4/2024 5:04:58 PM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 4/4/2024 5:04:58 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comment_TaskId]    Script Date: 4/4/2024 5:04:58 PM ******/
CREATE NONCLUSTERED INDEX [IX_Comment_TaskId] ON [dbo].[Comment]
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Comment_UserId]    Script Date: 4/4/2024 5:04:58 PM ******/
CREATE NONCLUSTERED INDEX [IX_Comment_UserId] ON [dbo].[Comment]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Task_AssignedByUserId]    Script Date: 4/4/2024 5:04:58 PM ******/
CREATE NONCLUSTERED INDEX [IX_Task_AssignedByUserId] ON [dbo].[Task]
(
	[AssignedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Task_AssignedToUserId]    Script Date: 4/4/2024 5:04:58 PM ******/
CREATE NONCLUSTERED INDEX [IX_Task_AssignedToUserId] ON [dbo].[Task]
(
	[AssignedToUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserTeam_TeamId]    Script Date: 4/4/2024 5:04:58 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserTeam_TeamId] ON [dbo].[UserTeam]
(
	[TeamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT ((0)) FOR [Department]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (N'') FOR [Name]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Task_TaskId] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Task] ([Id])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Task_TaskId]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_AspNetUsers_AssignedByUserId] FOREIGN KEY([AssignedByUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_AspNetUsers_AssignedByUserId]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_AspNetUsers_AssignedToUserId] FOREIGN KEY([AssignedToUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_AspNetUsers_AssignedToUserId]
GO
ALTER TABLE [dbo].[UserTeam]  WITH CHECK ADD  CONSTRAINT [FK_UserTeam_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTeam] CHECK CONSTRAINT [FK_UserTeam_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[UserTeam]  WITH CHECK ADD  CONSTRAINT [FK_UserTeam_Team_TeamId] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTeam] CHECK CONSTRAINT [FK_UserTeam_Team_TeamId]
GO
USE [master]
GO
ALTER DATABASE [TMSDB] SET  READ_WRITE 
GO
