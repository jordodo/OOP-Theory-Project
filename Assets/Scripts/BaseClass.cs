using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseClass : MonoBehaviour
{
    [SerializeField] protected int health;
    protected int damage;
    protected float shotDelay;
    protected float horizontalAxis;
    [SerializeField] protected float movementSpeed = 5;
    protected Rigidbody playerRB;
    [SerializeField] protected bool isGrounded;
    [SerializeField] protected bool ableToShoot;
    [SerializeField] protected GameObject projectile;
    protected Projectile projectileScript;
    protected float jumpForce;
    

    protected virtual void CreateCharacter()
    {
        projectileScript = projectile.GetComponent<Projectile>();
        playerRB = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        ConstrainMovement();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Z) && ableToShoot)
        {
            print("Yes");
            Shoot();
        }


    }

    protected virtual void Move()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * horizontalAxis * Time.deltaTime * movementSpeed);
    }

    protected virtual void ConstrainMovement()
    {
        if (transform.position.x < -12)
        {
            transform.position = new Vector3(-12, transform.position.y, transform.position.z);
        }

        if (transform.position.x > 8)
        {
            transform.position = new Vector3(8, transform.position.y, transform.position.z);
        }
    }

    protected virtual void Shoot()
    {
        Vector3 projectileSpawnPos = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        Instantiate(projectile, projectileSpawnPos, projectile.transform.rotation);
        ableToShoot = false;
        Invoke("ResetShotCooldown", shotDelay);
    }

    private void ResetShotCooldown()
    {
        ableToShoot = true;
    }

    protected virtual void Jump()
    {
        playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        } 

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Projectile incomingProjectileScript = other.gameObject.GetComponent<Projectile>();
            health = health - incomingProjectileScript.damage;
            Destroy(other.gameObject);
        }
    }


    protected abstract void SpecialAbility();


}
