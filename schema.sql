-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: localhost    Database: kbzlifeinsurancecodetest
-- ------------------------------------------------------
-- Server version	8.0.39

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tbl_giftcard`
--

DROP TABLE IF EXISTS `tbl_giftcard`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_giftcard` (
  `GiftCardId` varchar(60) NOT NULL,
  `GiftCardNo` varchar(15) NOT NULL,
  `Title` varchar(50) NOT NULL,
  `Description` varchar(250) NOT NULL,
  `ExpiryDate` varchar(45) DEFAULT NULL,
  `Amount` decimal(18,2) NOT NULL,
  `Status` varchar(10) NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `QRCode` longblob NOT NULL,
  `GiftCardDuration` int NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT '0',
  `PhoneNumber` varchar(15) DEFAULT NULL,
  `CashbackPercentage` int DEFAULT NULL,
  PRIMARY KEY (`GiftCardId`),
  UNIQUE KEY `GiftCardNo_UNIQUE` (`GiftCardNo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_giftcard`
--

LOCK TABLES `tbl_giftcard` WRITE;
/*!40000 ALTER TABLE `tbl_giftcard` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_giftcard` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_purchase_invoice`
--

DROP TABLE IF EXISTS `tbl_purchase_invoice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_purchase_invoice` (
  `PurchaseInvoiceId` varchar(60) NOT NULL,
  `UserId` varchar(60) NOT NULL,
  `InvoiceNo` varchar(60) NOT NULL,
  `TotalAmount` decimal(18,2) NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `PaymentMethod` varchar(60) NOT NULL,
  PRIMARY KEY (`PurchaseInvoiceId`),
  UNIQUE KEY `InvoiceNo_UNIQUE` (`InvoiceNo`),
  KEY `FK_User_idx` (`UserId`),
  CONSTRAINT `FK_User` FOREIGN KEY (`UserId`) REFERENCES `tbl_user` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_purchase_invoice`
--

LOCK TABLES `tbl_purchase_invoice` WRITE;
/*!40000 ALTER TABLE `tbl_purchase_invoice` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_purchase_invoice` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_purchase_invoice_detail`
--

DROP TABLE IF EXISTS `tbl_purchase_invoice_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_purchase_invoice_detail` (
  `Id` varchar(60) NOT NULL,
  `InvoiceNo` varchar(60) NOT NULL,
  `GiftCardId` varchar(60) NOT NULL,
  `Quantity` int NOT NULL,
  `SubTotal` decimal(18,2) NOT NULL,
  `TypeOfBuying` varchar(45) NOT NULL,
  `RecipientName` varchar(45) DEFAULT NULL,
  `RecipientPhoneNumber` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_GiftCard_idx` (`GiftCardId`),
  CONSTRAINT `FK_GiftCard_Purchase_Invoice_Detail` FOREIGN KEY (`GiftCardId`) REFERENCES `tbl_giftcard` (`GiftCardId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_purchase_invoice_detail`
--

LOCK TABLES `tbl_purchase_invoice_detail` WRITE;
/*!40000 ALTER TABLE `tbl_purchase_invoice_detail` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_purchase_invoice_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_user`
--

DROP TABLE IF EXISTS `tbl_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_user` (
  `UserId` varchar(60) NOT NULL,
  `UserName` varchar(100) NOT NULL,
  `PhoneNumber` varchar(15) NOT NULL,
  `Password` varchar(200) NOT NULL,
  `UserRole` varchar(30) NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`UserId`),
  UNIQUE KEY `UserId_UNIQUE` (`UserId`),
  UNIQUE KEY `PhoneNumber_UNIQUE` (`PhoneNumber`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_user`
--

LOCK TABLES `tbl_user` WRITE;
/*!40000 ALTER TABLE `tbl_user` DISABLE KEYS */;
INSERT INTO `tbl_user` VALUES ('01J9C8GD3T07FEW9BT1JDJ1598','Lin Thit Htoo','09773871112','vYAOpSg6friSnT70Qrei/rttablaeg1DUa1YajEuoVQ=','Admin',0);
/*!40000 ALTER TABLE `tbl_user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-10-04 23:55:07
