using UnityEngine;
using System.Collections;

public class Random_Searching : MonoBehaviour
{                             

    //this two points will spawn the grid
    public Vector3 point1;
    public Vector3 point2;

    public Vector3 random;

    public NavMeshAgent agent;

    void Start()
    {
        Debug.Log(transform.position);
        agent = GetComponent<NavMeshAgent>();
        random = transform.position;
        agent.SetDestination(random);
    }

    void Update()
    {
        if (transform.position.x == random.x && transform.position.z == random.z)
        {
            random = pickRandomPoint();
            agent.SetDestination(random);
        }
    }
    public Vector3 pickRandomPoint()
    {
        float randomx = 0f;
        if (point1.x < point2.x)
            randomx = Random.Range(point1.x, point2.x);
        else
            randomx = Random.Range(point2.x, point1.x);
        
        float randomz = 0f;
        if (point1.z < point2.z)
            randomz = Random.Range(point1.z, point2.z);
        else
            randomz = Random.Range(point2.z, point1.z);


        Vector3 next = new Vector3(randomx, 0.5f, randomz);
        Debug.Log(randomx + ";" + randomz);
        while (isObject(next))
        {
            if (point1.x < point2.x)
                randomx = Random.Range(point1.x, point2.x);
            else
                randomx = Random.Range(point2.x, point1.x);
            
            if (point1.z < point2.z)
                randomx = Random.Range(point1.z, point2.z);
            else
                randomx = Random.Range(point2.z, point1.z);

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
}
