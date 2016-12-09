using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

   public Vector3 point1;
    public Vector3 point2;
   private Vector3 pointToGo = new Vector3();
    // Use this for initialization
    void Start () {
        point1 = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
        point2= new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        pointToGo = point2;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 1.0f, 0);
        
        if (transform.position.Equals(point1)) {
            pointToGo = point2;
        }
        else if(transform.position.Equals(point2))
        {
            pointToGo = point1;
        }
        transform.position = Vector3.MoveTowards(transform.position, pointToGo, Time.deltaTime * 1.0f);
        
    }
}
