using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : BaseClass
{

    void Start()
    {
        CreateCharacter();
    }

    protected override void CreateCharacter()
    {
        base.CreateCharacter();
        projectileScript.damage = 15;
        projectileScript.projectileSpeed = 2;
        ableToShoot = true;
        shotDelay = 2;
        movementSpeed = 2;
        health = 30;
        jumpForce = 550;
    }

    protected override void SpecialAbility()
    {
        
    }
}
