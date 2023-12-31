USE [master]
GO
/****** Object:  Database [db-bank-account-simulator]    Script Date: 10/10/2023 12:52:02 p. m. ******/
CREATE DATABASE [db-bank-account-simulator]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'db-bank-account-simulator', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\db-bank-account-simulator.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'db-bank-account-simulator_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\db-bank-account-simulator_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [db-bank-account-simulator] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db-bank-account-simulator].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [db-bank-account-simulator] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET ARITHABORT OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db-bank-account-simulator] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [db-bank-account-simulator] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET  DISABLE_BROKER 
GO
ALTER DATABASE [db-bank-account-simulator] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db-bank-account-simulator] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [db-bank-account-simulator] SET  MULTI_USER 
GO
ALTER DATABASE [db-bank-account-simulator] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [db-bank-account-simulator] SET DB_CHAINING OFF 
GO
ALTER DATABASE [db-bank-account-simulator] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [db-bank-account-simulator] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [db-bank-account-simulator] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [db-bank-account-simulator] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [db-bank-account-simulator] SET QUERY_STORE = ON
GO
ALTER DATABASE [db-bank-account-simulator] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [db-bank-account-simulator]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 10/10/2023 12:52:03 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[DocumentNumber] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[State] [bit] NOT NULL,
	[CustomerTypeID] [int] NOT NULL,
	[PhoneNumber] [varchar](50) NULL,
	[LegalRepresentativeID] [int] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerType]    Script Date: 10/10/2023 12:52:03 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerType](
	[CustomerTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[State] [bit] NOT NULL,
 CONSTRAINT [PK_CustomerType] PRIMARY KEY CLUSTERED 
(
	[CustomerTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FinancialMovements]    Script Date: 10/10/2023 12:52:03 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinancialMovements](
	[FinancialMovementsID] [int] IDENTITY(1,1) NOT NULL,
	[FinancialProductID] [int] NOT NULL,
	[MovementDate] [date] NOT NULL,
	[MovementType] [varchar](10) NOT NULL,
	[Value] [numeric](18, 2) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[Balance] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_FinancialMovements] PRIMARY KEY CLUSTERED 
(
	[FinancialMovementsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FinancialProduct]    Script Date: 10/10/2023 12:52:03 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinancialProduct](
	[FinancialProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductTypeID] [int] NOT NULL,
	[DateInit] [date] NOT NULL,
	[State] [bit] NOT NULL,
	[CustomerID] [int] NOT NULL,
	[Balance] [numeric](18, 2) NULL,
	[MontlyInterest] [numeric](18, 2) NULL,
 CONSTRAINT [PK_FinancialProduct] PRIMARY KEY CLUSTERED 
(
	[FinancialProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductType]    Script Date: 10/10/2023 12:52:03 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductType](
	[ProductTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[State] [bit] NOT NULL,
	[MontlyInterest] [numeric](18, 2) NULL,
 CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED 
(
	[ProductTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[CustomerType] ON 

INSERT [dbo].[CustomerType] ([CustomerTypeID], [Description], [State]) VALUES (1, N'Persona', 1)
INSERT [dbo].[CustomerType] ([CustomerTypeID], [Description], [State]) VALUES (2, N'Empresarial', 1)
INSERT [dbo].[CustomerType] ([CustomerTypeID], [Description], [State]) VALUES (3, N'Representante Legal', 1)
SET IDENTITY_INSERT [dbo].[CustomerType] OFF
GO

SET IDENTITY_INSERT [dbo].[ProductType] ON 

INSERT [dbo].[ProductType] ([ProductTypeID], [Description], [State], [MontlyInterest]) VALUES (1, N'Cuenta de ahorros', 1, CAST(0.04 AS Numeric(18, 2)))
INSERT [dbo].[ProductType] ([ProductTypeID], [Description], [State], [MontlyInterest]) VALUES (2, N'Cuenta corriente', 1, NULL)
INSERT [dbo].[ProductType] ([ProductTypeID], [Description], [State], [MontlyInterest]) VALUES (3, N'CDT', 1, NULL)
SET IDENTITY_INSERT [dbo].[ProductType] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [CK_DocumentNumber_Unique]    Script Date: 10/10/2023 12:52:03 p. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [CK_DocumentNumber_Unique] ON [dbo].[Customer]
(
	[DocumentNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_CustomerType] FOREIGN KEY([CustomerTypeID])
REFERENCES [dbo].[CustomerType] ([CustomerTypeID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_CustomerType]
GO
ALTER TABLE [dbo].[FinancialProduct]  WITH CHECK ADD  CONSTRAINT [FK_FinancialProduct_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FinancialProduct] CHECK CONSTRAINT [FK_FinancialProduct_Customer]
GO
ALTER TABLE [dbo].[FinancialProduct]  WITH CHECK ADD  CONSTRAINT [FK_FinancialProduct_ProductType] FOREIGN KEY([ProductTypeID])
REFERENCES [dbo].[ProductType] ([ProductTypeID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FinancialProduct] CHECK CONSTRAINT [FK_FinancialProduct_ProductType]
GO
/****** Object:  StoredProcedure [dbo].[spGetAverageBalanceByProductTypeID]    Script Date: 10/10/2023 12:52:03 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Luis Alberto Sotelo
-- Create date: 2023-10-09
-- Description:	Procedimiento para obtener el saldo promedio de los clientes según el tipo de producto
-- =============================================
CREATE PROCEDURE [dbo].[spGetAverageBalanceByProductTypeID] 
	@ProductTypeID int
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT
		PT.Description ProductType,
		C.DocumentNumber,
		C.Name,
		C.PhoneNumber,
		AVG(FM.BALANCE) as Balance
	FROM
		[dbo].[FinancialMovements] FM 
		INNER JOIN [dbo].[FinancialProduct] P ON (FM.FinancialProductID = P.FinancialProductID)
		INNER JOIN [dbo].[ProductType] PT ON (PT.ProductTypeID = P.ProductTypeID)
		INNER JOIN [dbo].[Customer] C ON (C.CustomerID = FM.CustomerID)
	WHERE
		PT.ProductTypeID =  @ProductTypeID
	GROUP BY
		PT.Description,
		C.DocumentNumber,
		C.Name,
		C.PhoneNumber
END
GO
/****** Object:  StoredProcedure [dbo].[spGetTopBalanceCustomers]    Script Date: 10/10/2023 12:52:03 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Luis Alberto Sotelo
-- Create date: 2023-10-09
-- Description:	Procedimiento para obtener el TOP 10 de saldo total de los clientes según el tipo de producto
-- =============================================
CREATE PROCEDURE [dbo].[spGetTopBalanceCustomers]
	@ProductTypeID int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT TOP 10	
		PT.Description ProductType,
		C.DocumentNumber,
		C.Name,
		C.PhoneNumber,
		FP.Balance
	FROM
		[dbo].[FinancialProduct] FP
		INNER JOIN Customer C ON (C.CustomerID = FP.CustomerID)
		INNER JOIN ProductType PT ON (PT.ProductTypeID = FP.ProductTypeID)
	WHERE
		FP.ProductTypeID = @ProductTypeID
		AND FP.State = 1
	ORDER BY
		FP.Balance DESC
END
GO
USE [master]
GO
ALTER DATABASE [db-bank-account-simulator] SET  READ_WRITE 
GO
