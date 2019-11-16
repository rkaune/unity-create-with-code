﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private float initialHealth = 0;

    [SerializeField] private GameObject spawnOnDeathPrefab = null;
    [SerializeField] private int spawnOnDeathPrefabCount = 0;

    [SerializeField] private HealthBarController healthBarController = null;
    private float health = 0;

    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
        
        if (healthBarController)
        {
            healthBarController.UpdateHealthBar(health, initialHealth, name);
        }
    }

    public void DealDamage(float damage)
    {
        //Debug.Log(damage + " Damage dealt to " + gameObject.name);

        health -= damage;

        if (healthBarController)
        {
            healthBarController.UpdateHealthBar(health, initialHealth, name);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (spawnOnDeathPrefab)
        {
            for (int i = 0; i < spawnOnDeathPrefabCount; i++)
            {
                Instantiate(spawnOnDeathPrefab, transform.position, spawnOnDeathPrefab.transform.rotation);
            }
        }

        GameOverOnDeath go = GetComponent<GameOverOnDeath>();

        if (go)
        {
            go.TriggerGameOver();
        }

        Destroy(gameObject);
    }
}
