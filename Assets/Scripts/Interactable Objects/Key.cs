using UnityEngine;
using System.Collections;

public class Key : PickUpAble {

    public int keyCode = 00;
    private Vector3 pos1 = new Vector3(-28.85f, 0.81544f, 22.7f);
    private Vector3 pos2 = new Vector3(-32.36f, 0.81544f, 8.63f);
    private Vector3 pos3 = new Vector3(-5.2f, 0.81544f, 25.9f);
    private Vector3 pos4 = new Vector3(25.35f, 0.81544f, 16.22f);
    private Vector3 pos5 = new Vector3(34.77f, 0.81544f, 29.53f);
    private Vector3 pos6 = new Vector3(-5.35f, 0.81544f, 39.03f);
    public void Start()
    {
        placeCarKeys();
    }
    public override bool GetAction()
    {
        if (base.GetAction())
        {
            return true;
        }
        return false;
    }
    private void placeCarKeys()
    {
        float rnd = Random.Range(1f, 7f);
        int randi = (int) Mathf.Floor(rnd);
        switch (randi)
        {
            case 1:
                transform.position = pos1;
                break;
            case 2:
                transform.position = pos2;
                break;
            case 3:
                transform.position = pos3;
                break;
            case 4:
                transform.position = pos4;
                break;
            case 5:
                transform.position = pos5;
                break;
            default:
                transform.position = pos6;
                break;
        }
    }
}
