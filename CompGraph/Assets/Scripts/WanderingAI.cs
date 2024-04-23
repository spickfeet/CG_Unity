using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private float _obstacleRange = 5.0f;
    [SerializeField] private GameObject _fireballPrefab;
    private const float BASE_SPEED = 3.0f;
    private GameObject _fireball;
    private bool _alive;
    public bool Alive
    {
        set { _alive = value; }
    }
    void Start()
    {
        _alive = true;
    }
    void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }
    void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }
    void Update()
    {
        if (_alive)
        {
            transform.Translate(0, 0, _speed * Time.deltaTime);
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (_fireball == null)
                    {
                        _fireball = Instantiate(_fireballPrefab);
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        _fireball.transform.rotation = transform.rotation;
                    }
                }
                if (hit.distance < _obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }
    private void OnSpeedChanged(float value)
    {
        _speed = BASE_SPEED * value;
    }
}
