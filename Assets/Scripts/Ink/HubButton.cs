using UnityEngine;
using UnityEngine.UI;

public class HubButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Animator _anim;

    public void Initialize(ChoiceButton.ChoiceDelegate callback)
    {
        _button.onClick.AddListener(() => callback());
        _anim.SetBool("hover", false);
    }

    public void OnPointerEnter()
    {
        _anim.SetBool("hover", true);
    }

    public void OnPointerExit()
    {
        _anim.SetBool("hover", false);
    }
}