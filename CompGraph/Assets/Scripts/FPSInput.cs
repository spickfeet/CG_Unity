using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]

public class FPSInput : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;

    private float _speed = 6.0f;
    private float _gravity = -9.8f;
    private CharacterController _charController;

    private const float BASE_SPEED = 6.0f;

    void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }
    void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }
    void Update()
    {
        float deltaX = _joystick.Horizontal * _speed;
        float deltaZ = _joystick.Vertical * _speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, _speed);
        movement.y = _gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);
    }
    private void OnSpeedChanged(float value)
    {
        _speed = BASE_SPEED * value;
    }
}