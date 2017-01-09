using UnityEngine;
using System.Collections;

public class THMovement : MonoBehaviour {

    public GameObject player;
    public Transform firePoint;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 1f;

    private float waitUntilGo = 0.5f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null && fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (waitUntilGo != 0)
        {
            waitUntilGo = waitUntilGo - 0.05f;
        }

        if (bullet != null)
            ;
    }
}
