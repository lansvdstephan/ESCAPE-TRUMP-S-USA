using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class smartSearching : MonoBehaviour
{
    public cridEditor cE;
    public List<Transform> grid = new List<Transform>();
    public List<List<int>> visitable = new List<List<int>>();
    public List<int> times_visited = new List<int>();
    public int previous;
    public int current;
    public int next;

    //Test-Zone
    public List<float> test = new List<float>();
    public List<float> test2 = new List<float>();
    public List<int> testi = new List<int>();
    public List<int> testi2 = new List<int>();

    public float randi = 0f;
    public float ftest = 0f;
    public int itest = 0;

    //BETWEEN TWO POINTS IN THE GRID NO ENEMY OR PLAYER CAN BE PLACED!!!!!!

    // Use this for initialization
    void Start ()
    {
        grid = cE.grid_points;
        visitable = allPoints();
        times_visited[0] = 1;
        previous = 13;
        current = 0;
        next = 1;
    }

    // Update is called once per frame
    void Update ()
    {
        moveToNextPoint(next);
    }
    


    private void moveToNextPoint(int index)
    {
        transform.position = Vector3.MoveTowards(transform.position, grid[index].transform.position, Time.deltaTime * 4.0f);
        Quaternion rotation = Quaternion.LookRotation(grid[index].transform.position - transform.position); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
        if(transform.position == grid[index].transform.position)
        {
            times_visited[index] = times_visited[index] + 1;
            previous = current;
            current = next;
            next = chanceList();
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
        for(int i = 0; i < currVisitable.Count; i++)
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
            if(currVisited[i] != 0)
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
            if(i!=0)
            {
                ratio.Add(chance[i] + ratio[i - 1]);
            }
            else
            {
                ratio.Add(chance[0]);
            }
            if(ratio[i] > random)
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
        for(int i = 0; i < grid.Count; i++)
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
        for(int i = 0; i < grid.Count; i++)
        {
            if(index != i)
            {
                bool temp = vistablePoints(grid[index].transform.position, grid[i].transform.position);
                Vector3 point1 = grid[i].transform.position;
                if(temp)
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
        float toReturn = (1f/listSize);
        for(int i = 0; i < difference; i++)
        {
            toReturn = toReturn /2.0f;
        }
        return toReturn;
    }

    private float plusTerm(int amount, float total_minus, int listSize)
    {        
        return (1f / listSize) + (total_minus/amount);
    }


    private int removePrevious(int prev)
    {
        List <int> toCheck = visitable[next];
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
        for(int i = 0; i < floats.Count; i++)
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

        for(int i = 1; i < lijst.Count; i++)
        {
            if(lijst[i] < curMin)
            {
                curMin = lijst[i];
            }
        }
        return curMin;
    }
}
