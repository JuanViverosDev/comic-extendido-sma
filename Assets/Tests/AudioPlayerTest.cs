using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;
using System.Collections;

public class AudioPlayerTests
{
    private GameObject _gameObject;
    private AudioPlayer _audioPlayer;
    private AudioClip _testClip;

    private bool _audioStarted;
    private bool _audioFinished;

    [SetUp]
    public void Setup()
    {
        _gameObject = new GameObject();
        _audioPlayer = _gameObject.AddComponent<AudioPlayer>();
        _audioPlayer.audioSource = _gameObject.AddComponent<AudioSource>();

        _gameObject.AddComponent<AudioListener>();

        _testClip = AudioClip.Create("TestClip", 44100, 1, 44100, false);

        _audioStarted = false;
        _audioFinished = false;

        _audioPlayer.OnAudioStarted = new UnityEvent();
        _audioPlayer.OnAudioStarted.AddListener(() => _audioStarted = true);

        _audioPlayer.OnAudioFinished = new UnityEvent();
        _audioPlayer.OnAudioFinished.AddListener(() => _audioFinished = true);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(_gameObject);
    }

    [UnityTest]
    public IEnumerator PlayClickSound_ShouldInvokeOnAudioStarted()
    {
        _audioPlayer.PlayClickSound(_testClip);

        yield return null;
        Assert.IsTrue(_audioStarted, "OnAudioStarted no fue invocado.");
    }

    [UnityTest]
    public IEnumerator PlayClickSound_ShouldInvokeOnAudioFinished()
    {
        _audioPlayer.audioSource.clip = _testClip;
        _audioPlayer.audioSource.clip.SetData(new float[44100], 0); // 1 segundo de duración

        _audioPlayer.PlayClickSound(_testClip);

        yield return new WaitForSeconds(1); // Esperar a que termine el clip (1 segundo)
        Assert.IsTrue(_audioFinished, "OnAudioFinished no fue invocado.");
    }
}
