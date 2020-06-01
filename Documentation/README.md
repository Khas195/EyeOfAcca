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
* [Ousama_Andari](./PersonalDocumentation/OusamaAnderi).

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

#### _**Version 0.0.6, 0.0.7, 0.0.8, 0.0.9 (April 27th, 2020 - May 2nd, 2020)**_

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



##### Version 0.0.8

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



##### Version 0.0.9

Bugs:

    Fixed animation bugs where there are transition time between 2d Animations.

    Fixed bugs where the axe hit the surface behind the character at throw.

Sound:

        Added footstep sound walk.
        Added footstep sound run.
        Added axe spinning sound.
        Added axe hit sound.
        Added player Jump sounds 
        Added Axe Throw.

Camera:

        Increase Base camera zoom: 7 -> 10.
        Increase Camera Max Zoom: 10 -> 15.

Character:

        Adjusted the player character's speed and jump force.

        Holding Axe Stats:
            Walk Speed: 2 -> 4
            Jump Force: 3 
        Without the Axe Stats:
            Walk Speed: 5 -> 8
            Jump Force: 10

Level:

        Added new play test level.
        Added new Menu scene.

#### _**Version 0.1.0, 0.1.1, 0.1.2, 0.1.3 (May 3rd, 2020 - May 11th, 2020)**_

##### Version 0.1.0

Change Notes:

Bugs fixed:

    Fixed issue where the axe miss the character on recall.

Character:

    Same speed regardless of holding the axe or not.

Axe:

    Changed recall behavior.
    Reduce recall time from : 1s - > 0.4s.
    Min Recall Duration: 12s.
    Max Recall Velocity Scale Distance: 12.
    Min Recall velocity Fall Off distance: 4.

Notes:

    These duration are calculated at the time of recall.
    Recall duration will scale based on percentage between 4 - 12 distance.
    Fly Velocity: 20 -> 35.

Camera:

    Camera will no long center between the axe and player if he get too far. (Represent by the cyan color circle in editor).


##### Version 0.1.1

Change Notes:

Bugs fixed:

    Fixed axe sliding instead of sticking.

Axe:

    Added new gem effect when going through a gem.

Block:

    The block will move toward the side of the character regardless where the axe was stuck.

Pot:

    Add breakables pot.

Dead Zone (Spikes):

    Added dead zone.
    Restart level on character touching deadzone.
    Axe's ability gained before touching dead zone will be nullified.
    Axe recalls immediately on touching dead zone.

Timed Door:

    Added door that open depend on block movement.
    Door can close automatically or by moving block.
    Added Door close automatically time: 5s.

Block and Timed Door: 

    Now using MoveAB script for movement.

 
##### Version 0.1.2

Change Notes:

Bugs fixed:

    Axe has blue power as a natural state.
    Fixed crazy shaking.
    Fixed Crazy spinning sounds.

Axe:

    added Animation effect when axe stuck.
    added teleport gathering power.
    added recall gathering power.

One-way Platform:

    Added Oneway platform Grid.

Timed Door:

    Implemented the timed door lever.
    Setting up the lever and the door is a time sensitive manner. -> They do not move in conjuncture in their own but setting up the movement time can create this effect,

Dead Arrow:
    
    Added dead arrow and dead arrow spawner.

##### Version 0.1.3


Change Notes:

Bugs fixed:

    Axe throw at an angled when in the air.


Arrow Trap:

    Added arrow traps + juice.

One-way Platform:

    Added Oneway platform Grid.

Gem and Gem On axe:

    Remove gathering Power
    Added juice.

Pots:

    Added normal state sprite and broken state sprite.

Player:

    Can now press down + jump to drop down form one way platform.


#### _**Version 0.1.4 (May 11th, 2020 - May 19th, 2020)**_

##### Version 0.1.4

Change Notes:

This version is more a less about improving quality of life in the Unity Editor to make it easier and more intuitive in the process of creating levels.

Unity Editor:
    
    Created a data object for game setting so people don't have to open the gameobject in Master scene to change in.
    Created a drop down menu to quickly create prefab of the environment without having to drag it from the project view.
    Created a quick and dirty check point system so that you don't have to re run your whole level if you die.

Level:
    
    Added a tutorial levels with puzzles inspired by the instances that everyone came up with.

#### _**Version 0.1.5, 0.1.6, 0.1.7, 0.1.8, 0.1.9(May 20th, 2020 - May 26th, 2020)**_

##### Version 0.1.5

Change Notes:


Unity Editor:

    Added level Door Creation in Game System.

Character:

    The player will accelerate up, on jump, until he reached the jump height for deceleration. Then he will start decelerating his velocity up until he reached max Jump Height.
    Decrease Max Velocity when jumpuing: 25 -> 20
    Decrease Max Jump Height: 4 -> 3
    Decrease Jump height point for decelleration: 3 -> 2

Camera Added Dead Zone to Camera

Level:

    Added new test levels to test Connection.
    The levels are linked together via lever door.
    Upon entering a door, the level will remember the door and will respawn the character there on death.

##### Version 0.1.6

Level

    Add Loading screen between level.
    Made the level more compact by removing wasted space.

Particle Effect:

    Removed Particles from Door Lever and Rail block.

Player Camera:

    Move the camera above the character.

Fixed:

    Sound glitch.
    Character sometimes lost his axe.

#### Version 0.1.7

Arts: 

    Added vines and grasses
        Vines and grass has animation.
    Vines and Grasses now interact with the player and the axe.
    Added new tilesets.
    Added new door sprite.


#### Version 0.1.8

Bug fixes:

    Fixed an error where sounds are not play probably.

Sounds:

    Removed:
        Axe shake sound

    Added:
        player Jump.
        player land.
        player run.
        player walk.
        teleport.
        Axe fllying.
        axe hit.

#### Version 0.1.9

Editor:

    Added preview button to sfxs.
    Added new menu button so developers don't have to drag in the master scene to run the game.

Arts:

    Added new tile sets.
    Added level map.

Level:

    Decorate levels with new tile set.
    Removing looping in the last level: player cannot go back from the last level.

Juicing:

    Added time slow and screen shake for when the axe hit and when the player pull the axe.
        Time Slow: 0.02s.
        Time scale: 0.02f

    Added a jump buffer after the player start falling -> It makes the control feels better.
        Time: 0.3f

Builds:

    Added a build version for mac but haven't tested it yet.

#### _**Version 0.2.0, Version 0.2.1 (May 27th, 2020 - June 3rd, 2020)**_

#### Version 0.2.0:

Juice:

    Added ripple effects for axe's impact.
    Added screen transitions for dead and level loading.

UI:

    Added OffScreen Indicator.
    Added Aim Indicator.

#### Version 0.2.1:

Bug Fixes:

    Fixed Douple jump.
    Fixed Pots sound not playing probably.
    Fixed an issue where the character keep missing the axe.
    Fixed an issue where the character keep flipping constantly when walking into walls.

Art:

    Introduce new character sprites and animation.

Juice:

    Reduce axe's impact ripple effect.
        Amount : 20 -> 10.
        Friction: 0.9 -> 0.7.
    Added ripple effect for when axe passes through Gem.
        Amount: 15.
        Friction: 0.9.
    Added slight screen freeze when axe passes through Gem.
        Time Scale: 0.2.
        Time: 0.009s.
    Added Spatial sounds in 2D world for RailBlocks, Pots, Axe's impact, axe spinning. 
        Linear dropoff - Dropoff distance: 10.

Editor:

    Streamline the door creation process.
        No longer need to remember the index of the door.
        Level no longer need to know about the doors.
    Move Game Master Settings into ScriptableObject Folder.

Level:

    Replaced old tilesets with new tile sets.
