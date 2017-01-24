using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveonPath : MonoBehaviour
{
    public EditorPath pathToFolow;
    public PlayerSight fow;
    public UnityEngine.AI.NavMeshAgent agent;
    public PhilMovement player;
    public bool damageTaken = false;

    public int currentWayPointID = 0;
    public int step = 0;
    public float speed = 2.0f;
    public float maxSpeed = 3.0f;
    public float rotationSpeed = 5.0f;
    private Animator anim;
    private int animWalkingHash = Animator.StringToHash("GuardWalking");
    private int animRunningHash = Animator.StringToHash("GuardRunning");

    private float reachDistance = 1.0f; //difference between the centre of the enemy and the point created by the EditorPath
    public bool check;
    public bool hitPlayer;
    private float timeLeft = 1f;
    private Vector3 curPoint;
    private Vector3 prevPoint;

    private float timer = 2f;
    private Vector3 point1 = new Vector3();
    private Vector3 point2 = new Vector3();
    private Vector3 point3 = new Vector3();

    // Use this for initialization
    void Start()
    {
        curPoint = transform.position;
        prevPoint = Vector3.zero;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //pathToFolow = GameObject.Find(pathName).GetComponent<EditorPath>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        prevPoint = curPoint;
        curPoint = transform.position;
        stucky();
        if(hitPlayer)
        {
            pauseMovement();
            agent.Stop();
        }

        else if (fow.playerSeen)
        {
            followPlayer();
            if (!check)
                this.gameObject.GetComponent<AudioSource>().Play();
            check = true;
        }
        
        else if (fow.sees || fow.hear)
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
        TakeDamage();
    }

    void pauseMovement()
    {
        anim.SetBool(animRunningHash, false);
        anim.SetBool(animWalkingHash, false);
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            damageTaken = false;
            hitPlayer = false;
            timeLeft = 1f;
            anim.SetBool(animWalkingHash, true);
        }
    }

    void followPath()
    {
        anim.SetBool(animRunningHash, false);
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
        anim.SetBool(animRunningHash, true);
        agent.Resume();
        agent.speed = Min(agent.speed + 0.05f, 4.2f);
        agent.SetDestination(fow.playerLastSeen);
        Quaternion rotation = Quaternion.LookRotation(fow.playerLastSeen - transform.position); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    void walkShortestRoute()
    {
        
        agent.Resume();
        anim.SetBool(animRunningHash, false);
        agent.speed = Maxf(agent.speed - 0.2f, 3.0f);
        agent.SetDestination(pathToFolow.path_objects[currentWayPointID].position);
        Quaternion rotation = Quaternion.LookRotation(curPoint - prevPoint); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        float eps = 1f;
        if (Mathf.Abs(pathToFolow.path_objects[currentWayPointID].position.x - transform.position.x) < eps && Mathf.Abs(pathToFolow.path_objects[currentWayPointID].position.z - transform.position.z) < eps)
        {
            agent.Stop();
            check = false;
        }
    }

    void walkToOther()
    {
        anim.SetBool(animRunningHash, true);
        agent.Resume();
        agent.speed = Min(agent.speed + 0.05f, 3f);
        agent.SetDestination(fow.toGo);
        Quaternion rotation = Quaternion.LookRotation(curPoint - prevPoint); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        float eps = 0.5f;
        if (Mathf.Abs(fow.toGo.x - transform.position.x) < eps && Mathf.Abs(fow.toGo.z - transform.position.z) < eps)
        {
            agent.Stop();
            fow.hear = false;
            fow.sees = false;
            check = true;
        }
        

    }

    private void TakeDamage()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (!damageTaken && dist < 1.25f)
        {
            damageTaken = true;
            hitPlayer = true;
            player.health = Max(player.health - 20,0);
            player.damageImage.color = player.flashColor;
        }
    }

    public float Min(float f1, float f2)
    {
        if (f1 < f2)
            return f1;
        else
            return f2;
    }
    public int Max(int f1, int f2)
    {
        if (f1 < f2)
            return f2;
        else
            return f1;
    }
    public float Maxf(float f1, float f2)
    {
        if (f1 < f2)
            return f2;
        else
            return f1;
    }

    private void stucky()
    {
        if (timer > 1.90f)
        {
            point1 = transform.position;
        }
        else if (timer > 1f)
        {
            point2 = transform.position;
        }
        else if (timer< 0.1f)
        {
            float eps = 0.2f;
            point3 = transform.position;
            Debug.Log("p1:"  + point1 + "p2:" + point2+ "p3:" + point3 );
            if ((Mathf.Abs(point1.x - point2.x) < eps) && (Mathf.Abs(point1.x - point3.x) < eps) && (Mathf.Abs(point1.z - point2.z) < eps) && (Mathf.Abs(point1.z - point3.z) < eps))
            {
                fow.hear = false;
                fow.sees = false;
                check = true;
                Debug.Log("Been here");
            }
            timer = 2f;
        }
        Debug.Log(timer);
        timer = timer - Time.deltaTime;
    }
}
