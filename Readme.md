# blind robot simulator

Idea of the game is to simulate your rover remote control 

even after the attack of an alien who broke cam & mic

you can give instruction authorized of course

but you are also alowed to use your own forth function 

during the game. 

# Can you participate to code & doc ?

With pleasure, yes.

# instructions to pass:

North

northeast

is

South East


South


South West


Where is


North West


expect


statusbatterie


deployerpanneauxsolaires


replierpanneauxsolaires


deployerbras


attrapperobjet


detecterobjet


replierbras


ping


help


analyseerreur


analayseobjet


drill


analyseechantillon


transmit


pause


reset





#map
novice map 20x20 1 alien 0 pebbles 0 traps left top left indicated


normal map 20x20 to 40x40 1 alien 1 pebbles 0 traps exit at bottom right


pro map 60x60 to 120x120 3 alien 3 pebbles 1 traps exit top right or left


expert map 60x60 to 120x120 1 alien 1 pebbles 5 traps exit at the top or bottom right or left


# pebbles: 
must analyze at least one before or after having already found the alien and before leaving if not lost


energy:> = 0 otherwise lost


alien: novie / rookie have found it + analyzed enough to go out without losing


in the other levels it is necessary moreover to have transmitted the infos before going out otherwise it is lost


traps: causes a reset of pebbles or alien + random coordinates and -5% of energy (user warned)





#Special conditions to win:


find the pebbles


analysis


transmission


find alien


analysis


transmission


zero trap


go out with 5% or more of energy


no reset

# status actuel  


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

influence énergétique et temps + pannes possibles ?	
	
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
	attrapperobjet			-5		5 secondes		blocage possible / lâcher possible / panne possible si seconde =22 / 23 / 24	06/07/08
	detecterobjet			-10		10 secondes		détection ratée possible / panne possible si seconde =25 / 26			09/0A
	replierbras			-5		6 secondes	 	blocage possible / lâcher possible / panne possible si seconde =22 / 23 / 24	0B/0C/0D
	ping				-1		1 secondes		=n° de la seconde en cours / si <=10ème alors noreponse			0E
	aide				0		0			afficher l'aide									00 
	checkerror			0		0			afficher liste des errorcodes							00
	analayseobjet			-6		30 secondes												00	
	forer				-30		45 secondes		peut casser (définitif) si seconde=1						0F
	analyseechantillon		-15		30 secondes             peut échouer ou casser secondes 1 ou 2 						10	
	transmettre			-5		30 secondes		prend 10+n°seconde mais peut rater si >= 59					11
	pause				-1		10 secondes             test ping automatique avec le risque de reset en plus si seconde=54		12
	reset				-30		60 secondes												décompte FF-13
	charger				+30		45 secondes		peut échouer si seconde 43 < x < 45 						13

maps :

	rookie  map 10x10 		1 alien 	0 cailloux     0 pièges		sortie en haut à droite indiquée
	novice  map 20x20 		1 alien 	0 cailloux     0 pièges         sortie en haut à gauche indiquée 
	normal  map 20x20 a 40x40	1 alien 	1 cailloux     0 pièges         sortie en bas à droite
	pro  	map 60x60 a 120x120	3 alien 	3 cailloux     1 pièges         sortie en haut à droite ou gauche
	expert 	map 60x60 a 120x120	1 alien 	1 cailloux     5 pièges         sortie en haut ou bas à droite ou gauche
	
cailloux : doit en analyser au moins un avant ou après avoir déjà trouvé l'alien et avant de sortir sinon perdu
énergie  : >= 0 sinon perdue
alien : novice/rookie l'avoir trouvé + analysé suffit pour sortir sans perdre
	dans les autres niveaux il faut en plus avoir transmis les infos avant de sortir sinon c'est perdu
pièges : provoque un reset des trouvailles cailloux ou alien + coordonnées aléatoires et -5% d'énergie (utilisateur prévenu)

conditions spéciale pour gagner : 
	expert ordre impératif : 
		trouver le cailloux 
		analyse 
		transmission 
		trouver alien
		analyse 
		transmission
		zéro piège
		sortir avec 5% ou plus d'énergie 
		aucun reset



style d'ordres à générer :  résultats
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
	status			30% énergie
	deployerpanneauxsolaires
	charger			50% énergie
	




[code](BRS.fs)
