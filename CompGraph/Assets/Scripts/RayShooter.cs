using UnityEngine;
using System.Collections;

public class RayShooter : MonoBehaviour
{

    [SerializeField] private float _fireRate;
    [SerializeField] private Camera _camera;
    [SerializeField] private AudioClip _shotSFX;
    [SerializeField] private AudioSource _audioSource;
    private float _nextFire = 0;
    void Start()
    {
        _camera = GetComponent<Camera>();
    }
    void Update()
    {
        if(Input.GetMouseButton(0) && Time.time > _nextFire)
        {
            _nextFire = Time.time + 1 / _fireRate;
            Shoot();
        }
    }
    private void FixedUpdate()
    {          
    }


    private void Shoot()
    {
         _audioSource.PlayOneShot(_shotSFX);
         Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
         Ray ray = _camera.ScreenPointToRay(point);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
            if (target != null)
            {
                target.ReactHit();
            }
            else
            {
                StartCoroutine(SphereIndicator(hit.point));
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        yield return new WaitForSeconds(1);
        Destroy(sphere);
    }
}