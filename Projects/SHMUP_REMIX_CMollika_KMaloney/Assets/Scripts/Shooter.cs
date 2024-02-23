using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour // shooter class responsible for firing projectiles 
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab; // prefab for the projectile fired 
    [SerializeField] float projectileSpeed = 10f; // speed of the projectiles 
    [SerializeField] float projectileLifetime = 1f; // life of the projectiles 

    [SerializeField] float basefiringRate = 0.2f;// Base rate (fire)

    [Header("AI")] // AI controlled firing enabled 

    [SerializeField] bool useAI;

    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;// minimum fire rate for AI 

    [HideInInspector] public bool isFiring;// determines if shooter is firing

    Coroutine firingCoroutine; // continuous firing 

    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();

    }

   

   
    void Start()
    {
        if(useAI) // If AI-controlled firing is enabled, set isFiring to true
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire(); // handling firing logic 
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null) // If firing is enabled and the firingCoroutine is null, start firing
        {
          firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }
    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab, //Instantiate a projectile at the shooter's position with no rotation
            transform.position, Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed; //If the Rigidbody2D component exists, set its velocity to move upward with the specified speed
            }


            Destroy(instance, projectileLifetime);
            float timeToNextProjectile = Random.Range(basefiringRate - firingRateVariance,
            basefiringRate + firingRateVariance);

            // Ensure the timeToNextProjectile is within the defined minimumFiringRate and float.MaxValu

            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

            audioPlayer.PlayShootingClip();


            yield return new WaitForSeconds(timeToNextProjectile);   // Wait for the calculated time before firing the next projectile
        }
    }


}
