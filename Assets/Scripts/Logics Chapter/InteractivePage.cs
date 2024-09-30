using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[System.Serializable]
public struct UIInteractive
{
    public GameObject interactivepanel;
    public GameObject infoPanel;
    public TextMeshProUGUI questiontext;
    public TextMeshProUGUI titletext;
    public TextMeshProUGUI contenttext;
    public GameObject contentOptions;
    public GameObject optionPrefab;
    public Button confirmBttn;
    public Button infoBttn;

}

[System.Serializable]
public struct UIRetro
{
    public GameObject mainPanel;
    public TextMeshProUGUI titletext;
    public TextMeshProUGUI contenttext;

}

[System.Serializable]
public struct UIConfirm
{
    public GameObject mainPanel;
    public Button confirmBttn;
    public TextMeshProUGUI contenttext;
}

public class InteractivePage : MonoBehaviour
{
    public UIInteractive uIInteractive;
    public UIRetro uIRetro;
    public UIConfirm uIConfirm;
    private Book book;
    private BookTimeLine bookTimeLine;
    private Decision decision;
    private ToggleGroup toggles;
    // Start is called before the first frame update
    void Start()
    {
        book = transform.GetComponent<Book>();
        bookTimeLine = transform.GetComponent<BookTimeLine>();
        toggles = uIInteractive.contentOptions.GetComponent<ToggleGroup>();
    }

    public void StartInteractivty(Decision d)
    {
        DeleteNextPages(d);
        decision = d;
        uIInteractive.infoBttn.onClick.AddListener(() => uIInteractive.infoPanel.SetActive(false));
        uIInteractive.interactivepanel.SetActive(true);
        uIInteractive.questiontext.text = d.text;
        DeleteAllChildren(uIInteractive.contentOptions.transform);
        for (int i = 0; i < d.options.Count; i++)
        {
            int index = i;
            GameObject newChild = Instantiate(uIInteractive.optionPrefab);
            var text = newChild.GetComponentInChildren<TextMeshProUGUI>();
            if(text != null)
            {
                text.text = d.options[i].optionLabel;
            }
            newChild.transform.GetChild(1).transform.GetComponent<Button>().onClick.AddListener(() => SetButton(d.options[index]));
            newChild.transform.SetParent(uIInteractive.contentOptions.transform);
            var toogle = newChild.transform.GetComponentInChildren<Toggle>();
            if(toogle != null)
            {
                
                toggles.RegisterToggle(toogle);
                toogle.group = toggles;
                toogle.onValueChanged.AddListener(state => handToggle(state,toogle, d.options[index]));
            }
            newChild.transform.localScale = Vector3.one;

        }

        uIInteractive.confirmBttn.onClick.AddListener(()=> CheckOption(d));
        

    }

    public void SetButton(DecisionOption d)
    {
        uIInteractive.infoPanel.SetActive(true);
        uIInteractive.titletext.text = d.optionLabel;
        uIInteractive.contenttext.text = d.contentLabel;
    }

    public void DeleteAllChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }

    void CheckOption(Decision d)
    {

        if (toggles.ActiveToggles().Count() == 1 && decision != null)
        {
            var index = FindObjectsOfType<Toggle>().Reverse().ToList().FindIndex(t => t.isOn); ;
            uIConfirm.confirmBttn.onClick.RemoveAllListeners();
            uIConfirm.contenttext.text = d.options[index].optionLabel;
            uIConfirm.mainPanel.SetActive(true);

            if (string.IsNullOrEmpty(d.options[index].retroalimentation))
            {
                uIConfirm.confirmBttn.onClick.AddListener(HandDecision);
            }
            else
            {
                uIConfirm.confirmBttn.onClick.AddListener(()=>
                uIRetro.mainPanel.SetActive(true));
                uIRetro.titletext.text = d.options[index].optionLabel;
                uIRetro.contenttext.text = d.options[index].retroalimentation;
            }

            bookTimeLine.decisionManager.MakeDecision(d.id, d.options[index].OptionID);
        }
    }

    public void HandDecision()
    {
        uIConfirm.mainPanel.SetActive(false);
        uIInteractive.interactivepanel.SetActive(false);

        if (book.currentPage < book.bookPages.Count - 1)
        {
            book.FlipToPage(book.currentPage + 1);
        }
        else
            bookTimeLine.handFinal();

    }


    void handToggle(bool state, Toggle t, DecisionOption d)
    {
        if (t.isOn)
        {
            foreach (var toggle in toggles.ActiveToggles())
            {
                if (toggle != t)
                {
                    toggle.isOn = false;
                }
            }
        }

        if (state)
            CreateNextPagesByOption(d);
        else
            DeleteNextPagesByOption(d);
    }

    void CreateNextPagesByOption(DecisionOption d)
    {
        bookTimeLine.pages.AddRange(d.pages);
        var sprites = d.pages.Select(p => p.background);
        book.bookPages.AddRange(sprites);
    }

    void DeleteNextPagesByOption(DecisionOption d)
    {
        bookTimeLine.pages.RemoveAll(p => d.pages.Contains(p));
        var sprites = d.pages.Select(p => p.background);
        book.bookPages.RemoveAll(sp => sprites.Contains(sp));
    }

    void DeleteNextPages(Decision d)
    {
        var pages = d.options.SelectMany(op => op.pages).ToList();
        var sprites = d.options.SelectMany(op => op.pages.Select(p => p.background));
        book.bookPages.RemoveAll(sp => sprites.Contains(sp));
        bookTimeLine.pages.RemoveAll(p => pages.Contains(p));
    }
}
