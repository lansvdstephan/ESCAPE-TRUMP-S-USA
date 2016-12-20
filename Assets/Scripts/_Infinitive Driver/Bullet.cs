using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    private Vector3 targetPosition;

    public float speed = 70f;

    public int damage = 50;

    public float explosionRadius = 5f;
    public GameObject impactEffect;

    public void Seek(Transform _target)
    {
        targetPosition = _target.position;
        float targetSpeed = _target.GetComponent<Movement>().speed;
        float zCorrection = ((this.transform.position - targetPosition).magnitude / (speed+targetSpeed*2)) * targetSpeed * 2;
        targetPosition = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z +zCorrection);
    }

    // Update is called once per frame
    void Update()
    {

        if (targetPosition == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = targetPosition - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);
        Explode();
        Destroy(this.gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Player")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        /*Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
        }*/
    }
}
