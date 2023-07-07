using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    
    void Start()
    {
        this.gameObject.SetActive(false);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }
}
