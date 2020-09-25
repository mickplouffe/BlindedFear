using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    float CurrentHealth { get; set; }

    void Damage(float damageAmount);

    void Death();
}
