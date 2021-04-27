using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public const int chunkWidth = 16;
    public const int chunkHeight = 100;

    private Vector2 houseChunk;
    public bool houseHasSpawned = false;

    private int randomNumber;
    private GameObject house;
    private GameObject housePos;
    private House checkFloor;


    // Start is called before the first frame update
    void Awake()
    {
        randomNumber = Random.Range(0, 10);
        house = null;
        housePos = GameObject.Find("housePos");
        checkFloor = GameObject.FindObjectOfType<House>();

    }

    // Update is called once per frame
    void Update()
    {
        showHouse();
        
    }

    public bool SpawnHouse(LoadWorld loadWorldScript)
    {
        
        if (randomNumber == 2)
        {    
            house = loadWorldScript.CreateHouse();
            house.transform.SetParent(this.gameObject.transform);
            house.transform.localPosition = new Vector3(0, 0, 0);
            houseChunk = new Vector2(transform.position.x, transform.position.z);
            houseHasSpawned = true;

            house.transform.SetParent(null);
            housePos.transform.position = transform.position;
            house.transform.SetParent(housePos.gameObject.transform);
            
            
            
            return true;
        }

        return false;
        
    }
  
    void showHouse()
    {
        if(houseHasSpawned)
        {
            house.SetActive(checkFloor.checkForFloor());
        }

        
    }

    

}
