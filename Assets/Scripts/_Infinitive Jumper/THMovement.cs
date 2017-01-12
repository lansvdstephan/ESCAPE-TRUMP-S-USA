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
    public float speed = 10f;
    public float test;
    

    // Use this for initialization
    void Start () {
        Physics.gravity = Physics.gravity * 9f;
        throwAngle = (throwAngle / 180) * Mathf.PI;
    }
	
	// Update is called once per frame
	void Update () {
       /* Vector3 pos = this.transform.position;
        this.transform.position = new Vector3(pos.x, player.transform.position.y, pos.z);
        */
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

        if (bullet != null)
        {
            Vector3 pos = this.transform.position;
            Vector3 targetPosition = player.transform.position;
            /*float targetSpeed = player.GetComponent<Movement>().correctedSpeed;
            float zCorrection = ((this.transform.position - targetPosition).x / (targetSpeed)) * targetSpeed;
            targetPosition = new Vector3(targetPosition.x + zCorrection, targetPosition.y, targetPosition.z);*/

            float xf = targetPosition.x - pos.x;
            float zf = targetPosition.z - pos.z;
            float hf = targetPosition.y - pos.y;

            Vector3 plainDir = new Vector3(xf, 0, zf);
            plainDir = plainDir.normalized;
            float yf = Mathf.Tan(throwAngle);
            Vector3 forceDir = new Vector3(plainDir.x, yf, plainDir.z);
            forceDir = forceDir.normalized;

            float cost = Mathf.Cos(throwAngle);
            float g = Mathf.Abs(Physics.gravity.y);
            float R = Mathf.Sqrt(xf * xf + zf * zf);

            test = R * R * g / (R * Mathf.Sin(2 * throwAngle) - 2 * hf * cost * cost);

            speed = Mathf.Sqrt(R * R * g / Mathf.Abs(R * Mathf.Sin(2 * throwAngle) - 2 * hf * cost * cost));

            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
            if (bulletRB != null)
            {
                bulletRB.AddForce(forceDir * speed, ForceMode.VelocityChange);
            }
        }

    }
}
