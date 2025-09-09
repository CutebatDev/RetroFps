Okay so we can talk here and write whatever

Day 1:
    Anyway Ive added all of what you can see
    On top of that each map piece has a Marker in the middle so maybe we can use these to move the player - 
     - instead of via rigidbody which can be unpredictable and not move to exactly where we want to a more decided location
    ALSO we can make the movement not instant and lerp between the positions so its more understandable to the eye and players
    ALSO ALSO the camera snapping is a little to snappy for me, I cant understand when you turn and from where and when - 
    - but I dont know how to change it because it uses some RigidBody method that I dont know so I cant really use it but - 
    - if we can animate the rotation so it's smooth and visible it will be much better

Day 2:
    I've improved the snappy-ness of the movement ( Camera Rotation is still to be fixed ) 
    The main problem I've run into right now is the actual positioning, due to some ? reason movement doesn't really work,
    it becomes very offset-ed after a few steps because the center of the rooms / tiles
    A solution I have began working on is placing a marker as a child of each tile, this way we can set the position to that target
    instead of a random position, so the player will always go to each tile center
    also using this method we can determine whether there is a way to go or if there is a wall, if the function returns null there is nothing ahead
    I had a little problem so I didn't fully implement it but we can see to it soon


Other than that I've added a buncha stuff 
Thanks for listening to my TED talk  

Day (we counting irl days? i guess 3 than) - No not really just marked the time iv'e spent so you know what I did when
    
    In what resolution game supposed to be played? it kinda breaks with my janky pixel effect... works good now with 
    vertical "phone" screen size, but if needed can be changed by tweaking - FIXED
    "Assets/OldSchoolEffect.renderTexture" size value. (set it to half/quarter of desired resolution)
    Offset was caused by "map" scale being 0.7. actual cells fit perfectly with unity 1 unit of measure, so their scale 
    should be ...2.0/1.0/0.5/0.25...
    We can add colliders to walls, and if player hits a wall he goes back to starting position, also can add "ouch" effect here
    I added water and fog! Water is a gameObject inside of the scene, and fog is a unity thingy 
    (to turn off fog : Ctrl+9 -> Enviroment -> Fog (toggle off)) - The fog is really cool
    

 : 
    Do you know if there is a way to make the UV's on the wall texture "Global" so they are seamless between the different pieces
    Walk forward once and turn backwards to see the item I added

    
udp: Nope, never worked with 3d textures before. but it doesnt look bad?
nice item :thumbsup:

I changed skybox to pitchblack, it adds to the shadows, now if we do illumination inside they should look better (think torches)
Also cleaned up code a bit, coz infinite logs were bothering me (apperantly "new Quaternions == new Quaternions" is false xdd)