using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour, IHitReceiver
{
    public float health = 100;
    public UnityEvent deathEvents;
    public void Hit(float damage)
    {
        health -= damage;
        if (health < 0)
            deathEvents.Invoke();
    }
}
