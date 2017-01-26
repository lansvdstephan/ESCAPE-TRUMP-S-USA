using UnityEngine;
using System.Collections;

public class THMovement : MonoBehaviour
{

    public GameObject player;
    public Transform firePoint;
    public Animator trumpAnim;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float fireRate = 0.10f;
    public float horinzontalSpeed = 30f;

    private float fireCountdown = 1f;
    private float maxFireRate = 0.25f;
    private float lastUpdated = 10f;
    private int animThrowingHash = Animator.StringToHash("Throw");

    public float speed = 10f;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float y = this.transform.position.y;
        if (player != null && fireCountdown <= 0f)
        {

            StartCoroutine(Shoot());
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
        if (fireRate < maxFireRate)
        {
            fireRate += 0.05f * Time.deltaTime;
        }
        if (Mathf.RoundToInt(y) % 100 == 0 && Mathf.RoundToInt(y) > Mathf.RoundToInt(lastUpdated))
        {
            maxFireRate += 0.25f;
            maxFireRate = Mathf.Min(1f, maxFireRate);
            lastUpdated = y;
            print(lastUpdated);
        }
    }

    IEnumerator Shoot()
    {
        trumpAnim.SetTrigger(animThrowingHash);
        yield return new WaitForSeconds(1.2f);
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            Vector3 pos = firePoint.position;
            Vector3 targetPosition = player.transform.position;
            Vector3 targetSpeed = player.GetComponent<JumpMovement>().correctedSpeed;
            float xCorrection = ((pos - targetPosition).magnitude / (targetSpeed.x + horinzontalSpeed)) * targetSpeed.x;
            targetPosition = new Vector3(targetPosition.x + xCorrection, targetPosition.y + 1f, targetPosition.z);
            float xf = targetPosition.x - pos.x;
            float zf = targetPosition.z - pos.z;
            float hf = targetPosition.y - pos.y;

            Vector3 plainDir = new Vector3(xf, 0, zf);
            plainDir = plainDir.normalized;

            float g = Mathf.Abs(Physics.gravity.y);
            float R = Mathf.Sqrt(xf * xf + zf * zf);
            float verticalSpeed = (R * g) / (2 * horinzontalSpeed) + (hf * horinzontalSpeed) / R;
            float bulletSpeed = Mathf.Sqrt(Mathf.Pow(verticalSpeed, 2) + Mathf.Pow(horinzontalSpeed, 2));


            float throwAngle = Mathf.Atan2(verticalSpeed, horinzontalSpeed);
            float yf = Mathf.Tan(throwAngle);

            Vector3 forceDir = new Vector3(plainDir.x, yf, plainDir.z);
            forceDir = forceDir.normalized;

            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
            if (bulletRB != null)
            {
                bulletRB.AddForce(forceDir * bulletSpeed, ForceMode.VelocityChange);
            }
        }

    }
}