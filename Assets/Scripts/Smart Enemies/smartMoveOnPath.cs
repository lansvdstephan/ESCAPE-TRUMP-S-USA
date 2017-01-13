using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class smartMoveOnPath : MonoBehaviour
{
    public PlayerSight fow;
    public NavMeshAgent agent;
    public smartSearching ss;
    public PhilMovement player;
    public int step = 0;
    public float speed = 2.0f;
    public float maxSpeed = 3.0f;
    public float rotationSpeed = 5.0f;
    public float timeLeft2 = 3f;
    public bool damageTaken = false;

    private float reachDistance = 1.0f; //difference between the centre of the enemy and the point created by the EditorPath
    public bool check;
    public bool hitPlayer;
    public bool firstpoint;
    public bool pause;
    public bool firstTime;
    public Vector3 toGo;

    private int count = 0;
    private float timeLeft = 1f;
    private Vector3 curPoint;
    private Vector3 prevPoint;

    //SHOOTING
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate = 0.2f;
    public float fireCountDown = 0f;
    private bool richten = false;


    // Use this for initialization
    void Start()
    {
        firstpoint = false;
        pause = false;
        firstTime = false;
        agent = GetComponent<NavMeshAgent>();

        curPoint = transform.position;
        prevPoint = Vector3.zero;
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
            check = true;
            speed = Min(maxSpeed, speed + 0.005f);
            followPlayer();
            firstTime = false;
            firstpoint = false;
            pause = false;
            count = 0;
     
            if (fireCountDown > 0.11f && fireCountDown < 0.15f && !richten)
            {
                richten = true;
                fireCountDown = 0.1f;
            }
            else if (richten)
            {
                pauseMovement();
            }
            else if (fireCountDown <= 0)
            {
                Shoot();
                fireCountDown = 1 / fireRate;
                fireCountDown = fireCountDown - Time.deltaTime;
            }
            else
            {
                fireCountDown = fireCountDown - Time.deltaTime;
            }


        }

        else if (fow.sees || fow.hear)
        {
            walkToOther();
            firstTime = false;
            firstpoint = false;
            pause = false;
        }

        else if (!check)
        {
            speed = 2.0f;
            searchSmart();
            firstTime = false;
            firstpoint = false;
            pause = false;
        }
        else if (check)
        {
            if (!firstpoint && !pause)
            {
                nextPoint();
            }
            else if (pause)
            {
                firstTime = false;
                ss.pauseMovement();
            }
            else if (firstpoint && !pause)
            {
                moveToRandom();
            }
           
        }
        else
        {
            firstTime = false;
            firstpoint = false;
            pause = false;
        }
        TakeDamage();   
    }

    void pauseMovement()
    {
        timeLeft -= Time.deltaTime;
        agent.Stop();
        if (timeLeft < 0)
        {
            agent.Resume();
            damageTaken = false;
            hitPlayer = false;
            timeLeft = 1f;
            richten = false;
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
        agent.SetDestination(ss.pointToGO);
        Quaternion rotation = Quaternion.LookRotation(curPoint - prevPoint); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        float eps = 0.5f;
        if (Mathf.Abs(ss.pointToGO.x - transform.position.x) < eps && Mathf.Abs(ss.pointToGO.z - transform.position.z) < eps)
        {
            check = false;
        }
    }

    void walkToOther()
    {
        agent.Resume();
        agent.SetDestination(fow.toGo);
        Quaternion rotation = Quaternion.LookRotation(curPoint - prevPoint); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        //Debug.Log(transform.position == (fow.toGo));
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
        check = false;
    }

    void nextPoint()
    {
        agent.Resume();
        int nextpoint = ss.getClosestPoint();
        agent.SetDestination(ss.grid[nextpoint].transform.position);
        Quaternion rotation = Quaternion.LookRotation(ss.grid[nextpoint].transform.position - transform.position); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
        float eps = 0.001f;
        if (transform.position.x - ss.grid[nextpoint].transform.position.x <eps && transform.position.z - ss.grid[nextpoint].transform.position.z < eps)
        {
            count = count + 1;
            firstpoint = true;
            pause = true;
            ss.times_visited[nextpoint] = ss.times_visited[nextpoint] + 1;
            agent.Stop();
            if (count == 2)
            {
                count = 0;
                check = false;
                pause = false;
                firstpoint = false;
            }
        }
    }

    void moveToRandom()
    {
        agent.Resume();
        if (!firstTime)
        {
            toGo = ss.randomPoint();
            firstTime = true;
        }
        agent.SetDestination(toGo);
        Quaternion rotation = Quaternion.LookRotation(toGo - transform.position); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        float eps = 0.1f;
        if (Mathf.Abs(toGo.x - transform.position.x) < eps && Mathf.Abs(toGo.z - transform.position.z) < eps)
        {
            firstpoint = false;
            pause = true;
            agent.Stop();
            
        }
    }

    private void TakeDamage()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);

        if(!damageTaken && dist < 1.25f)
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
    private int Max(int f1, int f2)
    {
        if (f1 > f2)
            return f1;
        else
            return f2;
    }

    private void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bullet, firePoint.position, firePoint.rotation);
        Bullets bullets = bulletGO.GetComponent<Bullets>();
        
        if (bullet != null)
            bullets.Seek(player.transform.position);
    }
}

