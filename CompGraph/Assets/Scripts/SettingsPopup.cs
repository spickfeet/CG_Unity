using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SettingsPopup : MonoBehaviour
{
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private Slider _sensitivitySlider;

    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void OnSubmitName(string name)
    {
        Debug.Log(name);
    }
    public void OnSpeedValue(float speed)
    {
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
    }
    public void OnSensitivityValue(float sensitivity)
    {
        Messenger<float>.Broadcast(GameEvent.SENSITIVITY_CHANGED, sensitivity);
    }
    void Start()
    {
        _speedSlider.value = PlayerPrefs.GetFloat("speed", 1);
        _sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivitySlider", 1);
    }
    public void Escape()
    {
        Application.LoadLevel("Menu");
    }
}