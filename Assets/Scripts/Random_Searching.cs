using UnityEngine;
using System.Collections;

public class Random_Searching : MonoBehaviour
{                             

    //this two points will spawn the grid
    public Vector3 point1;
    public Vector3 point2;
    private bool pauze;
    public Vector3 random;
    private float time = 0.2f;
    public NavMeshAgent agent;

    void Start()
    {
        pauze = false;
        Debug.Log(transform.position);
        agent = GetComponent<NavMeshAgent>();
        random = transform.position;
        agent.SetDestination(random);
    }

    void Update()
    {

        if (pauze)
            PauseMovements();
        if (Mathf.Abs(transform.position.x - random.x) < 0.3f && Mathf.Abs(transform.position.z - random.z) < 0.3f)
        {
            pauze = true;
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

    void PauseMovements()
    {
        time = time - Time.deltaTime;
        if(time <0)
        {
            pauze = false;
            time = 0.2f;
        }
    }
    private bool isObject(Vector3 p1)
    {
        float extraRange = 0.8f;
        float heigth = 10f;
        int count = 0;
        Vector3 toCompare = p1;
        Vector3 above = p1;
        above.x = above.x + extraRange;
        Vector3 left = p1;
        left.z = left.z - extraRange;
        Vector3 right = p1;
        right.z = right.z + extraRange;
        Vector3 down = p1;
        down.x = down.x - extraRange;

        toCompare.y = toCompare.y + heigth;
        above.y = above.y + heigth;
        left.y = left.y + heigth;
        right.y = right.y + heigth;
        down.y = down.y + heigth;

        for (int i = 0; i < 5; i++)
        {
            Vector3 temp = new Vector3();
            switch (i)
            {
                case 0:
                    temp = toCompare;
                    break;
                case 1:
                    temp = above;
                    break;
                case 2:
                    temp = left;
                    break;
                case 3:
                    temp = right;
                    break;
                case 4:
                    temp = down;
                    break;
            }
            RaycastHit hit; //Kind of an boolean variable for raycast hitting
            if (Physics.Raycast(temp, Vector3.down, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.CompareTag("Ground") || hit.transform.gameObject.CompareTag("Player") || hit.transform.gameObject.CompareTag("Enemy"))
                    count = count + 1;
                else
                    return true;
            }
        }
        return false;
    }
}
