# Stage Based Progression
a mod that changes the difficulty scaling formula in Risk of Rain 2 from time-based to stage-based.
**perfect for people who:**
* like to take the game slower
* like to explore the environments
* have a hard time finding the teleporter in reasonable time
* like to hunt for secrets

## why?
because the time pressure stresses me out (especially when playing with others), but reducing the difficulty scaling makes the game piss easy, so instead i rewrote the scaling code.

## compatability
the mod is written in a way that should be compatible with \*most\* difficulties (depending on if they edit `RecalculateDifficultyCoefficent`). The mod will change the ambient level accordingly with scaling speed.
however, since this mod **completely overwrites** the default function, expect some issues with mods that do modify that function.

## the math?
this is the __default__ scaling function
```
playerfactor = 1 + 0.3*(playercount-1)
timefactor = 0.0506*difficultyscaling*(playercount^0.2)
stagefactor=1.15^stagescompleted

difficulty=(playerfactor + minutesingame * timefactor) * stagefactor
```

and this is this mod's scaling function
```
playerfactor= 1 + 0.3*(playercount-1)
stagefactor= (1+.05*(stagescompleted-1)) * stagescompleted *1.25

difficulty= 1+(stagefactor*playerfactor*difficultyscaling)+1.25*difficultyscaling*playerfactor
```

## effect on balance?
i'm not really sure what effect it'll have. more time probably means more time to get items, and might even lead to grinding (since the clock stands still), but the numbers can be tweaked at a later date after balance is gagued better
it'll probably be easier if you take a lot of time per stage, but harder if you speedrun stages.
but this is just speculation.