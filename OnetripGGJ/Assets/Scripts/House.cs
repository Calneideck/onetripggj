using UnityEngine;

public class House : MonoBehaviour
{
    private HouseStruct houseInfo;

    public void Setup(HouseColour colour, PackageType packageType)
    {
        houseInfo = new HouseStruct(colour, packageType);
    }

    public HouseStruct HouseInfo
    {
        get { return houseInfo; }
    }
}
