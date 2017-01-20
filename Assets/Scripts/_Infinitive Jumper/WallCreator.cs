using UnityEngine;
using System.Collections;

public class WallCreator : MonoBehaviour {

    public float dropDistance = 50f;
    public GameObject Platform;

    private Vector3 offset;
    private float lastDropped;

    void Start()
    {
        lastDropped = 0;
    }

    void Update()
    {
        if (Mathf.RoundToInt(this.transform.position.y % dropDistance) == 0 && Mathf.RoundToInt(this.transform.position.y) > Mathf.RoundToInt(lastDropped))
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Instantiate(Platform, new Vector3(0, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
        lastDropped = this.transform.position.y;
    }

}
