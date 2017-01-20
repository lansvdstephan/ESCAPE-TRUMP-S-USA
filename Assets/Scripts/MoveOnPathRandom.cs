using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveOnPathRandom : MonoBehaviour
{
    public PlayerSight fow;
    public UnityEngine.AI.NavMeshAgent agent;


    public float speed;
    public float rotationSpeed = 5.0f;
    private float reachDistance = 1.0f; //difference between the centre of the enemy and the point created by the EditorPath

    public Vector3 point1;
    public Vector3 point2;

    private bool pauze;
    private float time = 0.2f;
    public Vector3 random;
    public bool check;

    private Animator anim;
    private int animWalkingHash = Animator.StringToHash("GuardWalking");
    private int animRunningHash = Animator.StringToHash("GuardRunning");

    private GameObject playerHead;

    private Vector3 currPos = new Vector3();
    private Vector3 prevPos = new Vector3();

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        random = pickRandomPoint();
        agent.SetDestination(random);
        check = false;
        anim = GetComponent<Animator>();
        //pathToFolow = GameObject.Find(pathName).GetComponent<EditorPath>();
        playerHead = PhilMovement.head;
        prevPos = transform.position;
        currPos = Vector3.zero;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Debug.Log(agent.speed);
        prevPos = currPos;
        currPos = transform.position;
        if (!fow.playerSeen || playerHead.transform.childCount > 0)
        {
            agent.speed = 2f;
            if (pauze)
                PauseMovements();
            if (Mathf.Abs(transform.position.x - random.x) < 0.3f && Mathf.Abs(transform.position.z - random.z) < 0.3f || check)
            {
                check = false;
                Debug.Log(random);
                random = pickRandomPoint();
                anim.SetBool(animRunningHash, false);
                agent.SetDestination(random);
                Quaternion rotation = Quaternion.LookRotation(currPos - prevPos); // position we are going to minus the position we are looking at
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            }

        }
        else if (fow.playerSeen)
        {
            anim.SetBool(animRunningHash, true);
            followPlayer();
            if(!check)
            {
                this.gameObject.GetComponent<AudioSource>().Play();
            }
            check = true;
        }
        else if (check)
        {
            check = false;
            random = pickRandomPoint();
            agent.speed = 2.5f;
            anim.SetBool(animRunningHash, false);
            agent.SetDestination(random);
        }
        
    }

    void followPlayer()
    {
        Debug.Log(agent.speed);   
        agent.SetDestination(fow.playerLastSeen);//Move from current position to next position
        agent.speed = 4.2f;
        Quaternion rotation = Quaternion.LookRotation(fow.playerLastSeen - transform.position); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
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
        while (isObject(next))
        {
            if (point1.x < point2.x)
                randomx = Random.Range(point1.x, point2.x);
            else
                randomx = Random.Range(point2.x, point1.x);

            if (point1.z < point2.z)
                randomz = Random.Range(point1.z, point2.z);
            else
                randomz = Random.Range(point2.z, point1.z);

            next = new Vector3(randomx, 0.5f, randomz);
        }
        return next;
    }


    void PauseMovements()
    {
        time = time - Time.deltaTime;
        if (time < 0)
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
    public float Min(float f1, float f2)
    {
        if (f1 < f2)
            return f1;
        else
            return f2;
    }
}
