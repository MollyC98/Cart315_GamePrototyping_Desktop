using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// The AudioPlayer class responsible for playing audio clips
public class AudioPlayer : MonoBehaviour
{
   [Header("Shooting")]
   [SerializeField] AudioClip shootingClip;// Audio clip for shooting
   [SerializeField] [Range(0f,1f)]float shootingVolume =1f; // max volume for shooting audio 

   [Header("Damage")]
   [SerializeField] AudioClip damageClip;
   [SerializeField] [Range(0f,1f)]float damageVolume =1f;

   public void PlayShootingClip()
   {
    /*if(shootingClip != null)
    {
        AudioSource.PlayClipAtPoint(shootingClip, Camera.main.transform.position, shootingVolume);
        
    }*/

    PlayClip(shootingClip, shootingVolume);
   }

   public void PlayDamageClip() // audio to be played when damage happens 
   {
    /*if(damageClip != null)
    {
        AudioSource.PlayClipAtPoint(damageClip, Camera.main.transform.position, damageVolume);
        
    }*/

    PlayClip(damageClip, damageVolume); // Call the generic PlayClip method to play the damage clip
   }

   void PlayClip(AudioClip clip, float volume) // specified volume 
   {
    if(clip != null) // check for null 
    {
        Vector3 cameraPos = Camera.main.transform.position; // getting position for the main camera 
        AudioSource.PlayClipAtPoint(clip, cameraPos, volume); // play audio at specified camera pos with specified vol 

    }

   }


}
