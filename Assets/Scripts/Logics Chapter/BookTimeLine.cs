using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


[Serializable]
public struct UIDialog
{
    public GameObject Normalpanel;
    public GameObject Narrativepanel;
    public TextMeshProUGUI panelTextName;
    public TextMeshProUGUI panelTextContent;
    public TextMeshProUGUI panelTextNarrative;
    public TextMeshProUGUI indexDialog;
    public Image otherImage;
    public Image iconCharacter;
    public List<Image> imageCharacters;
}

public class BookTimeLine : MonoBehaviour
{
    private Book book;
    private InteractivePage interactivePage;
    [Range(0.01f, 0.1f)]
    public float speed = 0.06f;
    public float wait = 1f;
    public UIDialog uiDialog;
    public Chapter chapter;
    public CharacterList characterList;
    private string currentId = "";
    private bool isShowing = false;
    private bool skip = false;
    private Coroutine coroutine = null;


    private void Start()
    {
        book = transform.GetComponent<Book>();
        interactivePage = transform.GetComponent<InteractivePage>();
    }

    public void HandTap()
    {
        if (isShowing)
        {
            skip = true;
        }
        else if (currentId != null)
        {
            SendDialog(currentId);
        }

    }

    public void BeginDialogs(int page)
    {
        if (chapter.pages.Count > 0)
        {
            if (chapter.pages[page].isInteractive)
            {
                var dc = chapter.pages[page].decisions;
                if (dc.Count > 0)
                    interactivePage.StartInteractivty(dc[0]);
            }
            else
            {
                var ds = chapter.pages[page].dialogs;
                if (ds.Count > 0)
                    SendDialog(ds[0].id);
            }

        }

    }



    public void SendDialog(string id)
    {

        Dialog d = chapter.pages[book.currentPage].dialogs.Find(d => d.id == id);

        if (d?.id != null && !isShowing)
        {
            uiDialog.indexDialog.text = $"{chapter.pages[book.currentPage].dialogs.IndexOf(d) + 1}/{chapter.pages[book.currentPage].dialogs.Count}";
            StartDialog(d);
            isShowing = true;
        }
    }


    public void Hide()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            isShowing = false;
        }
        uiDialog.Narrativepanel.SetActive(false);
        uiDialog.Normalpanel.SetActive(false);
        interactivePage.uIInteractive.interactivepanel.SetActive(false);
    }

    private void NormalDialog(int index, Dialog d)
    {
        uiDialog.panelTextName.text = characterList.characters[index].name;
        uiDialog.iconCharacter.sprite = characterList.characters[index].icon;
        for (int i = 0; i < 3; i++)
        {
            if (i < d.screenCharacters.Count)
            {

                uiDialog.imageCharacters[i].sprite = characterList.characters[((int)d.screenCharacters[i])].image;
                uiDialog.imageCharacters[i].preserveAspect = true;
                uiDialog.imageCharacters[i].enabled = true;
            }
            else
            {
                uiDialog.imageCharacters[i].enabled = false;
            }

        }
        coroutine = StartCoroutine(WriteText(d, uiDialog.panelTextContent));
    }

    private void MainDialog(Dialog d)
    {
        uiDialog.otherImage.gameObject.SetActive(false);
        coroutine = StartCoroutine(WriteText(d, uiDialog.panelTextNarrative, () =>
        {
            if (d.otherImage != null)
            {
                uiDialog.otherImage.gameObject.SetActive(true);
                uiDialog.otherImage.sprite = d.otherImage;
                uiDialog.otherImage.preserveAspect = true;
            }

        }));
    }

    private void StartDialog(Dialog d)
    {

        int cIndex = (int)d.DialogCharacter;

        if (cIndex >= 0)
        {
            uiDialog.Narrativepanel.SetActive(false);
            uiDialog.Normalpanel.SetActive(true);
            NormalDialog(cIndex, d);
        }
        else
        {
            uiDialog.Narrativepanel.SetActive(true);
            uiDialog.Normalpanel.SetActive(false);
            MainDialog(d);
        }
    }

    IEnumerator WriteText(Dialog d, TextMeshProUGUI panel, Action onFinish = null)
    {
        panel.text = "";
        foreach (char caracter in d.text)
        {
            panel.text += caracter;
            yield return new WaitForSeconds(speed);
            if (skip)
            {
                skip = false;
                break;
            }
        }
        panel.text = d.text;
        isShowing = false;

        if (onFinish != null)
        {
            onFinish();
        }

        currentId = d.nextId;

    }
   
}
