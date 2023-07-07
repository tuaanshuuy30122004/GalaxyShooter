using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3.5f;
    private float canFire = 0f;
    [SerializeField]
    private float cdTime = 0.2f;
    public int lives = 3;
    [SerializeField]
    private GameObject SpawnManager;
    [SerializeField]
    private GameObject laserPrefabs;
    [SerializeField]
    private GameObject shieldPrefabs;
    [SerializeField]
    private GameObject GameoverPanel;
    [SerializeField]
    private GameObject FireWingLeft;
    [SerializeField]
    private GameObject FireWingRight;
    [SerializeField]
    private AudioSource LaserSound;
    public float SpeedBoost = 1;
    private bool isSpeedBoostEnable = false;
    private bool isTripleShootEnable = false;
    private bool isShieldEnable = false;
    private GameObject ShieldInstance;
    
    

    
    void Update()
    {
        FireLaser();
        Movement();
        MoveShield();
        
       
    }

    public void Movement()
    {
        float Ypos = Input.GetAxis("Vertical");
        float Xpos = Input.GetAxis("Horizontal");

        this.transform.Translate(new Vector3(Xpos, Ypos, 0) * speed * Time.deltaTime * SpeedBoost);

    }

    void FireLaser()
    {
        if (Input.GetKeyUp(KeyCode.Space) && Time.time > canFire)
        {
            
           Instantiate(laserPrefabs, this.transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);   
           LaserSound.Play();
           canFire = Time.time + cdTime;           
        }
    }

    public void Damage()
    {
        if(isShieldEnable)
        {
            isShieldEnable = false;
            Destroy(ShieldInstance.gameObject);
        }
        else
        {
            lives--;
            
        }
        if(lives == 2)
            FireWingLeft.gameObject.SetActive(true);
        if(lives == 1)
            FireWingRight.gameObject.SetActive(true);
        
        if (lives == 0)
        {
            this.gameObject.SetActive(false);
            GameoverPanel.gameObject.SetActive(true);
            Time.timeScale = 0;           
            SpawnManager.gameObject.GetComponent<SpawnManager>().SetShouldSpawn(false);
            
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpeedBoost")
        {
            SpeedBoost = 2f;            
            isSpeedBoostEnable = true;
            collision.gameObject.GetComponent<SpeedBoost>().PlaySound();
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(collision.gameObject,1);
            StartCoroutine(DisnableSpeedBoost());
        }

        if(collision.gameObject.tag == "TripleShoot")
        {
            isTripleShootEnable = true;
            collision.gameObject.GetComponent<TripleShoot>().PlaySound();
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(collision.gameObject,1);            
            StartCoroutine(DisnableTripleShoot());
            StartCoroutine(TripleShoot());        
        }

        if(collision.gameObject.tag == "Shield")
        {
            collision.gameObject.GetComponent<SpeedBoost>().PlaySound();
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            if (isShieldEnable)
            {
                Destroy(ShieldInstance.gameObject);
            }
            isShieldEnable = true;
            ShieldInstance = Instantiate(shieldPrefabs);
            Destroy(collision.gameObject,1);
            if (isShieldEnable)
            StartCoroutine(DisableShield());    
        }
        
    }

    IEnumerator DisnableSpeedBoost()
    {
        while(isSpeedBoostEnable)
        {
            yield return new WaitForSeconds(5f);
            SpeedBoost = 1f;
            isSpeedBoostEnable = false;
        }
        
    }

    IEnumerator DisnableTripleShoot()
    {
        yield return new WaitForSeconds(10f);
        isTripleShootEnable = false;
    }

    IEnumerator TripleShoot()
    {
        while(isTripleShootEnable)
        {
            yield return new WaitForSeconds(0.05f);
            Instantiate(laserPrefabs, this.transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            LaserSound.Play();
        }
        

    }

    IEnumerator DisableShield()
    {
        while(isShieldEnable)
        {
            yield return new WaitForSeconds(10f);
            isShieldEnable = false;
            Destroy(ShieldInstance.gameObject);
        }
        
    }
    void MoveShield()
    {
        if (ShieldInstance != null && isShieldEnable)
        {
            ShieldInstance.transform.position = this.transform.position;
        }
    }

    
}
