using UnityEngine;
using System.Collections;

public class FuelDropper : MonoBehaviour {

    public GameObject player;
    public GameObject Fuel;
    public float spawnMin = 10f;
    public float spawnMax = 15f;

    private Vector3 offset;

    void Start()
    {
        Invoke("Spawn", Random.Range(spawnMin, spawnMax));
        offset = this.transform.position - player.transform.position;
    }

    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, player.transform.position.z + offset.z);
    }

    void Spawn()
    {
        Instantiate(Fuel, new Vector3(this.gameObject.transform.position.x + Random.Range(-8,8), this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
        Invoke("Spawn", Random.Range(spawnMin, spawnMax));
    }
}
