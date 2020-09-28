using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEndGoal : MonoBehaviour
{
    public static event Action OnEnemyReached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            OnEnemyReached?.Invoke();
        }
    }
}
