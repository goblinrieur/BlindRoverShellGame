# blind robot simulator

Idea of the game is to simulate your rover remote control 

even after the attack of an alien who broke cam & mic

you can give instruction authorized of course

but you are also allowed to use your own forth function 

during the game. 

## a party will look like that in terminal : 

```
  ok
where X : 2 Y : 3 
 ok
s .moving. X : 2 Y : 2 
 ok
se .moving. X : 3 Y : 2 
.moving. X : 3 Y : 1 
.moving. X : 3 Y : 1 
 ok
batterystatus  BATTERY STATE :83 %
 ok
w .moving. X : 2 Y : 1 
 ok
batterystatus  BATTERY STATE :78 %
 ok
```


# Can you participate to code & doc ?

With pleasure, yes.

#Special conditions to win:

Player have to both find the rock and the alien first 

then he goes to the extraction point 

# current state 

Player start with some instructions

- where
- wait
- pause
- batterystatus
- solarpaneldeploy
- solarcapture
- solarpaneloff
- armdeploy
- armoff
- catchobject
- captureobject
- reset
- checkmove
- north
- south
- east
- west
- northeast
- southeast
- southwest
- northwest
- ping
- help
- transmit
- extract
- autodestroy

[code](BRS.fs)
