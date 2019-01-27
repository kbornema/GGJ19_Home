EXTERNAL hasItem(name)
EXTERNAL giveItem(name)
EXTERNAL removeItem(name)

EXTERNAL destroyDialogueOwner()

EXTERNAL enableNpc(who, where, val)

EXTERNAL triggerInteractable(int_id)

EXTERNAL endGame()

//
// Fallback functions:
//

//
// Items:
//

=== function hasItem(itemName) ===
    ~return true

=== function giveItem(itemName) ===
    ~return

=== function removeItem(itemName) ===
    ~return 

//
// Other:
//
    
=== function destroyDialogueOwner() ===
    ~return 
    
        
=== function enableNpc(who, where, val) ===
    ~return 
    
=== function triggerInteractable(int_id) ===
    ~return 
    
=== function endGame() ===
    ~return 