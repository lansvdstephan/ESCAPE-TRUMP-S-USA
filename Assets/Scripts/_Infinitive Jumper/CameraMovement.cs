using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public GameObject player;
    public float speed = 0.1f;

    private float maxSpeed = 2f;
    private float lastUpdated = 3f;
    private Vector3 offset;

    void Start()
    {
        offset = this.transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!JumpMovement.player.GetComponent<JumpMovement>().rocketOn)
        {
            float y = this.transform.position.y;
            this.transform.position = new Vector3(this.transform.position.x, y + speed * Time.deltaTime, this.transform.position.z);
            if (speed < maxSpeed)
            {
                speed += 0.05f * Time.deltaTime;
            }
            if (Mathf.RoundToInt(y) % 100 == 0 && Mathf.RoundToInt(y) > Mathf.RoundToInt(lastUpdated))
            {
                maxSpeed += 0.5f;
                maxSpeed = Mathf.Min(3.5f, maxSpeed);
                lastUpdated = y;
            }
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, player.transform.position.y + offset.y, this.transform.position.z);
        }
    }
}
