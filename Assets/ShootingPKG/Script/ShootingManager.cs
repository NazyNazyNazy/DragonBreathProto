using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ShootingManager : MonoBehaviour
{

    [SerializeField]
    PlayerController player = null;
    [SerializeField]
    MeteorSpawner[] spawners = null;
    [SerializeField]
    Canvas gameStartCanvas = null;
    [SerializeField]
    Canvas gameOverCanvas = null;


    [SerializeField]
    Canvas bgCity1 = null;
    [SerializeField]
    Canvas bgCity2 = null;
    [SerializeField]
    Canvas bgCity3 = null;
    [SerializeField]
    Canvas bgCity4 = null;
    [SerializeField]
    Canvas bgCity5 = null;


    [SerializeField]
    Text scoreText = null;  // ★追加

    // ★追加
    int score = 0;
    private System.TimeSpan LeftTime;
    private System.DateTime EndTime;
    public Text LeftTimeText;
    private bool OnGame;
    public Text GameOverText;
    private int HPexp;
    public GameObject HomeBtn;
    private System.DateTime StartTime;
    private System.TimeSpan ChangeBgTime;
    private int BgNo;

    public int Score
    {
        set
        {
            score = Mathf.Clamp(value, 0, 9999999);
            scoreText.text = score.ToString();
        }
        get
        {
            return score;
        }
    }


    void Start()
    {
        player.gameObject.SetActive(false);
        SetActiveSpawners(false);

        // タイトル画面を表示
        gameStartCanvas.gameObject.SetActive(true);
        // ゲームオーバー画面を非表示
        gameOverCanvas.gameObject.SetActive(false);
        // 背景画面を非表示
        bgCity1.gameObject.SetActive(false);
        bgCity2.gameObject.SetActive(false);
        bgCity3.gameObject.SetActive(false);
        bgCity4.gameObject.SetActive(false);
        bgCity5.gameObject.SetActive(false);
        // ★追加
        Score = 0;

        //LeftTime = new System.TimeSpan(0, 0, 5 * Location.encountNo); //検証用で一定時間のシューティングできるようにしている
        LeftTime = new System.TimeSpan(0, 0, 5 * 5);
        ChangeBgTime = new System.TimeSpan(0, 0, 5 * 1);


        OnGame = false;
        HPexp = PlayerPrefs.GetInt("HPexp");

    }

    public void GameStart()
    {

        // ★追加。スコアをリセットする
        Score = 0;

        // タイトル画面やゲームオーバー画面を非表示
        gameStartCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);
        HomeBtn.SetActive(false);

        // 背景画面を表示
        bgCity1.gameObject.SetActive(true);

        player.gameObject.SetActive(true);

        // スポナーを起動
        SetActiveSpawners(true);

        // Location.encountNo = 0;
        // PlayerPrefs.SetInt("encountNo", Location.encountNo);
        // PlayerPrefs.Save();

        EndTime = System.DateTime.Now + LeftTime;
        OnGame = true;
        StartTime = System.DateTime.Now;
        BgNo = 1;
    }

    public void GameOver()
    {
        OnGame = false;
        // ゲームオーバー画面の表示
        gameOverCanvas.gameObject.SetActive(true);
        // 背景画面を非表示
        bgCity1.gameObject.SetActive(false);
        bgCity2.gameObject.SetActive(false);
        bgCity3.gameObject.SetActive(false);
        bgCity4.gameObject.SetActive(false);
        bgCity5.gameObject.SetActive(false);

        // 画面上に残っている隕石を削除
        GameObject[] meteors = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject meteor in meteors)
        {
            Destroy(meteor);
        }

        GameOverText.text = "ゲームーオーバー！\n" + "HP経験値: " + ((int)Mathf.Ceil((float)Score / 1000f)).ToString() + "を獲得";
        
        HPexp = HPexp + (int)Mathf.Ceil((float)Score / 1000f); // 1000点毎に1exp入手、 は数切り上げ
        PlayerPrefs.SetInt("HPexp", HPexp);
        PlayerPrefs.Save();

        // スポナーを停止
        SetActiveSpawners(false);
    }

    void SetActiveSpawners(bool value)
    {
        foreach (MeteorSpawner spawner in spawners)
        {
            spawner.isActive = value;
        }
    }


    public void GoHome()
    {
        SceneManager.LoadScene("HomeScene");
    }

    void Update()
    {
        if(OnGame)
        {

            LeftTimeText.text =
            "残り時間 " + (EndTime - System.DateTime.Now).ToString("ss");

            if(EndTime <= System.DateTime.Now)
            {
                GameOver();
            }

            if (System.DateTime.Compare(StartTime + ChangeBgTime, System.DateTime.Now) < 0)
            {
                BgNo = BgNo + 1;
                StartTime = System.DateTime.Now;
            }


            if(BgNo ==2)
            {
                bgCity1.gameObject.SetActive(false);
                bgCity2.gameObject.SetActive(true);
                bgCity3.gameObject.SetActive(false);
                bgCity4.gameObject.SetActive(false);
                bgCity5.gameObject.SetActive(false);
            }
            else if(BgNo == 3)
            {
                bgCity1.gameObject.SetActive(false);
                bgCity2.gameObject.SetActive(false);
                bgCity3.gameObject.SetActive(true);
                bgCity4.gameObject.SetActive(false);
                bgCity5.gameObject.SetActive(false);
            }
            else if(BgNo ==4)
            {
                bgCity1.gameObject.SetActive(false);
                bgCity2.gameObject.SetActive(false);
                bgCity3.gameObject.SetActive(false);
                bgCity4.gameObject.SetActive(true);
                bgCity5.gameObject.SetActive(false);
            }
            else if(BgNo == 5)
            {
                bgCity1.gameObject.SetActive(false);
                bgCity2.gameObject.SetActive(false);
                bgCity3.gameObject.SetActive(false);
                bgCity4.gameObject.SetActive(false);
                bgCity5.gameObject.SetActive(true);
            }
            else
            {
                bgCity1.gameObject.SetActive(true);
                bgCity2.gameObject.SetActive(false);
                bgCity3.gameObject.SetActive(false);
                bgCity4.gameObject.SetActive(false);
                bgCity5.gameObject.SetActive(false);
                BgNo = 1;
            }



        }
        else
        {
            LeftTimeText.text =
            "残り時間 " + (LeftTime).ToString("ss");
        }


    }

}
