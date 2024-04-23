using UnityEngine;
using System.Collections;
public class MouseLook : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;
    private float _rotationX = 0;

    void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SENSITIVITY_CHANGED, OnSensitivityChanged);
    }
    void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SENSITIVITY_CHANGED, OnSensitivityChanged);
    }

    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;
    }
    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, _joystick.Horizontal * sensitivityHor, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            _rotationX -= _joystick.Vertical * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
    private void OnSensitivityChanged(float value)
    {
        sensitivityHor = value;
        sensitivityVert = value;
    }
}