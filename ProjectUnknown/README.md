## Getting Started

To run the game, Drag and drop the [MasterScene](./Scenes/SetupScene) into the scene Hierarchy and press play.

As a rule of thumb, you always run the game from the  [MasterScene](./Scenes/SetupScene). 

---

## The Master Scene

The master scene governs the flow of the game. It has componenents which handles loading and unloading of scenes, the order of which these scenes are loaded. 

Currently, the master scene loading sequence for a level is as follow:

- Master Scene
- Main Menu (Optional)
- In Game Menu
- EntitiesScene
- "The level Name"

_Note: all of the scenes above need to be added into the build settings for the loading operation to work._

---
## The Entities Scene

This scene holds everything that is related to game object that is in the game world that travels between scenes. For example, the player would be in multiple level scenes.

---
## Level

The level is a physical representation of the game world.

The Level should holds a limited number of Game Object that is relied on the other scenes. The level should work in isolation which also means that the scripts in the level should work even without any other scenes are loaded.

_Note: this is something the developers are trying to work toward even if the current state of the project is not so_

---

## Making new Level

It is recommended that you duplicate from an exisiting level and change the tilemap in that scene.

### Door 

Currently, every level has doors which is where the player will be spawn when that level is loaded.
Door is a script that use a door profile which can be created via the Create Profile button. 
Said button can be seen after adding the Door Script to a game object.

### Game Master Settings.

This scriptable object can be found in Resources/GameSettings . Then you can drag the door profile in and set which door the player will spawn on start. If a door is in  level 1 then the player will spawn in level 1 at that door.