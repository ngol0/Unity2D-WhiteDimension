using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;

    private void Start() 
    {
        GameManager.Instance.HasWin += SetWinSprite;
    }

    void SetWinSprite()
    {
        playerAnimator.SetBool("isWin", true);
    }
}
