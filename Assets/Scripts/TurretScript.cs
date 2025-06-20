using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public float launchCooldown;
    public float launchSpeed;

    public Transform firePoint;
    public GameObject projectile;

    private bool blocked;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("LaunchProjectile", 1.0f, launchCooldown);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LaunchProjectile()
    {
        /*
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(firePoint.transform.position, projectile.GetComponent<CircleCollider2D>().radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.name != "Player")
            {
                return;
            }
        }
        */

        GameObject instance = Instantiate(projectile, firePoint.transform.position, transform.rotation);
        instance.GetComponent<Rigidbody2D>().velocity = transform.up * launchSpeed;
    }
}
