CONST string_gah_short = "*Gah*"
CONST string_gah_medium = "*Gaaaahhhh*"
CONST string_gah_long = "*Gaaaaaaaaaaaaaaaaahhhhh*"

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
    