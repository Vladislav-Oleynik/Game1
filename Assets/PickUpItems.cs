using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    #region Singleton
    public static PickUpItems instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There than one instance of Inventory found!");
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject parentOfPickupItems;
    public GameObject[] pickUpItems;
    public bool[] pickUpItemsStates;

    private void Start()
    {
        Debug.Log("Start PickUpItems");
    }

    static public void LoadArrayOfPickUpItems ()
    {
        for (int i = 0; i < instance.pickUpItems.Length; i++)
        {
            instance.pickUpItems[i] = instance.parentOfPickupItems.transform.GetChild(i).gameObject;
        }
    }

    static public void LoadArrayOfStatesOfPickUpItems()
    {
        for (int i = 0; i < instance.pickUpItems.Length; i++)
        {
            instance.pickUpItemsStates[i] = instance.pickUpItems[i].GetComponent<ItemPickup>().pickedUp;
        }
    }
}
