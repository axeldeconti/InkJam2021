using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    public delegate void ChoiceDelegate();

    [SerializeField] private RectTransform _rect;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _button;

    public void Initialize(string text, ChoiceDelegate callback)
    {
        _text.text = text;
        _button.onClick.AddListener(() => callback());

        LayoutRebuilder.ForceRebuildLayoutImmediate(_rect);
    }
}