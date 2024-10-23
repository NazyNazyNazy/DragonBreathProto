using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody = null;
    [SerializeField] GameObject explosionPrefab = null;
    [Min(1), Space] public int hp = 1;
    public float speed = 5;
    [Min(0)] public int score = 100;
    bool isVisible = false;
    public int damage;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ChangeDirection());
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = ((Vector2)player.transform.position - rigidBody.position).normalized;
            Vector2 wander = new Vector2(Mathf.Sin(Time.time * speed), Mathf.Cos(Time.time * speed)) * 0.5f;  // 括弧を追加
            rigidBody.velocity = direction * speed + wander;
        }
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            // 随時速度を変更しない
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Damage(1);
        }
        else if (collision.gameObject.CompareTag("DamageArea"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<ShootingManager>().HP += -1 * damage;
            Destroy(gameObject);
        }
    }

    void OnBecameVisible()
    {
        isVisible = true;
    }

    void OnBecameInvisible()
    {
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
            GameObject.FindGameObjectWithTag("GameController").GetComponent<ShootingManager>().Score += score;
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
