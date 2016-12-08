using UnityEngine;
using System.Collections;

public class EditorRandom : MonoBehaviour
{

    public Color rayColor = Color.white;
    public Random_Searching rs;
    // Use this for initialization
    void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        Gizmos.DrawLine(rs.point1, rs.point2);
        Gizmos.DrawLine(rs.point1, transform.position);
        Gizmos.DrawLine(rs.random, rs.point1);
    }
}
