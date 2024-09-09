using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class Snake : MonoBehaviour
{
    // Current movement direction of the snake
    private Vector2 direction = Vector2.right;

    // List to keep track of the snake's body parts
    private List<Transform> parts = new List<Transform>();

    // Prefab for the snake's body
    public Transform BodyPrefab;
    
    // Initial size of the snake
    public int size = 5;

    // Current score of the game
    int count = 0;
    
    // UI Text component to display the score
    public TextMeshProUGUI scoreText;

    // Flag to determine if the snake is in reversed mode
    private bool isReversed = false;

    // Colors for normal and reversed modes
    public Color normalColor = Color.green;
    public Color reversedColor = Color.red;

    // SpriteRenderer components for the snake and body parts
    public SpriteRenderer snakeRenderer;
    public SpriteRenderer bodyRenderer;

    // Reference to the Mouse script to avoid spawning on the snake
    public Mouse mouse;

    private void Start(){
        // Initialize the snake with its body parts
        parts.Add(transform);

        // Instantiate the initial body parts of the snake
        for(int i = 1; i < size; i++){
            parts.Add(Instantiate(BodyPrefab));
        }

        // Set the initial position of the snake to zero
        transform.position = Vector3.zero;

        // Initialize score and update the score text
        count = 0;
        scoreText.text = "Score: " + count;

        // Set the reversed mode flag to false
        isReversed = false;

        // Update the color of the snake based on the reversed mode
        UpdateSnakeColor();

        // Pass the snake parts list to the Mouse script
        mouse.snakeParts = parts;
    }

    public void MoveUp(){
        // Prevent reversing direction if moving down
        if (direction != Vector2.down) {
            if(isReversed){
                if(direction != Vector2.up) {
                    direction = Vector2.down;
                }
            }
            else
                direction = Vector2.up;
        }
    }

    public void MoveDown(){
        // Prevent reversing direction if moving up
        if (direction != Vector2.up) {
            if(isReversed){
                if(direction != Vector2.down) {
                    direction = Vector2.up;
                }
            }
            else
                direction = Vector2.down;
        }
    }

    public void MoveLeft(){
        // Prevent reversing direction if moving right
        if (direction != Vector2.right) {
            if(isReversed){
                if(direction != Vector2.left) {
                    direction = Vector2.right;
                }
            }
            else
                direction = Vector2.left;
        }
    }

    public void MoveRight(){
        // Prevent reversing direction if moving left
        if (direction != Vector2.left) {
            if(isReversed){
                if(direction != Vector2.right) {
                    direction = Vector2.left;
                }
            }
            else
                direction = Vector2.right;
        }
    }

    private void FixedUpdate(){
        // Move each body part to the position of the previous part
        for(int i = parts.Count-1; i > 0; i--){
            parts[i].position = parts[i-1].position;
        }

        // Move the snake head in the current direction
        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + direction.x, 
            Mathf.Round(transform.position.y) + direction.y,
            0.0f
        );
    }

    private void Grow(){
        // Instantiate a new body part and add it to the snake
        Transform part = Instantiate(BodyPrefab);
        part.position = parts[parts.Count - 1].position;

        parts.Add(part);

        // Update the color of the new body part based on the reversed mode
        SpriteRenderer partRenderer = part.GetComponent<SpriteRenderer>();
        if (partRenderer != null){
            if (isReversed){
                partRenderer.color = reversedColor;
            } else {
                partRenderer.color = normalColor;
            }
        }
    }

    void GameOver(){
        // Destroy all body parts except the head
        for(int i = 1; i < parts.Count; i++){
            Destroy(parts[i].gameObject);
        }

        // Clear the list of body parts
        parts.Clear();

        // Load the GameOver scene
        SceneManager.LoadScene("GameOver");
    }

    void OnTriggerEnter2D(Collider2D other){
        // Handle collision with the Mouse object
        if(other.tag == "Mouse"){
            Grow();
            UpdateScore();
        }
        // Handle collision with an obstacle
        else if(other.tag == "Obstacle"){
            GameOver();
        }
    }

    void UpdateScore(){
        // Increment score and update the UI text
        count++;
        scoreText.text = "Score: " + count;
    }

    public void ToggleReversal(){
        // Toggle the reversed mode and update the snake's color
        isReversed = !isReversed;
        UpdateSnakeColor();
    }

    void UpdateSnakeColor(){
        // Update the color of the snake's head
        if (snakeRenderer != null){
            if (isReversed){
                snakeRenderer.color = reversedColor;
            } else {
                snakeRenderer.color = normalColor;
            }
        }

        // Update the color of each body part
        foreach (Transform part in parts){
            SpriteRenderer partRenderer = part.GetComponent<SpriteRenderer>();
            if (partRenderer != null){
                if (isReversed){
                    partRenderer.color = reversedColor;
                } else {
                    partRenderer.color = normalColor;
                }
            }
        }
    }
}
