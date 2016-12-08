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
        random = pickRandomPoint();
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
}
