using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="ItemScriptableObjectData", menuName = "ScriptableObject/item")]
public class BaseObject : ScriptableObject
{
    [SerializeField] string nameItem;
    [SerializeField] float price;
    [SerializeField] Sprite sprite;
    public float GetPrice()
    {
        return price;
    }
    public string GetNameItem()
    {
        return nameItem;
    }

}
