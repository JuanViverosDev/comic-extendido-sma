using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public UnityEvent OnAudioStarted;
    public UnityEvent OnAudioFinished;

    public void PlayClickSound(AudioClip clip)
    {
        if (audioSource == null)
        {
            Debug.LogError("El audio no está asignado.");
            return;
        }

        audioSource.clip = clip;

        audioSource.Play();

        OnAudioStarted?.Invoke();
        //Debug.Log("El audio ha comenzo a reproducirse.");

        StartCoroutine(OnFinished());
    }

    private IEnumerator OnFinished()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        //Debug.Log("El audio ha finalizado.");
        OnAudioFinished?.Invoke();
    }
}