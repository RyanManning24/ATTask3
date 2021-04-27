using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadWorld : MonoBehaviour
{
    private string[] fileName = new string[5];
    private string[] file = new string[4];

    public string text;

    private string[] gameObj = new string[5];
    //maybe turn this struct to be all in one?
    private pos[] Pos = new pos[5];
    private pos[] Rot = new pos[5];
    

    // Start is called before the first frame update
    void Awake()
    {
       
        fileName[0] = Path.Combine(Application.dataPath, "Resources/Floor.txt");
        fileName[1] = Path.Combine(Application.dataPath , "Resources/NPC.txt");
        fileName[2] = Path.Combine(Application.dataPath , "Resources/House.txt");

        ReadLines(fileName[0], 0);

        ReadLines(fileName[1], 1);

        ReadLines(fileName[2], 2);
        //CreateNPC();

        /*file[0] = Path.Combine(Application.dataPath,"Resources/Floor.txt");
        file[1] = Path.Combine(Application.dataPath, "Resources/NPC.txt");
        file[2] = Path.Combine(Application.dataPath, "Resources/House.txt");

        ReadLines(file[0], 0);

        ReadLines(file[1], 1);

        ReadLines(file[2], 2);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReadLines(string fileName, int num)
    {
        int lineNumber = 0; //Int to show which line it is
        foreach(string option in File.ReadLines(fileName))
        {
            //Loops once for each line in the file
            switch (lineNumber)
            {
                case 0://Object
                    gameObj[num] = option;
                    break;
                case 1://Position X
                    Pos[num].x = option;
                    break;
                case 2://Position Y
                    Pos[num].y = option;
                    break;
                case 3://Position Z
                    Pos[num].z = option;
                    break;
                case 4://Rotation X
                    Rot[num].x = option;
                    break;
                case 5://Rotation Y
                    Rot[num].y = option;
                    break;
                case 6://Rotation Z
                    Rot[num].z = option;
                    break;
                default:
                    break;
            }
            lineNumber++;//Adds 1 to the int so that the next loop uses the next value.            
        }       
    }

    public GameObject CreateFloor(float xPos, float zPos)
    {
        if (gameObj[0] == "Floor")
        {
            GameObject f1 = Instantiate(Resources.Load("Models/Floor/Floor", typeof(GameObject))) as GameObject;
            f1.transform.position = new Vector3(xPos, 0, zPos);
            return f1;
        }
        return null;
    }

    public void CreateNPC()
    {
        if(gameObj[1] == "NPC")
        {
            GameObject NPC = Instantiate(Resources.Load("Models/NPC/NPC", typeof(GameObject))) as GameObject;
            NPC.transform.position = new Vector3(float.Parse(Pos[1].x),float.Parse(Pos[1].y), float.Parse(Pos[1].z));
            //return NPC;
        }
        //return null;
    }

    public GameObject CreateHouse()
    {
        if(gameObj[2] == "House")
        {
            GameObject House = Instantiate(Resources.Load("Models/House/House", typeof(GameObject))) as GameObject;
            House.transform.position = new Vector3(0, 0, 0);

            return House;
        }
        return null;
    }



    public struct pos
    {
        public string x;
        public string y;
        public string z;
    };
    

}

