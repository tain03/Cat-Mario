# Unity Platformer Game

This project is a 2D platformer game built using Unity. The game features player movement, jumping, and interactions with various game elements such as power-ups and pipes.

## Features

- **Player Movement**: Control the player using on-screen buttons for left, right, and jump actions.
- **Power-Ups**: Collect different types of power-ups to gain abilities or points.
- **Pipes**: Enter and exit pipes to navigate through different parts of the level.

## Setup

1. **Clone the Repository**:
   ```sh
   git clone <repository-url>
   cd <repository-directory>
   ```

2. **Open the Project in Unity**:
   - Open Unity Hub.
   - Click on "Add" and select the project directory.

3. **Assign UI Buttons**:
   - In the Unity Editor, assign the `leftButton`, `rightButton`, `jumpButton`, and `enterButton` fields in the `ButtonController` script to the corresponding UI buttons in the scene.

## Scripts

### ButtonController.cs

Handles the input from on-screen buttons for player movement and actions.

### PlayerMovement.cs

Controls the player's movement, including walking, jumping, and applying gravity.

### PowerUp.cs

Defines different types of power-ups and their effects on the player.

### Pipe.cs

Handles the player's interaction with pipes, allowing them to enter and exit different areas.

## How to Play

- Use the on-screen buttons to move the player left, right, jump and enter.
- Collect power-ups to gain abilities or points.
- Use the enter button to interact with pipes and navigate through the level.
