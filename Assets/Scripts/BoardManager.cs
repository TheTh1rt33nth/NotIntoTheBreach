using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;



public class BoardManager : MonoBehaviour {
    
    public static BoardManager instance;
  
    private static int[][] currentMap;
    public Tile[][] CurrentMapTiled { get; set; }

    public static int[][] CurrentMap
    {
        get
        {
            return currentMap;
        }

        set
        {
            currentMap = value;
        }
    }

    public GameObject GroundTile;
    public GameObject MountainTile;
    public GameObject WaterTile;
    public GameObject[] ForestTile;
    public GameObject[] CityTile;

    private Transform boardHolder;
    public void ReadFromMap(int mapnumber)
    {
        string map = "Map_" + mapnumber.ToString() + ".txt";
        string path = "Assets\\Data\\" + map;
        string[] grid = File.ReadAllLines(path);
        CurrentMap = new int[grid.Length][];
        CurrentMapTiled = new Tile[grid.Length][];
        for (int i = 0; i < grid.Length; i++)
        {
            string[] stupidArray = grid[i].Split(' ');
            int[] grids = new int[stupidArray.Length];
            for (int j = 0; j < stupidArray.Length; j++)
            {
                grids[j] = Convert.ToInt32(stupidArray[j]);
            }
            CurrentMap[i] = new int[grids.Length];
            CurrentMapTiled[i] = new Tile[grids.Length];
            for (int j = 0; j < grids.Length; j++)
            {
                CurrentMap[i][j] = grids[j];
            }
        }
        BuildMap(CurrentMap);
    }
    void BuildMap(int[][] Map)
    {
        boardHolder = new GameObject("Board").transform;
        for (int i = 0; i < Map.Length; i++)
        {

            for (int j = 0; j < Map[i].Length; j++)
            {

                int type = CurrentMap[i][j];
                int x = i;
                int y = j;
                int health = 0;
                GameObject toInstantiate = GroundTile;
                bool ifObject = false;
                switch (type)
                {
                    case 0:
                        toInstantiate = GroundTile;

                        break;
                    case 1:
                        toInstantiate = MountainTile;
                        ifObject = true;
                        health = 2;
                        break;
                    case 2:
                        toInstantiate = WaterTile;

                        break;
                    case 3:
                        toInstantiate = ForestTile[0];

                        break;
                    case 5:
                        toInstantiate = CityTile[0];
                        ifObject = true;
                        health = 1;
                        break;
                    case 6:
                        toInstantiate = CityTile[1];
                        ifObject = true;
                        health = 2;
                        break;
                    case 7:
                        toInstantiate = CityTile[2];
                        ifObject = true;
                        health = 3;
                        break;

                }
                if (!ifObject)
                {
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    CurrentMapTiled[i][j] = new Tile(x, y, instance, 0, CurrentMap[x][y]);
                }
                else
                {
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    CurrentMapTiled[i][j] = new ObjectTile(x, y, instance, 0, CurrentMap[x][y], health);
                }
            }

        }
    }
  
        // Use this for initialization
    void Start () {
       
      
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }
}
