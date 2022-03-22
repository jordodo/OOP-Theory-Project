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
        health = 30;
        base.CreateCharacter();
        projectileScript.damage = 15;
        projectileScript.projectileSpeed = 5;
        ableToShoot = true;
        abilityReady = true;
        shotDelay = 2;
        movementSpeed = 5;
        jumpForce = 550;
        
    }
    
    protected override IEnumerator SpecialAbility()
    {
        print("1");
        abilityReady = false;
        yield return new WaitForSeconds(2);
        print("2");
        abilityReady = true;

    }
}
