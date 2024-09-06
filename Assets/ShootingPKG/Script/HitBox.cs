using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public ShootingManager shootingManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Bulletタグを持つゲームオブジェクトに当たったらHPを1引く
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Damage(1);
            // shootingManager.HP -= collision.gameObject.GetComponent<MonsterController>().damage;
        } 
        // else if (collision.gameObject.CompareTag("DamageArea"))
        // {
        //     // GameObject.FindGameObjectWithTag("GameController").GetComponent<ShootingManager>().HP += -1 * damage;
        //     // Debug.Log("Damage" + GameObject.FindGameObjectWithTag("GameController").GetComponent<ShootingManager>().HP.ToString());
        //     Destroy(gameObject);
        // }
    }
}
