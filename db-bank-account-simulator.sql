USE [master]
GO

/****** Object:  Database [db-bank-account-simulator]    Script Date: 10/10/2023 3:48:42 a. m. ******/
CREATE DATABASE [db-bank-account-simulator]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'db-bank-account-simulator', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\db-bank-account-simulator.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'db-bank-account-simulator_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\db-bank-account-simulator_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
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

ALTER DATABASE [db-bank-account-simulator] SET  READ_WRITE 
GO

