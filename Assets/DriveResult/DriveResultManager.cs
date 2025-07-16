using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DriveResultManager : MonoBehaviour
{
    
    public TextMeshProUGUI LevelText;
    public DriveDragon Dragon;
    private int level;

    // Start is called before the first frame update
    void Start()
    {
        // LevelText.text = "Milage": + PhoneSensor.Milage.ToString + 
        //                 "\nLevel:" + DriveDragon.Level.ToString;
        level = DriveDragon.Level;
        // level = 3; // ← ここでレベルを3に設定（例として）
        

        StartCoroutine(LevelingUpDragon());
        // for (int i = 0; i < level; i++)
        // {
        //     // Dragon.LevelUp();

        //     // Coroutine（コルーチン）を開始

        // }
    }
    IEnumerator LevelingUpDragon()
    {
        for (int i = 0; i < level; i++)
        {
            Dragon.LevelUp();
            yield return new WaitForSeconds(1f);//１秒待つ
        }
    }
    // Update is called once per frame
    void Update()
    {
        LevelText.text = "Milage:" + PhoneSensor.milageTotal.ToString()
                         + "\nLevel:" + DriveDragon.Level.ToString();
        
    }

    public void GoShooting()
    {
        SceneManager.LoadScene("Shooting");
    }

    public void GoDriving()
    {
        SceneManager.LoadScene("DrivingScene");
    } 
}
