using UnityEngine;
using UnityEngine.UI;

public class HubButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    public void Initialize(ChoiceButton.ChoiceDelegate callback)
    {
        _button.onClick.AddListener(() => callback());
    }
}