simulateur de robot aveugle

instructions à passer :
	nord 
	nordest
	est
	sudest
	sud
	sudouest
	ouest
	nordouest
	attendre
	statusbatterie
	deployerpanneauxsolaires
	replierpanneauxsolaires
	deployerbras
	attrapperobjet
	detecterobjet
	replierbras
	ping
	aide
	analyseerreur
	analayseobjet
	forer
	analyseechantillon
	transmettre
	pause
	reset

influance energetique et temps + pannes possibles ?	
	
	nord 				-1		1 seconde		blocage possible								01
	nordest				-1		1 seconde		blocage possible								01
	est				-1		1 seconde		blocage possible								01
	sudest				-1		1 seconde		blocage possible								01
	sud				-1		1 seconde		blocage possible								01
	sudouest			-1		1 seconde		blocage possible								01
	ouest				-1		1 seconde		blocage possible								01
	nordouest			-1		1 seconde		blocage possible								01
	attendre			-1/secondes	N secondes												00
	statusbatterie			-1		1 seconde		ne pas afficher possible							02
	deployerpanneauxsolaires	-10		20 secondes		panne possible si seconde en cours=0						03
	replierpanneauxsolaires		-15		25 secondes		panne possible si seconde en cours=33						04
	deployerbras			-20		20 secondes		panne possible si seconde en cours=45						05
	attrapperobjet			-5		5 secondes		blocage possible / lacher possible / panne possible si seconde =22 / 23 / 24	06/07/08
	detecterobjet			-10		10 secondes		detection ratée possible / panne possible si seconde =25 / 26			09/0A
	replierbras			-5		6 secondes	 	blocage possible / lacher possible / panne possible si seconde =22 / 23 / 24	0B/0C/0D
	ping				-1		1 secondes		=n° de la seconde en cours / si <=10ieme alors noreponse			0E
	aide				0		0			afficher l'aide									00 
	checkerror			0		0			afficher liste des errorcodes							00
	analayseobjet			-6		30 secondes												00	
	forer				-30		45 secondes		peut casser (définitif) si seconde=1						0F
	analyseechantillon		-15		30 secondes             peut echouer ou casser secondes 1 ou 2 						10	
	transmettre			-5		30 secondes		prend 10+n°seconde mais peut rater si >= 59					11
	pause				-1		10 secondes             test ping automatique avec le risque de reset en plus si seconde=54		12
	reset				-30		60 secondes												decompte FF-13
	charger				+30		45 secondes		peut echouer si seconde 43 < x < 45 						13

maps :

	rookie  map 10x10 		1 alien 	0 cailloux     0 pieges		sortie en haut à droite indiquée
	novice  map 20x20 		1 alien 	0 cailloux     0 pieges         sortie en haut à gauche indiquée 
	normal  map 20x20 a 40x40	1 alien 	1 cailloux     0 pieges         sortie en bas à droite
	pro  	map 60x60 a 120x120	3 alien 	3 cailloux     1 pieges         sortie en haut à droite ou gauche
	expert 	map 60x60 a 120x120	1 alien 	1 cailloux     5 pieges         sortie en haut ou bas à droite ou gauche
	
cailloux : doit en analyser au moins un avant ou apres avoir déjà trouvé l'alien et avant de sortir sinon perdu
energie  : >= 0 sinon perdue
alien : novie/rookie l'avoir trouvé + analysé suffit pour sortir sans perdre
	dans les autres niveaux il faut en plus avoir transmi les infos avant de sortir sinon c'est perdu
pieges : provoque un reset des trouvailles cailloux ou alien + coordonnées aleatoires et -5% d'energie (utilisateur prévenu)

conditions spéciale pour gagner : 
	expert orde imperatif : 
		trouver le cailloux 
		analyse 
		transmission 
		trouver alien
		analyse 
		transmission
		zéro piege
		sortir avec 5% ou plus d'énérgie 
		aucun reset



style d'ordres à générer :  resultats
	ouest
	analyse			bloqué
	est 
	analyse			objet
	deployerbras		ERREUR 05
	05 CHECKERREUR		bras
	repliserbras
	deployerbras
	analyseobjet		alien
	transmission		ping first
	ping			
	transmission
	status			30% energie
	deployerpanneauxsolaires
	charger			50% energie
	





