using System;
using UnityEngine;

[Serializable]
public class Furniture
{
    public int id;
    public string name;
    public FurnitureType type;
    public Sprite iconImage;
    public GameObject furniturePrefab;

    //if this constructor is used, name is used as name of icon and model
//    public Furniture(int id, string name, FurnitureType type)
//    {
//        this.id = id;
//        this.name = name;
//        this.type = type;
//        this.iconImage =  Resources.Load <Sprite> ("Furniture/Icons/" + name);
//        this.furnitureModel = Resources.Load <GameObject> ("Furniture/Models/" + name);
//    }

    public Furniture(int id, string name, FurnitureType type, string icon, string model)
    {
        this.id = id;
        this.name = name;
        this.type = type;
        iconImage = Resources.Load<Sprite>("Furniture/Icons/" + icon);
        furniturePrefab = Resources.Load<GameObject>("Furniture/Models/" + model);
    }
    

    public string Name
    {
        get { return name; }
        set { name = value; }
    }


    public FurnitureType Type
    {
        get { return type; }
        set { type = value; }
    }
}