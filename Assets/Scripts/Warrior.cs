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
        projectileScript.projectileSpeed = 2;
        ableToShoot = true;
        shotDelay = 2;
        movementSpeed = 2;
        health = 5;
        jumpForce = 550;
    }

    
    protected override void SpecialAbility()
    {

    }

}
