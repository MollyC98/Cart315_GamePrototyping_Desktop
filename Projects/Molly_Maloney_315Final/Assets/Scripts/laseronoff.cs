using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class Laser : MonoBehaviour
{
    public TilemapRenderer laserTilemapRenderer; // Reference to the TilemapRenderer component of the laser
    public ParticleSystem collisionParticleEffect; // Reference to the particle effect prefab

    private bool isOn = true; // Flag to control if the laser is on or off
    public float onTime = 2f; // Time the laser stays on
    public float offTime = 1f; // Time the laser stays off

    private TilemapCollider2D laserCollider; // Reference to the BoxCollider2D component of the laser

    // Start is called before the first frame update
    void Start()
    {
        // Get reference to the TilemapRenderer component
        if (laserTilemapRenderer == null)
        {
            laserTilemapRenderer = GetComponent<TilemapRenderer>();
        }

        // Get reference to the BoxCollider2D component
        laserCollider = GetComponent<TilemapCollider2D>();
        
        // Start the coroutine to toggle the laser state randomly
        StartCoroutine(ToggleLaserRandom());
    }

    // Coroutine to toggle the laser on/off randomly
    IEnumerator ToggleLaserRandom()
    {
        while (true)
        {
            // Toggle the laser state
            ToggleLaser(!isOn);
            
            // Wait for the specified time
            yield return new WaitForSeconds(isOn ? onTime : offTime);
        }
    }

    // Method to toggle the laser on/off
    public void ToggleLaser(bool on)
    {
        isOn = on;
        // Enable or disable the renderer based on the laser state
        laserTilemapRenderer.enabled = on;
        // Enable or disable the collider based on the laser state
        laserCollider.enabled = on;
    }

    // Detect collisions with the player when the laser is on
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOn && other.CompareTag("Player"))
        {
            Debug.Log("Player collided with laser");
            // Instantiating the particle effect at the collision point
            Instantiate(collisionParticleEffect, other.transform.position, Quaternion.identity);
            
            
    }
}*/
}