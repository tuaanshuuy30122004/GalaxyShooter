using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float speed = 1f;
    private Animator animator;
    [SerializeField]
    private AudioSource Explosion;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
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
            collision.gameObject.GetComponent<Player>().Damage();
            speed = 0f;
            animator.SetTrigger("AsteroidDestroy");
            Explosion.Play();
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
            Destroy(this.gameObject, 0.8f);
        }

        if(collision.gameObject.tag =="Laser")
        {
            speed = 0f;
            animator.SetTrigger("AsteroidDestroy");
            Explosion.Play();
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
            Destroy(collision.gameObject);
            Destroy(this.gameObject, 0.8f);
            
        }
    }
}
