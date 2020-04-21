# Project Change Logs.
---

_**Version 0.0.1 (April 16th, 2020 - April 19th, 2020) - Initalize project**_
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


_**Version 0.0.2 (April 20th, 2020 - April 26th, 2020) - Prototype Making**_

* Game's Change-logs:

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