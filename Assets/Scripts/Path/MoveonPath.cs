using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveonPath : MonoBehaviour
{
    public EditorPath pathToFolow;
    public PlayerSight fow;
    public ShortestPath2 shortest;

    public int currentWayPointID = 0;
    public int step = 0;
    public float speed = 2.0f;
    public float maxSpeed = 3.0f;
    public float rotationSpeed = 5.0f;

    private float reachDistance = 1.0f; //difference between the centre of the enemy and the point created by the EditorPath
    public string pathName;

    public float c;
    public bool check;
    private bool test;
    public List<Vector3> route = new List<Vector3>();
    // Use this for initialization
    void Start()
    {
        c = Min(10.0f, 8f + 2.1f);
        test = fow.playerSeen;
        //pathToFolow = GameObject.Find(pathName).GetComponent<EditorPath>();
        route = shortest.routes;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!fow.playerSeen && !check)
        {
            speed = 2.0f;
            followPath();
        }
        else if (fow.playerSeen)
        {
            speed = Min(maxSpeed, speed + 0.005f);
            followPlayer();
            check = true;
        }
        else if (check)
        {
            route = shortest.routes;
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
        float distance = Vector3.Distance(route[step], transform.position);
        transform.position = Vector3.MoveTowards(transform.position, route[step], Time.deltaTime * speed); //Move from current position to next position

        Quaternion rotation = Quaternion.LookRotation(route[step] - transform.position); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        if (distance <= reachDistance)
        {
            step++;
        }

        if (step == route.Count)
        {
            step = 0;
            check = false;
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
