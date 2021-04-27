using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public GameObject player;
    public Vector3 npcPos;
    public float destinationX;
    public float destinationZ;
    public float speed = 5;

    private GameObject floor;
    bool startCo = true;

    [Range(-1, 1)]
    public int movingUpX = 0;
    [Range(-1, 1)]
    public int movingUpZ = 0;

    

    [Range(-1,1)]
    public int stopNPCX = 0;
    [Range(-1, 1)]
    public int stopNPCZ = 0;

    public bool changewaypoint = true;

    //create a destinations
    //move the npc to this point (parent the npc to the floor its on?)
    //always check the position of the npc if it stays in place for a bit create new waypoint 
    //when the npc gets to this point create a new point

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("PlayerObj");
        setUpPoints();
        resetValues();
        
        
    }

    private void Start()
    {

        StartCoroutine(npcPosition());

    }

    // Update is called once per frame
    void Update()
    {
        GetChunk();
        checkPos();

        Debug.Log(destinationX);
    }

    private void LateUpdate()
    {
        stopMove();
        Move();
    }

    void setUpPoints()
    {
        destinationX = Random.Range(player.transform.position.x - 100, player.transform.position.x + 100);
        destinationZ = Random.Range(player.transform.position.z - 100, player.transform.position.z + 100);

        stopNPCX = 1;
        stopNPCZ = 1;
    }

    void Move()
    {
        if(stopNPCX == 1)
        {
            if (destinationX > 0)
            {
                //move npc uptowards value
                movingUpX = 1;
                Vector3 temp = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
                transform.position = new Vector3(temp.x, transform.position.y, transform.position.z);
            }
            else if (destinationX < 0)
            {
                movingUpX = -1;
                //move npc down towards value
                Vector3 temp = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
                transform.position = new Vector3(temp.x, transform.position.y, transform.position.z);
            }
            else
            {
                //move npc towards 0
            }
        }

        if(stopNPCZ == 1)
        {
            if (destinationZ > 0)
            {
                //move npc uptowards value
                movingUpZ = 1;
                Vector3 temp = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, temp.z);
            }
            else if (destinationZ < 0)
            {
                //move npc down towards value
                movingUpZ = -1;
                Vector3 temp = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, temp.z);
            }
            else
            {
                //move npc towards 0
            }
        }


    }

    void stopMove()
    {
        if(movingUpX == 1)
        {
            //moving up to point
            if(transform.position.x >= destinationX)
            {
                //stop movement
                stopNPCX = -1;
            }
        }
        else if(movingUpX == -1)
        {
            //moving down to point
            if (transform.position.x <= destinationX)
            {
                //stop movement
                stopNPCX = -1;
            }
        }

        if(movingUpZ == 1)
        {
            //moving up to point
            if (transform.position.z >= destinationZ)
            {
                //stop movement
                stopNPCZ = -1;
            }
        }
        else if(movingUpZ == -1)
        {
            //moving down to point
            if (transform.position.z <= destinationZ)
            {
                //stop movement
                stopNPCZ = -1;
            }
        }
    }

    IEnumerator npcPosition()
    {
        while(startCo)
        {
            if (npcPos == transform.position)
            {
                yield return new WaitForSecondsRealtime(3f);
                if (npcPos == transform.position)
                {
                    //change waypoints
                    changewaypoint = true;
                    if (changewaypoint)
                    {
                        changewaypoint = false;
                        setUpPoints();
                        resetValues();

                    }
                }
            }
            npcPos = transform.position;
        }
        yield return null;
    }

    void resetValues()
    {
        movingUpX = 0;
        movingUpZ = 0;
        stopNPCX = 1;
        stopNPCZ = 1;
    }
    void GetChunk()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -Vector3.up, out hit , Mathf.Infinity))
        {
            floor = hit.transform.gameObject;
            transform.SetParent(floor.gameObject.transform);
        }

    }
    void checkPos()
    {
        if(destinationX >= player.transform.position.x + 100 || destinationX <= player.transform.position.x - 100)
        {
            setUpPoints();
            resetValues();
        }
    }

}
