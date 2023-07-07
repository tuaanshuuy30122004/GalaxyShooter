using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : MonoBehaviour
{
    private float speed = 4f;
    void Update()
    {
        MoveDown();
    }
    void MoveDown()
    {
        this.transform.Translate(Vector3.down * (speed + Time.time/50) * Time.deltaTime);

        if (this.transform.position.y > 15)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<Player>().Damage();
        }
    }
}
