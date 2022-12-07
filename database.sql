USE [dbshop]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 12/7/2022 9:06:15 AM ******/
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
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AtrributesPrices]    Script Date: 12/7/2022 9:06:15 AM ******/
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
/****** Object:  Table [dbo].[Attributes]    Script Date: 12/7/2022 9:06:15 AM ******/
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
/****** Object:  Table [dbo].[Categories]    Script Date: 12/7/2022 9:06:15 AM ******/
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
/****** Object:  Table [dbo].[Customers]    Script Date: 12/7/2022 9:06:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](250) NULL,
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
/****** Object:  Table [dbo].[Locations]    Script Date: 12/7/2022 9:06:15 AM ******/
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
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 12/7/2022 9:06:15 AM ******/
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
	[ShipDate] [datetime] NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 12/7/2022 9:06:15 AM ******/
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
	[Deleted] [bit] NULL,
	[Paid] [bit] NULL,
	[PaymentDate] [datetime] NULL,
	[PaymentID] [int] NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pages]    Script Date: 12/7/2022 9:06:15 AM ******/
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
/****** Object:  Table [dbo].[Products]    Script Date: 12/7/2022 9:06:15 AM ******/
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
/****** Object:  Table [dbo].[Roles]    Script Date: 12/7/2022 9:06:15 AM ******/
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
/****** Object:  Table [dbo].[Shippers]    Script Date: 12/7/2022 9:06:15 AM ******/
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
/****** Object:  Table [dbo].[TinTucs]    Script Date: 12/7/2022 9:06:15 AM ******/
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
/****** Object:  Table [dbo].[TransactStatus]    Script Date: 12/7/2022 9:06:15 AM ******/
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
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CatID], [CatName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [MetaDesc], [MetaKey], [Cover], [SchemaMarkup]) VALUES (1, N'Men', N'Quần áo nam', 1, 1, 1, 1, N'default.jpg', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Categories] ([CatID], [CatName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [MetaDesc], [MetaKey], [Cover], [SchemaMarkup]) VALUES (2, N'Girl 1', N'Quần áo nữ', NULL, NULL, 1, 1, N'girl-1.jpg', NULL, N'girl-1', N'Quần áo nữ', N'Quan ao nu', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Pages] ON 

INSERT [dbo].[Pages] ([PageID], [PageName], [Contents], [Thumb], [Published], [Title], [MetaDesc], [MetaKey], [Alias], [CreateDate], [Ordering]) VALUES (1, N'Hướng dẫn mua hàng ', N'<p>abc&nbsp;</p>', N'huong-dan-mua-hang.jpg', 0, N'Hướng dẫn mua hàng', N'Hướng dẫn mua hàng', N'Huong dan mua hang', N'huong-dan-mua-hang', NULL, 1)
INSERT [dbo].[Pages] ([PageID], [PageName], [Contents], [Thumb], [Published], [Title], [MetaDesc], [MetaKey], [Alias], [CreateDate], [Ordering]) VALUES (2, N'Hướng dẫn thanh toán', N'<p>Hướng dẫn thanh toán</p>', N'huong-dan-thanh-toan.jpg', 1, N'Hướng dẫn thanh toán', N'Hướng dẫn thanh toán', N'Huong dan thanh toan', N'huong-dan-thanh-toan', CAST(N'2022-12-06T16:37:38.030' AS DateTime), 1)
INSERT [dbo].[Pages] ([PageID], [PageName], [Contents], [Thumb], [Published], [Title], [MetaDesc], [MetaKey], [Alias], [CreateDate], [Ordering]) VALUES (3, N'Liên hệ', N'<p>Liên hệ<br></p>', N'default.jpg', 1, N'Liên hệ', N'Liên hệ', N'Liên hệ', N'lien-he', CAST(N'2022-12-06T19:45:29.000' AS DateTime), 2)
INSERT [dbo].[Pages] ([PageID], [PageName], [Contents], [Thumb], [Published], [Title], [MetaDesc], [MetaKey], [Alias], [CreateDate], [Ordering]) VALUES (4, N'Giới thiệu', N'<p>Giới thiệu<br></p>', N'default.jpg', 1, N'Giới thiệu', N'Giới thiệu', N'Giới thiệu', N'gioi-thieu', CAST(N'2022-12-06T19:46:04.810' AS DateTime), 3)
SET IDENTITY_INSERT [dbo].[Pages] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (1, N'Áo thun nam', N'Áo thun nam', N'Áo thun nam', 1, 30000, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, N'Áo thun nam', N'ao thun nam', N'Áo thun nam', N'Ao thun nam', 20, N'ao nam')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (2, N'Áo thun nam', N'Áo thun nam', N'Áo thun nam', 2, 30000, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, N'Áo thun nam', N'ao thun nam', N'Áo thun nam', N'Ao thun nam', 20, N'ao nam')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (3, N'Áo thun nam', N'Áo thun nam', N'Áo thun nam', 1, 30000, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, N'Áo thun nam', N'ao thun nam', N'Áo thun nam', N'Ao thun nam', 20, N'ao nam')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (4, N'Áo thun nam', N'Áo thun nam', N'Áo thun nam', 1, 30000, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, N'Áo thun nam', N'ao thun nam', N'Áo thun nam', N'Ao thun nam', 20, N'ao nam')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (5, N'Áo thun nam', N'Áo thun nam', N'Áo thun nam', 1, 30000, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, N'Áo thun nam', N'ao thun nam', N'Áo thun nam', N'Ao thun nam', 20, N'ao nam')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (6, N'Áo thun nam', N'Áo thun nam', N'Áo thun nam', 1, 30000, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, N'Áo thun nam', N'ao thun nam', N'Áo thun nam', N'Ao thun nam', 20, N'ao nam')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (7, N'Áo thun nam', N'Áo thun nam', N'Áo thun nam', 1, 30000, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, N'Áo thun nam', N'ao thun nam', N'Áo thun nam', N'Ao thun nam', 20, N'ao nam')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (8, N'Áo thun nam', N'Áo thun nam', N'Áo thun nam', 1, 30000, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, N'Áo thun nam', N'ao thun nam', N'Áo thun nam', N'Ao thun nam', 20, N'ao nam')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (9, N'Áo thun nam', N'Áo thun nam', N'Áo thun nam', 1, 30000, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, N'Áo thun nam', N'ao thun nam', N'Áo thun nam', N'Ao thun nam', 20, N'ao nam')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (10, N'Áo thun nam', N'Áo thun nam', N'Áo thun nam', 1, 30000, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, N'Áo thun nam', N'ao thun nam', N'Áo thun nam', N'Ao thun nam', 20, N'ao nam')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (11, N'Áo thun nam', N'Áo thun nam', N'Áo thun nam', 1, 30000, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, N'Áo thun nam', N'ao thun nam', N'Áo thun nam', N'Ao thun nam', 20, N'ao nam')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (12, N'Áo thun nam', N'Áo thun nam', N'Áo thun nam', 1, 30000, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, N'Áo thun nam', N'ao thun nam', N'Áo thun nam', N'Ao thun nam', 20, N'ao nam')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (14, N'Áo mưa', NULL, N'abcxyz', 1, 20000, 20, NULL, NULL, NULL, NULL, 0, 0, 1, N'Áo', NULL, N'Áo mưa', N'Ao mua', 200, N'Áo mưa')
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (15, N'YesSir', NULL, N'<p>aCCC</p>', 2, 10000, 20, N'0', NULL, NULL, NULL, 0, 0, 0, NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (16, N'123', NULL, NULL, 1, 200, 20, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (17, N'2', NULL, NULL, 2, 200, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (18, N'Áo Thun', NULL, N'<p>test</p>', 1, 20000, 0, N'ao-thun.png', NULL, CAST(N'2022-12-05T20:59:32.550' AS DateTime), CAST(N'2022-12-05T20:59:32.550' AS DateTime), 0, 0, 1, NULL, N'ao-thun', NULL, NULL, 200, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (19, N'Áo Nam', NULL, NULL, 1, 2000, 20, N'ao-nam.jpg', NULL, CAST(N'2022-12-05T21:12:40.630' AS DateTime), CAST(N'2022-12-05T21:12:40.630' AS DateTime), 0, 0, 0, NULL, N'ao-nam', NULL, NULL, 20, NULL)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CatID], [Price], [Discount], [Thumb], [Video], [DateCreated], [DateModified], [BestSeller], [HomeFlag], [Active], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitslnStock], [Title]) VALUES (20, N'Quần Nam 1', NULL, N'<p>Quần nam 2</p>', 1, 200000, 0, N'quan-nam-1.jpg', NULL, CAST(N'2022-12-06T13:36:24.000' AS DateTime), CAST(N'2022-12-06T14:06:16.287' AS DateTime), 0, 0, 1, N'Quần', N'quan-nam-1', N'Quần nam', N'Quan nam', 200, N'Quần nam')
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [RoleName], [Description]) VALUES (1, N'Admin', N'Admin')
INSERT [dbo].[Roles] ([RoleID], [RoleName], [Description]) VALUES (2, N'Staff', N'Nhân viên')
INSERT [dbo].[Roles] ([RoleID], [RoleName], [Description]) VALUES (3, N'Seller', N'Người bán hàng')
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
INSERT [dbo].[TinTucs] ([PostID], [Title], [SContents], [Contents], [Thumb], [Alias], [CreatedDate], [Published], [Author], [AccountID], [Tags], [CatID], [IsHot], [IsNewfeed], [MetaKey], [MetaDesc], [Views]) VALUES (41, N'Tin mới', NULL, N'<p>Không có</p>', N'tin-moi.png', N'tin-moi', CAST(N'2022-12-06T19:23:58.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[TinTucs] OFF
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
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Products] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Products]
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
