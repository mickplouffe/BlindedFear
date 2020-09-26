using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour, IDamagable
{

    public static event Action<MonsterController> OnMonsterDisable;

    [SerializeField]
    private float _enemyValue = 5f;     //Used to store the points this enemy grants on death
    [SerializeField]
    private float _damageAmount = 10f;  //Used to damage the buildings
    [SerializeField]
    private float _maxHealth;
    public float CurrentHealth { get; set; }

    private void OnEnable()
    {
        CurrentHealth = _maxHealth;
    }

    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        if(CurrentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        //return to pool
        OnMonsterDisable?.Invoke(this);
        //disable monster
        Debug.Log("I'm dead " + gameObject.name);
    }
}
