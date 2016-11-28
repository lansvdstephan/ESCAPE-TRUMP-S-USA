using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlayerSight))]
public class PlayerSightEditor : Editor
{

    void OnSceneGUI()
    {
        PlayerSight fow = (PlayerSight) target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360.0f, fow.sightRadius);

        Vector3 AngleA = fow.directionFromAngle(-1 * fow.sightAngle / 2, false);
        Vector3 AngleB = fow.directionFromAngle(fow.sightAngle / 2, false);
        Vector3 AngleC = fow.directionFromAngle(0, false);
        Vector3 AngleD = fow.directionFromAngle(fow.angle, false);
        Handles.DrawLine(fow.transform.position, fow.transform.position + AngleA * fow.sightRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + AngleB * fow.sightRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + AngleC * fow.sightRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + AngleD * fow.distance);
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360.0f, fow.distance);
        
    }

        // Use this for initialization
        void Start () {
	
	}

}
