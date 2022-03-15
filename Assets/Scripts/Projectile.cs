using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float projectileSpeed = 5;
    public int damage = 1;
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
