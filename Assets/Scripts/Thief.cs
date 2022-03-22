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
        health = 15;
        base.CreateCharacter();
        projectileScript.damage = 2;
        projectileScript.projectileSpeed = 10;
        ableToShoot = true;
        abilityReady = true;
        shotDelay = 0.2f;
        movementSpeed = 10;
        jumpForce = 550;

    }

    protected override IEnumerator SpecialAbility()
    {
        Color color = gameObject.GetComponent<MeshRenderer>().material.color ;
        print(color);
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        color.a = 0.3f;
        gameObject.GetComponent<MeshRenderer>().material.color = color;
        abilityReady = false;
        print(color);
        yield return new WaitForSeconds(10);
        print("2");
        abilityReady = true;
        color.a = 1f;
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        gameObject.GetComponent<MeshRenderer>().material.color = color;

    }
}
