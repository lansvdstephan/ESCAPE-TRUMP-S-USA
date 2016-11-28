using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorPath : MonoBehaviour
{
    public Color rayColor = Color.white;
    public List<Transform> path_objects = new List<Transform> ();
    Transform[] theArray;

    //onDrawGizmoss makes it possible to draw several types of Gizmos in the editor
    void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        theArray = GetComponentsInChildren<Transform>();
        path_objects.Clear();
        foreach(Transform path_obj in theArray)//For each transform in theArray
        {
            if(path_obj !=this.transform)//We dont want the parent object to change
            {
                path_objects.Add(path_obj);
            }
        }

        for(int i = 0; i < path_objects.Count; i++)
        {
            Vector3 current = path_objects[i].position;
            if(i>0) // There is something in the list
            {
                Vector3 previous = path_objects[i - 1].position;
                Gizmos.DrawLine(current, previous);  
                Gizmos.DrawWireSphere(current, .3f);//so we can see our empty game object
            }
        }
    }
}
