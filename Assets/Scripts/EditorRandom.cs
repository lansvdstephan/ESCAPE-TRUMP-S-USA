using UnityEngine;
using System.Collections;

public class EditorRandom : MonoBehaviour
{

    public Color rayColor = Color.white;
    public Random_Searching rs;
    // Use this for initialization
    void OnDrawGizmos()
    {
        Vector3 point1 = rs.point1;
        Vector3 point2 = rs.point2;
        Vector3 point3 = new Vector3(rs.point1.x, rs.point1.y, rs.point2.z);
        Vector3 point4 = new Vector3(rs.point2.z, rs.point2.y, rs.point1.z);
        Gizmos.color = rayColor;
        Gizmos.DrawLine(point1, point3);
        Gizmos.DrawLine(point1, point4);
        Gizmos.DrawLine(point2, point3);
        Gizmos.DrawLine(point2, point4);
        Gizmos.DrawLine(rs.point1, transform.position);
        Gizmos.DrawLine(rs.random, rs.point1);
    }
}
