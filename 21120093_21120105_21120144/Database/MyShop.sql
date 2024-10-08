USE [master]
GO
CREATE DATABASE [MyShop]
 CONTAINMENT = NONE
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MyShop] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MyShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyShop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MyShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyShop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MyShop] SET  MULTI_USER 
GO
ALTER DATABASE [MyShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MyShop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MyShop] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [MyShop] SET QUERY_STORE = ON
GO
ALTER DATABASE [MyShop] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MyShop]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](100) NULL,
	[Password] [nvarchar](100) NULL,
	[Role] [nvarchar](100) NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CatId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CusId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Tel] [nvarchar](100) NULL,
	[Address] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrdDetId] [int] IDENTITY(1,1) NOT NULL,
	[OrdId] [int] NULL,
	[ProId] [int] NULL,
	[Quantity] [int] NULL,
	[PricePerItem] [int] NULL,
	[ProfitPerItem] [int] NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrdDetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrdId] [int] IDENTITY(1,1) NOT NULL,
	[CusId] [int] NULL,
	[FinalPrice] [int] NULL,
	[FinalProfit] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProId] [int] IDENTITY(1,1) NOT NULL,
	[CatId] [int] NULL,
	[Title] [nvarchar](100) NULL,
	[Description] [nvarchar](500) NULL,
	[Author] [nvarchar](100) NULL,
	[PublishedYear] [int] NULL,
	[Quantity] [int] NULL,
	[OriginalPrice] [int] NULL,
	[SellingPrice] [int] NULL,
	[ImagePath] [nvarchar](100) NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[PromId] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotions](
	[PromId] [int] IDENTITY(1,1) NOT NULL,
	[Detail] [nvarchar](200) NULL,
	[DiscountPercent] [int] NULL,
 CONSTRAINT [PK_Promotes] PRIMARY KEY CLUSTERED 
(
	[PromId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([AccId], [Username], [Password], [Role]) VALUES (1, N'admin', N'1', N'admin')
INSERT [dbo].[Accounts] ([AccId], [Username], [Password], [Role]) VALUES (2, N'sale1', N'1', N'sale')
INSERT [dbo].[Accounts] ([AccId], [Username], [Password], [Role]) VALUES (3, N'sale2', N'1', N'sale')
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CatId], [Name], [Description]) VALUES (1, N'Arts & Music', N'Delve into the vibrant worlds of color and sound! Explore the history, theory, and techniques of various art forms, weaving through musical melodies and captivating canvases. Immerse yourself in the creative process, understanding the emotions and inspirations behind stunning creations.')
INSERT [dbo].[Categories] ([CatId], [Name], [Description]) VALUES (2, N'Biographies', N'Biographies unveil the remarkable journeys of individuals, weaving the tapestry of personal experiences with societal contexts, offering windows into fascinating lives and sparking reflection on our own paths.')
INSERT [dbo].[Categories] ([CatId], [Name], [Description]) VALUES (3, N'Business', N'Descript dives deep into the world of business storytelling, revealing strategies and tools to craft compelling video narratives that capture attention, build trust, and drive results for your brand.')
INSERT [dbo].[Categories] ([CatId], [Name], [Description]) VALUES (4, N'Comics', N'Comics are captivating stories told through sequential panels, where vibrant illustrations and concise words dance together to spark your imagination. Each page bursts with action, emotion, and humor, weaving narratives across genres from superheroes to slice-of-life, inviting you to immerse yourself in worlds both familiar and fantastical.')
INSERT [dbo].[Categories] ([CatId], [Name], [Description]) VALUES (5, N'Computers & Technology', N'A Guide to Mastering Speech-to-Text and Video Editing" explores the innovative tech tool Descript, guiding users from basic transcriptions to advanced editing techniques, unlocking its full potential for creators and professionals.')
INSERT [dbo].[Categories] ([CatId], [Name], [Description]) VALUES (6, N'Cooking', N'Cooking books are delightful journeys through the culinary world, filled with tempting recipes, vibrant photos, and insightful tips that transform anyone into a kitchen maestro.')
INSERT [dbo].[Categories] ([CatId], [Name], [Description]) VALUES (7, N'Education & Reference', N'Aim to either impart knowledge (Education) or act as reliable sources for factual information (Reference). They can bridge the gap between learning new things and verifying existing knowledge, equipping readerswith vital understanding.')
INSERT [dbo].[Categories] ([CatId], [Name], [Description]) VALUES (8, N'Humor & Entertainment', N'Escape into laughter and lighthearted fun with books that tickle your funny bone and spark joy, from witty rom-coms to quirky memoirs and side-splitting comic strips.')
INSERT [dbo].[Categories] ([CatId], [Name], [Description]) VALUES (9, N'Health, Fitness & Dieting', N'Health and fitness books can focus on practical advice for nutrition, exercise, or overall well-being, or delve deeper into specific topics like weight loss, sports training, or managing a chronic condition. Whether you''re a beginner seeking guidance or a seasoned athlete honing your skills, there''s a health and fitness book out there to empower your journey.')
INSERT [dbo].[Categories] ([CatId], [Name], [Description]) VALUES (10, N'History', N'History books are captivating journeys into the past, unveiling the lives, events, and societies that shaped our world. From ancient civilizations to modern revolutions, they paint vivid portraits of human triumphs and struggles, offering valuable insights into who we are and where we came from.')
INSERT [dbo].[Categories] ([CatId], [Name], [Description]) VALUES (11, N'Horror', N'Horror delves into the darkest corners of our fears, weaving chilling tales of the supernatural, the monstrous, and the unsettlingly psychological. Be prepared for goosebumps and racing pulses as you confront haunted houses, bloodthirsty creatures, and the twisted machinations of the human mind.')
INSERT [dbo].[Categories] ([CatId], [Name], [Description]) VALUES (12, N'Kids', N'Whimsical adventures and unforgettable characters spark imaginations, with colorful stories and engaging language that grow alongside young minds!')
INSERT [dbo].[Categories] ([CatId], [Name], [Description]) VALUES (13, N'Medical', N'Unfortunately, I need more information before I can describe a medical book in two sentences. Is it a textbook for medical students, a self-help guide for patients, or something else? Knowing the target audience and purpose will help me tailor the description effectively.')
INSERT [dbo].[Categories] ([CatId], [Name], [Description]) VALUES (14, N'Romance', N'Romance novels spark emotional journeys where hearts collide, obstacles ignite tension, and happily-ever-afters paint dreamscapes of love conquering all. Dive into forbidden desires, witty banter, and soul-stirring connections that leave you breathlessly sighing, "Ah, love!"')
INSERT [dbo].[Categories] ([CatId], [Name], [Description]) VALUES (15, N'Traveling', N'Travel books transport you to faraway lands and diverse cultures, inviting you to experience the world through the eyes of intrepid adventurers, insightful chroniclers, or humor-laced globetrotters. Whether it''s scaling the Himalayas with a seasoned mountaineer or getting lost in the bustling streets of Tokyo with a wide-eyed foodie, travel books offer the ultimate escape and ignite a thirst for wanderlust.')
INSERT [dbo].[Categories] ([CatId], [Name], [Description]) VALUES (34, N'action', N'action')
INSERT [dbo].[Categories] ([CatId], [Name], [Description]) VALUES (35, N'demo', N'demo')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CusId], [Name], [Tel], [Address], [Email]) VALUES (2, N'Alex Michiel', N'093123456', N'TPHCM', N'alex123@gmail.com')
INSERT [dbo].[Customers] ([CusId], [Name], [Tel], [Address], [Email]) VALUES (3, N'Johnson', N'091123910', N'USA', N'john@gmail.com')
INSERT [dbo].[Customers] ([CusId], [Name], [Tel], [Address], [Email]) VALUES (4, N'Nguyễn Long', N'091123999', N'Huế', N'nguyenlong@gmail.com')
INSERT [dbo].[Customers] ([CusId], [Name], [Tel], [Address], [Email]) VALUES (5, N'Minh An', N'091020491', N'Đà Nẵng', N'minhan@gmail.com')
INSERT [dbo].[Customers] ([CusId], [Name], [Tel], [Address], [Email]) VALUES (6, N'test', N'demo', N'demo', N'demo')
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (91, 42, 3, 4, 160000, 20000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (108, 51, 9, 1, 100000, 30000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (109, 51, 7, 1, 150000, 50000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (110, 51, 8, 4, 110000, 30000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (111, 42, 33, 13, 540000, 40000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (112, 48, 1, 1, 140000, 40000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (113, 48, 3, 1, 180000, 40000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (115, 42, 2, 1, 280000, 80000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (117, 48, 7, 3, 150000, 50000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (122, 48, 12, 3, 110000, 30000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (123, 54, 20, 3, 140000, 40000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (128, 56, 25, 3, 340000, 40000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (129, 56, 32, 3, 300000, 50000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (130, 56, 37, 1, 120000, 20000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (132, 56, 13, 2, 110000, 20000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (133, 57, 3, 1, 200000, 60000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (134, 57, 7, 1, 150000, 50000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (135, 57, 9, 2, 100000, 30000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (136, 57, 10, 3, 80000, 30000)
INSERT [dbo].[OrderDetails] ([OrdDetId], [OrdId], [ProId], [Quantity], [PricePerItem], [ProfitPerItem]) VALUES (137, 57, 5, 1, 140000, 40000)
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrdId], [CusId], [FinalPrice], [FinalProfit], [CreatedAt], [UpdatedAt]) VALUES (42, 4, 7940000, 680000, CAST(N'2023-12-22T11:16:36.510' AS DateTime), CAST(N'2023-12-22T13:16:36.510' AS DateTime))
INSERT [dbo].[Orders] ([OrdId], [CusId], [FinalPrice], [FinalProfit], [CreatedAt], [UpdatedAt]) VALUES (48, 5, 1100000, 320000, CAST(N'2023-11-22T13:17:48.853' AS DateTime), CAST(N'2023-12-22T13:17:48.853' AS DateTime))
INSERT [dbo].[Orders] ([OrdId], [CusId], [FinalPrice], [FinalProfit], [CreatedAt], [UpdatedAt]) VALUES (51, 2, 690000, 200000, CAST(N'2024-01-04T16:48:03.457' AS DateTime), CAST(N'2024-01-04T16:48:03.457' AS DateTime))
INSERT [dbo].[Orders] ([OrdId], [CusId], [FinalPrice], [FinalProfit], [CreatedAt], [UpdatedAt]) VALUES (54, 6, 420000, 120000, CAST(N'2024-01-04T19:33:01.803' AS DateTime), CAST(N'2024-01-04T19:33:01.803' AS DateTime))
INSERT [dbo].[Orders] ([OrdId], [CusId], [FinalPrice], [FinalProfit], [CreatedAt], [UpdatedAt]) VALUES (56, 3, 2260000, 330000, CAST(N'2024-01-04T19:33:34.947' AS DateTime), CAST(N'2024-01-04T19:33:34.947' AS DateTime))
INSERT [dbo].[Orders] ([OrdId], [CusId], [FinalPrice], [FinalProfit], [CreatedAt], [UpdatedAt]) VALUES (57, 2, 930000, 300000, CAST(N'2024-01-04T19:34:40.037' AS DateTime), CAST(N'2024-01-04T19:34:40.037' AS DateTime))
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (1, 1, N'The Artist''s Way: A Spiritual Path to Higher Creativity', N'The Artist''s Wayis the seminal book on the subject of creativity. An international bestseller, millions of readers have found it to be an invaluable guide to living the artist''s life. Still as vital today-or perhaps even more so-than it was when it was first published one decade ago, it is a powerfully provocative and inspiring work. In a new introduction to the book, Julia Cameron reflects upon the impact of The Artist''s Wayand describes the work she has done during the last decade and the new.', N'Julia Cameron', 2015, 0, 100000, 155554, N'Assets/Images/01.jpg', CAST(N'2023-12-18T00:00:00.000' AS DateTime), CAST(N'2023-12-18T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (2, 1, N'Harold and the Purple Crayon', N'From beloved children''s book creator Crockett Johnson comes the timeless classic Harold and the Purple Crayon One evening Harold decides to go for a walk in the moonlight. Armed only with an oversize purple crayon, young Harold draws himself a landscape full of wonder and excitement. Harold and his trusty crayon travel through woods and across seas and past dragons before returning to bed, safe and sound. Full of funny twists and surprises, this charming story shows just how far your imaginatio', N'Crockett Johnson', 2002, 0, 200000, 280000, N'Assets/Images/02.jpg', CAST(N'2022-12-11T00:00:00.000' AS DateTime), CAST(N'2022-12-11T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (3, 1, N'Everyday Matters', N'In the tradition of Persepolis, In the Shadow of No Towers, and Our Cancer Year, an illustrated memoir of remarkable depth, power, and beauty Danny Gregory and his wife, Patti, hadn''t been married long. Their baby, Jack, was ten months old; life was pretty swell. And then Patti fell under a subway train and was paralyzed from the waist down. In a world where nothing seemed to have much meaning, Danny decided to teach himself to draw, and what he learned stunned him.', N'Danny Gregory', 2004, 0, 140000, 200000, N'Assets/Images/03.jpg', CAST(N'2023-11-11T00:00:00.000' AS DateTime), CAST(N'2023-11-11T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (4, 1, N'Dolly: My Life and Other Unfinished Business', N'The inspiring, tell-it-like-it-is autobiography of one of America''s best-loved stars. Dolly tells the rags-to-riches story of her life as only she can--with honesty, insight, and an unfailing sense of humor. Amazingly candid, incredibly warm, wise and funny, Dolly proves over and over again why she is so loved. 32 pages of photos.', N'Dolly Parton', 1998, 0, 300000, 380000, N'Assets/Images/04.jpg', CAST(N'2022-10-10T00:00:00.000' AS DateTime), CAST(N'2022-10-10T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (5, 1, N'Snowflake Bentley', N'In this Caldecott Medal-winning picture book, the true story of Wilson Bentley and his singular fascination with snowflakes is rendered in rich prose and gorgeous artwork, perfect for the holidays, snow days, and everyday.Wilson Bentley was always fascinated by snow. In childhood and adulthood, he saw each tiny crystal of a snowflake as a little miracle and wanted to understand them.His parents supported his curiosity and saved until they could give him his own camera and microscope.', N'Jacqueline Briggs Martin', 1999, 0, 100000, 140000, N'Assets/Images/05.jpg', CAST(N'2022-01-01T00:00:00.000' AS DateTime), CAST(N'2022-01-01T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (6, 2, N'The Audacity of Hope: Thoughts on Reclaiming the American Dream', N'In July 2004, four years before his presidency, Barack Obama electrified the Democratic National Convention with an address that spoke to Americans across the political spectrum. One phrase in particular anchored itself in listeners'' minds, a reminder that for all the discord and struggle to be found in our history as a nation, we have always been guided by a dogged optimism in the future, or what Obama called "the audacity of hope."', N'Barack Obama', 2008, 0, 150000, 200000, N'Assets/Images/06.jpg', CAST(N'2020-12-12T00:00:00.000' AS DateTime), CAST(N'2020-12-12T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (7, 2, N'Lincoln', N'A masterful work by Pulitzer Prize-winning author David Herbert Donald, Lincoln is a stunning portrait of Abraham Lincoln''s life and presidency. Donald brilliantly depicts Lincoln''s gradual ascent from humble beginnings in rural Kentucky to the ever-expanding political circles in Illinois, and finally to the presidency of a country divided by civil war.', N'David Herbert Donald', 2002, 1, 100000, 150000, N'Assets/Images/07.jpg', CAST(N'2021-10-10T00:00:00.000' AS DateTime), CAST(N'2021-10-10T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (8, 4, N'Weirdos from Another Planet!: A Calvin and Hobbes Collection V7', N'"Flashes of innovative genius abound. Exploring the world of Calvin and Hobbes is great therapy. The antics of the precocious boy and his suave stuffed tiger pal can pull anyone out of the doldrums." --Amarillo News Globe In Calvin and Hobbes book Weirdos From Another Planet , this power-packed extravaganza of creative energy and imagination feature the childhood fun and fantasy that was a Watterson trademark.', N'Bill Watterson', 1998, 0, 80000, 110000, N'Assets/Images/08.jpg', CAST(N'2020-12-01T00:00:00.000' AS DateTime), CAST(N'2020-12-01T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (9, 4, N'Rock Jaw: Master of the Eastern Border', N'Jeff Smith''s New York Times and USA Today bestselling, award-winning BONE books are one of the most popular graphic novel series of all time!Fone Bone and Smiley Bone strike out into the wilderness to return a lost Rat Creature cub to the mountains. It doesn''t take long before they run smack into Rock Jaw, a sly and mighty mountain lion with a none-too-friendly disposition. Life gets even more complicated when they befriend a group of baby animals who have been orphaned by Rat Creature attacks.', N'Jeff Smith', 1999, 2, 70000, 100000, N'Assets/Images/09.jpg', CAST(N'2019-05-05T00:00:00.000' AS DateTime), CAST(N'2019-05-05T00:00:00.000' AS DateTime), 3)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (10, 4, N'The Indispensable Calvin and Hobbes: A Calvin and Hobbes Treasury V11', N'Calvin, the six-year-old dirty tricksmeister and master of indignation and his warm, cuddly philosopher sidekick and Hobbes, a tiger whose idea of adventure is to lie on his back by the fire and have his stomach rubbed. This unlikely due captured the hearts, the minds, and, most of all, the funny bones of America. The Indispensable Calvin and Hobbes contains an original full-color section, as well as all the cartoons appearing in The Revenge of the Baby-Sat and Scientific Progress Goes "Boink."', N'Bill Watterson', 2003, 4, 50000, 80000, N'Assets/Images/10.jpg', CAST(N'2019-04-04T00:00:00.000' AS DateTime), CAST(N'2019-04-04T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (11, 5, N'Steve Jobs', N'Walter Isaacson''s "enthralling" (The New Yorker) worldwide bestselling biography of Apple cofounder Steve Jobs. Based on more than forty interviews with Steve Jobs conducted over two years--as well as interviews with more than 100 family members, friends, adversaries, competitors, and colleagues--Walter Isaacson has written a riveting story of the roller-coaster life and searingly intense personality of a creative entrepreneur.', N'Walter Isaacson', 2020, 6, 120000, 160000, N'Assets/Images/11.jpg', CAST(N'2019-06-06T00:00:00.000' AS DateTime), CAST(N'2019-06-06T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (12, 5, N'Excel for Dummies', N'Whatever you''re now doing with Microsoft Excel - there''s much more you could be doing. The world''s most popular spreadsheet program, Excel, grows richer in features with each new release. Excel For Dummies puts at your disposal all the powerful capabilities of version 5 -- capabilities for everything from manipulating databases to creating three-dimensional charts. Whether you''re new to Excel or just new to version 5, bestselling author Greg Harvey''s concise, clear.', N'Greg Harvey', 2005, 3, 80000, 110000, N'Assets/Images/12.jpg', CAST(N'2023-10-01T00:00:00.000' AS DateTime), CAST(N'2023-10-01T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (13, 5, N'Windows Communication Foundation 4 Step by Step', N'Your hands-on, step-by-step guide to building connected, service-oriented applications. Teach yourself the essentials of Windows Communication Foundation (WCF) 4 -- one step at a time. With this practical, learn-by-doing tutorial, you get the clear guidance and hands-on examples you need to begin creating Web services for robust Windows-based business applications. Discover how to: Build and host SOAP and REST services Maintain service contracts and data contracts Control configuration.', N'John Sharp', 2020, 4, 90000, 110000, N'Assets/Images/13.jpg', CAST(N'2022-10-10T00:00:00.000' AS DateTime), CAST(N'2022-10-10T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (14, 5, N'Developing Decision Support Systems with Microsoft Office Excel', N'Chris Albright''s VBA FOR MODELERS, 4TH EDITION is an essential tool for helping you learn to use Visual Basic for Applications (VBA) as a means to automate common spreadsheet tasks, as well as to create sophisticated management science applications. VBA is the programming language for Microsoft Office. VBA FOR MODELERS contains two parts. The first part teaches the essentials of VBA for Excel. The second part illustrates how a number of management science models can be automated with VBA.', N'S. Christian Albright', 2002, 0, 130000, 160000, N'Assets/Images/14.jpg', CAST(N'2023-03-03T00:00:00.000' AS DateTime), CAST(N'2023-03-03T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (15, 5, N'Project Management for the 21st Century', N'The challenge of managing projects is to combine the technology of the future with lessons from the past. In the Third Edition of Project Management for the 21st Century, noted authors Bennet Lientz and Kathryn Rea provide a modern, proven approach to project management. Properly applied without massive administrative overhead, project management can supply structure, focus, and control to drive work to success.Third Edition revisions include: 35% new material.', N'Bennet P. Lientz, Kathryn P. Rea, Kathryn Rea', 1999, 5, 500000, 600000, N'Assets/Images/15.jpg', CAST(N'2023-02-02T00:00:00.000' AS DateTime), CAST(N'2023-02-02T00:00:00.000' AS DateTime), 3)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (16, 6, N'French Cooking Academy: 100 Essential Recipes for the Home Cook', N'Designed with beginners in mind, the 100 recipes included in this book showcase the variety of France''s culinary delights: this includes everything from classic, widely beloved sweet and savory fare to authentic delicacies hailing from various French regions. These accessible recipes are broken down into a classic bistro-style menu, complete with everything from heavenly hors d''oeuvres and appetizers to succulent poultry, meat and veggie dishes.', N'Kate Blenkiron and Stephane Nguyễn', 2015, 20, 150000, 180000, N'Assets/Images/16.jpg', CAST(N'2023-06-10T00:00:00.000' AS DateTime), CAST(N'2023-06-10T00:00:00.000' AS DateTime), 6)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (17, 6, N'Snoop Dogg Presents Goon with the Spoon', N'The follow-up to From Crook to Cook, this next-level cookbook is all about bringing together memorable meals for any occasion, with 65+ recipes from rappers Snoop Dogg and E-40. Following the breakout success of his first cookbook, From Crook to Cook: Platinum Recipes from Tha Boss Dogg''s Kitchen (more than 1 million copies sold), Snoop Dogg returns with this new collection of recipes in collaboration with his friend and iconic Bay Area rapper E-40.', N'Snoop Dogg', 2009, 5, 200000, 260000, N'Assets/Images/17.jpg', CAST(N'2022-06-07T00:00:00.000' AS DateTime), CAST(N'2022-06-07T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (18, 7, N'A Lively and Entertaining Guide to Reading', N'A thoroughly revised and expanded edition of Thomas C. Foster''s classic guide--a lively and entertaining introduction to literature and literary basics, including symbols, themes and contexts, that shows you how to make your everyday reading experience more rewarding and enjoyable.', N'Thomas C. Foster', 2006, 5, 100000, 140000, N'Assets/Images/18.jpg', CAST(N'2023-09-08T00:00:00.000' AS DateTime), CAST(N'2023-09-08T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (19, 8, N'The Authoritative Calvin and Hobbes: A Calvin and Hobbes Treasury V6', N'The Authoritative Calvin and Hobbes , is a large-format treasury of the cartoons from Yukon Ho and Weirdos from Another Planet (including full-color Sunday cartoons) plus a full-color original story unique to this collection. Millions of readers have enjoyed the tremendous talent of Bill Watterson. His skill as both artist and writer brings to life a boy, his tiger, and the imagination and memories of his ardent readers.', N'Bill Watterson', 2004, 6, 120000, 150000, N'Assets/Images/19.jpg', CAST(N'2022-12-12T00:00:00.000' AS DateTime), CAST(N'2022-12-12T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (20, 8, N'Furiously Happy: A Funny Book about Horrible Things', N'In Furiously Happy, #1 New York Times bestselling author Jenny Lawson explores her lifelong battle with mental illness. A hysterical, ridiculous book about crippling depression and anxiety? That sounds like a terrible idea. But terrible ideas are what Jenny does best. As Jenny says: Some people might think that being ''furiously happy'' is just an excuse to be stupid and irresponsible and invite a herd of kangaroos over to your house without telling your husband first because you suspect him.', N'Jenny Lawson', 2022, 2, 100000, 140000, N'Assets/Images/20.jpg', CAST(N'2023-08-08T00:00:00.000' AS DateTime), CAST(N'2023-08-08T00:00:00.000' AS DateTime), 3)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (21, 8, N'Broken (in the Best Possible Way)', N'From the #1 New York Times bestselling author of Furiously Happy and Let''s Pretend This Never Happened comes a deeply relatable book filled with humor and honesty about depression and anxiety. As Jenny Lawson''s hundreds of thousands of fans know, she suffers from depression. In Broken, Jenny brings readers along on her mental and physical health journey, offering heartbreaking and hilarious anecdotes along the way.', N'Jenny Lawson', 2019, 5, 200000, 237500, N'Assets/Images/21.jpg', CAST(N'2023-03-03T00:00:00.000' AS DateTime), CAST(N'2023-03-03T00:00:00.000' AS DateTime), 3)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (22, 9, N'Your Path to Optimal Health and Wellbeing', N'Your LifeBook is an interactive journal and workbook designed to support your progress on your health journey. Used independently or in conjunction with Dr. A''s Habits of Health, Your LifeBook is like having Dr. A walking you through the Habits of Health, giving you lightweight daily and weekly tasks to move you forward toward your goals.', N'Wayne Scott Andersen', 2022, 5, 120000, 160000, N'Assets/Images/22.jpg', CAST(N'2023-05-05T00:00:00.000' AS DateTime), CAST(N'2023-05-05T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (23, 9, N'The Hidden Dangers in Healthy Foods That Cause Disease and Weight Gain', N'From renowned cardiac surgeon Steven R. Gundry, MD, the New York Times bestselling The Plant Paradox is a revolutionary look at the hidden compounds in "healthy" foods like fruit, vegetables, and whole grains that are causing us to gain weight and develop chronic disease.', N'Steven R. Gundry', 2022, 5, 100000, 130000, N'Assets/Images/23.jpg', CAST(N'2023-02-04T00:00:00.000' AS DateTime), CAST(N'2023-02-04T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (24, 10, N'A Short History of Nearly Everything', N'One of the world''s most beloved writers and bestselling author of One Summer takes his ultimate journey--into the most intriguing and intractable questions that science seeks to answer. In A Walk in the Woods , Bill Bryson trekked the Appalachian Trail -- well, most of it. In A Sunburned Country , he confronted some of the most lethal wildlife Australia has to offer. Now, in his biggest book, he confronts his greatest challenge: to understand -- and, if possible, answer -- the oldest.', N'Bill Bryson', 2003, 6, 120000, 160000, N'Assets/Images/24.jpg', CAST(N'2022-01-01T00:00:00.000' AS DateTime), CAST(N'2022-01-01T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (25, 10, N'Killing Kennedy: The End of Camelot', N'A riveting historical narrative of the shocking events surrounding the assassination of John F. Kennedy, and the follow-up to mega-bestselling author Bill O''Reilly''s Killing Lincoln. The basis for the 2013 television movie of the same name starring Rob Lowe as JFK.', N'Bill O''Reilly and Martin Dugard', 2018, 2, 300000, 340000, N'Assets/Images/25.jpg', CAST(N'2020-08-08T00:00:00.000' AS DateTime), CAST(N'2020-08-08T00:00:00.000' AS DateTime), 4)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (26, 11, N'The Reaping (Paperbacks from Hell)', N'Commissioned to paint a portrait at the Woolvercombe House, painter Tom Rigsby is drawn into the secluded mansion''s maze of horror and mystery. Original. This description may be from another edition of this product.', N'Bernard Taylor', 2004, 6, 200000, 280000, N'Assets/Images/26.jpg', CAST(N'2022-09-09T00:00:00.000' AS DateTime), CAST(N'2022-09-09T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (27, 11, N'Gwen, in Green', N'After receiving a large insurance settlement, young couple Gwen and George fulfill a dream by buying their own little island, a secluded, private paradise surrounded by a lush green landscape of plants. What the real estate man didn''t tell them was that a tragedy took place years earlier in the cool, clear pool near the house. And the waters still hold a terrifying, centuries-old secret. Soon George begins to notice strange changes in his wife.', N'Hugh Zachary', 2023, 7, 300000, 380000, N'Assets/Images/27.jpg', CAST(N'2023-12-12T00:00:00.000' AS DateTime), CAST(N'2023-12-12T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (28, 12, N'Everyone Poops', N'For use in schools and libraries only. Shows how creatures throughout the animal world--including humans--deal with the products of digestion.
', N'Taro Gomi', 2005, 6, 150000, 190000, N'Assets/Images/28.jpg', CAST(N'2023-11-11T00:00:00.000' AS DateTime), CAST(N'2023-11-11T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (29, 12, N'If You Give a Mouse a Cookie', N'If a hungry little mouse shows up on your doorstep, you might want to give him a cookie. And if you give him a cookie, he''ll ask for a glass of milk. He''ll want to look in a mirror to make sure he doesn''t have a milk mustache, and then he''ll ask for a pair of scissors to give himself a trim....This book is a great first introduction to Mouse, the star of the If You Give... series and a perennial favorite among children. With its spare, rhythmic text and circular tale.', N'Laura Joffe Numeroff', 2018, 6, 220000, 280000, N'Assets/Images/29.jpg', CAST(N'2023-09-09T00:00:00.000' AS DateTime), CAST(N'2023-09-09T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (30, 13, N'The Deadly Dinner Party: And Other Medical Detective Stories', N'ER and House meet Sherlock Holmes in these riveting and true stories of medical detective work. Picking up where Berton Rouech ''s The Medical Detectives left off, The Deadly Dinner Party presents fifteen edge-of-your-seat, real-life medical detective stories written by a practicing physician. Award-winning author Jonathan Edlow, M.D., shows the doctor as detective and the epidemiologist as elite sleuth in stories that are as gripping as the best thrillers.', N'Jonathan A. Edlow', 2015, 9, 200000, 260000, N'Assets/Images/30.jpg', CAST(N'2022-01-01T00:00:00.000' AS DateTime), CAST(N'2022-01-01T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (31, 14, N'The Selection', N'Fall in love--from the very beginning. Discover the first book in the captivating, #1 New York Times bestselling Selection series. Prepare to be swept into a world of breathless fairy-tale romance, swoonworthy characters, glittering gowns, and fierce intrigue perfect for readers who loved Divergent, Delirium , or The Wrath & the Dawn. For thirty-five girls, the Selection is the chance of a lifetime.', N'Kiera Cass', 2020, 4, 600000, 750000, N'Assets/Images/31.jpg', CAST(N'2021-12-31T00:00:00.000' AS DateTime), CAST(N'2021-12-31T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (32, 14, N'The Night Circus', N'The circus arrives without warning. No announcements precede it. It is simply there, when yesterday it was not. Within the black-and-white striped canvas tents is an utterly unique experience full of breathtaking amazements. It is called Le Cirque des R?ves, and it is only open at night. But behind the scenes, a fierce competition is underway: a duel between two young magicians, Celia and Marco, who have been trained since childhood expressly for this purpose by their mercurial instructors.', N'Erin Morgenstern', 2019, 3, 250000, 300000, N'Assets/Images/32.jpg', CAST(N'2022-12-31T00:00:00.000' AS DateTime), CAST(N'2022-12-31T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (33, 14, N'Outlander', N'#1 NEW YORK TIMES BESTSELLER - The first book in Diana Gabaldon''s acclaimed Outlander saga, the basis for the Starz original series. One of the top ten best-loved novels in America, as seen on PBS''s The Great American Read! Unrivaled storytelling. Unforgettable characters. Rich historical detail. These are the hallmarks of Diana Gabaldon''s work. Her New York Times bestselling Outlander novels have earned the praise of critics and captured the hearts of millions of fans.', N'Diana Gabaldon', 2017, 0, 500000, 600000, N'Assets/Images/33.jpg', CAST(N'2023-01-01T00:00:00.000' AS DateTime), CAST(N'2023-01-01T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (34, 14, N'S. Morgenstern''s Classic Tale of True Love and High Adventure', N'William Goldman''s beloved story of Buttercup, Westley, and their fellow adventurers.

This tale of true love, high adventure, pirates, princesses, giants, miracles, fencing, and a frightening assortment of wild beasts was unforgettably depicted in the 1987 film directed by Rob Reiner and starring Fred Savage, Robin Wright, and others. But, rich in character and satire, the novel boasts even more layers of ingenious storytelling.', N'William Goldman', 2019, 6, 450000, 610000, N'Assets/Images/34.jpg', CAST(N'2022-08-08T00:00:00.000' AS DateTime), CAST(N'2022-08-08T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (35, 14, N'Shatter Me', N'The gripping first installment in New York Times bestselling author Tahereh Mafi''s Shatter Me series.One touch is all it takes. One touch, and Juliette Ferrars can leave a fully grown man gasping for air. One touch, and she can kill.No one knows why Juliette has such incredible power. It feels like a curse, a burden that one person alone could never bear. But The Reestablishment sees it as a gift, sees her as an opportunity.', N'Tahereh Mafi', 2022, 6, 500000, 600000, N'Assets/Images/35.jpg', CAST(N'2023-02-02T00:00:00.000' AS DateTime), CAST(N'2023-02-02T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (36, 15, N'Lonely Planet Europe on a Shoestring', N'Cultural insights give you a richer, more rewarding travel experience - history, art, literature, cinema, landscapes Colour maps and images throughout Over 200 maps Covers Austria, Belgium, Britain, Bulgaria, Croatia, Czech Republic, France, Germany, Greece, Hungary, Ireland, Italy, Montenegro, Netherlands, Poland, Portugal, Romania, Russia, Scandinavia, Serbia, Spain, Switzerland, Turkey, Ukraine and more Useful features: First Time Europe, Big Adventures Small Budget, Off the Beaten Track.', N'Sarah Andrews, Sarah Johnstone, Lonely Planet', 2005, 6, 100000, 140000, N'Assets/Images/36.jpg', CAST(N'2023-03-03T00:00:00.000' AS DateTime), CAST(N'2023-03-03T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Products] ([ProId], [CatId], [Title], [Description], [Author], [PublishedYear], [Quantity], [OriginalPrice], [SellingPrice], [ImagePath], [CreatedAt], [UpdatedAt], [PromId]) VALUES (37, 15, N'360 Degrees Longitude: One Family''s Journey Around the World', N'In June 2005, John Higham, his wife September and his daughters Jordan and Katrina packed their bags and embarked on a 52 week global adventure, visiting 30 countries. They stayed with friends, strangers, college girls on spring break and Polish shipyard workers with a penchant for striped boxer shorts - and little else! A book to read with the family or perhaps en route to an adventure, 360 Degress Longitude is filled with funny stories, incredible pictures and a whole lot of sunshine.', N'John Higham', 2009, 4, 100000, 120000, N'Assets/Images/37.jpg', CAST(N'2022-11-11T00:00:00.000' AS DateTime), CAST(N'2022-11-11T00:00:00.000' AS DateTime), 6)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Promotions] ON 

INSERT [dbo].[Promotions] ([PromId], [Detail], [DiscountPercent]) VALUES (3, N'Tiki', 5)
INSERT [dbo].[Promotions] ([PromId], [Detail], [DiscountPercent]) VALUES (4, N'New Member', 15)
INSERT [dbo].[Promotions] ([PromId], [Detail], [DiscountPercent]) VALUES (5, N'Flash Sale', 10)
INSERT [dbo].[Promotions] ([PromId], [Detail], [DiscountPercent]) VALUES (6, N'Shopee', 10)
INSERT [dbo].[Promotions] ([PromId], [Detail], [DiscountPercent]) VALUES (7, N'New Year', 5)
INSERT [dbo].[Promotions] ([PromId], [Detail], [DiscountPercent]) VALUES (8, N'Tết', 20)
INSERT [dbo].[Promotions] ([PromId], [Detail], [DiscountPercent]) VALUES (12, N'test demo', 10)
SET IDENTITY_INSERT [dbo].[Promotions] OFF
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY([OrdId])
REFERENCES [dbo].[Orders] ([OrdId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Products] FOREIGN KEY([ProId])
REFERENCES [dbo].[Products] ([ProId])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Products]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Customers] FOREIGN KEY([CusId])
REFERENCES [dbo].[Customers] ([CusId])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customers]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CatId])
REFERENCES [dbo].[Categories] ([CatId])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Promotions] FOREIGN KEY([PromId])
REFERENCES [dbo].[Promotions] ([PromId])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Promotions]
GO
USE [master]
GO
ALTER DATABASE [MyShop] SET  READ_WRITE 
GO
