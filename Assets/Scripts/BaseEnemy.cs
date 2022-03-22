using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected int health;
    [SerializeField] protected TextMeshProUGUI healthText;
    protected Projectile projectileScript;

    // Start is called before the first frame update
    void Start()
    {
        projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.damage = 5;
        projectileScript.projectileSpeed = 6;
        health = 500;
        healthText = GameObject.Find("EnemyHealth").GetComponent<TextMeshProUGUI>();
        healthText.text = "Enemy HP: " + health;
        StartCoroutine(Shoot());

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

    protected virtual void DeathCheck(bool playerWin)
    {
        if (health <= 0)
        {
            MainManager.Instance.GameOver(true);
            healthText.text = "Enemy HP: 0";
        }  
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Projectile incomingProjectileScript = other.gameObject.GetComponent<Projectile>();
            health = health - incomingProjectileScript.damage;
            healthText.text = "Enemy HP: " + health;
            Destroy(other.gameObject);
            DeathCheck(true);
        }
    }
}
