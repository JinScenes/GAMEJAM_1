using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel; // Reference to the UI panel that represents the shop
    [SerializeField] private List<ShopItem> possibleItems; // List of all possible items and abilities
    [SerializeField] private Text expText; // Text to display the player's EXP
    [SerializeField] private Button[] itemButtons; // Buttons for the items
    [SerializeField] private Text[] itemNames; // Text for the item names
    [SerializeField] private Text[] itemPrices; // Text for the item prices
    [SerializeField] private Button closeButton;


    private List<ShopItem> itemsForSale = new List<ShopItem>(); // Current items for sale

    private void Start()
    {
        shopPanel.SetActive(false); // Hide shop at the start
        closeButton.onClick.AddListener(CloseShop); // Set the close button listener
    }

    // Call this when the wave ends
    public void OpenShop()
    {
        GenerateItemsForSale();
        UpdateShopDisplay();
        shopPanel.SetActive(true); // Show the shop UI
    }

    private void GenerateItemsForSale()
    {
        itemsForSale.Clear();
        for (int i = 0; i < itemButtons.Length; i++)
        {
            int itemIndex = Random.Range(0, possibleItems.Count);
            ShopItem shopItem = possibleItems[itemIndex];
            itemsForSale.Add(shopItem);
        }
    }

    private void UpdateShopDisplay()
    {
        // Update EXP display (Assume your teammate will provide the actual value)
        expText.text = "EXP: " + "PLACEHOLDER_FOR_EXP_VALUE";

        for (int i = 0; i < itemsForSale.Count; i++)
        {
            // Set the item names and prices
            itemNames[i].text = itemsForSale[i].itemName;
            itemPrices[i].text = itemsForSale[i].price.ToString() + " EXP";

            // Assign each button the corresponding purchase function
            int index = i; // Local copy for closure
            itemButtons[i].onClick.RemoveAllListeners();
            itemButtons[i].onClick.AddListener(() => BuyItem(index));
        }
    }

    // Call this function when the player chooses to buy an item
    public void BuyItem(int itemIndex)
    {
        ShopItem itemToBuy = itemsForSale[itemIndex];
        // Check if the player has enough EXP
        // Placeholder for EXP check, to be implemented by your teammate
        bool hasEnoughExp = true; // Placeholder

        if (hasEnoughExp)
        {
            // Deduct the EXP (to be implemented by your teammate)
            Debug.Log($"Bought {itemToBuy.itemName} for {itemToBuy.price} EXP");

            // Apply the item effect here
            // For now, just a log message
            Debug.Log($"Applying effect of {itemToBuy.itemName}");

            // Remove the item from the shop
            itemsForSale.RemoveAt(itemIndex);
            UpdateShopDisplay();
        }
        else
        {
            Debug.Log("Not enough Exp to buy this item!");
        }
    }

    // Call this to close the shop and resume the game
    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

}
