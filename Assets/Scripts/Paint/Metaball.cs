using UnityEngine;

public class Metaball : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite = null;

    [SerializeField] private TrailRenderer _trail = null;

    [SerializeField] private float _lifetime = 10;

    [SerializeField] private float _fadingTime = 2;

    [SerializeField] private Vector2 _decreaseSizeRand = Vector2.zero;

    private float _currentTime = 0;

    private float _decreaseSize;

    private bool _isFading = false;

    private void Start()
    {
        _decreaseSize = Random.Range(_decreaseSizeRand.x, _decreaseSizeRand.y);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _lifetime && !_isFading)
        {
            _isFading = true;
            _currentTime = 0;
        }
        else if(_isFading)
        {
            _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 1 - _currentTime / _fadingTime);

            _trail.colorGradient.alphaKeys[0].alpha = 1 - _currentTime / _fadingTime;
            _trail.colorGradient.alphaKeys[1].alpha = 1 - _currentTime / _fadingTime;

            transform.localScale -= _decreaseSize * Vector3.one;

            if (_currentTime >= _fadingTime)
                Destroy(gameObject);
        }
    }
}