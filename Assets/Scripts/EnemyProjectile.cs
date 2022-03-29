using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //POLYMORPHISM
    protected override void Update()
    {
        Move(Vector3.left);
        base.Update();
    }
}
