EXTERNAL hasItem(name)
EXTERNAL giveItem(name)
EXTERNAL removeItem(name)

EXTERNAL destroyDialogueOwner()

CONST KEY_ITEM = "Stone"

=== startKnot ===

    Das Gitter ist beschädigt. Vielleicht kann man es zerstören?
   
    +  { hasItem(KEY_ITEM) } [Stein benutzen] -> knot_open_with_stone
    + [Gewalt anwenden] -> knot_try_brute_force
    + [Gehen] -> END

    = knot_try_brute_force
        Mit bloßen Händen bekommst du das Tor nicht auf.
        + [Gewalt anwenden] -> startKnot
        + [Gehen] -> END
        
        
    = knot_open_with_stone
        Du zerstörst das Gitter mit dem Stein. Leider ist er dabei ebenfalls zerbrochen. 
        { removeItem(KEY_ITEM) } 
        { destroyDialogueOwner() }
        + [Gehen] -> END