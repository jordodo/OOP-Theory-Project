using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : BaseClass
{
    void Start()
    {
        CreateCharacter();
    }

    protected override void CreateCharacter()
    {
        base.CreateCharacter();
        projectileScript.damage = 5;
        projectileScript.projectileSpeed = 5;
        ableToShoot = true;
        shotDelay = 0.6f;
        movementSpeed = 5;
        health = 50;
        jumpForce = 550;
    }

    
    protected override void SpecialAbility()
    {

    }

}
