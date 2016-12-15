using UnityEngine;
using System.Collections;

public class ObstackleCreator : MonoBehaviour {

    public GameObject[] obj;
    public float spawnMin = 1f;
    public float spawnMax = 2f;
	
    void Start ()
    {
        Invoke("Spawn", Random.Range(spawnMin, spawnMax));
    }
    
    void Spawn()
    {
        Instantiate(obj[Random.Range(0, obj.Length)], new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.01f, this.gameObject.transform.position.z), Quaternion.identity);
        Invoke("Spawn", Random.Range(spawnMin, spawnMax));
    }
}
