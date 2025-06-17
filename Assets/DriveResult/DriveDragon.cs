using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DriveDragon : MonoBehaviour
{
    public GameObject Dragon;
    public GameObject PopUp;
    public static int Level = 1;
    public float DragonHeight;
    private float MovePitch;
    private float DragonHightIncrement = 0.001f;

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
        DragonHeight += 1.8f;
        // Vector3 move = new Vector3(0, DragonHeight, 0); ;
        // MovePitch = 1.8f;
        // move = new Vector3(0,DragonHeight, 0);
        // move = new Vector3(0, MovePitch, 0);
        // Debug.Log("Hight is " + slider.value.ToString());
        Debug.Log("Dragon Hight" + DragonHeight.ToString() + ", Level:" + Level.ToString());
        // transform.position += move;
        transform.DOMoveY(DragonHeight, 1);
        PopUp.SetActive(true);
        PopUp.transform.localScale = Vector3.one * 0.2f;
        PopUp.transform.DOScale(1f, 0.6f).SetEase(Ease.OutBack, 5f);
        // PopUp.SetActive(false);
        


    }
}


