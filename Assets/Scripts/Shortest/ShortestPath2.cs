using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShortestPath2 : MonoBehaviour
{
    public MoveonPath mop;

    public float range;       //Range 
    public float stepsize;     //Stepsize
    public float steps;       //Amount of steps
    private float costRecht = 10f;  //Costs to change one x OR one z
    private float costSchuin = 14f; //Costs to change one X AND one z

    //this to points will spawn the grid
    public Vector3 point1;
    public Vector3 point2;

    //Player Information
    public Vector3 playerPosition;
    public Vector2 playerPos;
    public Vector3 playerGoal;
    public Vector2 goal;

    //List of points on the grid that indicates whether there is an object at this point
    public int[,] map;
    public Vector4[,] infoMatrix;

    //List of Points that should be closed by the algoritm
    public List<Vector2> closedSet = new List<Vector2>();

    //test zone
    public Vector4 nieuw;
    public float dist;
    public Vector2 next;
    public List<Vector3> routes = new List<Vector3>();

    public bool active;
    // Use this for initialization
    void Start()
    {
        map = new int[(int)steps, (int)steps];
        infoMatrix = new Vector4[(int)steps, (int)steps];
        active = false;
    }

    void Update()
    {
        playerPosition = mop.transform.position;
        playerPosition = roundOfPositions(playerPosition,0);
        playerPos = getIndices(playerPosition);
        playerGoal = mop.pathToFolow.path_objects[mop.currentWayPointID].position;
        playerGoal = roundOfPositions(playerGoal,0);
        goal = getIndices(playerGoal);
        if(mop.check && !mop.fow.playerSeen && !active)
        {
            active = true;
            routes = getShortestPath();
        }
        if(!mop.check)
        {
            active = false;
        }
    }

    public List<Vector3> getShortestPath()
    {
        closedSet.Add(playerPos);
        dist = getShortestDistance(playerPos);
        Initialisation();
        AstarAlgorithm();
        routes = getRoute();
        return routes;
    }

    private List<Vector3> getRoute()
    {
        List<Vector2> route = new List<Vector2>();
        List<Vector3> path = new List<Vector3>();
        Vector2 currentPoint = goal;
        route.Add(goal);
        while (!currentPoint.Equals(playerPos))
        {
            Vector4 currentInfo = infoMatrix[(int)currentPoint.x, (int)currentPoint.y];
            currentPoint = new Vector2(currentInfo.z, currentInfo.w);
            route.Add(currentPoint);
        }

        for (int i = 0; i < route.Count; i++)
        {
            Vector3 point = getCoordinates(route[i]);
            path.Add(point);
        }
        path.Reverse(0, path.Count);
        return path;
    }

    //Combines all the functions below to a working algorithm
    //Checked
    private void AstarAlgorithm()
    {
        Vector2 currentPoint = playerPos;
        Vector4 currentInfo = infoMatrix[(int)playerPos.x, (int)playerPos.y];
        while (currentInfo.y != 0)
        {
            surroundingPointsUpdate(currentPoint, currentInfo);
            currentPoint = findNextclosedPoint();
            currentInfo = infoMatrix[(int)currentPoint.x, (int)currentPoint.y];
            Debug.Log(currentPoint);
            Debug.Log(currentInfo);
            closedSet.Add(currentPoint);
        }
    }

    //This method makes a map/matrix which indicates where an object can walk(1) and where it cant(0)
    //Checked
    private void Initialisation()
    {
        int miljoen = 1000000;
        Vector4 inf = new Vector4(miljoen, miljoen, miljoen, miljoen);
        for (int i = 0; i < steps; i++)
        {
            for (int j = 0; j < steps; j++)
            {
                infoMatrix[j, i] = inf;
                Vector3 temp = getCoordinates(new Vector2(j, i));
                if (!isObject(temp))
                    map[j, i] = 1;
                else
                    map[j, i] = 0;
            }
        }

        for (int i = 0; i < steps - 1; i++)
        {
            for (int j = 0; j < steps; j++)
            {
                if (map[j, i] == 1)
                {
                    if (j == 0)
                    {
                        if (map[j, i + 1] == 0 || map[j + 1, i + 1] == 0)
                            map[j, i] = 0;
                    }
                    else if (j == steps - 1)
                    {
                        if (map[j, i + 1] == 0 || map[j - 1, i + 1] == 0)
                            map[j, i] = 0;
                    }
                    else if ((map[j, i + 1] == 0 || map[j - 1, i + 1] == 0) || map[j + 1, i + 1] == 0)
                    {
                        map[j, i] = 0;
                    }
                }
            }
        }
        infoMatrix[(int)playerPos.x, (int)playerPos.y] = new Vector4(0, getShortestDistance(playerPos), playerPos.x, playerPos.y);
    }

    //Exploring the surrounding points of coordinate ands updates the infoMatrix if necessary
    //Checked
    private void surroundingPointsUpdate(Vector2 coordinate, Vector4 info)
    {
        int x = (int)coordinate.x;
        int y = (int)coordinate.y;
        int x_min = x - 1;
        int x_max = x + 2;
        int y_min = y - 1;
        int y_max = y + 2;

        //Checks whether you are on the edge of the grid
        if (x == 0)
        {
            x_min = 0;
        }
        else if (x == steps - 1)
        {
            x_max = x + 1;
        }
        else if (y == 0)
        {
            y_min = 0;
        }
        else if (y == steps - 1)
        {
            y_max = y + 1;
        }

        //Explores surrounding points
        for (int i = y_min; i < y_max; i++)
        {
            for (int j = x_min; j < x_max; j++)
            {
                if (j < steps && i < steps && j >= 0 && i >= 0)
                {
                    //if (j,i) is not (x,y), its possible to go to (j,i) & (j,i) is not in closedSet
                    if (((x != j || y != i) && map[j, i] == 1) && !closedSet.Contains(new Vector2(j, i)))
                    {
                        Vector4 temp = infoMatrix[j, i];
                        Vector4 replace = new Vector4();
                        //if you have to slant --> costs: + 14
                        if (x != j && y != i)
                        {
                            replace.x = Min((int)temp.x, (int)(info.x + costSchuin));
                        }
                        //if you dont have to slant --> costs: +10
                        else
                        {
                            replace.x = Min((int)temp.x, (int)(info.x + costRecht));
                        }
                        //if the new distance to the finish is shorter than the old distance --> change previous point
                        if (replace.x != temp.x)
                        {
                            replace.z = coordinate.x;
                            replace.w = coordinate.y;
                        }
                        else
                        {
                            replace.z = temp.z;
                            replace.w = temp.w;
                        }
                        replace.y = getShortestDistance(new Vector2(j, i));
                        infoMatrix[j, i] = replace;
                    }
                }
            }
        }
    }

    //Decides which of the points in the grid is the next closed point
    //Checked
    private Vector2 findNextclosedPoint()
    {
        float min = infoMatrix[0, 0].y;
        Vector2 minDistance = new Vector2(0, 0);
        for (int i = 0; i < steps; i++)
        {
            for (int j = 0; j < steps; j++)
            {
                Vector2 closedCheck = new Vector2(j, i);
                if (!closedSet.Contains(closedCheck) && min > infoMatrix[j, i].y)
                {
                    min = infoMatrix[j, i].y;
                    minDistance = closedCheck;
                }
            }
        }
        return minDistance;
    }
    //Checks wheter there is an object at position p1
    //Checked
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

    //Round of a float at decimals decimals
    //Checked
    private float roundOf(float coordinate, int decimals)
    {
        decimal coor = (decimal)coordinate;
        return (float)decimal.Round(coor, decimals);
    }

    //Round of the position of an object so this object goes to the closest gridpoint
    //Checked
    private Vector3 roundOfPositions(Vector3 coordinate, int decimaal)
    {
        coordinate.x = roundOf(coordinate.x, decimaal);
        coordinate.y = 0.5f;
        coordinate.z = roundOf(coordinate.z, decimaal);
        return coordinate;
    }

    //Given the coordinates of a point, the indices of this point will be returned
    //Checked 
    private Vector2 getIndices(Vector3 coordinate)
    {
        Vector2 toReturn = new Vector2((int)(coordinate.x - point1.x) / stepsize, (int)(coordinate.z - point1.z) / stepsize);
        return toReturn;
    }

    //Given the indices of a point, the coordinates of this point will be retured
    //Checked
    private Vector3 getCoordinates(Vector2 indices)
    {
        Vector3 toReturn = new Vector3(indices.x * stepsize + point1.x, 0.5f, indices.y * stepsize + point1.z);
        return toReturn;
    }

    //Returns the shortest distance between coordinate and the finish
    //Checked
    private float getShortestDistance(Vector2 coordinate)
    {
        int crossing = 0;
        int straight = 0;
        Vector2 temp = new Vector2(Mathf.Abs(coordinate.x - goal.x), Mathf.Abs(coordinate.y - goal.y));
        crossing = Min((int)temp.x, (int)temp.y);
        straight = Max((int)temp.x, (int)temp.y) - crossing;
        return crossing * costSchuin + straight * costRecht;
    }

    //Returns the minimum value between two integers
    //Checked
    private int Min(int f1, int f2)
    {
        if (f1 < f2)
            return f1;
        else
            return f2;
    }

    //Returns the maximum value between two integers
    //Checked
    private int Max(int f1, int f2)
    {
        if (f1 < f2)
            return f2;
        else
            return f1;
    }
}
