INCLUDE places
INCLUDE variables
INCLUDE functions


Your special one's birthday is coming. You have to pick a present for them, but you haven't any clue yet. You should go out and find out. You'll eventually think of something. 
* [Get out] -> hub

=== hub ===
You're {|back} in the street. Where do you want to go? #loc[street]
 + [Take {park > 0:another|a} walk in the park.] -> park
 + [Go hang aroung {bookshop > 0:again} in the bookshop] -> bookshop
 + [Take {museum > 0: another|a} stroll in the museum] -> museum


- They lived happily ever after.
    -> END
