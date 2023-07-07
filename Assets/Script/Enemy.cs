using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public Player player;
    [SerializeField] private GameObject laserPrefabs;
    [SerializeField] private AudioSource Explosion;
    public int lives = 2;
    private Animator animator;
    private bool shouldFire = true;
    
    void Start()
    {
        shouldFire = true;
        StartCoroutine(FireCroutine());
        animator = GetComponent<Animator>();    
    }

    
    void Update()
    {
        if(transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            MoveDown();
        }
    }

    void MoveDown()
    {
        this.transform.Translate(Vector3.down * (speed + Time.time / 50) * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player") 
        {
            other.gameObject.GetComponent<Player>().Damage();                     
            speed = 0f;
            shouldFire = false;
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            Explosion.Play();
            animator.SetTrigger("EnemyDeath");
            Destroy(this.gameObject, 2.3f);
        }
        else if(other.tag == "Laser" || other.tag == "ShieldInstance")
        {
            Damage();
            //Destroy(this.gameObject);
            Destroy(other.gameObject);
            
        }
    }
   
    IEnumerator FireCroutine()
    {
        while (shouldFire)
        {
            Instantiate(laserPrefabs, this.transform.position + new Vector3(0, -1f, 0), Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }
    public void Damage()
    {
        lives--;
        if(lives<1)
        {
            shouldFire = false;
            speed = 0f;
            animator.SetTrigger("EnemyDeath");
            Explosion.Play();
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            Destroy(this.gameObject, 2.3f);
        }
    }
}
