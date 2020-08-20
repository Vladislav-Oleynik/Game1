﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]

public class Equipment : Item
{
    public int armorModifier;
    public int damageModifier;
    public EquipmentSlot equipSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;

    public override void Use()
    {
        base.Use();
        //equip the item
        EquipmentManager.instance.Equip(this);
        //remove it from the inventory
        RemoveFromInventory();
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }
public enum EquipmentMeshRegion { Legs, Arms, Torso } //corresponds to body blendshapes