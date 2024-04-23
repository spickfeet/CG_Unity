using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSFX;
    [SerializeField] private AudioSource _audioSource;
    public void Escape()
    {
        _audioSource.PlayOneShot(_clickSFX);
        Application.Quit();
    }
    public void Play()
    {
        _audioSource.PlayOneShot(_clickSFX);
        SceneManager.LoadScene("SampleScene");
    }
}
