# TerrainGenerationAndGamePhysics-
A demonstration of terrain generation, game physics, and collision detection/resolution. All the code relating to game physics has been written manually; no built-in features from Unity (Ridigbody, Collider, etc.) has been used.
Specifically, the ground is generated randomly with Perlin noise. The movement of balloons and cannon balls are modeled using Verlet Integration. The balloon is modeled as points with some constraints with each other to demonstrate constraint resolution.
To Play: Use TAB to switch between the two cannons, UP/DOWN keys to change the angle of elevation of the current cannon, and LEFT/RIGHT keys to change the speed of cannon ball. When a cannon ball collides with a balloon, it destroys the balloon.

Done as part of COMP521 at McGill University.

Play the game at https://tianyizhang.dev/en/projects/terraingenerationandgamephysics
