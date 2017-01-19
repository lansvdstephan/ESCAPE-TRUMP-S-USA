using UnityEngine;
using System.Collections;

public class SetDestinationTest : MonoBehaviour {

    public UnityEngine.AI.NavMeshAgent agent;
    // Use this for initialization
    void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(Vector3.zero);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
