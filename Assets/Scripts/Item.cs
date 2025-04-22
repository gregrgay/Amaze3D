using UnityEngine;

public class Item
{
    public enum ItemType
    {
        bluegem,
        yellowgem,
        pinkgem,
        key,
        cat_small,
        panel_small,
        nitrogen,
    }
    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        { 
            default:
            case ItemType.bluegem:      return ItemAssets.Instance.bluegemSprite;
            case ItemType.yellowgem:    return ItemAssets.Instance.yellowgemSprite;
            case ItemType.pinkgem:      return ItemAssets.Instance.pinkgemSprite;
            case ItemType.key:           return ItemAssets.Instance.keySprite;
            case ItemType.cat_small:     return ItemAssets.Instance.cat_smallSprite;
            case ItemType.panel_small:   return ItemAssets.Instance.panel_smallSprite;
            case ItemType.nitrogen:      return ItemAssets.Instance.nitrogenSprite;
        }
    }
    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.bluegem:
            case ItemType.yellowgem:
            case ItemType.pinkgem:
                //return true;   
            case ItemType.key:
            case ItemType.cat_small:
            case ItemType.panel_small:
            case ItemType.nitrogen:
                return true;

        }


    }
}