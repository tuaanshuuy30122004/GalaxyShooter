using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShoot : MonoBehaviour
{
    private float speed = 3;
    [SerializeField] private AudioSource Power;
    void Update()
    {
        if (transform.position.y < -5f)
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
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    public void PlaySound()
    {
        Power.Play();
    }
}
