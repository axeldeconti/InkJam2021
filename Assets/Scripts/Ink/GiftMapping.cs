using UnityEngine;

[CreateAssetMenu(fileName = "Gift mapping", menuName = "Gift mapping")]
public class GiftMapping : ScriptableObject
{
    public GiftMappingItem[] mapping = new GiftMappingItem[0];

    public bool Contains(string name)
    {
        for (int i = 0; i < mapping.Length; i++)
        {
            if (mapping[i].name.Equals(name))
                return true;
        }

        return false;
    }

    public AudioClip Get(string name)
    {
        for (int i = 0; i < mapping.Length; i++)
        {
            if (mapping[i].name.Equals(name))
                return mapping[i].clip;
        }

        return null;
    }
}

[System.Serializable]
public class GiftMappingItem
{
    public string name;
    public AudioClip clip;
}