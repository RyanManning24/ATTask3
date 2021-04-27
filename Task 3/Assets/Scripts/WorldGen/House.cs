using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool checkForFloor()
    {
        RaycastHit hit;
        Vector3 housePos = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
        if (Physics.Raycast(housePos, -Vector3.up, out hit, Mathf.Infinity))
        {
            GameObject floorObj = hit.transform.gameObject;
            if(floorObj.activeSelf)
            {
                //load in the house
                return true;
            }
            else
            {
                //unload house
                return false;
            }
        }

        return false;
    }
}
