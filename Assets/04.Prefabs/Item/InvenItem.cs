using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ITEMTYPE
{
    All,
    POTION,
    ETC
}

[CreateAssetMenu(fileName = "InvenItem", menuName = "Scriptable Object/Item")]
public class InvenItem : ScriptableObject
{
    public ITEMTYPE itemtype;
    [SerializeField]
    private string itemCode;
    public string itemName;
    public string itemInfo;
    public Sprite itemImage;
    public int itemCnt;
    [SerializeField]
    private int amount;
    public bool isMax;

    public int hpHeal;
    public int mpHeal;
    public int increaseDamage;
    public float increaseSpeed;

    public float durationTime;

    public string ItemCode => itemCode;
    public bool IsMax() => itemCnt >= amount;
    public int ItemAmount => amount;

    public InvenItem Clone()
    {
        var clone = ScriptableObject.CreateInstance<InvenItem>();

        clone.itemtype = this.itemtype;

        clone.itemCode = this.itemCode;
        clone.itemName = this.itemName;
        clone.itemInfo = this.itemInfo;
        clone.itemImage = this.itemImage;
        clone.itemCnt = this.itemCnt;
        clone.amount = this.amount;
        clone.isMax = this.isMax;

        clone.hpHeal = this.hpHeal;
        clone.mpHeal = this.mpHeal;
        clone.increaseDamage = this.increaseDamage;
        clone.increaseSpeed = this.increaseSpeed;

        clone.durationTime = this.durationTime;

        return clone;
    }

    public string ConvertSpriteToString(Sprite sprite)
    {
        Texture2D texture = sprite.texture;
        byte[] imageData = texture.EncodeToPNG();

        string imageString = System.Convert.ToBase64String(imageData);

        return imageString;
    }

    public Sprite ConvertStringToSprite(string itemImageString)
    {
        byte[] imageData = System.Convert.FromBase64String(itemImageString);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);

        Sprite image = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        return image;
    }

    public JObject ToSaveInvenItem()
    {
        JObject json = new JObject();

        json["itemType"] = itemtype.ToString();
        json["itemCode"] = itemCode;
        json["itemName"] = itemName;
        json["itemInfo"] = itemInfo;
        json["itemImage"] = ConvertSpriteToString(itemImage);
        json["itemCnt"] = itemCnt;
        json["amount"] = amount;
        json["isMax"] = isMax;

        json["hpHeal"] = hpHeal;
        json["mpHeal"] = mpHeal;
        json["increaseDamage"] = increaseDamage;
        json["increaseSpeed"] = increaseSpeed;
        json["durationTime"] = durationTime;

        return json;
    }

    public void LoadFrom(JObject json)
    {
        itemCode = json["itemCode"].ToString();
        itemName = json["itemName"].ToString();
        itemInfo = json["itemInfo"].ToString();
        itemImage = ConvertStringToSprite(json["itemImage"].ToString());
        itemCnt = (int)json["itemCnt"];
        amount = (int)json["amount"];
        isMax = (bool)json["isMax"];

        hpHeal = (int)json["hpHeal"];
        mpHeal = (int)json["mpHeal"];
        increaseDamage = (int)json["increaseDamage"];
        increaseSpeed = (float)json["increaseSpeed"];
        durationTime = (float)json["durationTime"];
    }
}
