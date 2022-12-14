USE [master]
GO
/****** Object:  Database [dbshop]    Script Date: 12/30/2022 1:32:48 AM ******/
CREATE DATABASE [dbshop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbshop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\dbshop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbshop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\dbshop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbshop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbshop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbshop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbshop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbshop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbshop] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbshop] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [dbshop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbshop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbshop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbshop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbshop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbshop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbshop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbshop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbshop] SET  ENABLE_BROKER 
GO
ALTER DATABASE [dbshop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbshop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbshop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbshop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbshop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbshop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbshop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbshop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dbshop] SET  MULTI_USER 
GO
ALTER DATABASE [dbshop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbshop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbshop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbshop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbshop] SET DELAYED_DURABILITY = DISABLED 
GO
USE [dbshop]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 12/30/2022 1:32:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[Phone] [varchar](12) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Salt] [nvarchar](50) NULL,
	[Active] [bit] NOT NULL,
	[Fullname] [nvarchar](50) NULL,
	[RoleID] [int] NULL,
	[LastLogin] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[ImagePath] [nvarchar](100) NULL,
	[ImageName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AtrributesPrices]    Script Date: 12/30/2022 1:32:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AtrributesPrices](
	[AttributesPriceID] [int] IDENTITY(1,1) NOT NULL,
	[AttributeID] [int] NULL,
	[ProductID] [int] NULL,
	[Price] [int] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_AtrributesPrices] PRIMARY KEY CLUSTERED 
(
	[AttributesPriceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attributes]    Script Date: 12/30/2022 1:32:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attributes](
	[AttributeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
 CONSTRAINT [PK_Attributes] PRIMARY KEY CLUSTERED 
(
	[AttributeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 12/30/2022 1:32:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CatID] [int] IDENTITY(1,1) NOT NULL,
	[CatName] [nvarchar](250) NULL,
	[Description] [nvarchar](max) NULL,
	[ParentID] [int] NULL,
	[Levels] [int] NULL,
	[Ordering] [int] NULL,
	[Published] [bit] NOT NULL,
	[Thumb] [nvarchar](250) NULL,
	[Title] [nvarchar](250) NULL,
	[Alias] [nvarchar](250) NULL,
	[MetaDesc] [nvarchar](250) NULL,
	[MetaKey] [nvarchar](250) NULL,
	[Cover] [nvarchar](250) NULL,
	[SchemaMarkup] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 12/30/2022 1:32:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](250) NULL,
	[Birthday] [datetime] NULL,
	[Avatar] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[Email] [nvarchar](150) NULL,
	[Phone] [varchar](12) NULL,
	[LocationID] [int] NULL,
	[District] [int] NULL,
	[Ward] [int] NULL,
	[CreateDate] [datetime] NULL,
	[Password] [nvarchar](50) NULL,
	[Salt] [nchar](10) NULL,
	[LastLogin] [datetime] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 12/30/2022 1:32:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[LocationID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Type] [nvarchar](20) NULL,
	[Slug] [nvarchar](100) NULL,
	[NameWithType] [nvarchar](250) NULL,
	[PathWithType] [nvarchar](255) NULL,
	[ParentCode] [int] NULL,
	[Levels] [int] NOT NULL,
 CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 12/30/2022 1:32:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[ProductID] [int] NULL,
	[OrderNumber] [int] NULL,
	[Quantity] [int] NULL,
	[Discount] [int] NULL,
	[Total] [int] NULL,
	[CreateDate] [datetime] NULL,
	[Price] [int] NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 12/30/2022 1:32:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[OrderDate] [datetime] NULL,
	[ShipDate] [datetime] NULL,
	[TransactStatusID] [int] NULL,
	[Deleted] [bit] NOT NULL,
	[Paid] [bit] NOT NULL,
	[PaymentDate] [datetime] NULL,
	[PaymentID] [int] NULL,
	[Note] [nvarchar](max) NULL,
	[Total] [int] NULL,
	[LocationId] [int] NULL,
	[District] [int] NULL,
	[Ward] [int] NULL,
	[Address] [nvarchar](250) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pages]    Script Date: 12/30/2022 1:32:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pages](
	[PageID] [int] IDENTITY(1,1) NOT NULL,
	[PageName] [nvarchar](250) NULL,
	[Contents] [nvarchar](max) NULL,
	[Thumb] [nvarchar](250) NULL,
	[Published] [bit] NOT NULL,
	[Title] [nvarchar](250) NULL,
	[MetaDesc] [nvarchar](250) NULL,
	[MetaKey] [nvarchar](250) NULL,
	[Alias] [nvarchar](250) NULL,
	[CreateDate] [datetime] NULL,
	[Ordering] [int] NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[PageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 12/30/2022 1:32:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](250) NOT NULL,
	[ShortDesc] [nvarchar](250) NULL,
	[Description] [nvarchar](max) NULL,
	[CatID] [int] NULL,
	[Price] [int] NULL,
	[Discount] [int] NULL,
	[Thumb] [nvarchar](250) NULL,
	[Video] [nvarchar](255) NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[BestSeller] [bit] NOT NULL,
	[HomeFlag] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[Tags] [nvarchar](max) NULL,
	[Alias] [nvarchar](255) NULL,
	[MetaDesc] [nvarchar](255) NULL,
	[MetaKey] [nvarchar](255) NULL,
	[UnitslnStock] [int] NULL,
	[Title] [nvarchar](250) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/30/2022 1:32:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shippers]    Script Date: 12/30/2022 1:32:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shippers](
	[ShipperID] [int] IDENTITY(1,1) NOT NULL,
	[ShipperName] [nvarchar](150) NULL,
	[Phone] [nchar](10) NULL,
	[Company] [nvarchar](150) NULL,
	[ShipDate] [datetime] NULL,
 CONSTRAINT [PK_Shippers] PRIMARY KEY CLUSTERED 
(
	[ShipperID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TinTucs]    Script Date: 12/30/2022 1:32:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TinTucs](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[SContents] [nvarchar](255) NULL,
	[Contents] [nvarchar](max) NULL,
	[Thumb] [nvarchar](255) NULL,
	[Alias] [nvarchar](255) NULL,
	[CreatedDate] [datetime] NULL,
	[Published] [bit] NOT NULL,
	[Author] [nvarchar](255) NULL,
	[AccountID] [int] NULL,
	[Tags] [nvarchar](max) NULL,
	[CatID] [int] NULL,
	[IsHot] [bit] NOT NULL,
	[IsNewfeed] [bit] NOT NULL,
	[MetaKey] [nvarchar](255) NULL,
	[MetaDesc] [nvarchar](255) NULL,
	[Views] [int] NULL,
 CONSTRAINT [PK_TinTucs] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactStatus]    Script Date: 12/30/2022 1:32:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactStatus](
	[TransactStatusID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_TransactStatus] PRIMARY KEY CLUSTERED 
(
	[TransactStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([AccountID], [Phone], [Email], [Password], [Salt], [Active], [Fullname], [RoleID], [LastLogin], [CreateDate], [ImagePath], [ImageName]) VALUES (1, N'123456', N'phat@gmail.com', N'40fe684424fedf218c9cf9f5ebc71968', N'v^tdg', 1, N'Ngo Hoang Phat', 1, NULL, CAST(N'2022-12-21T23:10:00.840' AS DateTime), NULL, NULL)
INSERT [dbo].[Accounts] ([AccountID], [Phone], [Email], [Password], [Salt], [Active], [Fullname], [RoleID], [LastLogin], [CreateDate], [ImagePath], [ImageName]) VALUES (2, N'123456', N'staff@gmail.com', N'a87ad634f886ea2f8bc0debd1f44600b', N'1u9ji', 1, N'Ngo Hoang Phat', 2, NULL, CAST(N'2022-12-30T00:47:57.577' AS DateTime), N'Admin/StreetFighter6', N'StreetFighter6.jpg')
INSERT [dbo].[Accounts] ([AccountID], [Phone], [Email], [Password], [Salt], [Active], [Fullname], [RoleID], [LastLogin], [CreateDate], [ImagePath], [ImageName]) VALUES (3, N'123456', N'admin@gmail.com', N'ae4d12d62d6e69a480cd3736bc854a61', N'xze@c', 1, N'admin', 1, NULL, CAST(N'2022-12-29T22:56:39.823' AS DateTime), NULL, NULL)
INSERT [dbo].[Accounts] ([AccountID], [Phone], [Email], [Password], [Salt], [Active], [Fullname], [RoleID], [LastLogin], [CreateDate], [ImagePath], [ImageName]) VALUES (4, N'123456', N'info@gmail.com', N'b04ced2b12b514ef72e5af3660ef3a3e', N'#0)[*', 1, N'Kiếm Sĩ Wibu', 1, NULL, CAST(N'2022-12-30T00:57:24.153' AS DateTime), N'Admin/GenshinImpact', N'GenshinImpact.png')
INSERT [dbo].[Accounts] ([AccountID], [Phone], [Email], [Password], [Salt], [Active], [Fullname], [RoleID], [LastLogin], [CreateDate], [ImagePath], [ImageName]) VALUES (5, N'123456', N'superadmin@gmail.com', N'7a86286593e9ea1f220a4edb5426f552', N'hyz!t', 1, N'Phát Ngô', 4, NULL, CAST(N'2022-12-30T00:54:52.277' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CatID], [CatName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [MetaDesc], [MetaKey], [Cover], [SchemaMarkup]) VALUES (1, N'Men', N'Men clothes', 1, 1, 1, 1, N'default.jpg', NULL, N'men', NULL, NULL, NULL, NULL)
INSERT [dbo].[Categories] ([CatID], [CatName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [MetaDesc], [MetaKey], [Cover], [SchemaMarkup]) VALUES (2, N'Women', N'Women clothes', NULL, NULL, 1, 1, N'girl-1.jpg', NULL, N'girl-1', N'Quần áo nữ', N'Quan ao nu', NULL, NULL)
INSERT [dbo].[Categories] ([CatID], [CatName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [MetaDesc], [MetaKey], [Cover], [SchemaMarkup]) VALUES (3, N'Kid', N'Kid clothes', 1, 1, 1, 1, N'default.jpg', NULL, N'kid', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerID], [FullName], [Birthday], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active]) VALUES (1, N'Trần Khải Hoàn', NULL, NULL, N'ABCXYZ', N'hoantran1107@gmail.com', N'0914524761', 3, 0, 0, CAST(N'2022-12-08T14:03:18.717' AS DateTime), N'5c6bfea4c9a5f40ae19d77a518e91cb6', N'xm[5m     ', NULL, 1)
INSERT [dbo].[Customers] ([CustomerID], [FullName], [Birthday], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active]) VALUES (2, N'Phù Quốc Khánh', NULL, NULL, NULL, N'hoan1234@gmail.com', N'123456789', NULL, NULL, NULL, CAST(N'2022-12-08T15:02:30.840' AS DateTime), N'c170e8ff2df7eef51e4f813e5b904a4e', N'r8zu}     ', NULL, 1)
INSERT [dbo].[Customers] ([CustomerID], [FullName], [Birthday], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active]) VALUES (3, N'ABC', NULL, NULL, NULL, N'hoantran@gmail.com', N'123456', NULL, NULL, NULL, CAST(N'2022-12-08T15:31:55.733' AS DateTime), N'26ddb0a7cd2f1092a26c0718c9af4810', N'%j(!5     ', NULL, 1)
INSERT [dbo].[Customers] ([CustomerID], [FullName], [Birthday], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active]) VALUES (4, N'Khánh', NULL, NULL, NULL, N'hoantran1@gmail.com', N'123456789', NULL, NULL, NULL, CAST(N'2022-12-08T15:39:32.970' AS DateTime), N'3998f4fbc0b640642e7b5e08ef640e52', N'h26p0     ', NULL, 1)
INSERT [dbo].[Customers] ([CustomerID], [FullName], [Birthday], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active]) VALUES (5, N'Trần Khải Hoàn', NULL, NULL, NULL, N'hoantran1106@gmail.com', N'123456', NULL, NULL, NULL, CAST(N'2022-12-08T16:30:08.390' AS DateTime), N'6f74da4b50e16e8f651c35934bc8b702', N'y^d&p     ', NULL, 1)
INSERT [dbo].[Customers] ([CustomerID], [FullName], [Birthday], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active]) VALUES (6, N'hoan', NULL, NULL, NULL, N'hoan12@gmail.com', N'12345', NULL, NULL, NULL, CAST(N'2022-12-10T14:33:17.510' AS DateTime), N'62b193a3f9a2c22b9fe603c9ab8a37ac', N'efzbm     ', NULL, 1)
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Locations] ON 

INSERT [dbo].[Locations] ([LocationID], [Name], [Type], [Slug], [NameWithType], [PathWithType], [ParentCode], [Levels]) VALUES (3, N'ABC', NULL, NULL, N'ABC', N'ABC', 1234, 1)
SET IDENTITY_INSERT [dbo].[Locations] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [OrderNumber], [Quantity], [Discount], [Total], [CreateDate], [Price]) VALUES (1, 1, 2, NULL, 1, NULL, 1000000, CAST(N'2022-12-29T22:57:18.533' AS DateTime), 1000000)
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [ShipDate], [TransactStatusID], [Deleted], [Paid], [PaymentDate], [PaymentID], [Note], [Total], [LocationId], [District], [Ward], [Address]) VALUES (1, 1, CAST(N'2022-12-29T22:57:18.433' AS DateTime), CAST(N'2022-12-29T22:57:52.143' AS DateTime), 4, 0, 1, CAST(N'2022-12-29T22:57:56.067' AS DateTime), NULL, NULL, 1000000, NULL, NULL, NULL, N'ABCXYZ')
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Pages] ON 

INSERT [dbo].[Pages] ([PageID], [PageName], [Contents], [Thumb], [Published], [Title], [MetaDesc], [MetaKey], [Alias], [CreateDate], [Ordering]) VALUES (1, N'Shopping guide', N'<p>abc&nbsp;</p>', N'huong-dan-mua-hang.jpg', 1, N'Shopping guide', N'Shopping guide', N'Huong dan mua hang', N'huong-dan-mua-hang', CAST(N'2022-12-07T15:27:08.817' AS DateTime), 1)
INSERT [dbo].[Pages] ([PageID], [PageName], [Contents], [Thumb], [Published], [Title], [MetaDesc], [MetaKey], [Alias], [CreateDate], [Ordering]) VALUES (2, N'Payment Guide', N'<p>Payment Guide</p>', N'huong-dan-thanh-toan.jpg', 1, N'Payment Guide', N'Payment Guide', N'Huong dan thanh toan', N'huong-dan-thanh-toan', CAST(N'2022-12-06T16:37:38.030' AS DateTime), 1)
INSERT [dbo].[Pages] ([PageID], [PageName], [Contents], [Thumb], [Published], [Title], [MetaDesc], [MetaKey], [Alias], [CreateDate], [Ordering]) VALUES (3, N'Contact', N'<p>Contact<br></p>', N'default.jpg', 1, N'Contact', N'Contact', N'Contact', N'lien-he', CAST(N'2022-12-06T19:45:29.000' AS DateTime), 2)
INSERT [dbo].[Pages] ([PageID], [PageName], [Contents], [Thumb], [Published], [Title], [MetaDesc], [MetaKey], [Alias], [CreateDate], [Ordering]) VALUES (4, N'Introduce', N'<p>Introduce<br></p>', N'default.jpg', 1, N'Introduce', N'Introduce', N'Introduce', N'gioi-thieu', CAST(N'2022-12-06T19:46:04.000' AS DateTime), 3)
SET IDENTITY_INSERT [dbo].[Pages] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (1, N'ESSENTIALS CAMO PRINT FRENCH TERRY HOODIE', NULL, N'ESSENTIALS CAMO PRINT FRENCH TERRY HOODIE', 1, 600000, 0, N'Essentials_Camo_Print_French_Ter.jpg', NULL, CAST(N'2022-12-07T20:19:23.000' AS DateTime), CAST(N'2022-12-11T17:03:27.603' AS DateTime), 1, 1, 1, N'ESSENTIALS CAMO PRINT FRENCH TERRY HOODIE', N'essentials camo print french terry hoodie', N'Áo thun nam', N'essentials camo print french terry hoodie', 20, N'essentials camo print french terry hoodie')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (2, N'COLOMBIA CREW SWEATSHIRT', N'COLOMBIA CREW SWEATSHIRT', N'COLOMBIA CREW SWEATSHIRT', 1, 1000000, 0, N'Colombia_Crew_Sweatshirt_Red_HD8.jpg', NULL, CAST(N'2022-12-07T20:19:23.710' AS DateTime), CAST(N'2022-12-07T20:19:58.750' AS DateTime), 1, 1, 1, N'COLOMBIA CREW SWEATSHIRT', N'colombia crew sweatshirt', N'COLOMBIA CREW SWEATSHIRT', N'colombia crew sweatshirt', 19, N'colombia crew sweatshirt')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (3, N'AEROREADY GAME AND GO CAMO LOGO HOODIE', N'AEROREADY GAME AND GO CAMO LOGO HOODIE', N'AEROREADY GAME AND GO CAMO LOGO HOODIE', 1, 700000, 0, N'AEROREADY_Game_and_Go_Camo_Logo.jpg', NULL, CAST(N'2022-12-07T20:19:23.710' AS DateTime), CAST(N'2022-12-07T20:19:58.750' AS DateTime), 1, 1, 1, N'AEROREADY GAME AND GO CAMO LOGO HOODIE', N'aeroready game and go camo logo hoodie', N'AEROREADY GAME AND GO CAMO LOGO HOODIE', N'aeroready game and go camo logo hoodie', 20, N'aeroready game and go camo logo hoodie')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (4, N'ADICOLOR TREFOIL MESH TOP', N'ADICOLOR TREFOIL MESH TOP', N'ADICOLOR TREFOIL MESH TOP', 2, 550000, 0, N'adicolor_Trefoil_Mesh_Top_Green.jpg', NULL, CAST(N'2022-12-07T20:19:23.710' AS DateTime), CAST(N'2022-12-07T20:19:58.750' AS DateTime), 1, 1, 1, N'ADICOLOR TREFOIL MESH TOP', N'adicolor trefoil mesh top', N'ADICOLOR TREFOIL MESH TOP', N'adicolor trefoil mesh top', 20, N'adicolor trefoil mesh top')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (5, N'ADICOLOR TREFOIL CUTOUT LONG SLEEVE DRESS', N'ADICOLOR TREFOIL CUTOUT LONG SLEEVE DRESS', N'ADICOLOR TREFOIL CUTOUT LONG SLEEVE DRESS', 2, 999000, 0, N'adicolor_Trefoil_Cutout_Long_Sle.jpg', NULL, CAST(N'2022-12-07T20:19:23.710' AS DateTime), CAST(N'2022-12-07T20:19:58.750' AS DateTime), 1, 1, 1, N'ADICOLOR TREFOIL CUTOUT LONG SLEEVE DRESS', N'adicolor trefoil cutout long sleeve dress', N'ADICOLOR TREFOIL CUTOUT LONG SLEEVE DRESS', N'adicolor trefoil cutout long sleeve dress', 20, N'adicolor trefoil cutout long sleeve dress')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (6, N'ADICOLOR ESSENTIALS TIGHTS', N'ADICOLOR ESSENTIALS TIGHTS', N'ADICOLOR ESSENTIALS TIGHTS', 2, 800000, 0, N'Adicolor_Essentials_Tights_Grey.jpg', NULL, CAST(N'2022-12-07T20:19:23.710' AS DateTime), CAST(N'2022-12-07T20:19:58.750' AS DateTime), 1, 1, 1, N'ADICOLOR ESSENTIALS TIGHTS', N'adicolor essentials tights', N'ADICOLOR ESSENTIALS TIGHTS', N'adicolor essentials tights', 20, N'adicolor essentials tights')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (7, N'ADICOLOR TREFOIL HOODIE (PLUS SIZE)', N'ADICOLOR TREFOIL HOODIE (PLUS SIZE)', N'ADICOLOR TREFOIL HOODIE (PLUS SIZE)', 2, 1800000, 0, N'adicolor_Trefoil_Hoodie_Plus_Siz.jpg', NULL, CAST(N'2022-12-07T20:19:23.710' AS DateTime), CAST(N'2022-12-07T20:19:58.750' AS DateTime), 1, 1, 1, N'ADICOLOR TREFOIL HOODIE (PLUS SIZE)', N'adicolor trefoil hoodie (plus size)', N'ADICOLOR TREFOIL HOODIE (PLUS SIZE)', N'adicolor trefoil hoodie (plus size)', 20, N'adicolor trefoil hoodie (plus size)')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (8, N'7/8 LINEN WIDE LEG PANTS', N'7/8 LINEN WIDE LEG PANTS', N'7/8 LINEN WIDE LEG PANTS', 2, 880000, 0, N'7-8_Linen_Wide_Leg_Pants_Purple.jpg', NULL, CAST(N'2022-12-07T20:19:23.710' AS DateTime), CAST(N'2022-12-07T20:19:58.750' AS DateTime), 1, 0, 1, N'7/8 LINEN WIDE LEG PANTS', N'7/8 linen wide leg pants', N'7/8 LINEN WIDE LEG PANTS', N'7/8 linen wide leg pants', 20, N'7/8 linen wide leg pants')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (9, N'OWN THE RUN TEE', N'OWN THE RUN TEE', N'OWN THE RUN TEE', 1, 799000, 0, N'Own_the_Run_Tee_Pink_HL5985_25_m.jpg', NULL, CAST(N'2022-12-07T20:19:23.710' AS DateTime), CAST(N'2022-12-07T20:19:58.750' AS DateTime), 1, 0, 1, N'OWN THE RUN TEE', N'own the run tee', N'OWN THE RUN TEE', N'own the run tee', 20, N'own the run tee')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (10, N'MEXICO 22 HOME JERSEY', N'MEXICO 22 HOME JERSEY', N'MEXICO 22 HOME JERSEY', 3, 699000, 0, N'Mexico_22_Home_Jersey_Green_HE88.jpg', NULL, CAST(N'2022-12-07T20:19:23.710' AS DateTime), CAST(N'2022-12-07T20:19:58.750' AS DateTime), 1, 0, 1, N'MEXICO 22 HOME JERSEY', N'mexico 22 home jersey', N'MEXICO 22 HOME JERSEY', N'mexico 22 home jersey', 20, N'mexico 22 home jersey')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (11, N'ESSENTIAL COTTON JOGGERS', N'ESSENTIAL COTTON JOGGERS', N'ESSENTIAL COTTON JOGGERS', 3, 700000, 0, N'Essential_Cotton_Joggers_Black_G.jpg', NULL, CAST(N'2022-12-07T20:19:23.710' AS DateTime), CAST(N'2022-12-07T20:19:58.750' AS DateTime), 1, 0, 1, N'ESSENTIAL COTTON JOGGERS', N'essential cotton joggers', N'ESSENTIAL COTTON JOGGERS', N'essential cotton joggers', 20, N'essential cotton joggers')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (12, N'ADICOLOR SST TRACK JACKET', N'ADICOLOR SST TRACK JACKET', N'ADICOLOR SST TRACK JACKET', 3, 600000, 0, N'Adicolor_SST_Track_Jacket_Black.jpg', NULL, CAST(N'2022-12-07T20:19:23.710' AS DateTime), CAST(N'2022-12-07T20:19:58.750' AS DateTime), 1, 0, 1, N'ADICOLOR SST TRACK JACKET', N'adicolor sst track jacket', N'ADICOLOR SST TRACK JACKET', N'adicolor sst track jacket', 20, N'adicolor sst track jacket')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (14, N'NIKE ACG ICON FLEECE', N'NIKE ACG ICON FLEECE', N'NIKE ACG ICON FLEECE', 3, 1220000, 0, N'acg-icon-fleece-older-oversized.jpg', NULL, CAST(N'2022-12-07T20:19:23.710' AS DateTime), CAST(N'2022-12-07T20:19:58.750' AS DateTime), 0, 0, 1, N'NIKE ACG ICON FLEECE', NULL, N'NIKE ACG ICON FLEECE', N'Nike ACG Icon Fleece', 200, N'NIKE ACG ICON FLEECE')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (15, N'YesSir', NULL, N'<p>aCCC</p>', 2, 10000, 0, N'0', NULL, CAST(N'2022-12-07T20:19:23.710' AS DateTime), CAST(N'2022-12-07T20:19:58.750' AS DateTime), 0, 0, 0, NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (16, N'123', NULL, NULL, 1, 200, 0, N'ao-thun-nam.jpg', NULL, CAST(N'2022-12-07T20:19:23.710' AS DateTime), CAST(N'2022-12-07T20:19:58.750' AS DateTime), 0, 0, 0, NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (17, N'2', NULL, NULL, 2, 200, 0, N'ao-thun-nam.jpg', NULL, CAST(N'2022-12-07T20:19:23.710' AS DateTime), CAST(N'2022-12-07T20:19:58.750' AS DateTime), 0, 0, 0, NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (18, N'NIKE AIR', NULL, N'NIKE AIR', 3, 780000, 0, N'air-older-top-mcsmW7.jpg', NULL, CAST(N'2022-12-07T20:19:23.710' AS DateTime), CAST(N'2022-12-07T20:19:58.750' AS DateTime), 0, 0, 1, NULL, N'NIKE AIR', NULL, N'Nike Air', 200, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (19, N'Nike Dri-FIT Academy', NULL, NULL, 1, 659000, 0, N'dri-fit-academy-short-sleeve-foo.jpg', NULL, CAST(N'2022-12-07T20:19:23.710' AS DateTime), CAST(N'2022-12-07T20:19:58.750' AS DateTime), 0, 0, 0, NULL, N'Nike Dri-FIT Academy', NULL, NULL, 20, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (20, N'Nike Dri-FIT One', NULL, N'Nike Dri-FIT One', 2, 200000, 0, N'dri-fit-one-slim-fit-short-sleev.jpg', NULL, CAST(N'2022-12-07T20:19:23.710' AS DateTime), CAST(N'2022-12-07T20:19:58.750' AS DateTime), 0, 0, 1, N'Nike Dri-FIT One', N'Nike Dri-FIT One', N'Nike Dri-FIT One', N'Nike Dri-FIT One', 200, N'Nike Dri-FIT One')
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [RoleName], [Description]) VALUES (1, N'Admin', N'Admin')
INSERT [dbo].[Roles] ([RoleID], [RoleName], [Description]) VALUES (2, N'Staff', N'Nhân viên')
INSERT [dbo].[Roles] ([RoleID], [RoleName], [Description]) VALUES (3, N'Seller', N'Người bán hàng')
INSERT [dbo].[Roles] ([RoleID], [RoleName], [Description]) VALUES (4, N'Super Admin', N'Super Hyper Mega Ultra Ultimate Admin')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[TinTucs] ON 

INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (1, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:57:18.760' AS DateTime), 1, NULL, NULL, NULL, NULL, 0, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (2, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (3, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (4, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (5, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (6, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (7, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (8, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (9, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (10, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (11, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (12, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (13, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (14, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (15, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (16, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (17, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (18, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (19, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (20, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (21, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (22, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (23, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (24, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (25, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (26, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (27, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (28, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (29, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (30, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (31, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (32, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (33, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (34, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (35, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (36, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (37, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (38, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (39, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (40, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (41, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-12T20:43:29.677' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[TinTucs] OFF
GO
SET IDENTITY_INSERT [dbo].[TransactStatus] ON 

INSERT [dbo].[TransactStatus] ([TransactStatusID], [Status], [Description]) VALUES (1, N'Waiting for the goods', N'Confirmed and preparing goods')
INSERT [dbo].[TransactStatus] ([TransactStatusID], [Status], [Description]) VALUES (2, N'Wait for confirmation', N'Confirmed by seller')
INSERT [dbo].[TransactStatus] ([TransactStatusID], [Status], [Description]) VALUES (3, N'Delivering', N'The order has been delivered to the buyer')
INSERT [dbo].[TransactStatus] ([TransactStatusID], [Status], [Description]) VALUES (4, N'Delivered successfully', N'Order has been delivered successfully')
INSERT [dbo].[TransactStatus] ([TransactStatusID], [Status], [Description]) VALUES (5, N'Cancelled', N'Order has been successfully canceled')
INSERT [dbo].[TransactStatus] ([TransactStatusID], [Status], [Description]) VALUES (6, N'Returns', N'Order has been successfully refunded')
SET IDENTITY_INSERT [dbo].[TransactStatus] OFF
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Roles]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Locations] FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([LocationID])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Locations]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Products1] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Products1]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Customers] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customers]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_TransactStatus] FOREIGN KEY([TransactStatusID])
REFERENCES [dbo].[TransactStatus] ([TransactStatusID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_TransactStatus]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CatID])
REFERENCES [dbo].[Categories] ([CatID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
USE [master]
GO
ALTER DATABASE [dbshop] SET  READ_WRITE 
GO
