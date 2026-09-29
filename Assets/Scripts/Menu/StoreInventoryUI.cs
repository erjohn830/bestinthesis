using UnityEngine;

public class StoreInventoryUI : MonoBehaviour
{
    public GameObject storePanel;
    public GameObject inventoryPanel;

    void Start()
    {
        ShowStore(); // default view
    }

    public void ShowStore()
    {
        storePanel.SetActive(true);
        inventoryPanel.SetActive(false);
    }

    public void ShowInventory()
    {
        storePanel.SetActive(false);
        inventoryPanel.SetActive(true);
    }
}