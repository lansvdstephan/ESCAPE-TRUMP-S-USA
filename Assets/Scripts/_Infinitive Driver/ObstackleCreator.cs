using UnityEngine;
using System.Collections;

public class ObstackleCreator : MonoBehaviour {

    public GameObject player;
    public Creator mother;
    public MultiDimensionGameObjectArray[] obj;
	public float tumbleweedDistance;
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
		if (mother.GetI () < obj.Length) {
			i = mother.GetI ();
		}
		if (i == 1) {
			Instantiate (obj [1].gameObjectArr [Random.Range (0, obj [1].gameObjectArr.Length)], new Vector3 (this.gameObject.transform.position.x + Random.Range (-tumbleweedDistance, tumbleweedDistance) , this.gameObject.transform.position.y + 0.01f, this.gameObject.transform.position.z), this.transform.rotation);
		} else {
			Instantiate (obj [i].gameObjectArr [Random.Range (0, obj [i].gameObjectArr.Length)], new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.01f, this.gameObject.transform.position.z), this.transform.rotation);
		}
		float spawnMin = spawnDistMin / player.GetComponent<Movement> ().correctedSpeed;
		float spawnMax = spawnDistMax / player.GetComponent<Movement> ().correctedSpeed;
		Invoke ("Spawn", Random.Range (spawnMin, spawnMax));
	}
}
