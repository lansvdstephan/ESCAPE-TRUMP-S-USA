using UnityEngine;
using System.Collections;

public class PlatformDropper : MonoBehaviour
{
    public GameObject player;
    public GameObject Platform;

    private Vector3 offset;
    private float lastDropped;

    void Start()
    {
        offset = this.transform.position - player.transform.position;
        lastDropped = 0;
    }

    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, player.transform.position.y + offset.y, this.transform.position.z);
        if (Mathf.RoundToInt(this.transform.position.y % 4) == 0 && Mathf.RoundToInt(this.transform.position.y) > Mathf.RoundToInt(lastDropped))
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Instantiate(Platform, new Vector3(this.gameObject.transform.position.x + Random.Range(-8, 8), this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
        lastDropped = this.transform.position.y;
    }
}
