using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TempDialog : MonoBehaviour
{
    public float time;
    public UnityEvent onFinish;
    void Start()
    {
        StartCoroutine(showTemp());
    }

    IEnumerator showTemp()
    {
        yield return new WaitForSeconds(time);
        onFinish?.Invoke();
        gameObject.SetActive(false);
    }
}
