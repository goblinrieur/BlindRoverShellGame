variable solarflag
variable alienflag
variable rockflag
variable transmitflag
variable armflag
variable xpos
variable xpiege
variable ypiege
variable ypos
variable mapxsize
variable mapysize
variable mapextractionpoint_Y
variable mapextractionpoint_X
variable battery
variable arm
variable (rnd)  
variable got
variable alienx
variable alieny
variable rockx
variable rocky
\ variables

time&date  * + - * * (rnd) ! \ seed
100 battery !


\ randomize & random functions
: rnd ( -- n)
	(rnd) @ dup 13 lshift xor
	dup 17 rshift xor
	dup DUP 5 lshift xor (rnd) !
;
 
: 10rnd 
	rnd begin dup 0 < IF -1 * THEN
	10 / dup 10 < UNTIL got ! 
;
 
: 100rnd 
	rnd begin dup 0 < IF -1 * THEN
	10 / dup 100 < UNTIL got ! 
;
 
: 1000rnd 
	rnd begin 
	dup 0 < IF -1 * THEN
	10 / dup 1000 < UNTIL got ! 
;
 
: energiedec 
	>R battery @ R> - battery ! 
;

\ functions for map management and user actions
: detectmapborder 
	xpos @ mapxsize @ > IF ." ROBOT IS LOST : YOU LOOSE" CR bye THEN
	xpos @ 0 < IF ." ROBOT IS LOST : : YOU LOOSE" CR bye THEN
	ypos @ 0 < IF ." ROBOT IS LOST : : YOU LOOSE" CR bye THEN
	ypos @ mapysize @ > IF ." ROBOT IS LOST : : YOU LOOSE" CR bye THEN
	1 energiedec 
;

\ we need to know our coordonates 
: where 
	xpos @ ." X : " . ypos @ ." Y : " . CR detectmapborder 1 energiedec 
;

: wait  
	MS where 
;

: pause 
	10000 MS 
;

\ we need to check often battery state of the rover
: batterystatus 
	battery @ dup ." BATTERY STATE :" . ." %" CR 1 < IF ." ERROR BATTERY EMPTY : YOU LOOSE !" CR bye THEN
	1 energiedec 
;

\ we need to deploy solar pannels before charging batteries
: solarpaneldeploy 
	." .DEPLOYING CHARGING PANEL. " 
	80 0 DO 
		." ." 250 MS 
	LOOP CR 1 solarflag ! 
;

: solarcapture 
	solarflag @ 
	0 = IF 
		CR ." ERROR : solarpanel not deployed" CR 
	ELSE
		CR ." CHARGING PLEASE WAIT "  pause
		battery @ 
		100 < IF 
			BEGIN
				battery @ 30 < IF 
					BEGIN
						battery @ 3 + battery ! ." ."  battery @ . ." %.." 500 MS
						battery @ 35 > UNTIL
					THEN
					battery @ 50 < IF 
						BEGIN
							battery @ 2 + battery ! ." ."  battery @ . ." %.." 800 MS
							battery @ 87 
						> UNTIL
					THEN
					battery @ 1+ battery !
					." ." battery @ . ." %.." 1500 MS
			battery @ 100 = UNTIL
			pause
		THEN
		CR 
		battery @ 100 > IF 
			pause
			100 battery !
		THEN
	CR ." you may retract solar panel now : solarpaneloff command" CR 
	THEN 
;

: solarpaneloff 
	solarflag @ 
	1 = IF 
		." .STOPPING CHARGING PANEL. " 
		80 0 DO 
			." ." 250 MS 
		LOOP 0 solarflag ! CR 
	ELSE
		0 solarflag ! 
	THEN
; 


\ we need functions for the arm of the rover
: armdeploy 
	armflag @ 
	1 = if 
		." already deployed" CR 
        else ." .ARM DEPLOYING. " 
		80 0 DO 
			." ." 250 MS 
		LOOP 
		CR 1 armflag ! battery @ 4 - battery ! 1 armflag ! 
	THEN
;

: armoff 
	armflag @ 
	0 = if 
		." already retracted" CR 
	    else ." .ARM RETRACTION. " 
          	80 0 DO 
			." ." 250 MS 
		LOOP 
	    CR 0 armflag ! battery @ 4 - battery ! 
	THEN
;

: catchobject 
	armflag @ 1 = IF 
		xpos @ alienx @ = IF 
			ypos @ alieny @ = IF ." alien catched" 1 alienflag ! cR then
		then
		xpos @ xpiege @ = IF 
			ypos @ ypiege @ = IF ." IT'S A TRAP"  cR CR CR CR ." CONTACT LOST" CR CR BYE then
		then
		xpos @ rockx @ = IF 
			ypos @ rocky @ = IF ." rock catched"  then
		else
			." nothing here"
		then
	ELSE
		." arm not deployed" CR ." energy consumtion HIGH"  battery @ 10 - battery !
	THEN
;

: captureobject 
	armflag @ 
	1 = IF 
		." TRYING TO CATCH OBJECT" CR catchobject 
	ELSE
		." ARM FAILURE : CANNOT USE ARM" CR 
	THEN
	battery @ 5 - battery ! 
;

: reset 
	CR CR ." REBOOT : " 64 0 DO ." .." 75 MS  LOOP CR 
;

\ is move possible ?
: checkmove 
	solarflag @ 
	1 = IF ." cannot move please retract solarpanner " cr 
		rnd 100rnd got @ MS ." automatic retractation : battery consumtion is high" CR
		solarpaneloff battery @ 10 - battery ! 
	then
	armflag @ 
	1 = IF ." cannot move please retract arm " cr 
		rnd 100rnd got @ MS ." automatic retractation : battery consumtion is high" CR
		armoff battery @ 10 - battery ! 
	then
	battery @ 25 < if ." battery low" CR then
	battery @ 0 < if ." ROBOT LOST : too low battery" CR BYE then
;

: north 
	checkmove ypos @ 1 + ypos ! 4000 ." .moving. " wait 2 energiedec 
;
 
: south 
	checkmove ypos @ 1 - ypos ! 4000 ." .moving. " wait 2 energiedec 
;

: east 
	checkmove xpos @ 1 + xpos ! 4000 ." .moving. " wait 2 energiedec 
;

: west 
	checkmove xpos @ 1 - xpos ! 4000 ." .moving. " wait 2 energiedec 
;

: northeast 
	north east 4000 ." .moving. " wait 1 energiedec 
;

: southeast 
	east south 4000 ." .moving. " wait 1 energiedec 
;
 
: southwest 
	south west 4000 ." .moving. " wait 1 energiedec 
;

: northwest 
	west north 4000 ." .moving. " wait 1 energiedec 
;

\ fake ping to the rover
: ping CR ." PING ?" CR 
	5 0 DO 1000rnd got @ dup MS dup 
		0 = IF ." SIGNAL LOST" bye THEN
		. ."  ms"  CR 750 MS 
	LOOP 
;

\ help text for the player
: help
	." you can use north east west south and diags to move" CR
	." you can use ping transmit armdeploy armoff solarpaneldeploy solarpaneloff" CR
	." you can reboot as reset call help with help command" CR
	." you can use where batterystatus solarcapture wait and pause" CR CR
	." you may find alien to capture him & a rock before extracting" CR CR CR
	." when objectives are made, find extraction point & extract" CR CR
	." n, w, e ,s, nw, se, ad, bs & other shortcuts works" CR CR CR
	." you may add your own forth instructions to win the game" CR
;

\ intro text for the player wiht very minimal animation
: history
	page
	5 0 DO CR LOOP
	ping
	CR
	." ROVER IS DETECTING SOMETHING " CR rnd 1000rnd got @ 2* MS
	10 0 DO ." ." rnd 100rnd got @ MS LOOP CR CR CR CR
	." WARNING WARNING ALIEN DETECTED /!\ ALIEN SHOT /!\ " CR rnd 1000rnd got @ MS 
	." WARNING WARNING CAMERA FAILURE /!\ ALIEN SHOT /!\ " CR rnd 1000rnd got @ MS 
	." WARNING WARNING MIC FAILURE    /!\ ?????????? /!\ " CR rnd 1000rnd got @ MS 
	." WARNING WARNING REBOOT NEEDED  /!\ EMERGENCY  /!\ " CR rnd 1000rnd got @ MS 
	." WARNING WARNING BLIND MODE ==  /!\ BLIND OK   /!\ " CR CR CR  rnd 1000rnd got @ MS 
	reset
	." shell HELP can be used" CR
	help
;

\ we can transmit data 
: transmit 
	CR ." Transmission " CR  100rnd got @ 10 * MS ping ." Transmission .." CR 
	alienflag @ rockflag @ = IF 
		." OK" CR
		1 transmitflag ! 
	else
		." OK BUT NO DATA" CR
	then
;

\ main & final functions 
: genmap
	time&date  * + - * * (rnd) ! \ seed
	history
	." RADAR ON MAP: " 
	begin
		rnd 10rnd got @ 5 + mapxsize ! mapxsize @ mapysize !
		rnd 10rnd got @ 1+ 10rnd got @ 1+ alienx ! alieny !
		rnd 10rnd got @ 1+ 10rnd got @ 1+ rockx ! rocky !
		rnd 10rnd got @ 1+ 10rnd got @ 1+ ypos ! xpos !
		rnd 10rnd got @ 1+ 10rnd got @ 1+ ypiege ! xpiege !
	mapxsize @ 10 > until mapxsize @ .  ."  " mapysize @ . CR
	mapxsize @ 10rnd got @ - mapextractionpoint_X !
	mapysize @ 10rnd got @ - mapextractionpoint_Y !
;

: extract 
	transmitflag @ 1 = if
		xpos @ mapextractionpoint_X @ = if 
			ypos @ mapextractionpoint_Y @ = if 
				CR
				CR
				." YOU TRANSMIT DATAS JUST IN TIME BEFORE SIGNAL LOST ! " CR
				." VICTORY" CR
				bye
			then
		then
	else
		CR ." cannot extract datas : alien or data not found yet or tranmission failed"
	then
;

\ offering suicide to player is always a good idea
: autodestroy 
	CR CR CR ." ...." 10 MS ." ...." 100 MS ." ...." 100 MS ." ...." CR CR
	." B*O*O*M" CR CR CR bye 
;

\ few aliases
: n north ;
: s south ;
: e east ;
: w west ;
: ne northeast ;
: nw northwest ;
: se southeast ;
: sw southwest ;
: ad autodestroy ;
: co captureobject ;
: ca catchobject ;
: ad armdeploy ;
: ao armoff ;
: spo solarpaneloff ;
: sc solarcapture ;
: spd solarpaneldeploy ;
: bs batterystatus ;

\ start game
genmap
