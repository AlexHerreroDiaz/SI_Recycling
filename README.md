# SI_Recycling
## Interactive Systems
### Universitat Pompeu Fabra
#### Group Members
* Guillem Gauchia
* Àlex Herrero Díaz
* Jahanzeb Raja
#### Repository : https://github.com/AlexHerreroDiaz/SI_Recycling
<p><strong> First Delivery for Final Project - Recycling</strong></p>
<p>For this deliver we created a placeholder of the scenario and background music.

<p><strong> Second Delivery for Final Project - Recycling</strong></p>
<p>For this release we have implemented 4 grabage trucks of different colours to pick up the rubbish. To do this we have implemented the ability to select a truck using the tracker once you are standing on it and it follows us until we are at the point of unselection marked with a red rectangle.
<p>We have also implemented that while we are selecting a truck we can pick up objects of the same colour as the truck by passing over them and these objects will stay inside the truck.
<p>Finally we have also implemented the functionality to unload the truck once we pass over the same coloured rubbish bin as the truck.
<p>We only need to add the different rubbish objects to replace them with the test objects we currently have, add an animation to the factory which is supposed to create the rubbish, and set a victory condition to end the game.

<p><strong> Final Delivery for Final Project - Recycling</strong></p>
<p>New implementations:<p>
  
* Added an intro menu to start the game, with a tutorial guide if needed.
* Added two different types of assets to recolect for every one of the four type of garbage.
* Hability to grab and drop multiple objects at the same time.
* Spawn of garbage objects at the begining of every round.
* Spawn of tools after the recolect all the garbage objects at the end of the round that can be droped at the factory to "repair" it.
* Spawn of renewable energy sources near the factory at the end of each round.
* Added different sound for different interactions.
* Added a final particle effect system to simulate confetti for the end of the game.

<p>Before starting the game a menu appears where you can start or read some tutorial slides to understand the game. To go through the slides you click on the arrows on the sides of the slides, and to start the game you click on the start button. Once the game starts, a certain number of objects will spawn and you will have to pick them up and throw them into their respective containers. To do this we control the rubbish trucks by standing on top of them so that they will follow us. While they follow us we have to pick up the objects by driving over them with the truck and then throw them over the corresponding container. Once we have thrown all the rubbish, two tools will appear which we have to pick up with any truck and deposit them in the factory, pretending that we are ‘repairing’ them and a wind turbine or a solar panel will appear near the factory. This will conclude a round. The game consists of 6 rounds, each round increasing the number of objects to collect. Once all 6 rounds are completed, confetti will appear indicating the end of the game.
</p>
