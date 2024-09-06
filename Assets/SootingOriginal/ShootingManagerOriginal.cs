using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShootingManagerOriginal : MonoBehaviour
{
    public int hitpoint;
    public int achievement;
    public Slider HitPointGauge;
    public Slider AchievmentGauge;
    // Start is called before the first frame update
    void Start()
    {
        hitpoint = 100;
        achievement = 0;
        HitPointGauge.value = 100;
        AchievmentGauge.value  = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(hitpoint.ToString() + ", " + achievement.ToString());
        HitPointGauge.value = 80;//hitpoint / 100;
        AchievmentGauge.value = achievement;
    }

    public void GoDriveResult()
    {
        SceneManager.LoadScene("DriveResult");
    } 
}
