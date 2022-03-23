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
        shotDelay = 1.5f;
        movementSpeed = 5;
        jumpForce = 550;
        cooldownTime = 20;
        abilityActiveTime = 5f;

        ColorProjectile();
        
    }

    
    protected override IEnumerator SpecialAbility()
    {
        shotDelay /= 2;
        movementSpeed *= 1.5f;
        projectileScript.projectileSpeed *= 1.5f;
        ableToShoot = true;

        currentTime = abilityActiveTime +1;
        abilityReady = false;
        abilityActive = true;
        yield return new WaitForSeconds(abilityActiveTime);

        shotDelay *= 2;
        movementSpeed /= 1.5f;
        projectileScript.projectileSpeed /= 1.5f;

        currentTime = cooldownTime + 1;
        abilityActive = false;
        Invoke("ResetAbilityCooldown", cooldownTime);

    }
}
