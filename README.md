# Learning-to-kart
Hi there, I tried to train AI with Reinforcement Learning (RL) using UnityML-Agents. The task AI needed to be done is steering left/right, acceleration, and avoiding obstacles.

Don't know what is UnityML-Agents? Go ahead learn more about it [here](https://github.com/Unity-Technologies/ml-agents)

![mlagent](https://user-images.githubusercontent.com/41731559/104704139-3c452a80-5753-11eb-9907-eba134fe5932.png)

I also use Karting Microgame from Unity Asset Store that you can get it from [here](https://assetstore.unity.com/packages/templates/karting-microgame-150956)

![karting](https://user-images.githubusercontent.com/41731559/104704987-54697980-5754-11eb-9350-d9675c6bc331.png)

## Main Goal :
1. AI can learn how to steering left/right
2. AI can learn how to accelerate
3. AI can learn how to avoid obstacles
4. AI can drive in many different tracks without crashing

## Rules :
1. AI will be trained only on one track to learn everything that needed to be done such as steering left/right, accelerate, and avoid obstacles
2. AI will be tested on different environment such as different track (with and without obstacles)
3. If AI hit obstacle/track AI position will be reset to the starting point and will be given penalty -1
4. If AI success reaching checkpoint will be given reward +1

## Learning Track
**AI WILL ONLY LEARN ON THIS TRACK**

![trek 3 obstacle](https://user-images.githubusercontent.com/41731559/104701964-50d3f380-5750-11eb-86cb-aad456391a49.PNG)

## Testing Track
**These Are All The Track**

![all circuit](https://user-images.githubusercontent.com/41731559/104702092-78c35700-5750-11eb-82dd-52982ef26f1f.png)

## Training Progress
![RL_Training](https://user-images.githubusercontent.com/41731559/104711897-e5dce980-575c-11eb-86bb-be93a9e8ed85.gif)

## Result
![RL_Demo](https://user-images.githubusercontent.com/41731559/104712818-02c5ec80-575e-11eb-832b-f36e90a99524.gif)
