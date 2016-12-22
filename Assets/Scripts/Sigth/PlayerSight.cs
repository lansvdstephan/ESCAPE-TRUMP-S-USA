using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerSight : MonoBehaviour
{
    [Range(0, 360)]
    public float sightAngle = 110.0f;
    public float sightRadius = 10.0f;
    public float angle;

    public float hearRadius = 10.0f;

    public float distance;
    public GameObject player;

    public bool playerSeen;
    public Vector3 playerPosition;
    public Vector3 playerLastSeen;
    public Vector3 otherPosition;

    public bool sees;
    public bool hear;
    public int otherIndex = -1;
    public Vector3 toGo = Vector3.zero; 


    public GameObject[] enemies;
    public float[] distances;
    public bool[] currOther;

    // Use this for initialization
    void Start()
    {
        playerSeen = false;
        sees = false;
        hear = false;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        distances = new float[enemies.Length];
        currOther = new bool[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            distances[i] = Vector3.Distance(transform.position, enemies[i].transform.position);
            currOther[i] = enemies[i].GetComponent<PlayerSight>().playerSeen;
        }
    }

    void Update()
    {
        findPlayer();
        findEnemy();
    }
    void findPlayer()
    {
        //Get the position of an enemy
        Vector3 enemyPosition = transform.position;
        //Get the position of the player
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        //Determine the distance between the enemy and the player
        distance = getDistance(enemyPosition, playerPosition);
        //if this distance is smaller than the radius keep searching
        if (distance < sightRadius)
        {
            //Define the most left and most right view of the enemy
            Vector3 AngleA = directionFromAngle(-1 * sightAngle / 2, false); //
            Vector3 AngleB = directionFromAngle(sightAngle / 2, false);
            //The centerpoint of view of the enemy
            Vector3 AngleC = directionFromAngle(0, false);

            Vector3 radiusPointA = transform.position + AngleA * sightRadius; // most left point on the radius
            Vector3 radiusPointB = transform.position + AngleB * sightRadius; // most right point on the radius

            Vector3 playerRadiusA = transform.position + AngleA * getDistance(transform.position, playerPosition); //most left point on the player radius
            Vector3 playerRadiusB = transform.position + AngleB * getDistance(transform.position, playerPosition); //most right point on the player radius
            Vector3 playerRadiusC = transform.position + AngleC * getDistance(transform.position, playerPosition); // center point on the player radius

            //The angle between the player and the center point on the player radius
            angle = getAngle(playerRadiusC, playerPosition);
            Vector3 AngleD = directionFromAngle(angle, false);
            Vector3 playerRadiusD = transform.position + AngleD * getDistance(transform.position, playerPosition); //player point on the playerradius
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1.1f, transform.position.z), distance*AngleD, Color.red,2f);
            //if player is in view Radius
            if (angle < sightAngle / 2 && angle > (sightAngle * -1) / 2)
            {
                RaycastHit hit; //Kind of an boolean variable for raycast hitting
                
                if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1.8f, transform.position.z), AngleD.normalized, out hit, distance))//send out a Raycast in the direction of the player
                {
                    if (hit.transform.gameObject.CompareTag("Player"))//if the obstacle that is reached by the ray is tagged by "Player", than the enemy found the player
                    {
                        playerSeen = true;
                        playerLastSeen = playerPosition;
                    }
                    else
                        playerSeen = false;
                }
            }
            else { playerSeen = false; }
        }
        else
        {
            playerSeen = false;
        }
    }
    
    void findEnemy()
    {
        //Updates the distances and the enemies that sees the player
        for (int i = 0; i < enemies.Length; i++)
        {
            distances[i] = Vector3.Distance(transform.position, enemies[i].transform.position);
            currOther[i] = enemies[i].GetComponent<PlayerSight>().playerSeen;
        }

        //If an enemy still hasn't seen another enemy which saw to player --> Keep seaching(hearing/seeing)
        if(!sees )
        {
            int index = firstEnemySeesPlayer();
            if(index != -1)
            {
                sees = true;
                otherIndex = index;
                toGo = enemies[otherIndex].transform.position + ( -2 *  transform.forward);
            }
        }
        //When another enemy which is seeing the player is found --> Update the position ToGO until player escaped
        if(sees)
        {
            if(otherIndex != -1 && currOther[otherIndex])
            {
                toGo = enemies[otherIndex].transform.position + (-2 * transform.forward);
            }
            else
            {
                sees = false;
                otherIndex = -1;
            }
        }
    }

    /*
     * Returns its own enemy index
     */ 
    private int getOwnIndex()
    {
        for (int i = 0; i < distances.Length; i++)
        {
            if (distances[i] == 0f)
                return i;
        }

        return -1;
    }

    /*
     * Returns the index of the first enemie that sees the player, where the enemy is not far away
     */ 
    private int firstEnemySeesPlayer()
    {
        for (int i = 0; i < currOther.Length; i++)
        {
            if(currOther[i] && i != getOwnIndex())
            {
                //Debug.Log((distances[i] <= sightRadius) + ";" + EnemyRaycast(enemies[getOwnIndex()].transform.position, enemies[i].transform.position, sightRadius));
                //Debug.Log((distances[i] <= sightRadius) + ";" + hear);
                if (distances[i]<= sightRadius && EnemyRaycast(enemies[getOwnIndex()].transform.position, enemies[i].transform.position, sightRadius))
                {
                    sees = true;
                    return i;
                }
     
                else if (distances[i] <= hearRadius && hear == false)
                {
                    hear = true;
                    toGo = enemies[i].transform.position + (-1 * transform.forward);
                    return -1;
                }
            }
        }
        return -1;
    }

    /**
     * Returns the direction of an angle
     */
    public Vector3 directionFromAngle(float angleInDegrees, bool isGlobal)
    {
        if (!isGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    /**
     * Returns the angle between two point (pos1 & pos2) relative to the centerpoint of the enemy
     */
    public float getAngle(Vector3 pos1, Vector3 pos2)
    {
        Vector3 vector1 = (pos1 - transform.position); //centerpoint on the playerRadius
        Vector3 vector2 = (pos2 - transform.position); //playerpoint on the playerRadius
        float angle = Vector3.Angle(vector1, vector2); //Calculates the angle between these two vectors
        Vector3 cross = Vector3.Cross(vector1, vector2);//Calculate the cross-product of these two vectors
        if (cross.y < 0)                                //If the second component is negative, the angle should be negative
        {
            angle = -angle;
        }
        return angle;
    }

    /**
     * Looks wheter two objects can see eachother
     * Checked
     */ 
    public bool EnemyRaycast(Vector3 pos1, Vector3 pos2, float dist)
    {
        Vector3 newDirection = pos2 - pos1;
        RaycastHit hit; //Kind of an boolean variable for raycast hitting
        Debug.DrawRay(pos1, newDirection,Color.blue,2f);
        if (Physics.Raycast(pos1, newDirection.normalized, out hit, dist))//send out a Raycast in the direction of the player
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))//if the obstacle that is reacht by the ray is tagged by "Player", than the enemy found the player
            {
                return true;
            }
            return false;
        }
                return false;
    }
    /**
     * Returns the Euclidean length of a vector
     */
    public float getVectorLength(Vector3 vector)
    {
        return (Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2));
    }

    /**
     * Returns the distance between two points 
     */
    public float getDistance(Vector3 pos1, Vector3 pos2)
    {
        Vector3 difference = new Vector3((pos1.x - pos2.x) * (pos1.x - pos2.x), (pos1.y - pos2.y) * (pos1.y - pos2.y), (pos1.z - pos2.z) * (pos1.z - pos2.z));
        float sum = difference.x + difference.y + difference.z;
        float result = Mathf.Sqrt(sum);
        return result;
    }
}
