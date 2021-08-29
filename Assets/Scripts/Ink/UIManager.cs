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
    [SerializeField] private Transform _hubParent;

    [Header("Prefabs")]
    [SerializeField] private ChoiceButton _choiceButton;
    [SerializeField] private HubButton _hubButton;

    private Story _story;

    private List<string> _tags;

    private LocationManager _loc;
    private int _hubButtonNumber = -1;
    private HubButton _hb = null;


    private void Start()
    {
        _loc = GetComponent<LocationManager>();

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

        if (_hubParent.childCount > 0)
            Destroy(_hubParent.GetChild(0).gameObject);
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
            if (++index == _hubButtonNumber)
            {
                HubButton _hb = Instantiate(_hubButton, _hubParent);

                _hb.Initialize(() =>
                {
                    _story.ChooseChoiceIndex(choice.index);
                    Debug.Log("Go back to hub " + choice.index);
                    UpdateUI();
                });

                continue;
            }

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
        _hubButtonNumber = -1;
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
                    _loc.ChangeLocation(split[1]);
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
                case "hub":
                    _hubButtonNumber = int.Parse(split[1]);
                    Debug.Log("Hub button on " + _hubButtonNumber);
                    break;
                default:
                    return;
            }
        }
    }
}