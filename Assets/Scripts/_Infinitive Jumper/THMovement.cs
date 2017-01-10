using UnityEngine;
using System.Collections;

public class THMovement : MonoBehaviour {
    
    public GameObject player;
    public Transform firePoint;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    public float throwAngle = 30f;

    private float fireCountdown = 1f;
    private float waitUntilGo = 0.5f;
    private float speed = 10f;
    

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = this.transform.position;
        this.transform.position = new Vector3(pos.x, player.transform.position.y, pos.z);

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
        {
            float xf = player.transform.position.x - this.transform.position.x;
            float zf = player.transform.position.z - this.transform.position.z;
            Vector3 plainDir = new Vector3(xf, 0, zf);
            plainDir = plainDir.normalized;
            float yf = Mathf.Tan((throwAngle/180)*Mathf.PI);
            Vector3 forceDir = new Vector3(plainDir.x, yf, plainDir.z);

            float g = Mathf.Abs(Physics.gravity.y);
            float R = Mathf.Sqrt(xf * xf + zf * zf)/(2.5f);
            speed = Mathf.Sqrt(R * g / Mathf.Sin((throwAngle / 180) * Mathf.PI));

            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
            if (bulletRB != null)
            {
                bulletRB.AddForce(forceDir*speed, ForceMode.VelocityChange);
            }
        }

    }
}
