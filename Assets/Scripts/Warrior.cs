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
        health = 40;    
        base.CreateCharacter();    
        projectileScript.damage = 5;
        projectileScript.projectileSpeed = 3;
        ableToShoot = true;
        shotDelay = 0.6f;
        movementSpeed = 2;
        jumpForce = 550;
        cooldownTime = 30;
        abilityActiveTime = 0f;

        ColorProjectile();
        
    }

    
    protected override IEnumerator SpecialAbility()
    {
        health = health + 10;
        healthText.text = "Your HP: " + health;
        currentTime = abilityActiveTime +1;
        abilityReady = false;
        abilityActive = true;
        yield return new WaitForSeconds(abilityActiveTime);
        currentTime = cooldownTime + 1;
        abilityActive = false;
        Invoke("ResetAbilityCooldown", cooldownTime);


    }

}
