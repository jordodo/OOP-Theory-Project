using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class BaseClass : MonoBehaviour
{
    [SerializeField] protected int health;
    protected int damage;
    protected float shotDelay;
    protected float cooldownTime;
    protected float currentTime;
    protected float abilityActiveTime;
    protected float horizontalAxis;
    [SerializeField] protected float movementSpeed = 5;
    protected Rigidbody playerRB;
    [SerializeField] protected bool isGrounded;
    [SerializeField] protected bool ableToShoot;
    [SerializeField] protected bool abilityReady;
    [SerializeField] protected bool abilityActive;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected TextMeshProUGUI healthText;
    [SerializeField] protected TextMeshProUGUI abilityText;
    protected Projectile projectileScript;
    protected float jumpForce;

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

            if (!abilityReady && abilityActive)
            {
                CountDownTimer("Ability active:");
            }

            if (!abilityReady && !abilityActive)
            {
                CountDownTimer("Ability CD:");
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

    private void CountDownTimer(string text)
    {
        if (currentTime > 1)
        {
            int timeToDisplay = Mathf.FloorToInt(currentTime % 60);
            //print(timeToDisplay);
            abilityText.text = text + " " + timeToDisplay;
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0;
        }
    }


    private void ResetAbilityCooldown()
    {
        abilityReady = true;
        if (!MainManager.Instance.gameOver)
        {
            abilityText.text = "Ability: READY";
        }
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

    protected void IncomingProjectile(Collider other)
    {
            Projectile incomingProjectileScript = other.gameObject.GetComponent<Projectile>();
            health = health - incomingProjectileScript.damage;
            healthText.text = "Your HP: " + health;
            Destroy(other.gameObject);
            DeathCheck();
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
            IncomingProjectile(other);
        }
    }

    protected void ColorProjectile()
    {
        MeshRenderer currentMesh = gameObject.GetComponent<MeshRenderer>();
        MeshRenderer projectileMesh = projectile.GetComponent<MeshRenderer>();

        projectileMesh.material = currentMesh.material;
    }

    protected abstract IEnumerator SpecialAbility();


}
