VAR var_start_knot = -> knot_waking_up
VAR var_goblin_trust = 0

-> var_start_knot

=== knot_waking_up === 
    Wo bin ich?
    -> knot_in_starting_cell
    
#Gameplay
=== knot_in_starting_cell ===   
    
    + [Talk: Goblin] -> knot_talk_to_goblin
    * [Pickup: Tool] -> knot_pick_up_door_tool
    + [Use: Cell Door] -> knot_interact_with_cell_door
    
    #Dialogue
    = knot_talk_to_goblin
        Text of Goblin
        -> knot_in_starting_cell
    
    #Gameplay
    = knot_pick_up_door_tool
        You found a small rock
         -> knot_in_starting_cell


    = knot_interact_with_cell_door
    
        The door is closed but has some cracks
    
        + { knot_pick_up_door_tool } [UseRockOnCellDoor] -> knot_open_cell_door
        * [BruteForce] The cell door is still too robust.  -> knot_in_starting_cell
        + [DoNothing] -> knot_in_starting_cell
    
    = knot_open_cell_door
        You smash the rock several time against the door. The rock breaks but you could open the door.
        -> knot_left_starting_cell


#Gameplay
=== knot_left_starting_cell
You left the cell!

    + [Talk: Goblin] TODO -> DONE
    * [Pickup: Food] TODO -> DONE
    * [Pickup: Cell Key] TODO -> DONE
    * [Use: Cell Door A] TODO -> DONE
    
    = know_talk_to_goblin
    -> DONE



    
-> END


    