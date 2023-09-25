using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;

    private bool targetHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target") && !targetHit)
        {
  
            Targethealth enemy = other.GetComponent<Targethealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            targetHit = true;
            Destroy(gameObject);
        }
    }
}