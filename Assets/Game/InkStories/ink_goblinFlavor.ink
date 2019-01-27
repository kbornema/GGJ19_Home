CONST string_gah_short = "*Gah*"
CONST string_gah_medium = "*Gaaaahhhh*"
CONST string_gah_long = "*Gaaaaaaaaaaaaaaaaahhhhh*"

LIST PlayerInsults = Abschaum, Ausgeburt, Teufelsbrut, Lump, CountA

LIST PlayerNames = Wesen, Etwas, CountB

LIST PlayerFriendNames = Freund, Kumpel, CountC

=== function randGoblinGah() ===
    ~temp x = RANDOM(0, 2)
    
    {
        - x == 0:
        	~ return string_gah_short
        - x == 1:
        	~ return string_gah_medium
        - x == 2:
        	~ return string_gah_long
    }
    
    
=== function goblinPlayerLabel(float_goblinTrust) ===

    {
        - float_goblinTrust < 0:
        	~ return PlayerInsults(RANDOM(1, LIST_VALUE(CountA) - 1))
        	
        - float_goblinTrust >= 10:
        	~ return PlayerFriendNames(RANDOM(1, LIST_VALUE(CountC) - 1))
        	
        - float_goblinTrust >= 0:
        	~ return PlayerNames(RANDOM(1, LIST_VALUE(CountB) - 1))
    }