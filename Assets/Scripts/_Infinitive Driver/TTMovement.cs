using UnityEngine;
using System.Collections;

public class TTMovement : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;
    private float minDistance = 10;
    private float[] hArr;
    private float hBorder = 0.65f;
    private float speed;

    void Awake()
    {
        hArr = new float[3];
        for (int i = 0; i < 3; i++)
        {
            hArr[i] = 0f;
        }
    }

    void Start()
    {
        offset = this.transform.position - player.transform.position;
        speed = player.GetComponent<Movement>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        float h = GetHorizontalMovement();
        Vector3 movement = new Vector3(h, 0, 2);
        movement = movement * speed * Time.deltaTime;
        if (this.transform.position.z - player.transform.position.z > minDistance)
        {
            this.transform.position = this.transform.position + movement;
        }
        else
        {
            this.transform.position = this.transform.position + movement;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, player.transform.position.z + minDistance);
        }
    }

    float GetHorizontalMovement()
    {
        hArr[2] = hArr[1];
        hArr[1] = hArr[0];
        hArr[0] = Random.Range(-1f, 1f);
        float h = (hArr[2] + hArr[1] + hArr[0]) / 3;
        if (h < hBorder && h > -hBorder)
        {
            h = 0;
        }
        return h;
    }

    void OnCollisionEnter(Collision collision)
    {

    }
}
