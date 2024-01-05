# Problem

The current code solves the following problem 

Given a binary string representing the scores of a Volleyball match. The task is to find the winner of the match according to below conditions: 

In volleyball, the two teams play with each other and the team which scores 15 points first will be the winner except the case when both teams have reached to 14 points.

In the case when both teams have reached 14 points then the team maintaining a lead of two points will be the winner.

In the given binary string, 0 means TEAM 1lose a point and 1 means TEAM 1 win a point. You have to find whether TEAM 1 had won or lost the match.


# Test 
Complete the following tasks using SOLID Principles and Design Patterns and Unit tests
Please send you code back for review


## Task 1 

Refactor the code and supply unit tests that show the code handles the problem correctly

## Task 2
Expand the code to take two team names and specify which team won

provide tests

## Task 3
Alter the code to say which team won and what the score was for example
Ravens beat badgers 15 -7

provide tests

## Task 4
Expand the code to handle a match that has three sets

Output the winner in the following format for example 
Ravens beat badgers (2-1)
Score 15-7, 3-15 , 15 -5

## Task 5
Extend the code design to work with tennis and squash follwing the rules below

Provide unit tests showing that each sports condition has been met

### Tennis rules
Best of 3 games
Each game is point a rally scoring to 4. If the score in a game is tied at 4-4, a player must win by 2 clear points.

### Squash rules
Best of 3 games
Each game is point a rally scoring to 11. If the score in a game is tied at 10-10, a player must win by 2 clear points.


