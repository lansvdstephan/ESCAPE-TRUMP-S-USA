using UnityEngine;
using System.Collections;

public class TTMovement : MonoBehaviour {
    public GameObject player;
    public Transform firePoint;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 1f;

    private float minDistance = 10;
    private float maxDistance = 60;
    private float[] hArr;
    private float hBorder = 0.65f;
    private float speed;
    private float waitUntilGo = 0.5f;

    void Awake()
    {
        hArr = new float[3];
        for (int i = 0; i < 3; i++)
        {
            hArr[i] = 0f;
        }
    }
	
	void Start () {
        speed = player.GetComponent<Movement>().speed;
	}

    // Update is called once per frame
    void Update() {
        float h = GetHorizontalMovement();
        Vector3 movement = new Vector3(h, 0, 2);
        movement = movement * speed * Time.deltaTime;
        this.transform.position = this.transform.position + movement;
        // prefenting the tank to come to close or to go to far
        if (this.transform.position.z - player.transform.position.z < minDistance)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, player.transform.position.z + minDistance);
        }
        else if (this.transform.position.z - player.transform.position.z > maxDistance)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, player.transform.position.z + maxDistance);
        }

        // prefenting the tank to leave the road
        if (this.transform.position.x < -14f)
        {
            this.transform.position = new Vector3(-14f, this.transform.position.y, this.transform.position.z);
        }
        else if (this.transform.position.x  > 4f)
        {
            this.transform.position = new Vector3( 4f, this.transform.position.y, this.transform.position.z);
        }

        if (player != null && fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    private float GetHorizontalMovement()
    {
        hArr[2] = hArr[1];
        hArr[1] = hArr[0];
        hArr[0] = Random.Range(-1f, 1f);
        float h = (hArr[2] + hArr[1] + hArr[0]) / 3;
        if ( h < hBorder && h > -hBorder)
        {
            h = 0;
        }
        return h;
    }

    void OnCollisionEnter(Collision collision)
    {

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
            bullet.Seek(player.transform, waitUntilGo);
    }
}
