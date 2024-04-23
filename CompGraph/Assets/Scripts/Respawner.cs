using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawner : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSFX;
    [SerializeField] private AudioSource _audioSource;

    void Start()
    {
        _audioSource.PlayOneShot(_clickSFX);
        StartCoroutine(Respawn());
    }
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("SampleScene");
    }
}
