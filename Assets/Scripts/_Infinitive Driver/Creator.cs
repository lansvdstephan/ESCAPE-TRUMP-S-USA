﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Creator : MonoBehaviour {

    public GameObject player;
    public MultiDimensionGameObjectArray[] obj;
    public int maxPerStage;

    private int counter;
    private int i;
    private Vector3 offset;

    void Start()
    {
        counter = 0;
        i = 0;
       
        offset = this.transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, player.transform.position.z + offset.z);
    }


    public void OnTriggerExit(Collider other)
    {
        if (counter != maxPerStage)
        {
            if (other.CompareTag("Plane"))
            {
                if (counter % 2 == 0)
                {
                    Instantiate(obj[i].gameObjectArr[Random.Range(0, obj[i].gameObjectArr.Length)], 
                        new Vector3(0, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
                    counter++;
                }
                else
                {
                    Instantiate(obj[i].gameObjectArr[Random.Range(0, obj[i].gameObjectArr.Length)], 
                        new Vector3(0, this.gameObject.transform.position.y + 0.01f, this.gameObject.transform.position.z), Quaternion.identity);
                    counter++;
                }
            }
        }
        else
        {
            if (i < obj.Length-1)
            {
                i++;
                counter = 0;
            }
            else
            {
                counter = 0;
                print("This is the End.");
            }
        }
    }

    public int GetI()
    {
        return i;
    }
}
