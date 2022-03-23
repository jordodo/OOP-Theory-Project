using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // ENCAPSULATION
    [SerializeField] private float private_projectileSpeed = 5;
    public float projectileSpeed
    {
        get{return private_projectileSpeed;}
        set{private_projectileSpeed = value;}
    }

    [SerializeField] private int private_damage = 5;
    public int damage
    {
        get{return private_damage;}
        set{private_damage = value;}
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void Move(Vector3 direction)
    {
        transform.Translate(direction * projectileSpeed * Time.deltaTime);
    }
    
    public virtual void DestroyOutOfBounds()
    {
        if (Mathf.Abs(transform.position.x) > 15)
        {
            Destroy(gameObject);
        }
    }
    
}
