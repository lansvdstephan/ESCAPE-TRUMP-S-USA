using UnityEngine;
using System.Collections;

public class TumbleWeedDropper : MonoBehaviour {

	public GameObject tumbleWeed;
	public float spawnMin = 10f;
	public float spawnMax = 15f;

	void Start () {
		Invoke("Spawn", Random.Range(spawnMin, spawnMax));
	}

	void Spawn()
	{
		Instantiate(tumbleWeed, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z + Random.Range(-33,33)), Quaternion.identity);
		Invoke("Spawn", Random.Range(spawnMin, spawnMax));
	}
}
