using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int maxHealth = 5
        ;
    private int health;
    
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"{health}/{maxHealth}");
        if (health <= 0)
        {
            Debug.Log("You are dead");
            transform.position = new Vector3(0,0);
        }
    }

    public void TakeDamge(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("You Died");
        }
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    internal int GetHealth()
    {
        return health;
    }
}
