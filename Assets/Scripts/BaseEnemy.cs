using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual IEnumerator Shoot()
    {
        Vector3 projectileSpawnPos = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        float projectileDelay = Random.Range(0.1f, 2f);
        Instantiate(projectile, projectileSpawnPos, projectile.transform.rotation);
        yield return new WaitForSeconds(projectileDelay);
        StartCoroutine(Shoot());
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
}
