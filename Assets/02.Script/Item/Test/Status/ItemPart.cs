using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPart : MonoBehaviour
{
    [SerializeField]
    private WeaponSelect weaponItem;
    [SerializeField]
    private ArmorSelect armorItem;
    [SerializeField]
    private ItemPartStatus itemPartStatus;

    public WeaponSelect WeaponItem => weaponItem;
    public ArmorSelect ArmorItem => armorItem;
    public ItemPartStatus StatusItemPart => itemPartStatus;

    public void ChangeItem(WeaponSelect weapon, ItemPartStatus status)
    {
        weaponItem.CopySelect(weapon);

        ItemPartStatus newItem = status;

        itemPartStatus.StatusCopy(newItem);
    }
    public void ChangeItem(ArmorSelect armor, ItemPartStatus status)
    {

        armorItem.CopySelect(armor);

        ItemPartStatus newItem = status;

        itemPartStatus.StatusCopy(newItem);
    }

    public void ChangeItem()
    {
        weaponItem = null;
        armorItem = null;

        itemPartStatus.StatusCopy(null);
    }
}
