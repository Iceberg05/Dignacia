using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName ="ScriptableObject/Item")]
public class Item : ScriptableObject
{

    [Header("Only GamePlay")]
    public TileBase tile;
    public ItemType type;
    public ActionType actiontype;
    public Vector2Int range = new Vector2Int(5, 4);
    [Header("Only UI")]
    public bool stackable = true;
    [Header("Both")]
    public Sprite image;

   public enum ItemType
    {
        Material,
        Tool,
        Food,
        Roguelike

    }

    // Update is called once per frame
   public enum ActionType
    {
        Craft,
        Use,
        Eat,
        shoot
    }
}
