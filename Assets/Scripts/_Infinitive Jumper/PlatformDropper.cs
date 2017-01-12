using UnityEngine;
using System.Collections;

public class PlatformDropper : MonoBehaviour
{
    public GameObject player;
    public GameObject[] Platform;

    private Vector3 offset;
    private Vector3 lastDropped;
    private float lastXScale;
    private float lastUpdated = 3f;

    private float spawnRange = 8f;
    private float maxDistance = 3.5f;
    private float minDistance = 3f;
    

    void Start()
    {
        offset = this.transform.position - player.transform.position;
        lastDropped = this.transform.position;
    }

    void Update()
    {
        float y = this.transform.position.y;
        if (Mathf.RoundToInt(y % 4) == 0 && Mathf.RoundToInt(y) > Mathf.RoundToInt(lastDropped.y))
        {
            Spawn();
        }
        if (Mathf.RoundToInt(y % 100) == 0 && Mathf.RoundToInt(y) > Mathf.RoundToInt(lastUpdated))
        {
            if (maxDistance < 5) maxDistance += 0.5f;
            if (minDistance > 2.5) minDistance -= 0.5f;
            lastUpdated = y;
            print("updated");
        }
    }

    void Spawn()
    {
        // Create the spot where the platfomr should be dropped and the platform to drop
        Vector3 dropSpot = new Vector3(this.gameObject.transform.position.x + Random.Range(-spawnRange, spawnRange), this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        GameObject dropPlatform = Platform[Random.Range(0, Platform.Length)];
        float currentXScale = dropPlatform.transform.lossyScale.x;
        // Ensures a big platform can't entirly overlap a smaller platform
        float x = dropSpot.x;
        if (Mathf.Abs(dropSpot.x - lastDropped.x) < minDistance)
        {
            x = lastDropped.x + (1 - 2* Mathf.Round(Random.Range(0f,1f)))*minDistance;
        }
        else{
            if (dropSpot.x < 0 && lastDropped.x > 0 && ((lastDropped.x - lastXScale / 2) - (dropSpot.x + currentXScale/2)) > maxDistance)
            {
                x = lastDropped.x - lastXScale / 2 - maxDistance - currentXScale / 2;
            }
            else if (dropSpot.x > 0 && lastDropped.x < 0 && ((dropSpot.x - currentXScale / 2) - (lastDropped.x + lastXScale / 2)) > maxDistance)
            {
                x = lastDropped.x + lastXScale / 2 + maxDistance + currentXScale / 2;
            }
        }
        dropSpot.Set(x, dropSpot.y, dropSpot.z);
        // Drop the platform
        Instantiate(dropPlatform, dropSpot, Quaternion.identity);
        // Update the last dropped position
        lastDropped = dropSpot;
        lastXScale = currentXScale;
    }
}
