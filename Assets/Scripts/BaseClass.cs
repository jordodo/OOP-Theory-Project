using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseClass : MonoBehaviour
{
    private int health;
    private int damage;
    private float rateOfFire;

    public virtual void Move()
    {

    }

    public virtual void ConstrainMovement()
    {

    }

    public virtual void Shoot()
    {

    }

    public virtual void Jump()
    {

    }

    public abstract void SpecialAbility();
}
