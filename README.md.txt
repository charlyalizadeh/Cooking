Il ne devrait pas y avoir de probl�mes pour faire tourner le code mais au cas o�
voici les d�pendances du projet (sans prendre en compte celles de base + MySql.Data)  :
	* PresentationCode
	* WindowsBase

Il se peut que l'execution s'arr�te sans pour autant planter (ContextSwitchDeadlock).  
Il suffit alors d'appuyer sur continuer (sous Visual Studio).  
  
Ce projet ne tourne que sur Windows.

Peuplement SQL : 
 	* Pour une base de donn�es peupl�e il faut utiliser la fichier DB_cooking	
	* Pour une vision plus claire de la base de donn�es le fichier DB_creation est fourni	