using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : BaseClass
{
    void Start()
    {
        projectileScript = projectile.GetComponent<Projectile>();
        base.playerRB = GetComponent<Rigidbody>();
        projectileScript.damage = 5;
        ableToShoot = true;
        rateOfFire = 1;
        movementSpeed = 2;
        health = 5;
    }

    protected override void SpecialAbility()
    {

    }
}
