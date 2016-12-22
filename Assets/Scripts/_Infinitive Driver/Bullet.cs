using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed = 70f;
    public int damage = 10;
    public float explosionRadius = 5f;
    public GameObject impactEffect;

    private Vector3 targetPosition;
    private bool TargetHitted = false;
    private GameObject target;

    public void Seek(Transform _target)
    {
        target = _target.gameObject;
        targetPosition = _target.position;
        float targetSpeed = _target.GetComponent<Movement>().correctedSpeed;
        float zCorrection = ((this.transform.position - targetPosition).magnitude / (speed+targetSpeed)) * targetSpeed;
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

        if (dir.magnitude <= distanceThisFrame && !TargetHitted)
        {
            HitTarget();
            TargetHitted = !TargetHitted;
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);
        Explode();
        //this.gameObject.SetActive(false);
    }

    void Explode()
    {
        this.gameObject.GetComponent<AudioSource>().Play();
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Player")
            {
                Damage(collider.transform);
            }
        }
        Invoke("Dead", 3);
    }

    void Damage(Transform enemy)
    {
        if (target != null)
        {
            target.GetComponent<Movement>().health = target.GetComponent<Movement>().health - damage;
        }
    }

    void Dead()
    {
        Destroy(this.gameObject);
    }
}
