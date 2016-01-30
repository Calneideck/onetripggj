using UnityEngine;
using System.Collections;

public class House : MonoBehaviour
{
    private HouseColour colour;
    private PackageType packageType;

    void Start ()
    {
		
	}

    public void Setup(HouseColour colour, PackageType packageType)
    {
        this.colour = colour;
        this.packageType = packageType;
    }
}
