# conways-game-of-life
A C# implementation of Conway's Game Of Life

## TODO:
 * Create video recorder for playback
 * Optimize the Update method
 * Create options menu

## Projects(by folder name):

### gameOfLife
This is the main class library containing the implementation of Conway's Game of Life. To use, you simply declare a 
GameOfLife object, with the specified parameters. To run, simply call the Update method. Update will return a boolean 
indicating whether or not moves were made.
Suggested syntax:
```
bool running = true;
while(running)
    running = gameOfLifeObject.Update();
```

### conways-game-of-life
This is a console project for testing the class library.

### GameOfLife-WinProj
This is a Monogame Windows project for use with the dll.

### IniLoader
Depricated. This was an ill-fated attempt at creating a .ini parser. I eventually realized that it would be quicker 
to create and serialize a custom Settings object to an xml file.