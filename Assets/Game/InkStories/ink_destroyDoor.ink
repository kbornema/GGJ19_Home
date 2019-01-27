INCLUDE ink_globalVars.ink
INCLUDE ink_externalFunctions.ink

=== startKnot ===

    Das Gitter ist beschädigt. Vielleicht kann man es zerstören?
   
    + { hasItem(STRING_ITEM_STONE) } [Stein benutzen] -> knot_open_with_stone
    + { not hasItem(STRING_ITEM_STONE) } [Gegen Stangen schlagen] -> knot_try_brute_force
    + [Gehen] -> END

    = knot_try_brute_force
    
        Mit bloßen Händen bekommst du das Tor nicht auf.
        
        + [Gehen] -> END
        
        
    = knot_open_with_stone
    
        Das Gitter lies sich mit dem Stein zerstören. Der Stein ist dabei zerbrochen.
        
        { removeItem(STRING_ITEM_STONE) } 
        { destroyDialogueOwner() }
        { enableNpc("CellWindow", "", false) }
        
        + [Gehen] -> END