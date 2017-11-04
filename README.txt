
Game Overview:

From the main menu, you can select to start a New Game, or Load an existing game if you have one. Use the drop down menu to select a game to load – if you have none, the load menu and load game button will be grayed out.

This game keeps track of player and enemy stats and their positions. It does not keep track of what is currently recorded in the console, or any projectiles currently on screen. It saves by serializing assets and saving in Unity’s persistent data path. I consider this one step above using PlayerPrefs, which saves in the registry and I’m amazed that Unity still uses that as a saving solution. There is currently no limit to the amount of save games that you can have.

This game is a FPS game that is controlled with the mouse and keyboard. WASD to move, Space to jump, left click to fire. The 1 and 2 keys switch between your active weapons. One weapon is a hitscan gun, which registers a hit as long as the cursor is over the enemy – the bullet effectively has infinite speed. The other is a projectile gun that shoots bullets that travels through the scene. 

The `/~ key opens the log. Whenever an enemy reaches the other side of the arena, you lose some health. Enemies have health, armor, and evasion, so some of your shots may not appear to do damage because they have “missed” the enemy.  

The Escape key brings up a Pause Menu, which lets you save your current game, resume your current game, and exit to the main menu.

You lose when your health reaches 0.

C key toggles an overhead view camera that loops around the arena.
V key toggles a third person view camera.

The in-game UI has 3 elements: a player health bar, the current amount of time that you've survived in the level, and the number of enemies you've killed.


The game scripts are broken up into Model, View, and Controller scripts, with an additional folder for general Helper scripts – things that are useful no matter what the game is. These things include stuff like a Singleton script, a Service Wrangler script, and a serializable Vector3 extension.
