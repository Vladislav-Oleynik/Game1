using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

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

    Equipment[] currentEquipment;
    Inventory inventory;
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    SkinnedMeshRenderer[] currentMeshes;
    public SkinnedMeshRenderer targetMesh;
    public Equipment[] defaultItems;
    public Transform Shield;
    public Transform Sword;

    private void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];
        Debug.Log(inventory.items.Count);
        EquipDefaultItems();
        if (inventory.items.Count > 0)
        {
            EquipAll();

            int j = inventory.items.Count;

            for (int i = j - 1; i >= 0; i--)
            {
                instance.inventory.Remove(instance.inventory.items[i]);
            }
        }
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = Unequip(slotIndex);

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        SetEquipmentBlendshapes(newItem, 100);
        //insert item into the slot
        currentEquipment[slotIndex] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        currentMeshes[slotIndex] = newMesh;
        if (newItem != null && newItem.equipSlot == EquipmentSlot.Weapon)
        {
            newMesh.rootBone = Sword;
        } else if(newItem != null && newItem.equipSlot == EquipmentSlot.Shield)
        {
            newMesh.rootBone = Shield;
        }
        else
        {
            newMesh.transform.parent = targetMesh.transform;
            newMesh.bones = targetMesh.bones;
            newMesh.rootBone = targetMesh.rootBone;
        }
        
        
    }

    public Equipment Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }

            Equipment oldItem = currentEquipment[slotIndex];
            SetEquipmentBlendshapes(oldItem, 0);

            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
            return oldItem;
        }

        return null;
    }

    public void EquipAll()
    {
        foreach (Equipment item in inventory.items)
        {
            Equip(item);
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
        EquipDefaultItems();
    }

    void SetEquipmentBlendshapes(Equipment item, float weight)
    {
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void EquipDefaultItems()
    {
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }

    void OnApplicationQuit()
    {
        UnequipAll();
    }
}
