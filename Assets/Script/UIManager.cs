using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] livesSprite;
    [SerializeField]
    private Image livesImage;
    [SerializeField]
    private GameObject Player;
    void Update()
    {
        livesImage.sprite = livesSprite[Player.gameObject.GetComponent<Player>().lives]; 
    }
}
