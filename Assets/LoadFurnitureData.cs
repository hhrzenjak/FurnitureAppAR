using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LoadFurnitureData
{
    public Dictionary<String, Furniture> furnitureData = new Dictionary<String, Furniture>();

    //read from csv file separated by ';', save data in dictionary, key is id
    public void Load()
    {
        TextAsset dataInput = Resources.Load<TextAsset>("FurnitureDataFile");
        if (dataInput != null)
        {
            String[] data = dataInput.text.Split('\n');
            for (int i = 1; i < data.Length - 1; i++)
            {
                String[] row = data[i].Split(';');

                Furniture newFurniture = new Furniture(Int32.Parse(row[0]), row[1],
                    (FurnitureType) Enum.Parse(typeof(FurnitureType), row[2]), row[3],
                    row[4].Trim());
                furnitureData.Add(row[0], newFurniture);
            }
        }
    }
}




