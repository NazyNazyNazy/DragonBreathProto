using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject dungionBtn;
    public GameObject drivingBtn;
    public GameObject dungionGoFrame;
    public GameObject driveGoFrame;

    //public static int firstLoad =1;
    public static int stamina;
    public int maxStamina;
    public int stars;

    public Text DungionChlgMsg;
    public Text ErrorMsg;

    private System.DateTime CurrentTime;
    private static System.DateTime lastDgnTime;
    private static string LDTS;//lastDgnTimeString
    private string recoverTime;
    private System.TimeSpan debounceTime;
    public Text StaminaText;

    // Start is called before the first frame update
    void Start()
    {

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //player default status

        maxStamina = 3;
        stars = 3;
        debounceTime = new System.TimeSpan(2, 0, 0);

        //load status
        LoadData();


        System.DateTime dT;
        if (System.DateTime.TryParse(LDTS, out dT))
        {
            lastDgnTime = System.DateTime.Parse(LDTS);
        }
            //lastDgnTime = new System.DateTime(2021, 10, 02, 12, 17, 00);
    
    }

    // Update is called once per frame
    void Update()
    {

        if(stamina < maxStamina)
        {
            CurrentTime = System.DateTime.Now;
            int checkTime = System.DateTime.Compare(lastDgnTime + debounceTime, CurrentTime);


            if (checkTime > 0)
            {
                System.TimeSpan ttt = (lastDgnTime + debounceTime - CurrentTime);
                recoverTime = new System.DateTime(ttt.Ticks).ToString("HH:mm:ss");

                if (StaminaText != null)
                {
                    StaminaText.text =
                    "残" + GameManager.stamina + "回/ "
                    + maxStamina.ToString() + "回" + "\n"
                    + "次まで" + recoverTime.ToString();
                }
            }
            else if (checkTime <= 0)
            {
                lastDgnTime = lastDgnTime + debounceTime;
                GameManager.stamina = GameManager.stamina + 1;
            }

        }
        else
        {
            if (StaminaText != null)
            {
                StaminaText.text =
                "残" + GameManager.stamina + "回/ "
                + maxStamina.ToString() + "回";
            }
        }    

    }

    public void DungionOn()
    {
        if (dungionGoFrame.activeSelf)
        {
            dungionGoFrame.SetActive(false);
        }
        else
        {
            DungionChlgMsg.text = "ダンジョンへ出発する";
            dungionGoFrame.SetActive(true);
        }

        if (driveGoFrame.activeSelf)
        {
            driveGoFrame.SetActive(false);
        }
    }

    public void DriveOn()
    {
        if (driveGoFrame.activeSelf)
        {
            driveGoFrame.SetActive(false);
        }
        else
        {
            driveGoFrame.SetActive(true);
        }

    }


    public void StartDriving()
    {
        SceneManager.LoadScene("Driving");
    }

    public void ChllengeDungion()
    {
        if(GameManager.stamina ==  maxStamina)
        {
            GameManager.stamina = stamina -1;
            GameManager.lastDgnTime = System.DateTime.Now;
            GameManager.LDTS = System.DateTime.Now.ToString();// "G");
            SaveData();
            SceneManager.LoadScene("Dungion");
        }
        else if (GameManager.stamina > 0)
        {
            GameManager.stamina = stamina - 1;
            SaveData();
            SceneManager.LoadScene("Dungion");
        }

        else
        {
            ErrorMsg.text = "挑戦回数が足りません。";
        }
    }

    public void GoShooting()
    {
        SceneManager.LoadScene("Shooting");
    }    



    public void GoHome()
    {
        SceneManager.LoadScene("HomeScene");
        SaveData();
    }

    public void CancesGui()
    {
        if (dungionGoFrame.activeSelf)
        {
            dungionGoFrame.SetActive(false);
        }
        if (driveGoFrame.activeSelf)
        {
            driveGoFrame.SetActive(false);
        }
    }


    //Save Data
    private void SaveData()
    {

        
        // PlayerPrefs.SetString("Character", myCharacter.CharName.ToString());
        // PlayerPrefs.SetString("Weapon", myCharacter.WpnName.ToString());
        // PlayerPrefs.SetString("Armer", myCharacter.ArmName.ToString());
        //他のキャラクターの装備品がセーブされない問題あり
        //キャラー名をkeyにしてforで（武器、防具をセーブする必要あり
        //とりあえず今日は実装を見送る

        PlayerPrefs.SetInt("Stamina", stamina);
        PlayerPrefs.SetString("LastDgnTime", LDTS);

        PlayerPrefs.Save();
        
    }

    private void LoadData()
    {
        //myCharacter.CharName = PlayerPrefs.GetString("Character");
        //myCharacter.newWeaponName = PlayerPrefs.GetString("Weapon");
        //myCharacter.newArmerName = PlayerPrefs.GetString("Armer");

        stamina = PlayerPrefs.GetInt("Stamina");
        //stamina = 3;
        LDTS = PlayerPrefs.GetString("LastDgnTime");
    }    
        
}
