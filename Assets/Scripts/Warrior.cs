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
        health = 50;    
        base.CreateCharacter();    
        projectileScript.damage = 5;
        projectileScript.projectileSpeed = 3;
        ableToShoot = true;
        shotDelay = 0.6f;
        movementSpeed = 2;
        jumpForce = 550;

    }

    
    protected override IEnumerator SpecialAbility()
    {
        print("1");
        yield return new WaitForSeconds(2);
        print("2");

    }

}
