using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataPath : MonoBehaviour
{
    #region Singleton
    public static DataPath instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There than one instance of DataPath found!");
            return;
        }
        instance = this;
        //path = Directory.GetFiles(Application.dataPath, "Save.json")[0];
        ////path = SaveLoader.path;
        //Debug.Log("Awake DataPath: " + path);
        data = LoadFileJSON.Load<Data>();
        Inventory.instance.items = data.items;
        PlayerManager.instance.player.transform.position = data.playerPosition;
        PlayerManager.instance.player.GetComponent<CharacterStats>().currentHealth = data.health;
        EquipmentManager.instance.GetComponent<Inventory>().items = data.items;
        PickUpItems.instance.pickUpItems = new GameObject[data.pickUpItemsStates.Length];
        PickUpItems.instance.pickUpItemsStates = new bool[data.pickUpItemsStates.Length];
        PickUpItems.LoadArrayOfPickUpItems();
    }
    #endregion

    [System.Serializable]
    public class Data
    {
        public int health;
        public Vector3 playerPosition;
        public List<Item> items = new List<Item>();
        public bool[] pickUpItemsStates;
    }
    [SerializeField] Data data;

    public void WriteData()
    {
        PickUpItems.LoadArrayOfPickUpItems();
        PickUpItems.LoadArrayOfStatesOfPickUpItems();
        data.health = PlayerManager.instance.player.GetComponent<CharacterStats>().currentHealth;
        data.playerPosition = PlayerManager.instance.player.transform.position;
        data.items = Inventory.instance.items;
        data.pickUpItemsStates = PickUpItems.instance.pickUpItemsStates;
    }

    void Start()
    {
        Inventory.instance.onItemChangedCallback.Invoke();
        Debug.Log("Start DataPath");
        Debug.Log("Debug from DataPath = " + PickUpItems.instance);
        for (int i = 0; i < data.pickUpItemsStates.Length; i++)
        {
            PickUpItems.instance.pickUpItems[i].GetComponent<ItemPickup>().pickedUp = data.pickUpItemsStates[i];
            PickUpItems.instance.pickUpItems[i].SetActive(!PickUpItems.instance.pickUpItems[i].GetComponent<ItemPickup>().pickedUp);
        }
    }

    //void OnApplicationQuit()
    //{
    //    Save();
    //}

    public void Save()
    {
        WriteData();
        SaveFileJSON.Save(data);
    }
}
