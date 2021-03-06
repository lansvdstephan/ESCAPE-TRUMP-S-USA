﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed = 70f;
    public int damage = 10;
    public float explosionRadius = 5f;
    public GameObject impactEffect;

    private Vector3 targetPosition;
    private bool targetHitted = false;
    private bool go = false;
    private GameObject target;

    public void Seek(Transform _target, float waitUntilGo)
    {
        Invoke("Go", waitUntilGo);
        target = _target.gameObject;
        targetPosition = _target.position;
        float targetSpeed = _target.GetComponent<Movement>().correctedSpeed;
        float zCorrection = ((this.transform.position - targetPosition).magnitude / (speed+targetSpeed)) * targetSpeed;
        targetPosition = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z +zCorrection);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (go && !targetHitted)
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

    }

    void HitTarget()
    {
        targetHitted = true;
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
        //this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        this.gameObject.GetComponent<Collider>().enabled = false;
        Invoke("Dead", 3);
    }

    void Damage(Transform target)
    {
        if (target != null)
        {
            if (target.GetComponent<Movement>() != null)
            {
                target.GetComponent<Movement>().health = target.GetComponent<Movement>().health - damage;
                target.GetComponent<Movement>().damaged = true;
            }
            else if (target.GetComponent<JumpMovement>() != null)
            {
                target.GetComponent<JumpMovement>().health = target.GetComponent<JumpMovement>().health - damage;
                target.GetComponent<JumpMovement>().damaged = true;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.tag);
        if (!collision.gameObject.CompareTag("Shield"))
        {
            HitTarget();
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }


    void Go()
    {
        go = true;
    }

    void Dead()
    {
        Destroy(this.gameObject);
    }
}
