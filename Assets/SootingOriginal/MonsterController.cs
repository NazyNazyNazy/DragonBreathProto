using UnityEngine;
using TMPro;

public class MonsterController : MonoBehaviour
{

    [SerializeField]
    Rigidbody2D rigidBody = null;
    [SerializeField]
    GameObject explosionPrefab = null;

    [Min(1), Space]
    public int hp = 1;
    public float speed = 5;
    [Min(0)]
    public int score = 100;

    bool isVisible = false;

    void Start()
    {
        rigidBody.velocity = - transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Bulletタグを持つゲームオブジェクトに当たったらHPを1引く
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Damage(1);
        }
    }

    void OnBecameVisible()
    {
        isVisible = true;
    }

    void OnBecameInvisible()
    {
        // 画面外に出たら消滅
        if (isVisible)
        {
            Destroy(gameObject);
        }
    }

    void Damage(int value)
    {
        if (value <= 0)
        {
            return;
        }

        hp -= value;

        if (hp <= 0)
        {

            // ★追加2
            GameObject.FindGameObjectWithTag("GameController").GetComponent<ShootingManager>().Score += score;

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

}