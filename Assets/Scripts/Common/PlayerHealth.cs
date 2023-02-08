using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float MaxHealth = 100;
    // Start is called before the first frame update
    public float health { get; set; }
    private bool dead = false; //Not big soup rice

    public Action onDamage;
    public Action onHeal;
    public Action onDeath;


    private void Awake()
    {
        health = MaxHealth;
    }

    public void OnApplyDamage(float damage)
    {
        if (dead) return;

        health -= damage;
        health = Mathf.Clamp(health, 0, MaxHealth);
        onDamage?.Invoke();
        if (health <= 0)
        {
            dead = true;
            onDeath?.Invoke();
        }
    }

    public void OnApplyHealth(float heal)
    {
        if (dead) return;

        health += heal;
        health = Mathf.Clamp(health, 0, MaxHealth);
        onHeal?.Invoke();
    }
}
