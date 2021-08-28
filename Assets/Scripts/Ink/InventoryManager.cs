using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventoryMapping _inventoryMapping;
    [SerializeField] private GameObject _iconPrefab;
    [SerializeField] private Transform _iconsParent;

    [Header("Ease")]
    [SerializeField] private float _openTime = 1;
    [SerializeField] private Ease _openEase = Ease.OutBounce;
    [SerializeField] private float _closeTime = 1;
    [SerializeField] private Ease _closeEase = Ease.OutBounce;

    private List<string> _items;
    private bool _isOpen = false;

    private void Start()
    {
        _items = new List<string>();
        Close();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            Open();
        else if(Input.GetKeyDown(KeyCode.Z))
            Close();
        else if (Input.GetKeyDown(KeyCode.Q))
            AddItem("Item 1");
        else if (Input.GetKeyDown(KeyCode.S))
            AddItem("Item 2");
        else if (Input.GetKeyDown(KeyCode.W))
            RemoveItem("Item 1");
        else if (Input.GetKeyDown(KeyCode.X))
            RemoveItem("Item 2");
        else if (Input.GetKeyDown(KeyCode.C))
            Clear();
    }

    public void AddItem(string itemName)
    {
        if (!_inventoryMapping.Contains(itemName))
            return;

        _items.Add(itemName);
        Instantiate(_iconPrefab, _iconsParent).GetComponent<Image>().sprite = _inventoryMapping.Get(itemName);

        if (!_isOpen)
            Open();
    }

    public void RemoveItem(string itemName)
    {
        if (!_items.Contains(itemName))
            return;

        Destroy(_iconsParent.GetChild(_items.IndexOf(itemName)).gameObject);
        _items.Remove(itemName);

        if (_items.Count.Equals(0))
            Close();
    }

    public void Clear()
    {
        _items.Clear();

        for (int i = _iconsParent.childCount - 1; i >= 0; i--)
        {
            Destroy(_iconsParent.GetChild(i).gameObject);
        }

        Close();
    }

    public void Open()
    {
        transform.DOLocalMoveY(0, _openTime).SetEase(_openEase);
        _isOpen = true;
    }

    public void Close()
    {
        transform.DOLocalMoveY(300, _closeTime).SetEase(_closeEase);
        _isOpen = false;
    }
}