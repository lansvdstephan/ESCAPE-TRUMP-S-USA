using UnityEngine;

public class Bullets : MonoBehaviour {

    private Vector3 target;
    public float speed = 50f;
    
    public void Seek(Vector3 _target)
    {
        target = _target;
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

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

	}

    void HitTarget()
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
