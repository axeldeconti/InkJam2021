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

    [Header("Parents")]
    [SerializeField] private Transform _buttonsParent;
    [SerializeField] private Transform _hubParent;
    [SerializeField] private Transform _mumParent;

    [Header("Prefabs")]
    [SerializeField] private ChoiceButton _choiceButton;
    [SerializeField] private HubButton _hubButton;
    [SerializeField] private ChoiceButton _mumButton;

    private Story _story;

    private List<string> _tags;

    private LocationManager _loc;

    private int _hubButtonNumber = -1;
    private HubButton _hb = null;
    private int _mumButtonNumber = -1;
    private ChoiceButton _mumb = null;


    private void Start()
    {
        _loc = GetComponent<LocationManager>();

        _story = new Story(_inkJSON.text);

        UpdateUI();
    }

    private void ClearUI()
    {
        _dialogueText.text = "";

        for (int i = 0; i < _buttonsParent.childCount; i++)
        {
            Destroy(_buttonsParent.GetChild(i).gameObject);
        }

        if (_hubParent.childCount > 0)
            Destroy(_hubParent.GetChild(0).gameObject);

        if (_mumParent.childCount > 0)
            Destroy(_mumParent.GetChild(0).gameObject);
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
            else if (index == _mumButtonNumber)
            {
                ChoiceButton _mumb = Instantiate(_mumButton, _mumParent);

                _mumb.Initialize(choice.text, () =>
                {
                    _story.ChooseChoiceIndex(choice.index);
                    Debug.Log("Make up your mind " + choice.index);
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
        _mumButtonNumber = -1;

        _tags = _story.currentTags;

        if (_tags.Count == 0)
            return;

        foreach(string t in _tags)
        {
            string[] split = t.Split('[', ']');

            switch (split[0])
            {
                case "loc":
                    _loc.ChangeLocation(split[1]);
                    break;
                case "thm":
                    Debug.Log(split[0] + " - " + split[1]);
                    break;
                case "fel":
                    Debug.Log(split[0] + " - " + split[1]);
                    break;
                case "mem":
                    Debug.Log(split[0] + " - " + split[1]);
                    break;
                case "hub":
                    _hubButtonNumber = int.Parse(split[1]);
                    Debug.Log("Hub button on " + _hubButtonNumber);
                    break;
                case "mum":
                    _mumButtonNumber = int.Parse(split[1]);
                    Debug.Log("mum button on " + _mumButtonNumber);
                    break;
                default:
                    return;
            }
        }
    }
}