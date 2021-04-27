using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGen : MonoBehaviour
{

    public Transform Player;

    public static Dictionary<ChunkPos, Chunk> chunks = new Dictionary<ChunkPos, Chunk>();
    List<Chunk> pooledChunks = new List<Chunk>();
    List<ChunkPos> toGenerate = new List<ChunkPos>();
    public LoadWorld LoadWorldScript;

    public int chunkDistance = 5;
    bool househasLoaded = false;

    public int amountOfNPCS = 3;
    private GameObject[] NPCS;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
       

        LoadChunks(true);
        for(int i = 0; i <= amountOfNPCS; i++)
        {
            LoadWorldScript.CreateNPC();
            //Debug.Log(NPCS.Length);

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        LoadChunks();
        
        
    }

    void buildChunk(int xPos, int zPos)
    {
        Chunk chunk;
        if (pooledChunks.Count > 0)
        {
            chunk = pooledChunks[0];
            chunk.gameObject.SetActive(true);
            pooledChunks.RemoveAt(0);
            chunk.transform.position = new Vector3(xPos, 0, zPos);
        }
        else
        {
            GameObject floor = LoadWorldScript.CreateFloor(xPos, zPos);
            chunk = floor.GetComponent<Chunk>();
            //findHouse(chunk);

        }
        
        chunks.Add(new ChunkPos(xPos, zPos), chunk);

        if(!househasLoaded)
        {
            if (chunk.SpawnHouse(LoadWorldScript))
            {
                househasLoaded = true;
            }
        }
       
    }

    ChunkPos curChunk = new ChunkPos(-1, -1);
    void LoadChunks(bool instant = false)
    {
        //is the player in current chunk 
        int curChunkPosX = Mathf.FloorToInt(Player.position.x /20) * 20;
        int curChunkPosZ = Mathf.FloorToInt(Player.position.z /20) * 20;

        //load chunk
        if (curChunk.x != curChunkPosX || curChunk.z != curChunkPosZ)
        {
            curChunk.x = curChunkPosX;
            curChunk.z = curChunkPosZ;

            for(int i = curChunkPosX - 20 * chunkDistance;i <= curChunkPosX + 20 * chunkDistance; i+= 20)
            {
                for(int j = curChunkPosZ - 20 * chunkDistance; j <= curChunkPosZ + 20 * chunkDistance; j += 20)
                {
                    ChunkPos cp = new ChunkPos(i, j);

                    if (!chunks.ContainsKey(cp) && !toGenerate.Contains(cp))
                    {
                        if (instant)
                        {
                            buildChunk(i, j);
                            
                        }
                        else 
                        {
                            toGenerate.Add(cp);
                        }
                    }
                }
            }
            //build house
            
            
            //remove chunks
            List<ChunkPos> toDestroy = new List<ChunkPos>();

            foreach(KeyValuePair<ChunkPos, Chunk> c in chunks)
            {
                 ChunkPos cp = c.Key;
                if(Mathf.Abs(curChunkPosX - cp.x) > 20 * (chunkDistance + 3) || Mathf.Abs(curChunkPosZ - cp.z) > 20 * (chunkDistance + 3))
                {
                    toDestroy.Add(c.Key);
                }
            }

            
            foreach (ChunkPos cp in toGenerate)
            {
                if (Mathf.Abs(curChunkPosX - cp.x) > 20 * (chunkDistance + 1) || Mathf.Abs(curChunkPosZ - cp.z) > 20 * (chunkDistance + 1))
                {
                    toGenerate.Remove(cp);
                    
                }
            }

            
            foreach(ChunkPos cp in toDestroy)
            {
                chunks[cp].gameObject.SetActive(false);
                pooledChunks.Add(chunks[cp]);
                chunks.Remove(cp);
            }

            StartCoroutine(DelayBuildChunks());

        }
    }

    IEnumerator DelayBuildChunks()
    {
        while(toGenerate.Count > 0)
        {
            buildChunk(toGenerate[0].x,toGenerate[0].z);
            toGenerate.RemoveAt(0);

            yield return new WaitForSeconds(.2f);
        }
    }

    public struct ChunkPos
    {
        public int x, z;
        public ChunkPos(int x, int z)
        {
            this.x = x;
            this.z = z;
        }
    }

}
