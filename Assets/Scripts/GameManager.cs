using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public BoardManager boardScript;
    private int map = 1;
    public Camera camera;
    private SpriteSelection prevselect;
    // Use this for initialization
    void Awake () {
        boardScript = GetComponent<BoardManager>();
        InitGame();
       
        

    }
	void InitGame()
    {
        boardScript.ReadFromMap(map);
        prevselect = boardScript.CurrentMapTiled[0][0].prefab.GetComponent<SpriteSelection>();
    }
	// Update is called once per frame
	void Update () {
       
        prevselect.DeSelect();
        RaycastHit hit;
        int x, y;
        x = 0;
        y = 0;
        Ray ray;
        ray = camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 1f);
        
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("That Shit is working");
            Transform objectHit = hit.transform;
            Debug.Log(x + " " + y);
            x = (int)objectHit.transform.position.x;
            y = (int)objectHit.transform.position.y;
            Debug.Log(x + " " + y);
        }
        
        else
        {
            
           
            Debug.Log("Drawn");
        }
        SpriteSelection select = boardScript.CurrentMapTiled[x][y].prefab.GetComponent<SpriteSelection>();
        select.SpriteSelect();
        prevselect = select;
        
    }
}
