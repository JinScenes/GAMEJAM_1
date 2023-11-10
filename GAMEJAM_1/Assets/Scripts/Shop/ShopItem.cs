using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Shop/Item")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public int price;
}
