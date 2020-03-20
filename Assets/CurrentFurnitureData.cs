
using System;
using UnityEngine;

//class represents instances of objects in scene
public class FurnitureGameObject
{
        public GameObject instance;

        public string name;
        //if pnly spawned == null, if placed true if not false
        public Nullable<bool> isPlaced = null;

        public FurnitureGameObject(GameObject instance, string name)
        {
                this.instance = instance;
                this.name = name;
        }
}


