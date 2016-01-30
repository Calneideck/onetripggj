using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SetupHouses : MonoBehaviour
{
    public Transform[] spawnLocations;
    public GameObject[] housePrefabs;
    public Transform houseHolderObject;

    private List<HouseStruct> houses = new List<HouseStruct>();

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
        for (int i = 0; i < spawnLocations.Length; i++)
        {
            HouseStruct houseInfo = houses[i];

            GameObject house = (GameObject)GameObject.Instantiate(housePrefabs[(int)houseInfo.colour], spawnLocations[i].position, Quaternion.identity);
            house.transform.SetParent(houseHolderObject);
            house.GetComponent<House>().Setup(houseInfo.colour, houseInfo.packageType);
            house.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = houseInfo.packageType.ToString();
        }
	}
}
