EXTERNAL giveItem(name)

CONST KEY_ITEM = "Stone"

=== startKnot ===

    Ein Haufen Müll.

    * {not knot_get_stone} [Durchsuchen] -> knot_get_stone
    + {knot_get_stone} [Durchsuchen] -> knot_failed_search
    + [Gehen] -> END

    = knot_get_stone
        Du findest einen großen Stein. { giveItem(KEY_ITEM) }
        + [Durchsuchen] -> knot_failed_search
        + [Gehen] -> END
        
    = knot_failed_search
        Du findest nichts Nützliches.
        + [Durchsuchen] -> knot_failed_search
        + [Gehen] -> END