using UnityEngine;
using System.Collections;

public class TTMovement : MonoBehaviour {
    public GameObject player;
    public Transform firePoint;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    public float throwAngle = 20f;
    private float fireCountdown = 1f;

    private float minDistance = 10;
    private float maxDistance = 60;
    private float[] hArr;
    private float hBorder = 0.65f;
    private float speed;
    private float waitUntilGo = 0.5f;
    private int animThrowingHash = Animator.StringToHash("Throw");

    private Animator anim;

    void Awake()
    {
        anim = this.transform.FindChild("trump").GetComponent<Animator>();
        hArr = new float[3];
        for (int i = 0; i < 3; i++)
        {
            hArr[i] = 0f;
        }
        throwAngle = (throwAngle / 180) * Mathf.PI;
    }
	
	void Start () {
        speed = player.GetComponent<Movement>().speed;
	}

    // Update is called once per frame
    void Update() {

        Movement();

        if (player != null && fireCountdown <= 0f)
        {
            StartCoroutine(Shoot());
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    private void Movement()
    {
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
        else if (this.transform.position.x > 4f)
        {
            this.transform.position = new Vector3(4f, this.transform.position.y, this.transform.position.z);
        }
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

    IEnumerator Shoot()
    {
        anim.SetTrigger(animThrowingHash);
        yield return new WaitForSeconds(1.2f);
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            Vector3 pos = firePoint.position;
            Vector3 targetPosition = player.transform.position;
            float targetSpeed = player.GetComponent<Movement>().correctedSpeed;
            float zCorrection = ((pos - targetPosition).magnitude / (targetSpeed + speed)) * targetSpeed;
            targetPosition = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z + zCorrection + 4f);

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

            float bulletSpeed = Mathf.Sqrt(R * R * g / Mathf.Abs(R * Mathf.Sin(2 * throwAngle) - 2 * hf * cost * cost));

            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
            if (bulletRB != null)
            {
                bulletRB.AddForce(forceDir * bulletSpeed, ForceMode.VelocityChange);
            }
        }

    }
}
