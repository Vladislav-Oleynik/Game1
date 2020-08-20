using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public bool pickedUp;

    void Start()
    {
        PickedUpCheck();
        Debug.Log("Start ItemPickup");
    }

    public override void Interact()
    {
        base.Interact();
        pickedUp = false;
        PickUp();
    }

    void PickUp() 
    {

        pickedUp = true;
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        // Add to inventory

        if (wasPickedUp)
            gameObject.SetActive(false);
    }

    void PickedUpCheck()
    {
        if (pickedUp)
            gameObject.SetActive(false);
    }
}
