INCLUDE places
INCLUDE variables
INCLUDE functions
INCLUDE present

Your special one's birthday is coming. You have to pick a present for them, but you haven't any clue yet. You should go out and find out. You'll eventually think of something. #loc[street]
* [Go out for a walk] -> hub

=== hub ===
{
- saw_theme == yes && saw_memor == yes && saw_feeli == yes:You're back again in the street. You can keep on roaming freely but you've seen enough to find an idea for a nice gift. Isn't time to make up your mind?#loc[street] #mum[4]
- else:You're {|back{| again}} in the street. Where do you want to go? #loc[street]
}
 + [Take {park > 0:another|a} walk in the park.] -> park
 + [Go hang aroung {bookshop > 0:again} in the bookshop] -> bookshop
 + [Take {museum > 0: another|a} stroll in the museum] -> museum
 * {saw_theme == yes && saw_memor == yes && saw_feeli == yes} [Make up your mind]
    All that lounging around was helpful to get some inspiration. You finally thought of something.
    ** [Continue]
    -> present