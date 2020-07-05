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
