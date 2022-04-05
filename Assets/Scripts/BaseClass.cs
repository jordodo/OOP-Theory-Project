using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//INHERITANCE - PARENT
public abstract class BaseClass : MonoBehaviour
{
    //Variables
    //Stats for each unit
    protected int health;
    protected int damage;
    protected float shotDelay;
    protected float cooldownTime;
    protected float currentTime;
    protected float abilityActiveTime;
    protected float horizontalAxis;
    protected float movementSpeed;
    protected float jumpForce;
    protected bool isGrounded;
    protected bool ableToShoot;
    protected bool abilityReady;
    protected bool abilityActive;

    //Necessary components
    protected Rigidbody playerRB;
    [SerializeField] protected GameObject projectile;
    protected TextMeshProUGUI healthText;
    protected TextMeshProUGUI abilityText;
    protected Projectile projectileScript;

    //Movement bounds
    protected float leftBound = -12;
    protected float rightBound = 8;


    protected virtual void CreateCharacter()
    {
        projectileScript = projectile.GetComponent<Projectile>();
        playerRB = GetComponent<Rigidbody>();
        healthText = GameObject.Find("PlayerHealth").GetComponent<TextMeshProUGUI>();
        healthText.text = "Your HP: " + health;
        abilityText = GameObject.Find("AbilityTimer").GetComponent<TextMeshProUGUI>();
        abilityText.text = "Ability: READY";
        abilityReady = true;
        abilityActive = false;
    }


    //ABSTRACTION
    protected virtual void Update()
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

            if (!abilityReady && abilityActive)
            {
                CountDownTimer("Ability active:");
            }

            if (!abilityReady && !abilityActive)
            {
                CountDownTimer("Ability CD:", true);
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
        if (transform.position.x < leftBound)
        {
            transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
        }

        if (transform.position.x > rightBound)
        {
            transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);
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

    //POLYMORPHISM
    private void CountDownTimer(string text)
    {
        if (currentTime > 1)
        {
            int timeToDisplay = Mathf.FloorToInt(currentTime % 60);
            abilityText.text = text + " " + timeToDisplay;
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0;
        }
    }

    //POLYMORPHISM
    private void CountDownTimer(string text, bool resetCooldown)
    {
        if (currentTime > 1)
        {
            int timeToDisplay = Mathf.FloorToInt(currentTime % 60);
            abilityText.text = text + " " + timeToDisplay;
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            if (resetCooldown)
            {
                //ABSTRACTION
                ResetAbilityCooldown();
            }

        }
    }


    private void ResetAbilityCooldown()
    {
        abilityReady = true;
        abilityText.text = "Ability: READY";
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
            MainManager.Instance.GameOver("Game Over: You Died");
            healthText.text = "Your HP: 0";
        }
    }

    protected void IncomingProjectile(Collider other)
    {
        Projectile incomingProjectileScript = other.gameObject.GetComponent<Projectile>();
        health = health - incomingProjectileScript.damage;
        healthText.text = "Your HP: " + health;
        Destroy(other.gameObject);
        //ABSTRACTION
        DeathCheck();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            //ABSTRACTION
            IncomingProjectile(other);
        }
    }

    protected void ColorProjectile()
    {
        MeshRenderer currentMesh = gameObject.GetComponent<MeshRenderer>();
        MeshRenderer projectileMesh = projectile.GetComponent<MeshRenderer>();

        projectileMesh.material = currentMesh.material;
        print("Coloring");
    }


    protected abstract IEnumerator SpecialAbility();


}
