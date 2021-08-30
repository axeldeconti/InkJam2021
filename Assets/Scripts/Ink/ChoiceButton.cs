using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class ChoiceButton : MonoBehaviour
{
    public delegate void ChoiceDelegate();

    [SerializeField] private RectTransform _rect;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;

    [Header("Ease")]
    [SerializeField] private float _openTime = 1;
    [SerializeField] private Ease _openEase = Ease.OutBounce;
    [SerializeField] private float _closeTime = 1;
    [SerializeField] private Ease _closeEase = Ease.OutBounce;

    [Header("SFX")]
    [SerializeField] private AudioClip _SFXOpen;
    [SerializeField] private AudioClip _SFXClose;
    [SerializeField] private AudioClip _click;

    private TweenerCore<float, float, FloatOptions> _tween;

    public void Initialize(string text, ChoiceDelegate callback)
    {
        _text.text = text;
        _button.onClick.AddListener(() => callback());

        LayoutRebuilder.ForceRebuildLayoutImmediate(_rect);

        _image.fillAmount = 0;
    }

    public void OnPointerEnter()
    {
        //transform.localScale = 1.5f * Vector3.one;
        if (_tween != null && !_tween.IsComplete())
            _tween.Kill();

        _tween = _image.DOFillAmount(1, _openTime).SetEase(_openEase);

        SFXManager.Instance.Play(_SFXOpen);
    }

    public void OnPointerExit()
    {
        //transform.localScale = Vector3.one;
        if (_tween != null && !_tween.IsComplete())
            _tween.Kill();

        _tween = _image.DOFillAmount(0, _closeTime).SetEase(_closeEase);

        SFXManager.Instance.Play(_SFXClose);
    }

    public void PlayClickSound()
    {
        SFXManager.Instance.Play(_click);
    }
}