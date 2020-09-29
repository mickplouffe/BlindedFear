using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IDamagable
{

    public static event System.Action<float> OnBuildingDamaged;

    [SerializeField]
    private float _penaltyPoints = 1f;  //Used to reduce the player's score when shot
    [SerializeField]
    private float _maxHealth = 200f;
    public float CurrentHealth { get; set; }

    private void OnEnable()
    {
        CurrentHealth = _maxHealth;
    }

    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        OnBuildingDamaged?.Invoke(-1 * _penaltyPoints);

        if(CurrentHealth <= 0f)
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("I'm dead " + gameObject.name);
    }
}
