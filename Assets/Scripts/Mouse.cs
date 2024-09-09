using UnityEngine;
using System.Collections.Generic;

public class Mouse : MonoBehaviour
{
    public BoxCollider2D area; // Collider defining the area within which the mouse can spawn
    public List<Transform> snakeParts; // Reference to the snake body parts

    private void Start(){
        // Spawn the mouse at the start of the game
        SpawnMouse();
    }

    private void SpawnMouse(){
        Bounds bounds = area.bounds; // Get the bounds of the area where the mouse can spawn
        Vector3 newPosition;

        do {
            // Generate a random position within the defined bounds
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);

            newPosition = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);

        // Repeat until the new position is not overlapping with any snake parts
        } while (IsOverlappingWithSnake(newPosition));

        // Set the mouse's position to the new valid position
        transform.position = newPosition;
    }

    // Check if the generated position overlaps with any snake parts
    private bool IsOverlappingWithSnake(Vector3 position){
        foreach (Transform part in snakeParts){
            if (part.position == position){
                return true; // Position overlaps with a snake part
            }
        }
        return false; // No overlap
    }

    void OnTriggerEnter2D(Collider2D other){
        // If the mouse collides with the snake, spawn a new mouse
        if(other.tag == "Snake"){
            SpawnMouse();
        }
    }
}
