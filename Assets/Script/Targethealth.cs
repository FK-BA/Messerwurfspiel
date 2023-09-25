using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targethealth : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public ParticleSystem myParticleSystem; // Reference to the Particle System


    public void TakeDamage(int damage)
    {

        health -= damage;
        if (health <= 0)
            DestroyEnemy(); 
    }


    public void DestroyEnemy()
    {
        // Activate the Particle System 
        if (myParticleSystem != null)
        {
            myParticleSystem.Play();
        }

        // Increase the score by 1 
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(1);
        }

        // Destroy the game object 
        Destroy(gameObject);
    }
}