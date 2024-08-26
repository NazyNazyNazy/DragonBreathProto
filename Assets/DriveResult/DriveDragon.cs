using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DriveDragon : MonoBehaviour
{
    public GameObject Dragon;
    public static int Level = 1;
    public float DragonHeight;

    // Start is called before the first frame update
    void Start()
    {
        DragonHeight = Dragon.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // LevelUp();
    }

    public void LevelUp()
    {
        Level += 1;
        Vector3 move;
        DragonHeight += 0.03f;
        move = new Vector3(0,DragonHeight, 0);
        // Debug.Log("Hight is " + slider.value.ToString());
        Debug.Log("Dragon Hight"+ DragonHeight.ToString() + ", Level:" + Level.ToString());
        transform.position += move;
    }

}
