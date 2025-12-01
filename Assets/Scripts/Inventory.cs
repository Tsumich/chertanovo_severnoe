using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public delegate void ItemHandler (string item_name);
    public event ItemHandler OnTakenItemEvent;

    public delegate void CoinsHandler(int coint);
    public event CoinsHandler OnCoinsValueEvent;


    public List<Item> items = new List<Item>();
    public static Inventory Instance;
    public Image ItemIcon_1;
    public Image ItemIcon_2;
    public Image ItemIcon_3;

    public TextMeshProUGUI coinsPanel;

    public GameObject inventoryUI;
    public bool inventoryIsVisible = false;

    private int coins = 0;

    [System.Serializable]
    public class Item
    {
        public string name;
        public string displayName;
        public Sprite icon; // для UI
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        SetInventoryVisible(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryIsVisible) {
                inventoryIsVisible = true;
                SetInventoryVisible(true);
                PlayerMovement.Instance.LockPlayerMovement();
            }
            else
            {
                
                inventoryIsVisible = false;
                SetInventoryVisible(false);
                PlayerMovement.Instance.UnlockPlayerMovement();
            }
        }
    }

    void SetInventoryVisible(bool visible)
    {
        inventoryUI.SetActive(visible);
        if (inventoryIsVisible) {
            Debug.Log(items.Count);
            foreach(Item item in items)
            {
                ItemIcon_1.sprite = item.icon;
                ItemIcon_1.GetComponentInChildren<TextMeshProUGUI>().text = item.displayName;
            }
            coinsPanel.text = $"{coins}";
        }
    }

    public void AddItem(string itemName, string displayName, Sprite icon = null)
    {
        Debug.Log("AddItem");
        items.Add(new Item
        {
            name = itemName,
            displayName = displayName,
            icon = icon
        });

        // Удаляем объект со сцены
        GameObject itemObject = GameObject.Find(itemName);
        if (itemObject != null)
        {
            //Destroy(itemObject);
            itemObject.SetActive(false);
        }
        this.OnTakenItemEvent?.Invoke(displayName);
    }

    public bool HasItem(string itemName)
    {
        return items.Exists(item => item.name == itemName);
    }

    public void AddCoins(int coins_amount)
    {
        Debug.Log("Монет было: " +  this.coins);
        this.coins += coins_amount;
        Debug.Log("Монет стало: " +  this.coins);

        this.OnCoinsValueEvent?.Invoke(coins_amount);
    }

    public bool HasEnoughtCoins(int coins_amount)
    {
        return coins_amount < coins;
    }

}
