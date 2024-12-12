# Game: Heaven's Postman
Paprika Chen

## Description
This is a heartfelt adventure game where you play as Julian, a 22-year-old courier in heaven delivering fragmented messages from the living to souls in paradise. Navigate a magical maze filled with dangers, piece together lost letters, and uncover the emotional stories behind each sender and recipient. 

# Game Features Documentation

## Updates (2024/12/12 - Final Version)

### **Canvas Control**
![CanvasControl](https://github.com/user-attachments/assets/28c31b9e-c505-4dc2-95dd-56054a95e290)

- **Description**: Manages multiple canvases for the starting pages of the game.
- **Script**: [`CanvasController.cs`](Assets/Scripts/CanvasController.cs)

---

### **Day-Night Cycle**
![daynight](https://github.com/user-attachments/assets/795d45c3-d41d-4499-8611-12f43cc2b496)

- **Description**: Smoothly transitions between four time states: sunrise, midday, sunset, and night. Adjusts ambient light color/intensity and the position, direction, and color of the main light source.
- **Script**: [`DayNightCycle.cs`](Assets/Scripts/DayNightCycle.cs)

---

### **Guide NPCs**
![guideNPC](https://github.com/user-attachments/assets/361d4153-33de-41b2-a588-5f1b0ecfd8ef)

- **Description**: Guide NPCs react to the player's approach, initiate a dialogue to guide them to the next destination, and then watch the player.
- **Script**: [`BehaviorUnique.cs`](Assets/Scripts/BehaviorUnique.cs)

---

### **Minion NPCs**
- **Light Control**:
  ![ghostlight](https://github.com/user-attachments/assets/3a75778f-ded2-4386-9f5e-b6bdbdc5fe9b)

  - **Description**: Ghosts emit light and shadow during the night to indicate danger.  
  - **Script**: [`LightController.cs`](Assets/Scripts/LightController.cs)
  
- **Behavior**:  
  - **Description**:  
    - Attack with fireballs, accompanied by sound effects to alert the player (useful if the player is reading a letter).  
    - Maintain distance from the player during attacks for visibility.  
    - Cease attacks and following behavior during the daytime or when the player returns to the home area.  
  - **Script**: [`BehaviorMinion.cs`](Assets/Scripts/BehaviorMinion.cs)

---

### **Health System**
- **Description**:  
  - Players start with 4 hearts.  
  - Lose 1 heart when hit by a fireball.
    ![health_attack](https://github.com/user-attachments/assets/326ea35d-b935-4ae1-abf4-ee93a5e84f98)

  - Recover 1 heart by collecting mushrooms.
    ![health_heal](https://github.com/user-attachments/assets/43b9b6f9-532a-4146-84a0-1ab845e00c67)

  - Death triggers respawn at the maze's starting point.
    ![reborn](https://github.com/user-attachments/assets/bb045750-3b1f-4ed0-af45-66198c3b5be9)

  - A UI displays and updates the heart count in real-time.
  
- **Scripts**:  
  - [`HealthSystem.cs`](Assets/Scripts/HealthSystem.cs): Handles heart updates, UI changes, and respawn functionality.  
  - [`AttackTrigger.cs`](Assets/Scripts/AttackTrigger.cs): Deducts 1 heart when a fireball hits the player.  
  - [`DisappearEffect.cs`](Assets/Scripts/DisappearEffect.cs): Restores 1 heart when a mushroom is collected and handles the mushroom's disappear animation.

---

### **Bus Transportation with Button**
![Bus](https://github.com/user-attachments/assets/d018994f-b5e4-42f4-89a3-f730c7a49653)

- **Description**:  
  - When near a bus stop, a button appears, allowing the player to teleport to the maze stop by clicking it.  
- **Scripts**:  
  - [`BusButton.cs`](Assets/Scripts/BusButton.cs): Executes the player's position transformation.  
  - [`ShowBusTransportButton.cs`](Assets/Scripts/ShowBusTransportButton.cs): Displays the button when the player approaches the bus stop.

---

### **Collect Letter Fragments**
![collectLetter](https://github.com/user-attachments/assets/a7fd3608-ae03-46cd-b000-b73c10768f1e)

- **Description**:  
  - Collect 5 rotating, glowing letter fragments.  
  - Each fragment disappears with an animation upon collection, and its content is revealed.  
  - The collection progress updates accordingly.  
- **Script**: [`CollectiveLetter.cs`](Assets/Scripts/CollectiveLetter.cs): Manages collection animations and content display for letter fragments.

---

### **Letter Button to Check Content Anytime**
 ![toggleLetter](https://github.com/user-attachments/assets/6634820c-10de-4ace-9332-a66aab2737ea)
- **Description**:  
  - Clicking the letter button or pressing SPACE toggles the display of the most recent letter content.  
  - The letter UI can be hidden using the same controls.  
- **Scripts**:  
  - [`LetterUI.cs`](Assets/Scripts/LetterUI.cs): Handles letter display toggling.  
  - [`ShowSendLetterButton.cs`](Assets/Scripts/ShowSendLetterButton.cs): Displays the button when the player is near the mailbox.

---

### **Deliver the Complete Letter**
  ![sendLetter](https://github.com/user-attachments/assets/2f532419-f0d1-4427-8870-5c7103599b00)
- **Description**:  
  - A button appears near the mailbox, allowing the player to send the letter if all 5 fragments are collected.  
  - Upon sending, the final letter content is displayed, and a hidden NPC appears, signaling the game's conclusion.  
- **Script**: [`SendLetterButton.cs`](Assets/Scripts/sendLetterButton.cs)

---

### **Quit Handler**
- **Description**: The game will exit in the following scenarios:  
  1. The player presses ESC.  
  2. The player successfully delivers the complete letter.  
- **Script**: [`GameQuitHandler.cs`](Assets/Scripts/GameQuitHandler.cs)

  

## 2024/11/15 Updates:
- NPC Settings:
  
Currently having 3 Minions that will attack the player, and 1 tour guide that will give the player hint:
the following shows the wandering and the position arrangements of all NPCs:

![c89bdbe4dbc96293efe2cda8004f075c](https://github.com/user-attachments/assets/248ee460-8150-4dc2-ae3c-0073be2a0614)



- Minoin NPC:
  
 BehaviorMinion.cs Behavior: Wander, follow and Attack the player:

![6f24e9a78346979993d11f0ae11830aa](https://github.com/user-attachments/assets/6889b02f-8b2e-41c2-9805-b56d1632c93e)


- Guide NPC:
  
  BehaviorUnique.cs Behavior: Wander and guide the player by giving hints:

![8bd9b8f62a439af99a171b606ff9151f](https://github.com/user-attachments/assets/b3ed5368-fea0-49b6-8a72-ec8b39472eef)

- Home Area: the garden in the center of the maze.



## 2024/11/8 Updates:
NevMesh and the wandering NPC:
![HelloUnity - A09_Path - Windows, Mac, Linux - Unity 2022 3 28f1_ _DX11_ 2024-11-08 14-41-43](https://github.com/user-attachments/assets/7db83ecc-f13b-4585-8948-2ae3a731ea9d)



## 2024/11/1 Updates:
- Collection Game and Object Spawner :
Video is with background music and sound effects:
https://github.com/user-attachments/assets/91e61ae2-9bf3-4153-9218-9afe2678b644



## 2024/10/25 Updates:
- Character Animation and Object Colliders:
![HelloUnity - A07_Motion - Windows, Mac, Linux - Unity 2022 3 28f1 _DX11_ 2024-10-25 17-07-33](https://github.com/user-attachments/assets/7be668e5-a632-4e87-a5d7-51a6ed564add)



## 2024/10/20 Updates:
- Linear camera:
![HelloUnity - A06_FollowPath - Windows, Mac, Linux - Unity 2022 3 28f1 _DX11_ 2024-10-20 23-47-35](https://github.com/user-attachments/assets/e974110c-1854-42c5-8a69-59cba593e0c1)



- cubic camera:
![HelloUnity - A06_FollowPath - Windows, Mac, Linux - Unity 2022 3 28f1 _DX11_ 2024-10-20 23-38-28](https://github.com/user-attachments/assets/1f057f89-ce4d-4202-a08e-71558d658a7b)



## 2024/10/3 Updates
- Added Rigid camera:
  
![HelloUnity - A05_Player - Rigid](https://github.com/user-attachments/assets/f5867ff5-4a8f-4e64-ad60-de82a847ef78)

- Added Spring camera:
  
![HelloUnity - A05_Player - Spring (3)](https://github.com/user-attachments/assets/776b4992-0e9c-4576-bc75-c60614055bdc)



## 2024/9/27 Updates
- Added a main character.
  
  <img width="629" alt="微信图片_20240927224247" src="https://github.com/user-attachments/assets/2a208316-a69d-4bec-abaa-b10486a0208c">
- Interactive Camera and Flythrough Tour:

  ![HelloUnity - A04_Tour - Windows, Mac, Linux - Unity 2022 3 28f1 _DX11_ 2024-09-27 22-25-00 (1)](https://github.com/user-attachments/assets/3d7b6700-dd75-45c7-b88e-71b735a77037)
  
  ![HelloUnity - A04_Tour - Windows, Mac, Linux - Unity 2022 3 28f1 _DX11_ 2024-09-27 22-25-00](https://github.com/user-attachments/assets/27a0701b-12d4-4443-b6f6-57778758aad4)


## Level Design
<img width="1140" alt="6dd844a1cff314c0e05daa711f152e1" src="https://github.com/user-attachments/assets/df38725c-0a43-46fc-a45b-c67633011db0">


### Tram Station (Birthplace)
- **Starting Point**: The character begins the adventure at a tram station, designed as the initial spawn point and transportation hub.
- **Transportation**: Characters use a tram to travel from the tram station to the maze.
<img width="860" alt="53aa3cb02690e148743ae4b515b6f7a" src="https://github.com/user-attachments/assets/9a73754f-0190-4fd6-a7a4-be897a47a027">
<img width="1097" alt="1e2d16d4b20e1a1b695101ad503baa2" src="https://github.com/user-attachments/assets/ed0aaa4d-3202-482d-bf85-14d3bd31dde5">



### Maze (Main Activity Area)
- **Objective**: After arriving at the maze, the character engages in various tasks, navigating through intricate paths and solving puzzles to progress.
- **Design**: The maze will be designed with multiple pathways and challenges that test the player's problem-solving skills and dexterity.
- <img width="630" alt="4e3f4628b0acdf3c3b668185757e5e0" src="https://github.com/user-attachments/assets/b1314541-b77e-419e-8ca6-8dcaf0ace050">
<img width="1097" alt="9034c20426bc3db00b091a5ac6379ee" src="https://github.com/user-attachments/assets/93d1ef60-5420-4d89-8ffd-35e96a8a899a">
<img width="1097" alt="8309b14db4827bfac131a4f8240bece" src="https://github.com/user-attachments/assets/148f18de-d3a3-45e5-b067-f4e51b43f764">

## Unsolved Issues

1. **Tram Animation**: The moving tram animation needs to be fully implemented to automate the transition between the tram station and the maze, so we need to move the character to test different places mamually now.
2. **Rigidbody Components**: Not every object in the scene has an appropriate `Rigidbody` component applied yet, which will be addressed in future updates.
3. **Horizontal lines**: I still have a bug where the two scenes are not aligned on the same horizontal plane, and I will address this issue later.
4. **Rebuilding Lost Content**: Due to a version control issue, the `README.md` for HW2 and the Lab scene were deleted. Plans are underway to recreate these assets within the week.

## Credits

The following assets were used in the project but were not created by me:

1. Tram: https://assetstore.unity.com/packages/3d/vehicles/land/street-vehicles-pack-autobus-tram-213421
2. Trees: https://assetstore.unity.com/packages/3d/props/exterior/low-poly-houses-free-pack-243926
3. Maze, pond, and trees: https://assetstore.unity.com/packages/3d/environments/low-poly-environment-park-242702
4. Floor and trees: https://assetstore.unity.com/account/assets
5. Flowers and grass: https://assetstore.unity.com/packages/3d/environments/landscapes/low-poly-simple-nature-pack-162153
6. Cafe house: https://assetstore.unity.com/packages/3d/environments/simplepoly-city-low-poly-assets-58899
7. Skybox pic: https://assetstore.unity.com/packages/2d/textures-materials/sky/allsky-free-10-sky-skybox-set-146014
