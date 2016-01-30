using UnityEngine;

public class House : MonoBehaviour
{
    public MeshRenderer[] renderers;

    private HouseStruct houseInfo;

    public void Setup(Material wallMaterial, HouseColour colour, PackageType packageType)
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
