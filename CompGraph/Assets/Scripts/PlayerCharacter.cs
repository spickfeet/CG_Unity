using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSFX;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private TMP_Text _healthLabel;
    private int _health;
    void Start()
    {
        _health = 3;
        _healthLabel.text = _health.ToString();
    }
    public void Hurt(int damage)
    {
        _audioSource.PlayOneShot(_clickSFX);
        _health -= damage;
        _healthLabel.text = _health.ToString();
        if (_health <= 0)
        {
            SceneManager.LoadScene("Death");
        }
    }
}
