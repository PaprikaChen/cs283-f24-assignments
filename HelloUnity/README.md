# HW3
Paprika Chen

## Overview

This project is a low-poly 3D level designed in Unity. The level is divided into two main parts: the tram station where the character is born and a maze where the main activities occur.

## 2024/11/1 Updates:
- Collection Game and Object Spawner:
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
