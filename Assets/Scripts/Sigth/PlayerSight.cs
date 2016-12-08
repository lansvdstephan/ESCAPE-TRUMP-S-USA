using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerSight : MonoBehaviour
{
    [Range(0,360)]
    public float sightAngle = 110.0f;
    public float sightRadius = 10.0f;
    public float angle;

    public LayerMask playerMask;

    public float distance;
    public GameObject player;

    public bool playerSeen;
    public Vector3 playerPosition;
    public Vector3 playerLastSeen;
    public Vector3 otherPosition;

    public bool other;
    public int otherIndex = -1;

    //Test Zone
    public bool[] test;
    public int indexn;


	// Use this for initialization
	void Start ()
    {
        playerSeen = false;
        other = false;
        
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

            //if player is in view Radius
            if (angle < sightAngle/2 && angle > (sightAngle * -1)/2)
            {
                RaycastHit hit; //Kind of an boolean variable for raycast hitting
                if (Physics.Raycast(transform.position,AngleD.normalized, out hit, distance))//send out a Raycast in the direction of the player
                {
                    if (hit.transform.gameObject.CompareTag("Player"))//if the obstacle that is reacht by the ray is tagged by "Player", than the enemy found the player
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
        GameObject [] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float[] distances = new float[enemies.Length];
        bool[] otherSee = new bool[enemies.Length];
        for(int i =0; i < enemies.Length; i++)
        {
            distances[i] = Vector3.Distance(transform.position, enemies[i].transform.position);
            otherSee[i] = enemies[i].GetComponent<PlayerSight>().playerSeen;
        }
        test = otherSee;
        if (!other)
        {
            otherIndex = firstEnemySeesPlayer(otherSee);
            if(otherIndex != -1 && otherIndex != getOwnIndex(distances))
                other = true;
        }
        if(other)
        {
            if(otherSee[otherIndex])
            {
                otherPosition = enemies[otherIndex].transform.position;
            }
            else
            {
                if(otherPosition.Equals(transform.position))
                {
                    other = false;
                    otherIndex = 1;
                }
            }
        }

    }
    private int getOwnIndex(float [] distances)
    {
        for(int i = 0; i<distances.Length; i++)
        {
            if (distances[i] == 0f)
                return i;
        }

        return -1;
    }
    private int firstEnemySeesPlayer(bool [] otherSee)
    {
        for(int i = 0; i < otherSee.Length;i++)
        {
            if (otherSee[i])
            {
                return i;
            }
        }
        return -1;
    }
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
     * Returns the Euclidean length of a vector
     */
    public float getVectorLength(Vector3 vector)
    {
        return (Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2));
    }

    /**
     * Returns the distance between two points 
     */
    public float getDistance(Vector3 pos1,Vector3 pos2)
    {
        Vector3 difference = new Vector3((pos1.x - pos2.x) * (pos1.x - pos2.x), (pos1.y - pos2.y) * (pos1.y - pos2.y), (pos1.z - pos2.z) * (pos1.z - pos2.z));
        float sum = difference.x + difference.y + difference.z;
        float result = Mathf.Sqrt(sum);
        return result;
    }
}
