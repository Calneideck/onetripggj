using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SetupHouses : MonoBehaviour
{
    public GameObject[] housePrefabs;
    public Transform[] houseSpawnLocations;
    public Transform houseHolderObject;
    public Material[] wallMaterials;

    public GameObject[] packageTypePrefabs;
    public Transform[] packageTypeSpawnLocations;
    public Transform packageTypeHolderObject;

    private List<HouseStruct> houses = new List<HouseStruct>();
    private float[] heightOffsets = new float[] { 1.89f, 1.2f, 1.34f, 2.1f, 1, 1.8f, 1.3f, 1.3f };

    void Start ()
    {
        // Create a list of each possible house combination then randomise the order
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 8; j++)
                houses.Add(new HouseStruct((HouseColour)i, (PackageType)j));

        for (int i = 0; i < houses.Count; i++)
        {
            HouseStruct temp = houses[i];
            int index = Random.Range(0, houses.Count);
            houses[i] = houses[index];
            houses[index] = temp;
        }

        // Create the actual house objects and assign their info using the above order
        for (int i = 0; i < houseSpawnLocations.Length; i++)
        {
            HouseStruct houseInfo = houses[i];
            GameObject house = (GameObject)GameObject.Instantiate(housePrefabs[Random.Range(0, housePrefabs.Length)], houseSpawnLocations[i].position, Quaternion.identity);
            house.transform.SetParent(houseHolderObject);
            house.GetComponent<House>().Setup(wallMaterials[(int)houseInfo.colour], houseInfo.colour, packageTypePrefabs[(int)houseInfo.packageType], houseInfo.packageType);

            // Create the package type object
            GameObject packageType = (GameObject)GameObject.Instantiate(packageTypePrefabs[(int)houseInfo.packageType], packageTypeSpawnLocations[i].position, Quaternion.identity);
            packageType.transform.SetParent(packageTypeHolderObject, true);
            packageType.transform.localPosition = new Vector3(packageType.transform.localPosition.x, heightOffsets[(int)houseInfo.packageType], packageType.transform.localPosition.z);
        }
	}
}
