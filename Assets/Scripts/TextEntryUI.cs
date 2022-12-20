using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextEntryUI : MonoBehaviour
{
    private Sequence sequence;
    [SerializeField] TextMeshProUGUI text;

    [Header("Stats")]
    [SerializeField] float fadeTo = 1f;
    [SerializeField] float fadeDuration = 0.5f;
    [SerializeField] float moveTo = 1f;
    [SerializeField] float moveDuration = 1f;
    [SerializeField] float showTime = 1.5f;

    public System.Action OnTextHide;

    public void Animate()
    {
        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence
            .Append(text.DOFade(fadeTo, fadeDuration))
            //Join(text.transform.DOLocalMoveY(moveTo, moveDuration))
            .AppendInterval(showTime)
            .Append(text.DOFade(0, fadeDuration))
            .OnComplete(Hide);
    }

    public void Hide()
    {
        text.gameObject.SetActive(false);
        OnTextHide?.Invoke();
    }

    public void Show()
    {
        text.gameObject.SetActive(true);
    }

    public void SetUpText(TextEntry entry)
    {
        text.text = entry.textEntry;
    }
}
