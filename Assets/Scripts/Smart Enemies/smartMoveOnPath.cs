using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class smartMoveOnPath : MonoBehaviour
{
    public PlayerSight fow;
    public NavMeshAgent agent;
    public smartSearching ss;
  
    public int step = 0;
    public float speed = 2.0f;
    public float maxSpeed = 3.0f;
    public float rotationSpeed = 5.0f;

    private float reachDistance = 1.0f; //difference between the centre of the enemy and the point created by the EditorPath
    public bool check;
    public bool hitPlayer;
    private float timeLeft = 1f;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //pathToFolow = GameObject.Find(pathName).GetComponent<EditorPath>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (hitPlayer)
        {
            pauseMovement();
        }

        else if (fow.playerSeen)
        {
            speed = Min(maxSpeed, speed + 0.005f);
            followPlayer();
            check = true;
        }

        else if (fow.sees || fow.hear)
        {
            walkToOther();
        }

        else if (!check)
        {
            speed = 2.0f;
            searchSmart();
        }
        else if (check)
        {
            walkShortestRoute();
        }
        else
        {
        }
    }

    void pauseMovement()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            hitPlayer = false;
            timeLeft = 1f;
        }
    }


    void followPlayer()
    {
        agent.Resume();
        agent.SetDestination(fow.playerLastSeen);
        Quaternion rotation = Quaternion.LookRotation(fow.playerLastSeen - transform.position); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    void walkShortestRoute()
    {
        agent.Resume();
        agent.SetDestination(ss.pointToGO);
        Quaternion rotation = Quaternion.LookRotation(ss.pointToGO - transform.position); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        float eps = 0.5f;
        if (Mathf.Abs(ss.pointToGO.x - transform.position.x) < eps && Mathf.Abs(ss.pointToGO.z - transform.position.z) < eps)
        {
            agent.Stop();
            check = false;
        }
    }

    void walkToOther()
    {
        agent.Resume();
        agent.SetDestination(fow.toGo);
        Quaternion rotation = Quaternion.LookRotation(fow.toGo - transform.position); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        Debug.Log(transform.position == (fow.toGo));
        float eps = 0.5f;
        if (Mathf.Abs(fow.toGo.x - transform.position.x) < eps && Mathf.Abs(fow.toGo.z - transform.position.z) < eps)
        {
            agent.Stop();
            fow.hear = false;
            fow.sees = false;
            check = true;
        }
    }

    void searchSmart()
    {
        ss.moveToNextPoint();
    }

    public float Min(float f1, float f2)
    {
        if (f1 < f2)
            return f1;
        else
            return f2;
    }
}
