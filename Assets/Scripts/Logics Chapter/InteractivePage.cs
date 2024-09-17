using System.Collections;
using System.Collections.Generic;
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

public class InteractivePage : MonoBehaviour
{
    public UIInteractive uIInteractive;
    private Book book;
    // Start is called before the first frame update
    void Start()
    {
        book = transform.GetComponent<Book>();
    }

    public void StartInteractivty(Decision d)
    {
        uIInteractive.infoBttn.onClick.AddListener(() => uIInteractive.infoPanel.SetActive(false));
        uIInteractive.interactivepanel.SetActive(true);
        uIInteractive.questiontext.text = d.text;
        DeleteAllChildren(uIInteractive.contentOptions.transform);
        foreach(DecisionOption op in d.options)
        {
            GameObject newChild = Instantiate(uIInteractive.optionPrefab);
            var text = newChild.GetComponentInChildren<TextMeshProUGUI>();
            if(text != null)
            {
                text.text = op.optionLabel;
            }
            newChild.transform.GetChild(1).transform.GetComponent<Button>().onClick.AddListener(() => SetButton(op));
            newChild.transform.SetParent(uIInteractive.contentOptions.transform);
            newChild.transform.localScale = Vector3.one;
        }
        uIInteractive.confirmBttn.onClick.AddListener(CheckOption);
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

    public void CheckOption()
    {

    }

}
