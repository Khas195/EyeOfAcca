# Journal

---

## Brainstorming sessions, 14th April - 19th April

We used the first week to brainstorm ideas for mechanics we could implement into toys, then on Sunday we would decide on the best one to develop into a second prototype. We had a lot of genuinely
interesting concepts, some of which could definitely be saved for later use and development.

Ultimately it was decided that we would go with Sam's prototype idea of the teleporting boomerang mechanic. This idea seemed to have elements that most appealed to all three of us; a strategic element and
spacial dynamism.

---

## Fleshing out the mechanic, 20th April

Today, while discussing the details, we realised that this was far from a trivial mechanic. There were many pitfalls that could potentially suffocate the mechanic and prevent if from being utilized most
effectively. Should the boomerang be allowed to bounce around indefinitely? Should the player be allowed to teleport indefinitely? How would restrictions be communicated to the player? We spent over 3
hours pondering questions such as these in today's meeting. The solutions we came to seemed to us the best ways to handle these conditions, however only trying them out first-hand will yield definitive
results.

---

## Sprints 1 & 2

We've made a lot of progress in the past two weeks, and I believe we'll have something really impressive by the end of the second sprint on Tuesday. The game has many interesting synergistic mechanics, 
but be once discussed an interaction which included solely the character and the axe such that the axe itself would feel justified, because if a level contained only the room, the player would have
nothing to do, other that throw the axe and recall it. One of the interactions we came up with was that the player could
hang on to the axe once it was stuck to a wall, and use that to travere levels. I liked the idea, however it would take much too long to implement so we decided to drop it.

---

## Sprint 3

This sprint we dedicated to coming up with various level concepts. We were to have three areas, all with their distinct aesthetics and color schemes, through which the player would progress
in the game. One level concept was a save room, after all, we needed a save feature as the game would be too long to expect everyone to be able
or willing to finish it in one sitting. So for the save room we thought it would be nice to have an atmosphere of great tranquility, with many relaxing ambient sounds, such as the bubbling of a fountain
spilling water into a basin. A streak of light shining through a hole in the ceiling, giving a warm mood to the scene. The idea for saving was that we would have a stone struture at which you would throw
the axe, and upon hitting it, the game would save. Our rationale was that since we use the axe as a primary way of interacting with the environment, it woud only make sense that saving would be achieved,
through the same action.
Here I came up an idea I thought was interesting, and that was having picese and cracks in the background wall which would reveal inner, darker layers of the wall. It was also here where we considerend
using 2d lights for our project, but felt strongly hesitant as that would significantly increase the time needed to decorate a level.
For one of the concepts we had and an area which would be wide and open, with lots of lights coming through big windows in the background.

---

## Sprint 4

I did a new iteration on the tilemap. I wanted to add an effect of layering of the stonework of the level, so the tilemap had to be drawn with three layers, the surface layer with which the player would
collide, and two aesthetic layers. Needles to say, the rest were not happy with spike in level drawing complexity. I also added foliage and rubble to the tilemap, which would add much needed
variety to the look. The foliage had to be animated, of course, since static foliage woul just look off-putting. And in addition to a static, 'idle' animation, we needed an animation for when the
player passes through the foliage (grass and vines), one option for this was to do a direction-neutral 'rustle' animation. But we are RangGang, and RangGang don't cut corners. So I made animations
for both left and rightward motion. The animation process was much more complex than I had anticipated, but after some frustration-induced migranes I think I came to a satisfactory result.

---

## Sprint 5

Now it was time to finalize the desing the our character. My first redesign involved orange and light beige. After an ambarrassing amount of iterations I realised it looked like the x-wing pilot outfit
form Star Wars, and with that I couldn't escape the sci-fi/space association. So I changed it to a brightly couloured top and dark couloured shorts (ironically, the same color scheme as the character
from my previous project). Tung suggested I make the arm the character holds the axe with a robot arm, because the otherwise the character posses the inexplicable strength needed to wield such a heavy
object. I objected initially, but honestly why not inject some weirdness into the whole thing (more weirdness)?
We also really went into the narrative and how it would be conveyed to the player. We wanted to tell the story through great murals which would be placed in the background in rooms inteded as a break
in the pacing between two sections of platforming. We narrowed the number of murals we needed to 6. After making sketch of the murals we sent them out to several people, friends and acquaintances, 
to see how they would interpret the murals. We sent them to about 5 different people and received as many different interpretations, some directly contradicting eachother. I found this to mean rethink
the design of the murals from scratch, but Sam stated that this was actually a boon, as having ambiguous narrative elements which yield diverse interpretations would be much more interesting. I agreed,
but chiefly on the grounds that it entailed less work for me.

---

## The Milestone

The milestone presentations were done differently due to the online nature of this semster, usually what would be all of us gathering in one physical space and holding public speaking sessions on
our progress, we devised Miro board sequences over which we spoke. It was a bit awkward but nothing too bad.

---

## The Special Assignment

The special assignment was a week where each team took on the project of another team to try to add or modify something. Our team took on 'Life of Cholas' from Hari and Junjie. While their game was
focused on a fixed, top-down perspective camera, our version had a freeform, third-person camera following the characters. The character redesigns themselves were my task, which I did following a sort
of tribal mask aesthetic. One of the characters (named Curly) was small, but could move and jump unhindered, another (named Moe) was large and wide, moving slowly and unable to jump. The third (Larry)
was tall and lanky, paralyzingly unable to either jump or move, and whom the other Cholas (presumably a name for the species, rather than proper names) had to push along through the level to progress.

As for our game, it was taken by the Gravball team, who did three interesting modifications. One was the inclusion of wall-jumping, which we found amusing but thought it introduced too many clashes
with the currect movement mehcanics. Another was a gem wich flips gravity when struck. This I think could be used to create some cool scenarios in the game. The final additions was one I already mentioned,
namely 2D lights, which was done to great effect by Jawad. If we continue wokring on the game post-semester, 2D lights is something I hope we invest in seriously.

---

## Sprint 6

In sprint 6 we added a bunch of background elements, divided into two categories: static, and animated. For the static elements I made a stone bech and stone table, to illustrate a kind of dining
or living quarters. I made two variants of skeletal remeains (one lying on the floor, one leaning on the wall) to add to the notion of danger or great misfortune. I added three variants of statues
in differing poses and shapes, for religious or profane purposes. I added weapon racks, trying to illustrate the obsidian based weaponry used by the Aztecs. I also made a stand on which a kind of
ceremonial garb was put, with feathered head-dress. Finally I made a tapestry with aztec motifs.

For the animated elements I made a stream of water falling from a crack in the wall. The animation was supplemented with particle efefcts for splashing. Another animated element was a fire-pit
which would provide light to the stone halls. This was heavly supplemented with particle effects for smoke and sparks. My favourite animated element were the gears in the wall which woud spin when
the railblock was moved.

## Sprint 7

In sprint seven I worked on the collectable. I really wanted it to be a 3D object in a 2D world because I thought the visual juxtaposition would be very amusing. So I made a very rudimentary blocky
model in Maya and applied a pixelated texture to it (very Minecraft). I'm really happy with how they turned out. For the opening of the game the idea was thus (according to Tung): 
the game opens on the character in a petrified state, after which they reanimate and begin the game. To this I thought it would be interesting to add other petrified figures as well,
thinking this could add much of the narrative ambiguity lost now that we scrapped the murals for time. I also did a lot of work on the ending sequence, which involved the caracter throwing
the ae into a stone mouth, after which the axe would shake, and finally break, unleashing an immense stream on energy rushing towards the character, who would then perform a triumphant powerstance
and the end credits would roll.