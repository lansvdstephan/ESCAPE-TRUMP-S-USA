using UnityEngine;
using System.Collections;

public class MoveonPath : MonoBehaviour
{
    public EditorPath pathToFolow;
    public PlayerSight fow;

    public int currentWayPointID = 0;
    public float speed = 2.0f;
    public float maxSpeed = 3.0f;
    public float rotationSpeed = 5.0f;

    private float reachDistance = 1.0f; //difference between the centre of the enemy and the point created by the EditorPath
    public string pathName;


    // Use this for initialization
    void Start()
    {
        //pathToFolow = GameObject.Find(pathName).GetComponent<EditorPath>();

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!fow.playerSeen)
        {
            speed = 2.0f;
            followPath();
        }
        else
        {
            speed = Max(maxSpeed, speed + 0.005f);
            followPlayer();
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

   void OnCollisionStay(Collider other)
    {
        StartCoroutine(pauseMovement());
    }
    public float Max(float f1, float f2)
    {
        if (f1 >= f2)
            return f1;
        else
            return f2;
    }

    IEnumerator pauseMovement()
    {
        yield return new WaitForSeconds(10);//assuming it takes 10 seconds to play the animation
     
    }
}
