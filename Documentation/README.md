# Process Documentation

_This is the process documentation for an Unknow Game. The process documentation here available here is the result of the works from the course Build a Toy First by Prof. Csongor Baranyai. The members of this project: [Tung T. Cao](https://khas195.itch.io/), Christopher Bukal Chilicuisa, Samartha Ingle._

---
### Note Board
Go to [Note Board](https://miro.com/app/board/o9J_ktqXOCM=/) for the notes of the developers when discussing about the project.


### To Do
The todo list of practical tasks for the project.
Task Management Tool - TBD.

### Personal Documentation
To read the developers personal documentation such as: Journal, Motivation, Research and Inspiration and Final Statement.

 * [Tung Cao](./PersonalDocumentation/TungCao).
 * [Samartha Ingle](./PersonalDocumentation/SamarthaIngle).
 * [ChristopherBukalChilicuisa](./PersonalDocumentation/ChristioherBukalChilicuisa).

### Change-Logs

#### _**Version 0.0.1 (April 16th, 2020 - April 19th, 2020) - Initalize project**_
Documentations and Project Planning:

    - Add Readmes for each developer.
    - Add Readmes for Documentation.
    - Setup links for Miro board.
    - Add notes of the first 4 meeting sessions to [Miro Board](https://miro.com/app/board/o9J_ktqXOCM=/).

Game's Change-logs:

    - Add In Game Console.
    - Add Main Menu.
    - Add choose Level menu.
    - Add Localization options.
    - Add Localization for English and Vietnamese.
    - Add save/load to json file.
    - Character's datas in scriptable object can now be saved and loaded into json.
    - Add editable json files for in game datas( Character movement, character data, localization).


#### _**Version 0.0.2 - 0.0.5 (April 20th, 2020 - April 26th, 2020) - Prototype Making**_

##### Version 0.0.2:


Boomeraxe:

    - Bounce Limit set to 2. ->I found that 2 bounces are better than once bounce because with 1 bounce it doesn't intuitively imply that the axe can bounce.
    - If the axe bounces 2 times then on the next contact with any surface it will teleport back.
    - If the axe exits the camera view then after 0.5s it will teleports back.
    - If axe hit any surface it will not return and continue going.
Character:

    - After teleport to the Axe, he will not be affected by gravity for 0.2s. ->I found 0.5s is too long for freezing in the air after some tries.
    - Replace normal 2d movement with 2D Platform movement.
Control:

    - Left Click to teleports axe back to player if the axe is not being held.
    - Left click to throw the axe if the axe is being held.
    - Right click to teleport to the axe.

Others:

    - Add level 3 for bounce testing.
    - New Level can be add by Duplicate the existing level -> alter it as u want, then add it to the build settings.

##### Version 0.0.3:


Boomeraxe:

    - The fly distance of boomeraxe will reset on bounce with a new anchor which is the contact point of the surface it touched.
    - On exceeding the fly distance after any bounce it will teleport back to the holder.
    - Remove throw limit on the axe.
    - Add Teleport limit to the axe - current value: 2 teleports.


Control:

    - REMOVE: Left click to throw the axe if the axe is being held.

Others:

    - Test building the project -> revamp some code for saving and loading the project so it is buildable.
    - Boomeraxe's data now available at:Prototype2-ProjectUnknown_Data/StreamingAssets/SavedData  in build version.
    - All saved datas now are loaded before loading any level.

##### Version 0.0.4:

Boomeraxe:

    Add lul period at the moment of throw.
    No longer using courotine for timing time scale.

Character: 

    The lul period no long change the gravity scale but the time scale of the character.
    Which mean he will move slower all together rather than just falls slower.

Balance:

    Lul time after Teleport: 0.5s.
    Lul time after throw: 0.4s.
    Time scale after teleport: 0.5.
    Time Scale after throw: 0.5.

##### Version 0.0.5:

Boomeraxe: 

    Added temporary boomeraxe's animation.
    Added temporary boomeraxe's on hit Particle effect.

Character:

    Added temporary character's walking animation.
    Added temporary character's on teleport Particle effect.

#### _**Version 0.0.6, 0.0.7, 0.0.8 (April 27th, 2020 - May 2nd, 2020)**_

##### Version 0.0.6

Bugs:

    The vfxs are properbly removed after they had finished playing.

Boomeraxe: 

    Remove Bounce.
    Track the player down if it reached it max range.(DEPRECATED)
    Track the player down if it hits anything.(DEPRECATED)
    The boomeraxe will redirect to the Character if it hit anything or at max range (once).
    The boomeraxe will avoid flying through wall by redirecting if touch any surface.
    The boomeraxe WILL fly through wall if the player run behind it.

Character:

    Remove Teleport.
    Move at different stats when holding the axe and otherwise.
    Holding Axe Stats:
        Walk Speed: 2
        Jump Force: 3
    Without the Axe Stats:
        Walk Speed: 5
        Jump Force: 10

Quality Of Life:

    Instead of bombarding everyone with useless log onto the screen. I had decided to add different layer of log. 
    
    The Current layer can be set in the Game Master Scene -> LogHelper.

    Possible Layers:
        - Console
        - Developer
        - PlayerFriendly


##### Version 0.0.7

Bugs:

    Fixed a problem where the boomeraxe's z position keep jumping infront of the character.
    The player will no longer teleport inside the environtment.

Boomeraxe **(REVAMPED)**:

    The Axe now sticks to any surface it hit.
    The axe can be recalled by Left Clicking.
    The axe will pass through everything on its way back.
    There is a lul time when the character is airborned when recalling the axe.

Gem **(NEW)**:

    Introducing gem that will alter the Axe's ability.
    Teleportation gem: ater the axe passes through this gem, instead of recalling on left click. The axe now will teleport the player to it.
    There is a lul time on teleportation.

**JUICE**:

    The axe shakes a bit before returning.
    The axe's movement is now more dynamic.



##### Version 0.0.7

Bugs

    Fixed a bug where the axe is behind the environtment when it is recalling.
    Fixed a bug where if spamming left click when the axe is stuck then it will not return.

Boomeraxe 

    Implement Flying animation.
    Implement charging animation.
    The axe now rotates so that it plugs its blade into the surface that is is stuck to.

Character

    Implement character free animaiton.
    Implement character with axe animation.

Gem

    Implement Gem Animation.
    Add Gem Sprites.

Block:

    When block is pull it will move along that pulling direction.
    The pull direction must be on the same side for pulling to work.
    Block can only move horizontal or vertical.
    Block can only move within restriction.


Level:

    Add a template level with new tiles.