using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool isActive = true;

    [Header("必要なコンポーネントを登録")]
    [SerializeField]
    Rigidbody2D rigidBody = null;
    [SerializeField]
    Transform bulletSpawn = null;
    [SerializeField]
    ShootingManager gameManager = null;
    [SerializeField]
    GameObject explosionPrefab = null; // ★追加その２

    [Header("移動設定")]
    [SerializeField]
    float powerToMove = 10;

    [Header("射撃設定")]
    [SerializeField]
    GameObject bulletPrefab = null;
    [SerializeField, Min(0)]
    float fireInterval = 0.5f;

    [Header("入力設定")]
    [SerializeField]
    string verticalButtonName = "Vertical";
    [SerializeField]
    string fireButtonName = "Fire1";

    bool fire = false;
    bool firing = false;
    float forwardInput;
    Vector2 mousePos;
    WaitForSeconds fireIntervalWait;
    Camera mainCamera;
    Transform thisTransform;
    Transform mainCameraTransform;

    void Start()
    {
        // transformでトランスフォームを参照すると少しだけ重いので、キャッシュしておく
        thisTransform = transform;
        mainCamera = Camera.main;
        mainCameraTransform = mainCamera.transform;

        // コルーチンの停止処理をキャッシュしておく
        // こうするとメモリにゴミが発生しづらくなり高速化できる
        fireIntervalWait = new WaitForSeconds(fireInterval);
    }

    void OnDisable()
    {
        StopCoroutine(nameof(Fire));
        firing = false;
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }

        GetInput();

        if (fire && !firing)
        {
            StartCoroutine(nameof(Fire));
        }
    }

    void FixedUpdate()
    {
        if (!isActive)
        {
            return;
        }

        MovePlayer();
    }

    void GetInput()
    {
        // 移動
        forwardInput = Input.GetAxis(verticalButtonName);

        // 方向
        // マウス座標（スクリーン座標）を取得し、ワールド座標に変換する
        Vector3 screenMousePos = Input.mousePosition;
        screenMousePos.z = mainCameraTransform.position.z;
        mousePos = mainCamera.ScreenToWorldPoint(screenMousePos);

        // 射撃
        fire = Input.GetButton(fireButtonName);
    }

    void MovePlayer()
    {
        // なめらかにマウスの方向を向く
        thisTransform.rotation = Quaternion.Lerp(thisTransform.rotation, Quaternion.LookRotation(Vector3.forward, (Vector3)mousePos - thisTransform.position), 0.1f);

        // 移動
        rigidBody.AddForce(thisTransform.up * forwardInput * powerToMove * rigidBody.mass, ForceMode2D.Force);
    }

    IEnumerator Fire()
    {
        firing = true;

        // 弾のゲームオブジェクトを生成　レベルの数だけ球が増える　＋そのうちレベルに合わせてタマのタイプが変わるようにする
        for (int i = 0; i < DriveDragon.Level; i++){
            Instantiate(bulletPrefab, bulletSpawn.position, thisTransform.rotation);
        }
        // Instantiate(bulletPrefab, bulletSpawn.position, thisTransform.rotation);

        yield return fireIntervalWait;

        firing = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // ★追加その2
            Instantiate(explosionPrefab, thisTransform.position, Quaternion.identity);
            gameManager.GameOver();
            gameObject.SetActive(false);
        }
    }

}