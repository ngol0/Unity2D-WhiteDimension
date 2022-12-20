using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] TextManager textManager;
    int currentIndex;

    private void Awake() 
    {
        
    }

    private void Start() 
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex != 0) 
        {
            GameManager.Instance.HasWin += ShowMessage;
            return;
        }
        ShowMessage();
    }

    private void ShowMessage()
    {
        textManager.AddToQueue();
    }
}
