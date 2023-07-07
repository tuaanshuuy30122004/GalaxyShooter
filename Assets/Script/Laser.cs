using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 8f;
    void Update()
    {
        MoveUp();
    }

    void MoveUp()
    {
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);

        if(this.transform.position.y > 15)
        {
            Destroy(this.gameObject);
        }
    }
}
