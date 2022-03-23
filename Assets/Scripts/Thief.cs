using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : BaseClass
{
    private bool inShadows;

    void Start()
    {
        CreateCharacter();
    }

    protected override void CreateCharacter()
    {
        health = 15;
        base.CreateCharacter();
        projectileScript.damage = 2;
        projectileScript.projectileSpeed = 10;
        ableToShoot = true;
        shotDelay = 0.2f;
        movementSpeed = 10;
        jumpForce = 550;
        inShadows = false;
        cooldownTime = 15;
        abilityActiveTime = 5;

        ColorProjectile();
        

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile") && !inShadows)
        {
            IncomingProjectile(other);
        }
    }

    protected override IEnumerator SpecialAbility()
    {
        Color color = gameObject.GetComponent<MeshRenderer>().material.color;

        inShadows = true;
        color.a = 0.3f;
        gameObject.GetComponent<MeshRenderer>().material.color = color;
        currentTime = abilityActiveTime +1;
        abilityReady = false;
        abilityActive = true;
        yield return new WaitForSeconds(abilityActiveTime);
        color.a = 1f;
        inShadows = false;
        currentTime = cooldownTime + 1;
        abilityActive = false;
        gameObject.GetComponent<MeshRenderer>().material.color = color;
        Invoke("ResetAbilityCooldown", cooldownTime);

    }
}
