using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public static Inventory Instance;
    public Image ItemIcon_1;
    public Image ItemIcon_2;
    public Image ItemIcon_3;

    public GameObject inventoryUI;
    public bool inventoryIsVisible = false;

    public int coins = 0;

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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Всегда закрываем инвентарь при загрузке любой сцены
        inventoryIsVisible = false;
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
            Debug.Log(items.Count);
            foreach(Item item in items)
            {
                ItemIcon_1.sprite = item.icon;
                ItemIcon_1.GetComponentInChildren<TextMeshProUGUI>().text = item.displayName;
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

        // Показываем UI уведомление
        ShowNotification($"Получено: {displayName}");
    }

    public bool HasItem(string itemName)
    {
        return items.Exists(item => item.name == itemName);
    }

    void ShowNotification(string text)
    {
        // Твоя логика показа уведомления
        Debug.Log(text);
    }

}
