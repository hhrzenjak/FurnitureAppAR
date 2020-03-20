using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class CreateButtons
{
    public TrackableBehaviour trackablePlane;
    public List<FurnitureGameObject> savedFurnitureObjects = new List<FurnitureGameObject>();
    public Dictionary<String, List<Material>> objectMaterials = new Dictionary<String, List<Material>>();

    // Start is called before the first frame update
    public CreateButtons(TrackableBehaviour trackablePlane)
    {
        this.trackablePlane = trackablePlane;
    }


    public void AddButtonList(Dictionary<String, Furniture> furnitureData)
    {
        FurnitureType currentCategory = FurnitureType.chair;
        Transform contentPanel = GameObject.Find("ButtonContent").GetComponent<Transform>();
        
        foreach (KeyValuePair<String, Furniture> entry in furnitureData)
        {
            //category name
            if (entry.Value.type != currentCategory)
            {
                GameObject newCategoryLabel = Resources.Load<GameObject>("Prefabs/Category");
                GameObject categoryLabel = GameObject.Instantiate(newCategoryLabel);
                categoryLabel.transform.SetParent(contentPanel, false);
                String categoryText = entry.Value.type.ToString();
                categoryText = char.ToUpper(categoryText[0]) + categoryText.Substring(1) + 's';
                categoryLabel.GetComponent<Text>().text = categoryText;
                currentCategory = entry.Value.type;
            }

            //cretaing buttons
            GameObject newButtonPrefab = Resources.Load<GameObject>("Prefabs/SampleButton");
            GameObject placeButton = GameObject.Instantiate(newButtonPrefab, contentPanel, false);
            //placeButton.GetComponent<Button>().GetComponentInChildren<Text>().text = entry.Value.name;

            placeButton.GetComponent<Image>().sprite = entry.Value.iconImage;
            placeButton.SetActive(true);
            placeButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                SpawnModel(entry.Value.furniturePrefab, entry.Value.name, trackablePlane);
            });
        }
    }


    private void SpawnModel(GameObject furniturePrefab, String furnitureName, TrackableBehaviour theTrackablePlane)
    {
        Vector3 currentPos = new Vector3(0, 0, 0);
        //if object isn't placed hide it
        foreach (FurnitureGameObject furnitureObject in savedFurnitureObjects)
        {
            if (furnitureObject.isPlaced == false)
            {
                furnitureObject.instance.SetActive(false);
                currentPos = furnitureObject.instance.transform.localPosition;
            }
            else if (furnitureObject.isPlaced == null)
            {
                furnitureObject.instance.SetActive(false);
                furnitureObject.isPlaced = false;
            }
        }

        // Create an object from prefab
        GameObject furnitureModel = GameObject.Instantiate(furniturePrefab, new Vector3(0, 0, 0), Quaternion.identity);

        // Add trackable as parent of created object
        furnitureModel.transform.SetParent(theTrackablePlane.transform);

        //Add scripts to object
        furnitureModel.AddComponent<Translate>();
        furnitureModel.AddComponent<Scale2>();
        furnitureModel.AddComponent<Rotate2>();

        // Adjust the position and scale
        furnitureModel.transform.localPosition = currentPos;
        //furnitureModel.transform.localRotation = Quaternion.identity;
        //furnitureModel.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        
        // Make sure it is active
        furnitureModel.SetActive(true);

        FurnitureGameObject newFurnitureModel = new FurnitureGameObject(furnitureModel, furnitureName);
        //add to list of saved objects
        savedFurnitureObjects.Add(newFurnitureModel);

        AddColorButtons(newFurnitureModel);
    }

    public void AddPlaceButtonListener()
    {
        Button placeButton = GameObject.Find("PlaceButton").GetComponent<Button>();
        placeButton.onClick.AddListener(() => { PlaceObject(); });
    }
    
    private void PlaceObject()
    {
        foreach (FurnitureGameObject furnitureObject in savedFurnitureObjects)
        {
            if (furnitureObject.isPlaced == null)
            {
                furnitureObject.instance.GetComponent<Translate>().enabled = false;
                furnitureObject.instance.GetComponent<Scale2>().enabled = false;
                furnitureObject.instance.GetComponent<Rotate2>().enabled = false;

                furnitureObject.isPlaced = true;
            }
        }
    }

    public void AddDeleteButtonListener()
    {
        Button placeButton = GameObject.Find("DeleteButton").GetComponent<Button>();
        placeButton.onClick.AddListener(() => { DeleteObjects(); });
    }

    private void DeleteObjects()
    {
        foreach (FurnitureGameObject furnitureObject in savedFurnitureObjects)
        {
            if (furnitureObject.isPlaced == true)
            {
                GameObject.Destroy(furnitureObject.instance);
                furnitureObject.isPlaced = false;
            }
        }

        savedFurnitureObjects.RemoveAll(item => item.isPlaced == false);
    }

    private void AddColorButtons(FurnitureGameObject furnitureModel)
    {
        Transform content = GameObject.Find("ColorsContent").GetComponent<Transform>();

        //remove old ones
        for (int i = 1; i < content.childCount; i++)
        {
            Transform child = content.GetChild(i);
            GameObject.Destroy(child.gameObject);
        }

        if (!objectMaterials.ContainsKey(furnitureModel.name))
        {
            //add available materials to list
            List<Material> newMaterialList = new List<Material>();
            int i = 1;
            while (true)
            {
                String name = "Furniture/Materials/" + furnitureModel.name + "_M" + i;
                Material material = Resources.Load<Material>(name);
                if (material == null)
                {
                    break;
                }

                newMaterialList.Add(material);
                i += 1;
            }

            objectMaterials.Add(furnitureModel.name, newMaterialList);
        }

        List<Material> materials = new List<Material>(objectMaterials[furnitureModel.name]);


        foreach (Material material in materials)
        {
            //cretaing buttons
            GameObject newButtonPrefab = Resources.Load<GameObject>("Prefabs/ColorButton");
            GameObject placeButton = GameObject.Instantiate(newButtonPrefab);
            placeButton.transform.SetParent(content, false);
            placeButton.GetComponent<Image>().color = material.GetColor("_SpecColor");
            //placeButton.GetComponent<Button>().GetComponentInChildren<Text>().text = entry.Value.name;
            placeButton.SetActive(true);
            placeButton.GetComponent<Button>().onClick.AddListener(() => { ChangeColor(furnitureModel, material); });
        }
    }

    private void ChangeColor(FurnitureGameObject furniture, Material material)
    {
        Renderer rend = furniture.instance.GetComponent<Renderer>();
        rend.material = material;
    }
}