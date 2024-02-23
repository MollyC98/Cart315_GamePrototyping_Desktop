using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;

    [SerializeField] int score= 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] int deathValue;

    [SerializeField] bool applyCameraShake;

    CameraShake cameraShake;

    AudioPlayer audioPlayer;

    ScoreKeeper scoreKeeper;

    LevelManager levelManager;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnCollisionEnter2D(Collision2D other)

    {   
        
        Debug.Log(other.gameObject.name);
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            audioPlayer.PlayDamageClip();
            //kills projectile
            damageDealer.Hit();
        }
    }

    public int GetHealth()
    {
        return health;

    }

    void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        if(health <= deathValue)
        {
            //Destroy(gameObject);
            Die();
        }
    }

    void Die()
    {
        if(!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
        }

        else
        {
            levelManager.LoadGameOver();

        }
        Destroy(gameObject);
    }

    void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();

        }

    }
}

