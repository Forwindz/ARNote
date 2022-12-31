# Project

Course Project for Serious game.

The repository only contains code,
original asset file and design documentations are not included.

# Build Project

You need:
 - Unity 2021.3.8f1

# How to interact with destop screen space

- Right mouse Drag + Card : Drag
- Drag instrument card & sheet card together to build card links, then you can :

- Left mouse Double Click: Flip the card
- Left mouse Click + Sheet Card (Front) : Play the music
- Left mouse Click + Sheet Card (Back) : Play the game
- Left mouse Click + Empty Sheet Card : Record your sheet (Click again to finish recording)

# Build the scene

Look into prefabs in `Assets/Prefabs/Card` folder, put them in the current scene, you can directly use it!

If you want to initialize the card with your own sheet, use `Assets/Prefabs/Card/SheetCard_Simple1` prefab. Select the sheet card game object, in the `Inspector` > `String Sheet` > `Sheet`, write simple notation sheet:
- numbers (1 2 3 4 5 6 7): Quarter Note (do re mi fa so la xi)
- 0: Empty Note
- number Suffix `_`: Half the note length
- number Suffix `-`: Double the not length

Each note should has a space or `|` to split them:
Example:

`1 2 3 4 | 5_ 6_ 7- | 2-- | 0- 5- |`

Chords are not supported, since this is a design prototype. 
