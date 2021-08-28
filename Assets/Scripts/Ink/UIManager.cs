using TMPro;
using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [Header("Ink")]
    [SerializeField] private TextAsset _inkJSON;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _speakerNameText;

    [Header("Parents")]
    [SerializeField] private Transform _buttonsParent;

    [Header("Prefabs")]
    [SerializeField] private ChoiceButton _choiceButton;

    private Story _story;

    private List<string> _tags;

    private int _hideButtonNumber = -1;


    private void Start()
    {
        _story = new Story(_inkJSON.text);

        UpdateUI();
    }

    private void ClearUI()
    {
        _dialogueText.text = "";
        _speakerNameText.text = "";

        for (int i = 0; i < _buttonsParent.childCount; i++)
        {
            Destroy(_buttonsParent.GetChild(i).gameObject);
        }
    }

    private void UpdateUI()
    {
        ClearUI();

        string chunk = LoadNextStoryChunk();

        HandleTags();

        _dialogueText.text = chunk;
        int index = 0;

        foreach(Choice choice in _story.currentChoices)
        {
            if (++index == _hideButtonNumber)
                continue;

            ChoiceButton cb = Instantiate(_choiceButton, _buttonsParent);
            
            cb.Initialize(choice.text, () =>
            {
                _story.ChooseChoiceIndex(choice.index);
                UpdateUI();
            });
        }
    }

    private string LoadNextStoryChunk()
    {
        string chunk = "";

        if(_story.canContinue)
        {
            chunk = _story.ContinueMaximally();
        }

        return chunk;
    }

    private void HandleTags()
    {
        _hideButtonNumber = -1;
        _tags = _story.currentTags;

        if (_tags.Count == 0)
            return;

        foreach(string t in _tags)
        {
            string[] split = t.Split('[', ']');

            switch (split[0])
            { 
                case "speaker":
                    Debug.Log(split[0]);
                    _speakerNameText.text = split[1];
                    break;
                case "loc":
                    Debug.Log(split[0]);
                    Debug.Log(split[1]);
                    break;
                case "thm":
                    Debug.Log(split[0]);
                    break;
                case "fel":
                    Debug.Log(split[0]);
                    break;
                case "mem":
                    Debug.Log(split[0]);
                    break;
                case "hide":
                    Debug.Log(split[0]);
                    Debug.Log(split[1]);
                    _hideButtonNumber = int.Parse(split[1]);
                    break;
                default:
                    return;
            }
        }
    }
}