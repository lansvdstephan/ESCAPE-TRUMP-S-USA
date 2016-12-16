using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cridEditor : MonoBehaviour
{
    public Color rayColor = Color.black;
    public List<Transform> grid_points = new List<Transform>();
    Transform[] theArray;

    //onDrawGizmoss makes it possible to draw several types of Gizmos in the editor
    void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        theArray = GetComponentsInChildren<Transform>();
        grid_points.Clear();
        foreach (Transform path_obj in theArray)//For each transform in theArray
        {
            if (path_obj != this.transform)//We dont want the parent object to change
            {
                grid_points.Add(path_obj);
            }
        }

        for (int i = 0; i < grid_points.Count; i++)
        {
            Vector3 current = grid_points[i].position;
            Gizmos.DrawWireSphere(current, .3f);//so we can see our empty game object
        }
    }
}
