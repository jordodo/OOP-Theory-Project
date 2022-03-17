using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : BaseClass
{
    void Start()
    {
        CreateCharacter();
    }

    protected override void CreateCharacter()
    {
        base.CreateCharacter();
        projectileScript.damage = 2;
        projectileScript.projectileSpeed = 10;
        ableToShoot = true;
        shotDelay = 0.2f;
        movementSpeed = 10;
        health = 15;
        jumpForce = 550;
    }

    protected override void SpecialAbility()
    {

    }
}
