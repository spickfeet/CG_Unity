using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreLabel;
    [SerializeField] private SettingsPopup _settingsPopup;

    [SerializeField] private AudioClip _clickSFX;
    [SerializeField] private AudioSource _audioSource;

    private int _score;
    void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }
    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }
    void Start()
    {
        _score = 0;
        _scoreLabel.text = _score.ToString();
        _settingsPopup.Close();
    }
    public void UIClicked()
    {
        _audioSource.PlayOneShot(_clickSFX);
    }
    private void OnEnemyHit()
    {
        _score += 1;
        _scoreLabel.text = _score.ToString();
    }
}