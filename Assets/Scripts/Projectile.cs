using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE - PARENT
public class Projectile : MonoBehaviour
{
    //ENCAPSULATION
    private float default_projectileSpeed = 5.0f;
    [SerializeField] private float private_projectileSpeed;
    public float projectileSpeed
    {
        get { return private_projectileSpeed; }
        set
        {
            if (value < 0.0f)
            {
                Debug.LogError("Projectile Speed can't be negative. Setting to default");
                private_projectileSpeed = default_projectileSpeed;
            }
            else
            {
                private_projectileSpeed = value;
            }
        }
    }

    //ENCAPSULATION
    private int default_damage = 5;
    [SerializeField] private int private_damage;
    public int damage
    {
        get { return private_damage; }
        set
        {
            if (value < 0.0f)
            {
                Debug.LogError("Projectile Damage can't be negative. Setting to default");
                private_damage = default_damage;
            }
            else
            {
                private_damage = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    protected virtual void Update()
    {
        //ABSTRACTION
        DestroyOutOfBounds();
    }

    protected virtual void Move(Vector3 direction)
    {
        transform.Translate(direction * projectileSpeed * Time.deltaTime);
    }

    protected virtual void DestroyOutOfBounds()
    {
        if (Mathf.Abs(transform.position.x) > 15)
        {
            Destroy(gameObject);
        }
    }

}
