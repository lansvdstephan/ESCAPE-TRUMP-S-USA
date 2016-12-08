using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveonPath : MonoBehaviour
{
    public EditorPath pathToFolow;
    public PlayerSight fow;
    public NavMeshAgent agent;

    public int currentWayPointID = 0;
    public int step = 0;
    public float speed = 2.0f;
    public float maxSpeed = 3.0f;
    public float rotationSpeed = 5.0f;

    private float reachDistance = 1.0f; //difference between the centre of the enemy and the point created by the EditorPath
    public bool check;
    public bool seePlayer;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //pathToFolow = GameObject.Find(pathName).GetComponent<EditorPath>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        seePlayer = fow.playerSeen;

        if (fow.playerSeen)
        {
            speed = Min(maxSpeed, speed + 0.005f);
            followPlayer();
            check = true;
        }

        else if (fow.other)
        {
            walkToOther();
        }

        else if (!check)
        {
            speed = 2.0f;
            followPath();
        }
        else if (check)
        {
            walkShortestRoute();
        }
        else
        {
        }
    }

    void followPath()
    {
        float distance = Vector3.Distance(pathToFolow.path_objects[currentWayPointID].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, pathToFolow.path_objects[currentWayPointID].position, Time.deltaTime * speed); //Move from current position to next position

        Quaternion rotation = Quaternion.LookRotation(pathToFolow.path_objects[currentWayPointID].position - transform.position); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        if (distance <= reachDistance)
        {
            currentWayPointID++;
        }

        if (currentWayPointID >= pathToFolow.path_objects.Count)
        {
            currentWayPointID = 0;
        }
    }

    void followPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, fow.playerLastSeen, Time.deltaTime * speed); //Move from current position to next position
        Quaternion rotation = Quaternion.LookRotation(fow.playerLastSeen - transform.position); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    void walkShortestRoute()
    {
        agent.SetDestination(pathToFolow.path_objects[currentWayPointID].position);
        Quaternion rotation = Quaternion.LookRotation(pathToFolow.path_objects[currentWayPointID].position - transform.position); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        check = false;
        agent.Stop();
    }

    void walkToOther()
    {
        Debug.Log(transform.position);
        Debug.Log(fow.otherPosition);
        agent.Resume();
        agent.SetDestination(fow.otherPosition);
        Quaternion rotation = Quaternion.LookRotation(fow.otherPosition - transform.position); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        float eps = 0.5f;
        if (Mathf.Abs(fow.otherPosition.x - transform.position.x) < eps && Mathf.Abs(fow.otherPosition.z - transform.position.z) < eps)
        {
            agent.Stop();
            fow.other = false;
        }
    }


    public float Min(float f1, float f2)
    {
        if (f1 < f2)
            return f1;
        else
            return f2;
    }
}
