using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Inventory;

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

    private int currentOffset = 0;

    public TextMeshProUGUI coinsPanel;

    public GameObject inventoryUI;
    public bool inventoryIsVisible = false;

    private int coins = 2000;

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
            }
            else
            {
                
                inventoryIsVisible = false;
                SetInventoryVisible(false);
            }
        }
    }

    void SetInventoryVisible(bool visible)
    {
        inventoryUI.SetActive(visible);
        if (inventoryIsVisible) {
            if (PlayerMovement.Instance != null)
            {
                PlayerMovement.Instance.LockPlayerMovement();
            }
            else
            {
                Debug.LogWarning("PlayerMovement.Instance is null!");
            }

            PlayerMovement.Instance.LockPlayerMovement();
            Debug.Log(items.Count);
            if (GetLenght() == 0) return;
            if(0 + currentOffset < GetLenght())
            {
                ItemIcon_1.sprite = items[0 + currentOffset].icon;
                ItemIcon_1.GetComponentInChildren<TextMeshProUGUI>().text = items[0 + currentOffset].displayName;
            }
            if (0 + currentOffset + 1 < GetLenght())
            {
                ItemIcon_2.sprite = items[0 + currentOffset + 1].icon;
                ItemIcon_2.GetComponentInChildren<TextMeshProUGUI>().text = items[0 + currentOffset + 1].displayName;
            }

            if (0 + currentOffset + 2 < GetLenght())
            {
                ItemIcon_3.sprite = items[0 + currentOffset + 2].icon;
                ItemIcon_3.GetComponentInChildren<TextMeshProUGUI>().text = items[0 + currentOffset + 2].displayName;
            }
            coinsPanel.text = $"{coins}";
        }
        else
        {
            // При скрытии инвентаря тоже проверяем
            if (PlayerMovement.Instance != null)
            {
                PlayerMovement.Instance.UnlockPlayerMovement();
            }
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

    public int GetLenght()
    {
        return this.items.Count;
    }

}
