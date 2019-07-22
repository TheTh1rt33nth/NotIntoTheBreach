using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public BoardManager boardScript;
    private int map = 1;
    
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
       if(prevselect)
        prevselect.SpriteDeSelect();
        RaycastHit hit;
        int x, y;
        x = 0;
        y = 0;
        Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hitInformation = Physics2D.GetRayIntersection(ray, 10);
        SpriteSelection select=null; 
        
        if (hitInformation)
        {
            x = System.Convert.ToInt32(hitInformation.transform.position.x);
            y = System.Convert.ToInt32(hitInformation.transform.position.y);
            if (hitInformation.transform.tag == "Tile")
            {
                select = boardScript.CurrentMapTiled[x][y].prefab.GetComponent<SpriteSelection>();
                select.SpriteSelect();
            }
        }
       
        prevselect = select;
        
    }
}
