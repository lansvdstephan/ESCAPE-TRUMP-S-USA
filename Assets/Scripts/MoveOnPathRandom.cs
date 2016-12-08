using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveOnPathRandom : MonoBehaviour
{
    public PlayerSight fow;
    public NavMeshAgent agent;


    public float speed = 2.0f;
    public float maxSpeed = 3.0f;
    public float rotationSpeed = 5.0f;
    private float reachDistance = 1.0f; //difference between the centre of the enemy and the point created by the EditorPath

    public Vector3 point1;
    public Vector3 point2;

    public Vector3 random;
    public bool check;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        random = pickRandomPoint();
        agent.SetDestination(random);
        check = false;
        //pathToFolow = GameObject.Find(pathName).GetComponent<EditorPath>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!fow.playerSeen && !check)
        {   
            speed = 2.0f;
            if (transform.position.x == random.x && transform.position.z == random.z)
            {
                random = pickRandomPoint();
                agent.SetDestination(random);
            }

        }
        else if (fow.playerSeen)
        {
            agent.Stop();
            speed = Min(maxSpeed, speed + 0.005f);
            followPlayer();
            check = true;
        }
        else if (check)
        {
            Debug.Log("Been here!");
            check = false;
            random = pickRandomPoint();
            agent.Resume();
            agent.SetDestination(random);
        }
        
    }

    void followPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, fow.playerLastSeen, Time.deltaTime * speed); //Move from current position to next position
        Quaternion rotation = Quaternion.LookRotation(fow.playerLastSeen - transform.position); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }


    public Vector3 pickRandomPoint()
    {
        float rangex = Mathf.Abs(point2.x - point1.x);
        float randomx = (Random.value * rangex) - point2.x;
        float rangez = Mathf.Abs(point2.z - point1.z);
        float randomz = (Random.value * rangez) - point2.z;
        Vector3 next = new Vector3(randomx, 0.5f, randomz);
        while (isObject(next))
        {
            rangex = Mathf.Abs(point2.x - point1.x);
            randomx = (Random.value * rangex) - point2.x;

            rangez = Mathf.Abs(point2.z - point1.z);
            randomz = (Random.value * rangez) - point2.z;
            next = new Vector3(randomx, 0.5f, randomz);

        }
        return next;
    }


    //Checks wheter there is an object at position p1
    //Checked
    private bool isObject(Vector3 p1)
    {
        Vector3 toCompare = p1;
        toCompare.y = toCompare.y + 10f;
        RaycastHit hit; //Kind of an boolean variable for raycast hitting
        if (Physics.Raycast(toCompare, Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.CompareTag("Ground") || hit.transform.gameObject.CompareTag("Player"))
                return false;
            else
                return true;
        }
        else
            return true;
    }
    public float Min(float f1, float f2)
    {
        if (f1 < f2)
            return f1;
        else
            return f2;
    }
}
