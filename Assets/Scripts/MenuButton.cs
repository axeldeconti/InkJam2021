using UnityEngine;
using TMPro;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;

    [Header("Ease")]
    [SerializeField] private float _openTime = 1;
    [SerializeField] private Ease _openEase = Ease.OutBounce;
    [SerializeField] private float _closeTime = 1;
    [SerializeField] private Ease _closeEase = Ease.OutBounce;

    private TweenerCore<float, float, FloatOptions> _tween;

    private void Start()
    {
        _image.fillAmount = 0;
        _text.color = Color.white;
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnPointerEnter()
    {
        if (_tween != null && !_tween.IsComplete())
            _tween.Kill();

        _tween = _image.DOFillAmount(1, _openTime).SetEase(_openEase);
        _text.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
    }

    public void OnPointerExit()
    {
        if (_tween != null && !_tween.IsComplete())
            _tween.Kill();

        _tween = _image.DOFillAmount(0, _closeTime).SetEase(_closeEase);
        _text.color = Color.white;
    }
}