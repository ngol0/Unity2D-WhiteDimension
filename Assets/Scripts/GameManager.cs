using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] Slider colorSlider;

    [SerializeField] float percentOfScore = 0.01f;
    private void Awake() 
    {
        Instance = this;
    }
    
    private void Start() 
    {
        colorSlider.value = 0f;
    }

    public void OnGetScore()
    {
        colorSlider.value += percentOfScore;
    }

    public void OnLoseGame()
    {
        Debug.Log("::::Player has died - game over");
    }
}
