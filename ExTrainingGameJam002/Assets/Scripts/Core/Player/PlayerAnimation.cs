using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation: MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] upSprites;
    public Sprite[] downSprites;
    public Sprite[] leftSprites;
    public Sprite[] rightSprites;
    public Sprite[] idleSprites;

    public float movementAnimationSpeed = 0.2f;
    public float idleAnimationSpeed = 0.2f;

    private enum Direction { Up, Down, Left, Right, Idle }
    private Direction currentDirection = Direction.Idle;
    private int currentFrame = 0;
    private float timeSinceLastFrame = 0f;

    private void Update()
    {
        // Get player input to move the character
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0f || verticalInput != 0f)
        {
            // Update the character's sprite based on the direction
            UpdateCharacterDirection(horizontalInput, verticalInput);
            UpdateAnimationFrame(movementAnimationSpeed);
        }
        else
        {
            // Player is not moving, set the character's sprite to idle
            currentDirection = Direction.Idle;
            UpdateAnimationFrame(idleAnimationSpeed);
        }

        // Add code to move the character based on input here
    }

    private void UpdateCharacterDirection(float inputX, float inputY)
    {
        // Calculate the magnitude of the input vector to determine the primary direction
        Vector2 inputVector = new Vector2(inputX, inputY).normalized;

        if (inputVector == Vector2.zero)
        {
            return;
        }

        if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
        {
            if (inputVector.x > 0f)
            {
                // Moving right
                currentDirection = Direction.Right;
            }
            else
            {
                // Moving left
                currentDirection = Direction.Left;
            }
        }
        else
        {
            if (inputVector.y > 0f)
            {
                // Moving up
                currentDirection = Direction.Up;
            }
            else
            {
                // Moving down
                currentDirection = Direction.Down;
            }
        }
    }

    private void UpdateAnimationFrame(float animationSpeed)
    {
        // Determine which set of sprites to use based on the current direction
        Sprite[] currentSprites = GetCurrentSprites();

        if (currentSprites.Length == 0)
        {
            return;
        }

        timeSinceLastFrame += Time.deltaTime;

        // Check if it's time to advance to the next frame
        if (timeSinceLastFrame >= animationSpeed)
        {
            timeSinceLastFrame -= animationSpeed;
            currentFrame = (currentFrame + 1) % currentSprites.Length;
        }

        // Set the current frame sprite
        spriteRenderer.sprite = currentSprites[currentFrame];
    }

    private Sprite[] GetCurrentSprites()
    {
        switch (currentDirection)
        {
            case Direction.Up:
                return upSprites;
            case Direction.Down:
                return downSprites;
            case Direction.Left:
                return leftSprites;
            case Direction.Right:
                return rightSprites;
            default:
                return idleSprites;
        }
    }
}






