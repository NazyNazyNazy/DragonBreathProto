using UnityEngine;
using UnityEngine.UI;  // ★追加

public class GameManager2 : MonoBehaviour
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
    Canvas citybgCanvas = null;
    [SerializeField]
    Canvas seabgCanvas = null;
    [SerializeField]
    Canvas mountainbgCanvas = null;
    [SerializeField]
    Text scoreText = null;  // ★追加

    // ★追加
    int score = 0;
    private int lenth;



    // ★追加
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
        //lenth = 

        player.gameObject.SetActive(false);
        SetActiveSpawners(false);

        // タイトル画面を表示
        gameStartCanvas.gameObject.SetActive(true);
        // ゲームオーバー画面を非表示
        gameOverCanvas.gameObject.SetActive(false);
        // 背景画面を非表示
        citybgCanvas.gameObject.SetActive(false);
        seabgCanvas.gameObject.SetActive(false);
        mountainbgCanvas.gameObject.SetActive(false);

        // ★追加
        Score = 0;

    }

    public void GameStart()
    {

        // ★追加。スコアをリセットする
        Score = 0;

        // タイトル画面やゲームオーバー画面を非表示
        gameStartCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);

        // 背景画面を表示
        //seabgCanvas.gameObject.SetActive(true);
        seabgCanvas.gameObject.SetActive(true);

        // 画面上に残っている隕石を削除
        //GameObject[] meteors = GameObject.FindGameObjectsWithTag("Enemy");
        //foreach (GameObject meteor in meteors)
        //{
        //    Destroy(meteor);
        //}

        // プレイヤーの座標を原点に戻し、アクティブ状態にする
        //player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);

        // スポナーを起動
        SetActiveSpawners(true);
    }

    public void GameStart1()
    {

        // ★追加。スコアをリセットする
        Score = 0;

        // タイトル画面やゲームオーバー画面を非表示
        gameStartCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);

        // 背景画面を表示
        citybgCanvas.gameObject.SetActive(true);

        // 画面上に残っている隕石を削除
        //GameObject[] meteors = GameObject.FindGameObjectsWithTag("Enemy");
        //foreach (GameObject meteor in meteors)
        //{
        //    Destroy(meteor);
        //}

        // プレイヤーの座標を原点に戻し、アクティブ状態にする
        //player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);

        // スポナーを起動
        SetActiveSpawners(true);
    }

    public void GameStart2()
    {

        // ★追加。スコアをリセットする
        Score = 0;

        // タイトル画面やゲームオーバー画面を非表示
        gameStartCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);

        // 背景画面を表示
        mountainbgCanvas.gameObject.SetActive(true);

        // 画面上に残っている隕石を削除
        //GameObject[] meteors = GameObject.FindGameObjectsWithTag("Enemy");
        //foreach (GameObject meteor in meteors)
        //{
        //    Destroy(meteor);
        //}

        // プレイヤーの座標を原点に戻し、アクティブ状態にする
        //player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);

        // スポナーを起動
        SetActiveSpawners(true);
    }

    public void GameStart3()
    {

        player.gameObject.SetActive(false);
        SetActiveSpawners(false);

        // タイトル画面を表示
        gameStartCanvas.gameObject.SetActive(true);
        // ゲームオーバー画面を非表示
        gameOverCanvas.gameObject.SetActive(false);
        // 背景画面を非表示
        citybgCanvas.gameObject.SetActive(false);
        seabgCanvas.gameObject.SetActive(false);
        mountainbgCanvas.gameObject.SetActive(false);

        // ★追加
        Score = 0;
    }

    public void GameOver()
    {
        // ゲームオーバー画面の表示
        gameOverCanvas.gameObject.SetActive(true);
        // 背景画面を非表示
        seabgCanvas.gameObject.SetActive(false);
        citybgCanvas.gameObject.SetActive(false);
        mountainbgCanvas.gameObject.SetActive(false);

        // 画面上に残っている隕石を削除
        GameObject[] meteors = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject meteor in meteors)
        {
           Destroy(meteor);
        }

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

}