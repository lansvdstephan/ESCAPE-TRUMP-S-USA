using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public GameObject player;
    public float speed = 0.1f;

    private Vector3 offset;
    private Vector3 previousPosition;

    void Start()
    {
        offset = this.transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + speed*Time.deltaTime, this.transform.position.z);

        /*
        previousPosition = this.transform.position;
        this.transform.position = new Vector3(this.transform.position.x, player.transform.position.y + offset.y, this.transform.position.z );
        if (this.transform.position.y <= previousPosition.y)
        {
            this.transform.position = previousPosition;
        }
        */
    }
}
