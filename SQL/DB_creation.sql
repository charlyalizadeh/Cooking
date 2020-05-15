DROP DATABASE IF EXISTS cooking;
CREATE DATABASE cooking;
USE cooking;

CREATE TABLE client(
	pseudo VARCHAR(15),
    mdp VARCHAR(20),
    nom VARCHAR(25),
    prenom VARCHAR(25),
    num_tel VARCHAR(10),
    solde FLOAT,
    PRIMARY KEY(pseudo)
    );
    
CREATE TABLE fournisseur(
	ref_fournisseur VARCHAR(7),
    nom VARCHAR(20),
    PRIMARY KEY(ref_fournisseur)
);

CREATE TABLE produit(
	nom VARCHAR(20),
    categorie ENUM('Boisson','Sucre','Produit laitier','Légume/Fruit','Viande/Poisson/Oeuf','Céréale/Féculent','Corps gras'),
    unite VARCHAR(5),
    stock_a FLOAT,
    stock_min FLOAT,
    stock_max FLOAT,
    ref_fournisseur VARCHAR(7),
    PRIMARY KEY(nom),
    CONSTRAINT ref_fournisseur FOREIGN KEY (ref_fournisseur)
		REFERENCES cooking.fournisseur (ref_fournisseur)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
);

CREATE TABLE recette(
	nom VARCHAR(20),
    type ENUM('Entrée','Plat','Dessert'),
    descriptif TINYTEXT,
    prix FLOAT,
    remuneration FLOAT, 
    PRIMARY KEY(nom),
    pseudo VARCHAR(15),
    CONSTRAINT pseudo FOREIGN KEY (pseudo)
		REFERENCES cooking.client (pseudo)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE listeProduit(
	nom_produit VARCHAR(20),
    nom_recette VARCHAR(30),
    quantite FLOAT,
    PRIMARY KEY(nom_produit,nom_recette),
    CONSTRAINT nom_produit FOREIGN KEY (nom_produit)
		REFERENCES cooking.produit (nom)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT nom_recette FOREIGN KEY (nom_recette)
		REFERENCES cooking.recette (nom)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE commandeRecette(
	date_commande DATETIME,
    nom_recetteC VARCHAR(30),
    client_pseudo VARCHAR(15),
    quantite INT,
    PRIMARY KEY(date_commande,nom_recetteC,client_pseudo),
	CONSTRAINT nom_recetteC FOREIGN KEY (nom_recetteC)
		REFERENCES cooking.recette (nom)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT client_pseudo FOREIGN KEY (client_pseudo)
		REFERENCES cooking.client (pseudo)
		ON DELETE CASCADE
		ON UPDATE CASCADE
    );


DROP USER IF EXISTS cookingadmin@localhost;
FLUSH PRIVILEGES;  
CREATE USER cookingadmin@localhost IDENTIFIED BY 'cooking';
GRANT ALL ON cooking.* TO 'cookingadmin'@'localhost';






