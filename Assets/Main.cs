using UnityEngine;
using Vuforia;

public class Main : MonoBehaviour
{
    public TrackableBehaviour theTrackablePlane;

    void Start()
    {
        LoadFurnitureData loader = new LoadFurnitureData();
        loader.Load();
        CreateButtons creator = new CreateButtons(theTrackablePlane);
        creator.AddButtonList(loader.furnitureData);
        creator.AddPlaceButtonListener();
        creator.AddDeleteButtonListener();

    }
}



