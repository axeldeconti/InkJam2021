using UnityEngine;
using UnityEngine.UI;

public class LocationManager : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] private Image _background;
    [SerializeField] private Image _blur;

    [Header("Sprites")]
    [SerializeField] private Sprite _street;
    [SerializeField] private Sprite _museum;
    [SerializeField] private Sprite _park;
    [SerializeField] private Sprite _bookshop;

    private void Start()
    {
        _background.sprite = _street;
        _blur.gameObject.SetActive(false);
    }

    public void ChangeLocation(string loc)
    {
        Debug.Log("Change to location " + loc);

        switch (loc)
        {
            case "street":
                _background.sprite = _street;
                break;
            case "museum":
                _background.sprite = _museum;
                break;
            case "park":
                _background.sprite = _park;
                break;
            case "bookshop":
                _background.sprite = _bookshop;
                break;
            default:
                return;
        }
    }
}