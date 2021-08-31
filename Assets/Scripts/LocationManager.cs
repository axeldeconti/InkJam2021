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

    private string _currentLocation = "";

    private void Start()
    {
        _blur.gameObject.SetActive(false);
    }

    public void ChangeLocation(string loc)
    {
        Debug.Log("Change to location " + loc);

        if (_background.transform.childCount > 0)
            Destroy(_background.transform.GetChild(0).gameObject);

        Location location;
        MusicManager.Instance.Volume = 1;

        switch (loc)
        {
            case "street":
                location = _street;
                MusicManager.Instance.Volume = 0.4f;
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

        if (_currentLocation == loc)
            return;
        else
            _currentLocation = loc;

        MusicManager.Instance.Play(location.clip);
    }
}

[Serializable]
public struct Location
{
    public Sprite sprite;
    public AudioClip clip;
    public GameObject prefabs;
}