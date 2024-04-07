using UnityEngine;
using System.Collections;

public class RayShooter : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fireEffectPrefab;
    [SerializeField] private Transform _effectSpawn;
    [SerializeField] private GameObject _hitMarkerPrefab;
    [SerializeField] private float _fireRate;
    [SerializeField] private Camera _camera;
    [SerializeField] private AudioClip _shotSFX;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _tracerPr;
    [SerializeField] private float _spread;

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
        
        _fireEffectPrefab.Play();

        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, new Vector3(Random.Range(-_spread, _spread), Random.Range(-_spread, _spread), Random.Range(-_spread, _spread)) + this.transform.forward, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
            if (target != null)
            {
                StartCoroutine(Tracer(hit.point));
                target.ReactHit();
                StartCoroutine(EffectIndicator(hit.point));
            }
            else
            {
                StartCoroutine(Tracer(hit.point));
                StartCoroutine(EffectIndicator(hit.point));
            }
        }
    }

    private IEnumerator EffectIndicator(Vector3 pos)
    {
        GameObject hitMarker;
        hitMarker = Instantiate(_hitMarkerPrefab);
        hitMarker.transform.position = pos;
        yield return new WaitForSeconds(0.01f);
        Destroy(hitMarker);
    }
    private IEnumerator Tracer(Vector3 pos)
    {
        GameObject t = Instantiate(_tracerPr);
        t.transform.position = new Vector3();
        LineRenderer tracer = t.GetComponent<LineRenderer>();
        tracer.startWidth = 0.05f;
        tracer.endWidth = 0.05f;
        tracer.SetPosition(0, _effectSpawn.position);
        tracer.SetPosition(1, pos);
        yield return new WaitForSeconds(0.02f);
        Destroy(t);
    }
}