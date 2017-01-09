using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;
    private Vector3 previousPosition;

    void Start()
    {
        offset = this.transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        previousPosition = this.transform.position;
        this.transform.position = new Vector3(this.transform.position.x, player.transform.position.y + offset.y, this.transform.position.z );
        if (this.transform.position.y <= previousPosition.y)
        {
            this.transform.position = previousPosition;
        }
    }
}
