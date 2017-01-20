using UnityEngine;
using System.Collections;

[System.Serializable]
public class Creator : MonoBehaviour {

    public bool driverLevel = true;
    public GameObject player;
    public MultiDimensionGameObjectArray[] obj;
	public int maxPerStage;
	public int driveHorizonDistance;

	private float endDistance;
	private bool endLevel = false;
    private int counter;
    private int i;
    private Vector3 offset;
	private GameObject[] obstackleCreators;

    public GameObject levelCompletedPanel;

    void Awake()
    {
        levelCompletedPanel = GameObject.Find("MainMenuCanvas").gameObject.transform.FindChild("Level Completed Panel").gameObject;
    }
    void Start()
    {
        counter = 0;
        i = 0;
       
        offset = this.transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (driverLevel)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, player.transform.position.z + offset.z);
        }
    }


    public void OnTriggerExit(Collider other)
    {
		if (other.CompareTag("Plane"))
		{
			if (counter % 2 == 0) {
				Instantiate (obj [i].gameObjectArr [Random.Range (0, obj [i].gameObjectArr.Length)], 
					new Vector3 (0, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
			} else {
				Instantiate (obj [i].gameObjectArr [Random.Range (0, obj [i].gameObjectArr.Length)], 
					new Vector3 (0, this.gameObject.transform.position.y + 0.01f, this.gameObject.transform.position.z), Quaternion.identity);
			}
			counter++;
		}
		if (GameObject.FindWithTag ("Player").activeSelf) {
			if (GameObject.FindWithTag ("Player").GetComponent<Movement> ().points > (endDistance + driveHorizonDistance)) {
				EndGame ();
			}
		}

        if (counter == maxPerStage)
        {
            if (i < obj.Length - 1)
            {
                i++;
                counter = 0;
            }

			else if (endLevel) {
				EndGame ();
			}
            else
            {
				endLevel = true;
                counter = 0;
				Destroy (GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraMovement1> ());
				endDistance = GameObject.FindWithTag ("Player").GetComponent<Movement> ().points;
				//DisableObsCreators ();
                print("This is the End.");
            }
        }
    }

//	public void DisableObsCreators(){
//		obstackleCreators = GameObject.FindGameObjectsWithTag ("Obstackle Creator");
//		foreach (GameObject j in obstackleCreators){
//			j.SetActive (false);
//		}
//		Destroy (GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraMovement1> ());
//	}


    public int GetI()
    {
        return i;
    }

	void EndGame(){
        Time.timeScale = 0.0f;
        int healthLeft = (int) GameObject.FindWithTag("Player").GetComponent<Movement>().health;
        int fuelLeft =   (int) GameObject.FindWithTag("Player").GetComponent<Movement>().fuel;

        levelCompletedPanel.GetComponent<CalculateScore>().fuelBool = true;
        levelCompletedPanel.GetComponent<CalculateScore>().healthBool = true;
        levelCompletedPanel.GetComponent<CalculateScore>().fuel = fuelLeft;
        levelCompletedPanel.GetComponent<CalculateScore>().health = healthLeft;
        levelCompletedPanel.SetActive(true);
    }
}
