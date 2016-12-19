using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class smartSearching : MonoBehaviour
{
    public PlayerSight fow;
    public cridEditor cE;
    public smartMoveOnPath smop;
    public List<Transform> grid = new List<Transform>();
    public List<List<int>> visitable = new List<List<int>>();
    public List<int> times_visited = new List<int>();
    public int previous;
    public int current;
    public int next;
    public Vector3 pointToGO = new Vector3();
    public bool pause;
    public bool firstPoint;
    private float timeLeft = 3f;
    public Vector3 furtherToGO = new Vector3();
    private int max = 0;

    //Test-Zone
    public List<float> test = new List<float>();
    public List<float> test2 = new List<float>();
    public List<int> testi = new List<int>();
    public List<int> testi2 = new List<int>();

    public List<float> distancesToLastSeen2 = new List<float>();

    public float randi = 0f;
    public float ftest = 0f;
    public int itest = 0;

    //BETWEEN TWO POINTS IN THE GRID NO ENEMY OR PLAYER CAN BE PLACED!!!!!!

    // Use this for initialization
    void Start()
    {
        grid = cE.grid_points;
        visitable = allPoints();
        times_visited[0] = 1;
        previous = 13;
        current = 0;
        next = 1;
        firstPoint = false;
        pause = false;
        getRandomAngle(20f, 170f);
    }

    // Update is called once per frame
    public void Update()
    {
        //furtherSearching();
        /*
        float range = 20;
        float angle = 0;
        Vector2 randomRange = getRandomAngle(range, angle);
        Vector3 dirA = fow.directionFromAngle(-1 * range, false);
        Vector3 dirB = fow.directionFromAngle(range, false);

        Vector3 pointA = transform.position + dirA * 5;
        Vector3 pointB = transform.position + dirB * 5;
        Debug.DrawLine(transform.position, pointA, Color.cyan);
        Debug.DrawLine(transform.position, pointB, Color.cyan);
        Debug.Log(randomRange.x + ";" + randomRange.y);
        */
       // randomPoint();
    }



    public void moveToNextPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, grid[next].transform.position, Time.deltaTime * 2.0f);
        Quaternion rotation = Quaternion.LookRotation(grid[next].transform.position - transform.position); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
        //Debug.Log(transform.position == grid[next].transform.position);
        float eps = 0.1f;
        if (Mathf.Abs(transform.position.x - grid[next].transform.position.x) < eps && Mathf.Abs(transform.position.z - grid[next].transform.position.z) < eps)
        {
            times_visited[next] = times_visited[next] + 1;
            previous = current;
            current = next;
            next = chanceList();
            pointToGO = grid[next].transform.position;
        }

    }

    private int chanceList()
    {
        List<int> currVisitable = visitable[next];
        if (currVisitable.Count != 1)
        {
            currVisitable.Remove(previous);
        }
        List<float> chance = new List<float>();
        List<float> ratio = new List<float>();
        List<int> currVisited = new List<int>();
        for (int i = 0; i < currVisitable.Count; i++)
        {
            int temp = times_visited[currVisitable[i]];
            currVisited.Add(temp);
        }
        int minIndex = Min(currVisited);
        itest = minIndex;

        float overallMinus = 0f;
        int amountMin = 0;

        for (int i = 0; i < currVisited.Count; i++)
        {
            currVisited[i] = currVisited[i] - minIndex;
            if (currVisited[i] != 0)
            {
                float tempi = minusTerm(currVisited[i], currVisited.Count);
                chance.Add(tempi);
                overallMinus = overallMinus + ((1f / currVisited.Count) - tempi);
            }
            else
            {
                amountMin = amountMin + 1;
                chance.Add(0f);
            }
        }

        float random = Random.Range(0f, 1f);
        randi = random;
        int indexNextPoint = 0;
        for (int i = 0; i < currVisited.Count; i++)
        {
            if (chance[i] == 0f)
            {
                chance[i] = plusTerm(amountMin, overallMinus, currVisited.Count);
            }
            if (i != 0)
            {
                ratio.Add(chance[i] + ratio[i - 1]);
            }
            else
            {
                ratio.Add(chance[0]);
            }
            if (ratio[i] > random)
            {
                indexNextPoint = currVisitable[i];
                break;
            }
        }
        //itest = minIndex;
        testi = currVisited;
        testi2 = currVisitable;


        test = chance;
        test2 = ratio;
        return indexNextPoint;
    }

    private List<List<int>> allPoints()
    {
        List<List<int>> toReturn = new List<List<int>>();
        for (int i = 0; i < grid.Count; i++)
        {
            List<int> temp = onePoint(i);
            times_visited.Add(0);
            toReturn.Add(temp);
        }
        return toReturn;
    }

    /*
     * Checks for one point which other points in the grid are visitable
     * CHECKED
     */
    private List<int> onePoint(int index)
    {
        List<int> toReturn = new List<int>();
        for (int i = 0; i < grid.Count; i++)
        {
            if (index != i)
            {
                bool temp = vistablePoints(grid[index].transform.position, grid[i].transform.position);
                Vector3 point1 = grid[i].transform.position;
                if (temp)
                {
                    toReturn.Add(i);
                }
            }
        }
        return toReturn;
    }
    /*
     * Checks wheter pos2 is directly visitable by the enemy at pos1
     * CHECKED
     */
    private bool vistablePoints(Vector3 pos1, Vector3 pos2)
    {
        Vector3 newDirection = pos2 - pos1;
        float dist = Vector3.Distance(pos1, pos2);
        RaycastHit hit; //Kind of an boolean variable for raycast hitting
        if (!Physics.Raycast(pos1, newDirection.normalized, out hit, dist))//send out a Raycast in the direction of the player
        {
            return true;
        }
        return false;
    }

    private float minusTerm(int difference, int listSize)
    {
        float toReturn = (1f / listSize);
        for (int i = 0; i < difference; i++)
        {
            toReturn = toReturn / 2.0f;
        }
        return toReturn;
    }

    private float plusTerm(int amount, float total_minus, int listSize)
    {
        return (1f / listSize) + (total_minus / amount);
    }


    private int removePrevious(int prev)
    {
        List<int> toCheck = visitable[next];
        for (int i = 0; i < toCheck.Count; i++)
        {
            if (toCheck[i] == prev)
                return i;
        }
        return -1;
    }
    /*
     * Returns the sum of a list with float values
     */
    private float sum(List<float> floats)
    {
        float sum = 0f;
        for (int i = 0; i < floats.Count; i++)
        {
            sum = sum + floats[i];
        }
        return sum;
    }
    /*
     * Returns the minimum of a list of integers
     * CHECKED
     */
    private int Min(List<int> lijst)
    {
        int curMin = lijst[0];

        for (int i = 1; i < lijst.Count; i++)
        {
            if (lijst[i] < curMin)
            {
                curMin = lijst[i];
            }
        }
        return curMin;
    }

    /*
     * Returns the minimum of a list of integers and the index of the minimum  
     * CHECKED
     */
    private Vector2 Minf(List<float> lijst)
    {
        Vector2 toReturn = new Vector2();
        float curMin = lijst[0];
        int indexje = 0;
        for (int i = 1; i < lijst.Count; i++)
        {
            if (lijst[i] < curMin)
            {
                curMin = lijst[i];
                indexje = i;
            }
        }
        toReturn.x = curMin;
        toReturn.y = indexje;
        return toReturn;
    }

    ///////////////////////////////////////////////////////////
    //// Part 2  - Further searching after player escaped  ////
    ///////////////////////////////////////////////////////////

        /*
    public void furtherSearching()
    {
        fow.playerLastSeen = Vector3.zero;
        pointDistances();
        Vector2 mini = Minf(distancesToLastSeen2);
        float minDistance = mini.x;
        int minIndex = (int)mini.y;
        next = minIndex;
        if (!firstPoint && !pause)
            moveToNextPoint2();
        else if(firstPoint && pause)
          pauseMovement();
        //else if(firstPoint &&!pause)

    }
    */

    public int getClosestPoint()
    {
        pointDistances();
        Vector2 mini = Minf(distancesToLastSeen2);
        float minDistance = mini.x;
        int minIndex = (int)mini.y;
        next = minIndex;
        return next;
    }
    private void pointDistances()
    {
        List<float> distancesToLastSeen = new List<float>();

        for (int i = 0; i < grid.Count; i++)
        {
            float distan = Vector3.Distance(fow.playerLastSeen, grid[i].transform.position);
            distancesToLastSeen.Add(distan);
        }
        distancesToLastSeen2 = distancesToLastSeen;
    }
    /*
    public void moveToNextPoint2()
    {
        transform.position = Vector3.MoveTowards(transform.position, grid[next].transform.position, Time.deltaTime * 2.0f);
        Quaternion rotation = Quaternion.LookRotation(grid[next].transform.position - transform.position); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
        if (Mathf.Abs(transform.position.x - grid[next].transform.position.x) < 0.1f && Mathf.Abs(transform.position.z - grid[next].transform.position.z) < 0.1f)
        {
            smop.firstpoint = true;
            smop.pause = true;
            times_visited[next] = times_visited[next] + 1;
        }
    }
    */
    public void pauseMovement()
    {

        smop.timeLeft2 = smop.timeLeft2 - Time.deltaTime;
        if (smop.timeLeft2 > 2f)
            transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
        else if (smop.timeLeft2 > 0)
            transform.Rotate(new Vector3(0, -90, 0) * Time.deltaTime);
        else
        {
            smop.pause = false;
            smop.timeLeft2 = 3f;
        }
    }

    public Vector3 randomPoint()
    {
        Vector3 vectorA = transform.forward;
        Vector3 vectorB = fow.playerLastSeen - transform.position;
        float angle = Vector3.Angle(vectorA, vectorB);
        float range = 20f;
        Vector3 tempCross = Vector3.Cross(vectorA, vectorB);
        if (tempCross.y > 0)
            angle = -1 * angle;
        //Debug.Log(angle);
        Vector2 randomRange = getRandomAngle(range, angle);
        float randomAngle = Random.Range(randomRange.x, randomRange.y);
        float randomDistance = Random.Range(3f,5f);

       // Debug.Log(randomRange.x + ";" + randomRange.y);
        Debug.DrawLine(transform.position, transform.position + transform.forward * 10f, Color.blue);
        Debug.DrawLine(transform.position, fow.playerLastSeen, Color.blue);
        
        Vector3 randomAngles = fow.directionFromAngle(randomAngle, false);
        Vector3 dirA = fow.directionFromAngle(-1*randomRange.x, false);
        Vector3 dirB = fow.directionFromAngle(-1*randomRange.y, false);

        Vector3 pointA = transform.position + dirA *5;
        Vector3 pointB = transform.position + dirB *5;
        Debug.DrawLine(transform.position, pointA, Color.cyan);
        Debug.DrawLine(transform.position, pointB, Color.cyan);

        Vector3 dirToGo = fow.directionFromAngle(-1 * randomAngle, false);
        furtherToGO = transform.position + dirToGo *randomDistance;
        if(isObject(furtherToGO))
        {
            max = max + 1;
            if (max < 100)
                randomPoint();
            else
            {
               
                smop.check = false;
                smop.firstpoint = false;
                smop.firstTime = false;
                smop.pause = false;
                smop.agent.Stop();
            }

        }
        max = 0;
        Debug.DrawLine(transform.position, furtherToGO, Color.black);
        return furtherToGO;
    }

    
    private Vector2 getRandomAngle(float range, float angle)
    {
        Vector2 toReturn = new Vector2();
        if(angle > 180f - range)
        {
            float dif1 = range - (180f - angle);
            float kansje = Random.Range(0f, 1f);
            if(kansje > (dif1)/(2*range))
            {
                toReturn.x = angle - range;
                toReturn.y = 180f;
            }
            else
            {
                toReturn.x = -180f;
                toReturn.y = -180f + dif1;
            }
        }
        else if(angle < -180f  +  range)
        {
            float dif1 = range - (angle + 180f);
            float kansje = Random.Range(0f, 1f);
            if(kansje > (dif1)/(2*range))
            {
                toReturn.x = -180f;
                toReturn.y = angle + range;
            }
            else
            {
                toReturn.x = 180f - dif1;
                toReturn.y = 180f;
            }
        }
        else
        {
            toReturn.x = angle - range;
            toReturn.y = angle + range;
        }
        return toReturn;
    }

    //Checks wheter there is an object at position p1
    //Checked
    private bool isObject(Vector3 p1)
    {
        float extraRange = 0.6f;
        float heigth = 10f;
        int count = 0;
        Vector3 toCompare = p1;
        Vector3 above = p1;
        above.x = above.x + extraRange;
        Vector3 left = p1;
        left.x = left.z - extraRange;
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
            switch(i)
            {
                case 0: temp = toCompare;
                    break;
                case 1: temp = above;
                    break;
                case 2: temp = left;
                    break;
                case 3: temp = right;
                    break;
                case 4: temp = down;
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