using UnityEngine;

[CreateAssetMenu(fileName = "Inventory mapping", menuName = "Inventory mapping")]
public class InventoryMapping : ScriptableObject
{
    public InventoryMappingItem[] mapping = new InventoryMappingItem[0];

    public bool Contains(string name)
    {
        for (int i = 0; i < mapping.Length; i++)
        {
            if (mapping[i].name.Equals(name))
                return true;
        }

        return false;
    }

    public Sprite Get(string name)
    {
        for (int i = 0; i < mapping.Length; i++)
        {
            if (mapping[i].name.Equals(name))
                return mapping[i].sprite;
        }

        return null;
    }
}

[System.Serializable]
public class InventoryMappingItem
{
    public string name;
    public Sprite sprite;
}