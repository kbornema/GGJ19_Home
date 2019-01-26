CONST string_gah_short = "*Gah*"
CONST string_gah_medium = "*Gaaaahhhh*"
CONST string_gah_long = "*Gaaaaaaaaaaaaaaaaahhhhh*"

VAR float_goblin_trust = 0

-> startKnot

=== startKnot ===

    { string_gah_short } ... 
   
    * [Hallo? Kannst du reden?] -> knot_hello_goblin
    * [Ein Goblin?! Verrecke du Mistgeburt!] -> knot_insult_goblin
    
    + [Gehen] -> END
        
        
    = knot_hello_goblin
        { string_gah_medium } ... Was? Jemand lebend? Du mir helfen? { string_gah_short }
        
        * [Helfen? Ich bin selbst eingesperrt.] -> knot_imprisoned_self
        * [Hast du einen Namen?] -> knot_ask_name
        * [Gehen] -> END
    
    
     = knot_imprisoned_self
        { string_gah_long } ... Verloren, alles verloren. { string_gah_short }
        + [Gehen] -> END
     
     = knot_ask_name
        
        //{ float_goblin_trust > 1: A | { float_goblin_trust > 0 : B | C } }.
        
        //TODO: 
        * [Gehen] ->END
     
     = knot_insult_goblin
     
        //~ float_goblin_trust = float_goblin_trust - 0.1
        //TODO: 
        * ->END