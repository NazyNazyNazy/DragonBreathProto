using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField]
    Rigidbody2D rigidBody = null;
    [SerializeField]
    float speed = 15;

    void Start()
    {
        rigidBody.velocity = transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Enemyタグをもつゲームオブジェクト（＝隕石）に当たったら消滅
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        // カメラに映らなくなったら消滅
        Destroy(gameObject);
    }

}