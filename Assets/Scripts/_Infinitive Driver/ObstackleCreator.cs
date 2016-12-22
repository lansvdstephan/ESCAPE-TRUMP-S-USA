using UnityEngine;
using System.Collections;

public class ObstackleCreator : MonoBehaviour {

    public GameObject player;
    public Creator mother;
    public MultiDimensionGameObjectArray[] obj;
    public float spawnDistMin = 10f;
    public float spawnDistMax = 20f;
    
    private int i;
    private Vector3 offset;

    void Start()
    {
        float spawnMin = spawnDistMin / 50;
        float spawnMax = spawnDistMax / 50;
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
        float spawnMin = spawnDistMin / player.GetComponent<Movement>().correctedSpeed;
        float spawnMax = spawnDistMax / player.GetComponent<Movement>().correctedSpeed;
        Invoke("Spawn", Random.Range(spawnMin, spawnMax));
    }
}
