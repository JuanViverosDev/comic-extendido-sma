using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[System.Serializable]
public struct UIInteractive
{
    public GameObject interactivepanel;
    public TextMeshProUGUI questiontext;
    public GameObject contentOptions;
    public GameObject optionPrefab;
    public Button confirmBttn;

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
            newChild.transform.SetParent(uIInteractive.contentOptions.transform);
            newChild.transform.localScale = Vector3.one;
        }
        uIInteractive.confirmBttn.onClick.AddListener(CheckOption);
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
