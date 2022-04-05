using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE - CHILD
public class Thief : BaseClass
{
    protected bool inShadows;

    void Start()
    {
        CreateCharacter();
    }

    protected override void Update()
    {
        base.Update();
    }

    //POLYMORPHISM
    protected override void CreateCharacter()
    {
        health = 15;
        base.CreateCharacter();
        projectileScript.damage = 1;
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

    //POLYMORPHISM
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile") && !inShadows)
        {
            IncomingProjectile(other);
        }
    }

    //POLYMORPHISM
    protected override IEnumerator SpecialAbility()
    {
        Color color = gameObject.GetComponent<MeshRenderer>().material.color;

        inShadows = true;
        color.a = 0.3f;
        gameObject.GetComponent<MeshRenderer>().material.color = color;
        currentTime = abilityActiveTime + 1;
        abilityReady = false;
        abilityActive = true;
        yield return new WaitForSeconds(abilityActiveTime);
        color.a = 1f;
        inShadows = false;
        currentTime = cooldownTime + 1;
        abilityActive = false;
        gameObject.GetComponent<MeshRenderer>().material.color = color;
    }
}
