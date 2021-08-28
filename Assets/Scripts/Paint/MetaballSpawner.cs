using System.Collections;
using UnityEngine;

public class MetaballSpawner : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private Rigidbody2D _metaballPrefab = null;

    [SerializeField] private float _spawnDelay = 1;

    [SerializeField] private int _spawnNumber = 10;

    [SerializeField] private float _spawnForce = 10;

    [Header("Controls")]
    [SerializeField] private bool _canSpanw = true;

    [Header("Misc")]
    [SerializeField] private Transform _metaballParent = null;

    private float _currentTime = 0;

    private void Start()
    {
        StartCoroutine(SpawnMetaballs());
    }

    private void Update()
    {
        if (!_canSpanw)
            return;

        _currentTime += Time.deltaTime;

        if(_currentTime >= _spawnDelay)
        {
            _currentTime = 0;
            StartCoroutine(SpawnMetaballs());
        }
    }

    private IEnumerator SpawnMetaballs()
    {
        Rigidbody2D mb = null;
        float angle = Time.time % 360;
        Vector2 dir = Vector2.zero;

        for (int i = 0; i < _spawnNumber; i++)
        {
            mb = Instantiate(_metaballPrefab, transform.position, Quaternion.identity, _metaballParent);
            angle = Random.Range(0, 360);
            dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
            mb.AddForce(_spawnForce * dir);

            yield return null;
        }
    }
}