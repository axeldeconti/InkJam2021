using System;
using UnityEngine;
using UnityEngine.UI;

public class LocationManager : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] private Image _background;
    [SerializeField] private Image _blur;

    [Header("Sprites")]
    [SerializeField] private Location _street;
    [SerializeField] private Location _museum;
    [SerializeField] private Location _park;
    [SerializeField] private Location _bookshop;

    private void Start()
    {
        _background.sprite = _street.sprite;
        Instantiate(_street.prefabs, _background.transform);
        _blur.gameObject.SetActive(false);
    }

    public void ChangeLocation(string loc)
    {
        Debug.Log("Change to location " + loc);

        if (_background.transform.childCount > 0)
            Destroy(_background.transform.GetChild(0).gameObject);

        switch (loc)
        {
            case "street":
                _background.sprite = _street.sprite;
                Instantiate(_street.prefabs, _background.transform);
                break;
            case "museum":
                _background.sprite = _museum.sprite;
                Instantiate(_museum.prefabs, _background.transform);
                break;
            case "park":
                _background.sprite = _park.sprite;
                Instantiate(_park.prefabs, _background.transform);
                break;
            case "bookshop":
                _background.sprite = _bookshop.sprite;
                Instantiate(_bookshop.prefabs, _background.transform);
                break;
            default:
                return;
        }
    }
}

[Serializable]
public struct Location
{
    public Sprite sprite;
    public AudioClip clip;
    public GameObject prefabs;
}