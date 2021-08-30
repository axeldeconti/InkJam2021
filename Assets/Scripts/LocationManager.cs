using System;
using UnityEngine;
using UnityEngine.UI;

public class LocationManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;

    [Header("Images")]
    [SerializeField] private Image _background;
    [SerializeField] private Image _blur;

    [Header("Sprites")]
    [SerializeField] private Location _street;
    [SerializeField] private Location _museum;
    [SerializeField] private Location _park;
    [SerializeField] private Location _bookshop;

    private string _currentLocation = string.Empty;

    private void Start()
    {
        //_background.sprite = _street.sprite;
        //Instantiate(_street.prefabs, _background.transform);
        _blur.gameObject.SetActive(false);
    }

    public void ChangeLocation(string loc)
    {
        Debug.Log("Change to location " + loc);

        if (_currentLocation == loc)
            return;
        else
            _currentLocation = loc;

        if (_background.transform.childCount > 0)
            Destroy(_background.transform.GetChild(0).gameObject);

        Location location;

        switch (loc)
        {
            case "street":
                location = _street;
                break;
            case "museum":
                location = _museum;
                break;
            case "park":
                location = _park;
                break;
            case "bookshop":
                location = _bookshop;
                break;
            default:
                return;
        }

        _background.sprite = location.sprite;
        Instantiate(location.prefabs, _background.transform);
        _audio.clip = location.clip;
        _audio.Play();
    }
}

[Serializable]
public struct Location
{
    public Sprite sprite;
    public AudioClip clip;
    public GameObject prefabs;
}