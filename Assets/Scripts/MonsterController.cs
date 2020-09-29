using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour, IDamagable
{

    public static event Action<MonsterController> OnMonsterDisable;
    public static event Action<float> OnMonsterDeath;

    [SerializeField]
    private float _enemyValue = 5f;     //Used to store the points this enemy grants on death
    [SerializeField]
    private float _damageAmount = 10f;  //Used to damage the buildings
    [SerializeField]
    private float _maxHealth;
    public float CurrentHealth { get; set; }
    [SerializeField]
    private NavMeshAgent _agent;


    private void OnEnable()
    {
        CurrentHealth = _maxHealth;
        transform.position = transform.position;
        StartMoving();
    }

    private void OnDisable()
    {
        _agent.isStopped = false;
    }

    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        if(CurrentHealth <= 0)
        {
            Death();
        }
    }



    private void StartMoving()
    {
        //_agent.Warp(transform.position);
        _agent.enabled = true;
        _agent.SetDestination(Vector3.zero);

        //_agent.isStopped = false;
    }

    public void Death()
    {
        //return to pool
        //OnMonsterDisable?.Invoke(this);
        //disable monster
        Debug.Log("I'm dead " + gameObject.name);
        OnMonsterDeath?.Invoke(_enemyValue);
    }
}
