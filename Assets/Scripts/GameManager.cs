using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] Image colorSlider;
    [SerializeField] GameObject starfield;

    [SerializeField] float percentOfScore = 0.01f;

    public System.Action HasWin;
    [HideInInspector]
    public bool enemyStartShooting = false;
    private bool isWin = false;
    public bool Win => isWin;
    private void Awake() 
    {
        Instance = this;
    }
    
    private void Start() 
    {
        colorSlider.fillAmount = 0f;
        starfield.SetActive(false);
    }

    public void OnGetScore()
    {
        colorSlider.fillAmount += percentOfScore;
        enemyStartShooting = true;
        if (IsWin())
        {
            OnWin();
        }
    }

    public void OnLoseGame()
    {
        Debug.Log("::::Player has died - game over");
        StartCoroutine(OnLose());
    }

    private IEnumerator OnLose()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level1");
    }

    private bool IsWin()
    {
        return colorSlider.fillAmount == 1;
    }

    public void OnWin()
    {
        starfield.SetActive(true);
        enemyStartShooting = false;  
        isWin = true;
        HasWin?.Invoke();
    }
}
