using UnityEngine;
using System.Collections;

public class ObstackleCreator : MonoBehaviour {

    public GameObject player;
    public Creator mother;
    public MultiDimensionGameObjectArray[] obj;
    public float spawnMin = 1f;
    public float spawnMax = 2f;
    
    private int i;
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
        if (mother.GetI() < obj.Length)
        {
            i = mother.GetI();
        }
        Instantiate(obj[i].gameObjectArr[Random.Range(0, obj[i].gameObjectArr.Length)], new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.01f, this.gameObject.transform.position.z), Quaternion.identity);
        Invoke("Spawn", Random.Range(spawnMin, spawnMax));
    }
}
