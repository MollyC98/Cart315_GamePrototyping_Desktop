using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactPickup : MonoBehaviour
{
    [SerializeField] AudioClip artifactPickupSFX;
    [SerializeField] int pointsForArtifactPickup = 100;

    bool wasCollected = false;
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForArtifactPickup);
            AudioSource.PlayClipAtPoint(artifactPickupSFX, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
