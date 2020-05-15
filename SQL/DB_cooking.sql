-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: cooking
-- ------------------------------------------------------
-- Server version	8.0.19

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
-- Table structure for table `client`
--

DROP TABLE IF EXISTS `client`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `client` (
  `pseudo` varchar(15) NOT NULL,
  `mdp` varchar(20) DEFAULT NULL,
  `nom` varchar(25) DEFAULT NULL,
  `prenom` varchar(25) DEFAULT NULL,
  `num_tel` varchar(10) DEFAULT NULL,
  `solde` float DEFAULT NULL,
  PRIMARY KEY (`pseudo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `client`
--

LOCK TABLES `client` WRITE;
/*!40000 ALTER TABLE `client` DISABLE KEYS */;
INSERT INTO `client` VALUES ('TEST1','TEST','Courbin','Pierre','0158213544',426),('TEST2','TEST','Alizadeh','Charly','0621548244',0),('TEST3','TEST','Hamzi','Kenza','0425123655',4),('TEST4','TEST','Lesauvage','Juliette','0542178233',9980),('TEST5','TEST','Limnavong','Thomas','0872146322',0),('TEST6','TEST','Mejri','Sawsan','0542136988',0);
/*!40000 ALTER TABLE `client` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-10 20:09:55

-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: cooking
-- ------------------------------------------------------
-- Server version	8.0.19

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
-- Table structure for table `commanderecette`
--

DROP TABLE IF EXISTS `commanderecette`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `commanderecette` (
  `date_commande` datetime NOT NULL,
  `nom_recetteC` varchar(30) NOT NULL,
  `client_pseudo` varchar(15) NOT NULL,
  `quantite` int DEFAULT NULL,
  PRIMARY KEY (`date_commande`,`nom_recetteC`,`client_pseudo`),
  KEY `nom_recetteC` (`nom_recetteC`),
  KEY `client_pseudo` (`client_pseudo`),
  CONSTRAINT `client_pseudo` FOREIGN KEY (`client_pseudo`) REFERENCES `client` (`pseudo`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `nom_recetteC` FOREIGN KEY (`nom_recetteC`) REFERENCES `recette` (`nom`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `commanderecette`
--

LOCK TABLES `commanderecette` WRITE;
/*!40000 ALTER TABLE `commanderecette` DISABLE KEYS */;
INSERT INTO `commanderecette` VALUES ('2020-05-10 19:57:04','Boeuf bourguignon','TEST4',2),('2020-05-10 19:57:44','Ghormeh Sabzi','TEST1',1),('2020-05-10 19:57:46','Gâteau au yaourt','TEST1',1),('2020-05-10 19:57:49','Gâteau au yaourt','TEST1',1);
/*!40000 ALTER TABLE `commanderecette` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-10 20:09:55

-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: cooking
-- ------------------------------------------------------
-- Server version	8.0.19

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
-- Table structure for table `fournisseur`
--

DROP TABLE IF EXISTS `fournisseur`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `fournisseur` (
  `ref_fournisseur` varchar(7) NOT NULL,
  `nom` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`ref_fournisseur`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fournisseur`
--

LOCK TABLES `fournisseur` WRITE;
/*!40000 ALTER TABLE `fournisseur` DISABLE KEYS */;
INSERT INTO `fournisseur` VALUES ('F000','Metro Paris'),('F001','Metro Lyon'),('F002','Metro Marseille'),('F003','Metro Teheran');
/*!40000 ALTER TABLE `fournisseur` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-10 20:09:55

-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: cooking
-- ------------------------------------------------------
-- Server version	8.0.19

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
-- Table structure for table `listeproduit`
--

DROP TABLE IF EXISTS `listeproduit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `listeproduit` (
  `nom_produit` varchar(20) NOT NULL,
  `nom_recette` varchar(30) NOT NULL,
  `quantite` float DEFAULT NULL,
  PRIMARY KEY (`nom_produit`,`nom_recette`),
  KEY `nom_recette` (`nom_recette`),
  CONSTRAINT `nom_produit` FOREIGN KEY (`nom_produit`) REFERENCES `produit` (`nom`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `nom_recette` FOREIGN KEY (`nom_recette`) REFERENCES `recette` (`nom`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `listeproduit`
--

LOCK TABLES `listeproduit` WRITE;
/*!40000 ALTER TABLE `listeproduit` DISABLE KEYS */;
INSERT INTO `listeproduit` VALUES ('Ail','Boeuf bourguignon',2),('Beurre','Boeuf bourguignon',60),('Beurre','Gâteau au yaourt',1.67),('Beurre','Tarte aux fraises',125),('Boeuf','Boeuf bourguignon',1500),('Boeuf','Ghormeh Sabzi',1000),('Carotte','Boeuf bourguignon',2),('Champignon','Boeuf bourguignon',250),('Coriandre','Ghormeh Sabzi',1),('Eau','Tarte aux fraises',0.5),('Farine','Boeuf bourguignon',60),('Farine','Tarte aux fraises',250),('Fraise','Tarte aux fraises',500),('Haricot rouge','Ghormeh Sabzi',200),('Huile vegetale','Ghormeh Sabzi',1),('Jaune oeuf','Tarte aux fraises',2),('Lait','Tarte aux fraises',0.25),('Lardon','Boeuf bourguignon',200),('Oeuf','Gâteau au yaourt',1),('Oeuf','Tarte aux fraises',1),('Oignon vert','Ghormeh Sabzi',4),('Oignons','Boeuf bourguignon',10),('Persil','Ghormeh Sabzi',4),('Poivre','Boeuf bourguignon',1),('Poivre','Ghormeh Sabzi',1),('Sel','Boeuf bourguignon',1),('Sel','Ghormeh Sabzi',1),('Sel','Tarte aux fraises',1),('Sucre','Tarte aux fraises',70),('Vin rouge Bourgogne','Boeuf bourguignon',0.5),('Yaourt','Gâteau au yaourt',1);
/*!40000 ALTER TABLE `listeproduit` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-10 20:09:54

-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: cooking
-- ------------------------------------------------------
-- Server version	8.0.19

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
-- Table structure for table `produit`
--

DROP TABLE IF EXISTS `produit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `produit` (
  `nom` varchar(20) NOT NULL,
  `categorie` enum('Boisson','Sucre','Produit laitier','Légume/Fruit','Viande/Poisson/Oeuf','Céréale/Féculent','Corps gras') DEFAULT NULL,
  `unite` varchar(5) DEFAULT NULL,
  `stock_a` float DEFAULT NULL,
  `stock_min` float DEFAULT NULL,
  `stock_max` float DEFAULT NULL,
  `ref_fournisseur` varchar(7) DEFAULT NULL,
  PRIMARY KEY (`nom`),
  KEY `ref_fournisseur` (`ref_fournisseur`),
  CONSTRAINT `ref_fournisseur` FOREIGN KEY (`ref_fournisseur`) REFERENCES `fournisseur` (`ref_fournisseur`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `produit`
--

LOCK TABLES `produit` WRITE;
/*!40000 ALTER TABLE `produit` DISABLE KEYS */;
INSERT INTO `produit` VALUES ('Agneau','Viande/Poisson/Oeuf','g',0,0,0,'F003'),('Ail','Légume/Fruit','u',2,4,6,'F002'),('Beurre','Corps gras','g',436.67,281.67,560.01,'F002'),('Boeuf','Viande/Poisson/Oeuf','g',3500,3500,7500,'F000'),('Carotte','Légume/Fruit','u',2,4,6,'F000'),('Champignon','Légume/Fruit','g',250,500,750,'F001'),('Coriandre','Légume/Fruit','u',2,2,3,'F003'),('Eau','Boisson','l',1.5,1,1.5,'F000'),('Farine','Céréale/Féculent','g',810,560,930,'F001'),('Fraise','Légume/Fruit','g',1500,1000,1500,'F002'),('Haricot rouge','Légume/Fruit','g',400,400,600,'F003'),('Huile vegetale','Corps gras','l',2,2,3,'F003'),('Jaune oeuf','Viande/Poisson/Oeuf','u',6,4,6,'F002'),('Lait','Produit laitier','l',0.75,0.5,0.75,'F001'),('Lardon','Viande/Poisson/Oeuf','g',200,400,600,'F001'),('Oeuf','Viande/Poisson/Oeuf','u',4,3,6,'F001'),('Oignon vert','Légume/Fruit','u',8,8,12,'F002'),('Oignons','Légume/Fruit','u',10,20,30,'F001'),('Persil','Légume/Fruit','u',8,8,12,'F003'),('Poivre','Sucre','g',3,3,6,'F000'),('Sel','Sucre','g',6,3.5,9,'F002'),('Sucre','Sucre','g',210,140,210,'F000'),('Vin rouge Bourgogne','Boisson','l',0.5,1,1.5,'F000'),('Yaourt','Produit laitier','u',1,2,3,'F000');
/*!40000 ALTER TABLE `produit` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-10 20:09:54
-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: cooking
-- ------------------------------------------------------
-- Server version	8.0.19

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
-- Table structure for table `recette`
--

DROP TABLE IF EXISTS `recette`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recette` (
  `nom` varchar(20) NOT NULL,
  `type` enum('Entrée','Plat','Dessert') DEFAULT NULL,
  `descriptif` tinytext,
  `prix` float DEFAULT NULL,
  `remuneration` float DEFAULT NULL,
  `pseudo` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`nom`),
  KEY `pseudo` (`pseudo`),
  CONSTRAINT `pseudo` FOREIGN KEY (`pseudo`) REFERENCES `client` (`pseudo`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `recette`
--

LOCK TABLES `recette` WRITE;
/*!40000 ALTER TABLE `recette` DISABLE KEYS */;
INSERT INTO `recette` VALUES ('Boeuf bourguignon','Plat','Estouffade de boeuf.',10,2,'TEST1'),('Gâteau au yaourt','Dessert','Gâteau à base de yaourt',10,2,'TEST3'),('Ghormeh Sabzi','Plat','Plat typique iranien',10,2,'TEST1'),('Tarte aux fraises','Dessert','Tarte aux fraises',10,2,'TEST6');
/*!40000 ALTER TABLE `recette` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-10 20:09:54

DROP USER cookingadmin@localhost;
FLUSH PRIVILEGES;  
CREATE USER cookingadmin@localhost IDENTIFIED BY 'cooking';
GRANT ALL ON cooking.* TO 'cookingadmin'@'localhost'
