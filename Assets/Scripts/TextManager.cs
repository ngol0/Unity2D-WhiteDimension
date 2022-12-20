using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TextManager : MonoBehaviour
{
    [SerializeField] TextSO textData;
    [SerializeField] TextEntryUI textEntryUI;

    Queue<TextEntry> textQueue = new Queue<TextEntry>();

    bool turnedTextOff = true;
    [SerializeField] bool isInGame;

    public void AddToQueue()
    {
        foreach (var entry in textData.textEntries)
        {
            textQueue.Enqueue(entry);
        }
    }

    private void OnEnable()
    {
        textEntryUI.OnTextHide += NextMove;
    }

    void NextMove()
    {
        turnedTextOff = true;

        if (IsLastEntry())
        {
            if (isInGame) return;
            StartCoroutine(StartScene());
        }
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene("Level1");
    }

    void Update()
    {
        DequeueText();
    }

    private void DequeueText()
    {
        if (textQueue.Count > 0 && turnedTextOff)
        {
            var nextEntry = textQueue.Dequeue();
            textEntryUI.SetUpText(nextEntry);
            turnedTextOff = false;
            StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(0.8f);
        textEntryUI.Show();
        textEntryUI.Animate();
    }

    private bool IsLastEntry()
    {
        return textQueue.Count == 0;
    }
}
