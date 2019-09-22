# SwipeToScore

I made this project as an interview assignment, where the goal was to make an application that would allow the player to swipe a ball into a goal. I took some (allowed) liberties with the idea and expanded on it with:
- Level regeneration
- Obstacles
- Special skill support
- Adaptive tutorial text

The Settings, Presets (which I dumped into the Settings folder) and TextMesh Pro folders are automatically generated. The rest is my own work, aside from the Wood texture on the obstacles and the refresh icon.

### Swipe
I implemented the swipe mechanic for both touch screens and the mouse so I could test everything in the editor as well and would not have to build to my phone constantly.
Establishing the direction of the ball can be quite confusing in some cases, since a diagonal swipe starting from the ball is clear, but what should happen if the swipe happens across the goal? Is the end point the destination or is the direction of the swipe leading?
To solve these questions, I opted to go for enforcing the swipes to start at the ball and using the endpoint of the swipe as the destination. To make this clearer in the game, I added a world-space canvas underneath the ball that lights up when you start swiping from there.

### Level generation
One of the first things I did was setting up the ball and the goal as objects that could get respawned. In doing so, I immediately looked into how I could set up the goal to be scalable to get variation in size. Breaking down the goal prefab shows a wonderful bunch of meshes intersecting to make up the final goal. I chose this approach since simply scaling the goal in its entirety would change the thickness of the posts and bar as well, which I wanted to avoid. Now, I solely scale the bar and posts in length and adjust the positions of the other elements to maintain a single look.
Both the goal and the ball shift in location sideways, but maintain their distances.
A later addition was the obstacles, which adds some interesting twists into the system, but could prevent scoring entirely if unlucky, since their generation is entirely randomised. I added the fireball mechanic to overcome these obstacles by destroying them. If the game is more fun without the obstacles, then they can be turned off with a single checkbox in the _LevelGenerator_.

### State Machine
I initially started building with a "_GameHandler_", but very soon wanted to get rid of this class doing everything in the game. Instead, I made a small state machine, for which I separated the game into three separate states, each with their own specific UI:
- The _SwipeState_, in which the player gets to see the level, prepare a shot and swipe to shoot
- The _ShootState_, in which the ball is swiped and is moving
- And the _ScoreState_, in which the ball reached the goal

I'm quite happy about how these turned out. The system itself is quite extensible and each of the classes for the states are clearly structured, all remain quite small and readable, and are equally extensible.
I added a _GameData_ class that gets created by the state machine and gets shared by all states to maintain some shared information, such as a reference to the currently existing ball.
Throughout the states and the UI, I stuck to a structure of Initialisation, Activation and Deactivation (called _EnterState_ and _ExitState_ for the states), but to make sure that when deactivating, everything is returned to the state it was in before activation. For example, UI elements that were turned off before, will be turned off after.

### UI
Each state is responsible for its own UI and each has its own corresponding UI class (e.g. _SwipeState_ and _SwipeUI_). They pass the necessary data on to the Init and Activate methods to fill out the UI, but all logic should happen in the State classes.
A seeming oddity that arises because of this lies in the button classes. It is possible to use Unity editor buttons to directly call methods on whatever object in the project hierarchy. One thing I learned about this is that that gets messy real fast. In my current structure, all button classes are in the basis just that, buttons, that can be activated and clicked, but have no real logic (aside from UI layout and make-up), references or knowledge of other systems. All they do is fire an event that generally gets passed through up the hierarchy to the base UI class so the state can attach its functions to those events. This allows for much easier tracking of what gets called where and when than when half the function calls are hidden in the editor (the worst of which are animation triggers...).
For a project of this size this may seem like (or definitely is) overkill, but I wanted to set everything up with extensibility in mind. The same goes for the special skills and ability buttons. The system works in the sense that you are able to turn on multiple buttons and only have one active at a time, toggling off other selected buttons when another is selected, even though in regular gameplay, this situation would currently not occur, since only one skill is implemented and gets activated.
