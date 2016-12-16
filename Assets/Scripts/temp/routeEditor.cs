using UnityEngine;
using System.Collections;

public class routeEditor : MonoBehaviour {

    public Color rayColor = Color.black;
    public ShortestPath2 sp;

    void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
    }
}
