# **Tanks!**

# **Introduction**

The project that was chosen was topic 1. Reusing Tank! Tutorial with Jeremy’s AI program modifications. Reason I choose Tanks is to improve the NPBehave which I am knowledgeable of in comparison of lab 1. While the AI had certain behaviours, comparing to lab 1, the behaviour for these are better than the lab 1.

Tanks is a 1 v 1 shooter game with an AI implementation which the behaviours change depending on which the user wishes to select and whomever wins 5 rounds is the winner.

# **Implementation**

Using a behaviour tree (NPBehave) I wrote 3 different behaviours which each uses the same service UpdatePerception; however, each behaviour is different and yet improved than the last one. Which will give a better understanding on how AIs work depending on its behaviour.

## **Flow chart & How it works**

How it works is that it calls the service and then using the selector, which randomly picks one of its children, it then goes through one depending on its condition and stops.

The first behaviour, as demonstrated below, is a basic behaviour (taken from Jeremy’s modified code). What the first blackboard condition does is, it checks the target off centre if less than 0.1 and depending on its sequence it will follow each sequence and if successful then it would immediately restart the decorate. They second will check for distance and would slowly move towards the player, if it fails, it checks the last condition which checks which direction the player is moving, if moving right then it moves right otherwise it will check the last decorate and move left.

![alt behaviour1](https://drive.google.com/file/d/1c0O7grHukhOkLVZGfgg2rw6QgBp23nUK/view?usp=sharing)

The second behaviour tree, using the same service acts different than the previous behaviour, it checks if the front is false then it would stop moving and wait, half a second, then turns around, what this condition does is when the player moves behind the enemy, the AI would detect it and turn around and when it detects the player it would stop, as seen in the target off centre, however it uses a LOWER_PRIPORITY_IMMEDIATE_RESTART which means it would not restart as immediately as the first condition and the third has a LOWER_PRIPORITY which indicates that the selector would not choose it as much. The forth condition follows the player when it is close to the enemy and starts randomly firing and lastly the targetOnRight, does not change much and it looks at which x axis the player is heading to.

![image alt text](image_2.png)

 

The third behaviour uses something different, a random NPBehave which adds a probability and the condition so there is a 1% chance that the selector will go for this condition which is when the targetInFront is true, the enemy will slowly move back while turning left and shooting the player. It stops itself when the *"condition is no longer met"* (meniku, 2017)*.* Other change was switching the targetDistance and the targetOnRight as it seems to have a slight better movement when following the player around.

 

The final behaviour, which behaves different than all as the condition detects the player at a near distances it will stop moving, wait for 0.2 seconds and randomly chooses to stop moving and turn when the player is behind or retreats backwards whilst shooting the player (the shooting function for this condition has a 2 seconds cooldown before shooting the next bullet), it also has a 5% chance moving backwards, away from the player; also when it detects the off centre it will move towards the player and when it is close enough to it, it will start shooting until the player is dead.

 

## **Why is the last behaviour better than the others?**

That is because unlike the other behaviours this behaviour does not randomly shoot when it immediately detects the player while the other behaviours would detect the player and starts shooting, even when colliding with a building so it had a higher chance of killing itself before heading to the player.

The last behaviour proves that the AI would not immediately shoot when detecting the player but rather starts shooting when it is closer to the player giving it a "smarter" behaviour and a chance of it being slightly unpredictable with its movement.

# **Why behaviour trees over AI scripts?**

While it is good to have an AI script for an enemy or two, but it would be expensive to make more scripts for many different enemies. Using a behaviour tree structure, it would be less man power and, less expensive to do as each AI enemy can have a shared AI script but each would have a different behaviour structure depending on its behaviour tree.

However, a behaviour tree does not replace a finite state machine, but it combines it with the tree and as mentioned if you are creating multiple enemies then a behaviour tree is suitable as it is more efficient and flexible than a finite state machine.

# **Conclusion**

Overall, I believe I demonstrated a behaviour tree very well as it shows how each differ from the last and how changing certain conditions can make an AI behave differently also adding a flow chart helped me to see how each behaviour should be written down and how they work and if they need certain changes.

This gives a better understanding on how AIs work using different behaviours and what this could possibly do in the future.

If given enough time, I would have change the main AI script to something different to see how it would react based on health system and had a free for all multiplayer function and see how the AI enemies would communicate with each other and possibly and different game than Tank as it was used on lab 1 however, I have a better understanding on how behaviour tree finally works.

# **Video**

Demo for each behaviour: [https://youtu.be/GbzV_sIgOi4](https://youtu.be/GbzV_sIgOi4)

# **References**

Boston, U. (2015) *Unity*, [Online], Available:[ https://unity3d.com/learn/tutorials/s/tanks-tutorial](https://unity3d.com/learn/tutorials/s/tanks-tutorial) [02 Jan 2018].

Gow, J. (2017) *Learn.Gold*, 17 Oct, [Online], Available:[ https://learn.gold.ac.uk/mod/page/view.php?id=488255](https://learn.gold.ac.uk/mod/page/view.php?id=488255) [02 Jan 2018].

Maddogc (2012) *Unity*, 12 Jul, [Online], Available:[ https://answers.unity.com/questions/283377/how-to-delay-a-shot.html](https://answers.unity.com/questions/283377/how-to-delay-a-shot.html) [02 Jan 2018].

meniku (2017) *GitHub*, 16 Nov, [Online], Available:[ https://github.com/meniku/NPBehave](https://github.com/meniku/NPBehave) [06 Jan 2018].

 

 

