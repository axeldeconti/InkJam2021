using TMPro;
using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
    [SerializeField] private GiftMapping _giftMapping;
    [SerializeField] private ChoiceButton _choiceButton;
    [SerializeField] private HubButton _hubButton;
    [SerializeField] private ChoiceButton _mumButton;

    [Header("SFX")]
    [SerializeField] private AudioClip _footsteps;
    [SerializeField] private AudioClip _emotionChoice;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Menu");
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
                    SFXManager.Instance.Play(_footsteps);
                    break;
                case "thm":
                    Debug.Log(split[0] + " - " + split[1]);
                    SFXManager.Instance.Play(_emotionChoice);
                    break;
                case "fel":
                    Debug.Log(split[0] + " - " + split[1]);
                    SFXManager.Instance.Play(_emotionChoice);
                    break;
                case "mem":
                    Debug.Log(split[0] + " - " + split[1]);
                    SFXManager.Instance.Play(_emotionChoice);
                    break;
                case "hub":
                    _hubButtonNumber = int.Parse(split[1]);
                    Debug.Log("Hub button on " + _hubButtonNumber);
                    break;
                case "mum":
                    _mumButtonNumber = int.Parse(split[1]);
                    Debug.Log("mum button on " + _mumButtonNumber);
                    break;
                case "gift":
                    Debug.Log("Gift = " + split[1]);
                    HandleGift(split[1]);
                    break;
                default:
                    return;
            }
        }
    }

    private void HandleGift(string gift)
    {
        if(_giftMapping.Contains(gift))
        {
            AudioClip clip = _giftMapping.Get(gift);

            if (clip != null)
            {
                SFXManager.Instance.Play(clip);
                Debug.Log("Play gift sound " + clip.name);
            }
        }

        ChoiceButton cb = Instantiate(_choiceButton, _buttonsParent);

        cb.Initialize("Main menu", () =>
        {
            SceneManager.LoadScene("Menu");
        });
    }
}