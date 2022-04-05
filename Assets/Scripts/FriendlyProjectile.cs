using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyProjectile : Projectile
{
    // Start is called before the first frame update
    void Start()
    {

    }

    //POLYMORPHISM
    protected override void Update()
    {
        Move(Vector3.right);
        base.Update();
    }
}
