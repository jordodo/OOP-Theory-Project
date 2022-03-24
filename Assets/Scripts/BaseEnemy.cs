using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected GameObject projectile;
    protected int health;
    protected TextMeshProUGUI healthText;
    protected Projectile projectileScript;

    // Start is called before the first frame update
    void Start()
    {
        if (MainManager.Instance != null)
        {
            projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.damage = 5;
            projectileScript.projectileSpeed = 6;
            health = 500;
            healthText = GameObject.Find("EnemyHealth").GetComponent<TextMeshProUGUI>();
            healthText.text = "Enemy HP: " + health;
            StartCoroutine(Shoot());
        }        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual IEnumerator Shoot()
    {
        if (!MainManager.Instance.gameOver)
        {
            Vector3 projectileSpawnPos = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            float projectileDelay = Random.Range(0.1f, 2f);
            Instantiate(projectile, projectileSpawnPos, projectile.transform.rotation);
            yield return new WaitForSeconds(projectileDelay);
            StartCoroutine(Shoot());
        }
    }

    protected virtual void DeathCheck()
    {
        if (health <= 0)
        {
            MainManager.Instance.GameOver("Game Over: You Win!");
            healthText.text = "Enemy HP: 0";
        }  
    }

    protected void IncomingProjectile(Collider other)
    {
            Projectile incomingProjectileScript = other.gameObject.GetComponent<Projectile>();
            health = health - incomingProjectileScript.damage;
            healthText.text = "Enemy HP: " + health;
            Destroy(other.gameObject);
            DeathCheck();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            IncomingProjectile(other);
        }
    }
}
