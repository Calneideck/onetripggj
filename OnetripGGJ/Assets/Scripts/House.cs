using UnityEngine;

public class House : MonoBehaviour
{
    public MeshRenderer[] renderers;

    private HouseStruct houseInfo;

    // Sets up the house assiging its colour to its renderer and creating the object that is used to uniquely identify it
    public void Setup(Material wallMaterial, HouseColour colour, GameObject packageTypePrefab, PackageType packageType)
    {
        houseInfo = new HouseStruct(colour, packageType);
        foreach (MeshRenderer renderer in renderers)
            renderer.material = wallMaterial;
    }

    public HouseStruct HouseInfo
    {
        get { return houseInfo; }
    }
}
