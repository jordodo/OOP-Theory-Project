using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField] protected bool abilityReady;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected TextMeshProUGUI healthText;
    protected Projectile projectileScript;
    protected float jumpForce;
    

    protected virtual void CreateCharacter()
    {
        projectileScript = projectile.GetComponent<Projectile>();
        playerRB = GetComponent<Rigidbody>();
        healthText = GameObject.Find("PlayerHealth").GetComponent<TextMeshProUGUI>();
        healthText.text = "Your HP: " + health;
        print("create char");
    }


    // Update is called once per frame
    void Update()
    {
        if (!MainManager.Instance.gameOver)
        {
            Move();
            ConstrainMovement();

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }

            if (Input.GetKey(KeyCode.Z) && ableToShoot)
            {
                Shoot();
            }

            if (Input.GetKeyDown(KeyCode.X) && abilityReady)
            {
                StartCoroutine(SpecialAbility());
            }
        
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
        Vector3 projectileSpawnPos = new Vector3(transform.position.x + 1, transform.position.y, 0);
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
        isGrounded = false;
    }

    protected virtual void DeathCheck()
    {
        if (health <= 0)
        {
            MainManager.Instance.GameOver(false);
            healthText.text = "Your HP: 0";
        }  
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
            healthText.text = "Your HP: " + health;
            Destroy(other.gameObject);
            DeathCheck();
        }
    }


    protected abstract IEnumerator SpecialAbility();


}
