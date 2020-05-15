Il ne devrait pas y avoir de problèmes pour faire tourner le code mais au cas où
voici les dépendances du projet (sans prendre en compte celles de base + MySql.Data)  :
	* PresentationCode
	* WindowsBase

Il se peut que l'execution s'arrête sans pour autant planter (ContextSwitchDeadlock).  
Il suffit alors d'appuyer sur continuer (sous Visual Studio).  
  
Ce projet ne tourne que sur Windows.

Peuplement SQL : 
 	* Pour une base de données peuplée il faut utiliser la fichier DB_cooking	
	* Pour une vision plus claire de la base de données le fichier DB_creation est fourni	
