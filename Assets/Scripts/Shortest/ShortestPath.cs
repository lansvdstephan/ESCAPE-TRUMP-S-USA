using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShortestPath : MonoBehaviour
{
    public float steps = 400;
    public float range = 20;
    private float costRecht = 10f;
    private float costSchuin = 14f;

    public Vector3 point1 = new Vector3(-20f,0.5f,-20f);
    public Vector3 point2 = new Vector3(20f, 0.5f, 20f);
    public List<List<Vector3>> grid = new List<List<Vector3>>();
    public List<List<bool>> map = new List<List<bool>>();

    //Player Information
    public GameObject player;
    public Vector3 playerPosition;
    public Vector2 playerPos;
    public Vector3 playerGoal = new Vector3(5f,0.5f,0f);
    public Vector2 goal;

    private List <Vector2> closedSet = new List<Vector2>(); //Set of points in the grid that are closed by the A* algorithm
    private List<List<Vector4>> distanceInfo = new List<List<Vector4>>();

    private bool finished = false;
    public List<Vector2> route = new List<Vector2>();
    public float distance = 0f;
    public Vector2 closed = new Vector2();
    public Vector4 infos = new Vector4();
    public int count = 0;
    public bool test;
    public bool test2;
    public Vector2 index;
    // Use this for initialization
    void Start ()
    {
        //stepsize is equal to 0.1 & we need the playerposition at one of the point in the grid, so we can round of its coordinate to one decimal
        playerPosition = player.transform.position;
        playerPosition = roundOfPositions(playerPosition, 1);
        playerPos = getIndices(playerPosition);
        playerGoal = roundOfPositions(playerGoal, 1);
        goal = getIndices(playerGoal);
        // point1 = new Vector3(transform.position.x - range, transform.position.y, transform.position.z - range);
        //point2 = new Vector3(transform.position.x + range, transform.position.y, transform.position.z + range);
        createGrid();
        initializeInfoMatrix();
        test2 = isObject(Vector3.zero);
        Vector3 testje = roundOfPositions(Vector3.zero, 1);
        Vector2 toTest = getIndices(testje);
        index = toTest;
        test = map[150][150];
        closedSet.Add(playerPos);
        Vector4 start = new Vector4(getShortestDistance(playerPos), playerPos.x, playerPos.y, 0); //Starting note
        distanceInfo[(int)playerPos.x][(int)playerPos.y] = start; // checked
        surroundingPointsUpdate(playerPos, start);
        Vector2 next = findNextclosedPoint();
        closedSet.Add(next);
        Vector4 info = distanceInfo[(int)next.x][(int)next.y];
        infos = info;

        for(int i = 0; i < 80; i++)
        {
            surroundingPointsUpdate(next, info);
            next = findNextclosedPoint();
            closed = next;
            closedSet.Add(next);
            info = distanceInfo[(int)next.x][(int)next.y];
            Debug.Log(info);
            Debug.Log(next);
            infos = info;
            count++;
        }
        
        //AStarAlgorith();
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    void createGrid()
    {
        float stepsizex = Mathf.Abs(point2.x - point1.x) / steps;
        float stepsizez = Mathf.Abs(point2.z - point1.z) / steps;
        Vector3 copy = point1;
        for (int i = 0; i < steps; i++)
        {
            List<Vector3> temp = new List<Vector3>();
            List<bool> obstacle = new List<bool>(); 
            Vector3 tempvec = copy;
            for (int j = 0; j < steps; j++)
            {
                temp.Add(tempvec);
                obstacle.Add(isObject(tempvec));
                tempvec.x = tempvec.x + stepsizex;
            }           
            grid.Add(temp);
            map.Add(obstacle);
            copy.z = copy.z + stepsizez;
        }
    }

    private void initializeInfoMatrix()
    {
        for(int i = 0; i < steps; i++)
        {
            int miljoen = 1000000;
            List<Vector4> temp = new List<Vector4>();
            Vector4 inf = new Vector4(miljoen, miljoen, miljoen, miljoen);
            for (int j = 0; j < steps; j++)
            {
                temp.Add(inf);
            }
            distanceInfo.Add(temp);
        }
    }

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

    private void AStarAlgorith()
    {
        closedSet.Add(playerPos);
        closed = closedSet[0];
        Vector4 start = new Vector4(getShortestDistance(playerPos), playerPos.x, playerPos.y , 0); //Starting note
        distanceInfo[(int)playerPos.x][(int)playerPos.y] = start; // checked
        surroundingPointsUpdate(playerPos, start);
        Vector2 next = findNextclosedPoint();
        closedSet.Add(next);
        closed = closedSet[1];
        Vector4 info = distanceInfo[(int)next.x][(int)next.y];
        while (info.x != 0)
        {
            surroundingPointsUpdate(next, info);
            next = findNextclosedPoint();
            closedSet.Add(next);
            info = distanceInfo[(int)next.x][(int)next.y];
        }
        //List<Vector2> toReturn = getRoute();
        //return toReturn;
    }

    private void surroundingPointsUpdate(Vector2 coordinate, Vector4 info)
    {
        int x = (int)coordinate.x;
        int y = (int)coordinate.y;
        int x_min = x - 1;
        int x_max = x + 2;
        int y_min = y - 1;
        int y_max = y + 2;

       if (x == 0)
        {
            x_min = 0;
        }
       else if(x == steps - 1 )
        {
            x_max = x + 1;
        }
       else if(y == 0)
        {
            y_min = 0;
        }
       else if(y == steps - 1)
        {
            y_max = y + 1; 
        }

        for (int i = y_min; i < y_max; i++)
        {
            for (int j = x_min; j < x_max; j++)
            {
                if (((x != j || y != i) && !map[j][i]) && !closedSet.Contains(new Vector2(j,i)))
                {
                    Vector4 temp = distanceInfo[j][i];
                    Vector4 replace = new Vector4();
                    if (x != j && y != i)
                    {
                        replace.w = Min((int) temp.w, (int)(info.w + costSchuin));
                    }
                    else
                    {
                        replace.w = Min((int)temp.w, (int)(info.w + costRecht));
                    }
                    if (replace.w != temp.w)
                    {
                        replace.y = coordinate.x;
                        replace.z = coordinate.y;
                    }
                    replace.x = getShortestDistance(new Vector2(j, i));
                    distanceInfo[j][i] = replace;
                }
            }
        }
    }

    private float getShortestDistance(Vector2 coordinate)
    {
        int crossing = 0;
        int straight = 0;
        Vector2 temp = new Vector2(Mathf.Abs(coordinate.x - goal.x), Mathf.Abs(coordinate.y - goal.y));
        crossing = Min((int)temp.x,(int) temp.y);
        straight = Max((int)temp.x, (int)temp.y) - crossing;
        return crossing * costSchuin + straight * costRecht;
    }

    private Vector2 findNextclosedPoint()
    {
        float min = distanceInfo[0][0].x;
        Vector2 minDistance = new Vector2(0,0);
        for(int i = 0; i < steps; i++)
        {
            for(int j = 0; j < steps; j++)
            {
                Vector2 closedCheck = new Vector2(j, i);
                if(!closedSet.Contains(closedCheck) && min > distanceInfo[j][i].x)
                {
                    min = distanceInfo[j][i].x;
                    minDistance = closedCheck;
                }
            }
        }
        return minDistance;
    }

    private bool isInClosedSet(Vector2 newPos)
    {
        if (closedSet.Contains(newPos))
            return false;
        else
            return true;
    }
    private List <Vector2> getRoute()
    {
        List<Vector2> toReturn = new List<Vector2>();
        toReturn.Add(new Vector2((int)goal.x, (int)goal.y));
        Vector2 temp = new Vector2(distanceInfo[(int)goal.x][(int)goal.y].y, distanceInfo[(int)goal.x][(int)goal.y].z);
        toReturn.Add(temp);
        while (!temp.Equals(playerPos))
        {
            temp = new Vector2(distanceInfo[(int)temp.x][(int)temp.y].y, distanceInfo[(int)temp.x][(int)temp.y].z);
            toReturn.Add(temp);
        }
        return toReturn;
    }
    private float roundOf(float coordinate, int decimals)
    {
        decimal coor = (decimal) coordinate;
        return (float) decimal.Round(coor, decimals);
    }

    private Vector2 getIndices(Vector3 coordinate)
    {
        Vector2 toReturn = new Vector2((int)(coordinate.x - point1.x)/ 0.1f, (int)(coordinate.z - point1.z) / 0.1f);
        return toReturn;
    }
    private Vector3 roundOfPositions(Vector3 coordinate, int decimaal)
    {
        coordinate.x = roundOf(coordinate.x, decimaal);
        coordinate.y = roundOf(coordinate.y, decimaal);
        coordinate.z = roundOf(coordinate.z, decimaal);
        return coordinate;
    }

    private int Min(int f1, int f2)
    {
        if (f1 < f2)
            return f1;
        else
            return f2;
    }

    private int Max(int f1, int f2)
    {
        if (f1 < f2)
            return f2;
        else
            return f1;
    }
}
