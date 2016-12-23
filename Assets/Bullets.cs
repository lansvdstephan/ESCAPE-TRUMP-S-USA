using UnityEngine;

public class Bullets : MonoBehaviour {

    private Vector3 target;
    public float speed = 50f;
    
    public void Seek(Vector3 _target)
    {
        target = _target;
        target.y = target.y + 1.2f;
        target = target + 25*transform.forward;
        Debug.DrawRay(transform.position, target - transform.position, Color.blue,2f);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

	}

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    private int Max(int i1, int i2)
    {
        if (i1 > i2)
            return i1;
        else
            return i2;
    }
}
