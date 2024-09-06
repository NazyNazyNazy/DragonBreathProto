using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DriveResultManager : MonoBehaviour
{
    
    public TextMeshProUGUI LevelText;

    // Start is called before the first frame update
    void Start()
    {
        // LevelText.text = "Milage": + PhoneSensor.Milage.ToString + 
        //                 "\nLevel:" + DriveDragon.Level.ToString;
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
