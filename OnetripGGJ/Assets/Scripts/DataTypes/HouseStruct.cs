using UnityEngine;

public struct HouseStruct
{
    public HouseColour colour;
    public PackageType packageType;

    public HouseStruct(HouseColour colour, PackageType packageType)
    {
        this.colour = colour;
        this.packageType = packageType;
    }

    public HouseStruct(bool random)
    {
        this.colour = (HouseColour)Random.Range(0, System.Enum.GetNames(typeof(HouseColour)).Length);
        this.packageType = (PackageType)Random.Range(0, System.Enum.GetNames(typeof(PackageType)).Length);
    }

    public static bool operator ==(HouseStruct a, HouseStruct b)
    {
        return (a.colour == b.colour && a.packageType == b.packageType);
    }

    public static bool operator !=(HouseStruct a, HouseStruct b)
    {
        return !(a.colour == b.colour && a.packageType == b.packageType);
    }
}