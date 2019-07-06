using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementScript : MonoBehaviour {
    public float moveTime = 0.1f;
    private float inverseMoveTime;
    private Rigidbody2D rb2D;
    private BoardManager navigator;
    public struct Point
    {
        public int x { get; set; }
        public int y { get; set; }
    }
    // public LayerMask blockingLayer;

    protected virtual void Start () {
        inverseMoveTime = 1f / moveTime;
        navigator = GetComponent<BoardManager>();
        
    }
	protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        while (sqrRemainingDistance>float.Epsilon)
        {
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPostion);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }
    protected abstract void OnCantMove<T>(T component)
           where T : Component;

    protected Queue<Point> TraceRoute (int x, int y, int xEnd, int yEnd, Queue<Point> route)
    {
        Point p = new Point();
        p.x = x;
        p.y = y;
        route.Enqueue(p);
        if (x == xEnd && y == yEnd)
            return route;
        else
        {
            for (int i=0; i<4; i++)
            {
                switch(i)
                {
                    case 0: //вверх
                        if (y!=0)
                        {
                            if (navigator.CurrentMapTiled[x][y-1].ifTaken!=1&&!(navigator.CurrentMapTiled[x][y - 1] is ObjectTile))
                            {

                                
                                return TraceRoute(x, y - 1, xEnd, yEnd, route);
                            }
                                
                        }
                        break;
                    case 1: //вправо
                        if (x!=navigator.CurrentMapTiled[x].Length)
                        {
                            if (navigator.CurrentMapTiled[x+1][y].ifTaken != 1&&!(navigator.CurrentMapTiled[x+1][y] is ObjectTile))
                            {
                                
                                return TraceRoute(x+1, y , xEnd, yEnd, route);
                            }
                        }
                        break;
                    case 2: //вниз
                        if (y!=navigator.CurrentMapTiled.Length)
                        {
                            if (navigator.CurrentMapTiled[x][y + 1].ifTaken != 1 && !(navigator.CurrentMapTiled[x][y + 1] is ObjectTile))
                            {
                                
                                return TraceRoute(x, y + 1, xEnd, yEnd, route);
                            }
                        }
                        break;
                    case 3: //влево
                        if (x!=0)
                        {
                            if (navigator.CurrentMapTiled[x -1][y].ifTaken != 1 && !(navigator.CurrentMapTiled[x-1][y] is ObjectTile))
                            {
                                
                                return TraceRoute(x - 1, y, xEnd, yEnd, route);
                            }
                        }
                        break;
                }
               
            }
            return null;
        }
    }
    
    protected void MapAvailibleTiles(int x, int y, int move, int moved)
    {
        SpriteSelection select=navigator.CurrentMapTiled[x][y].prefab.GetComponent<SpriteSelection>();
        select.SpriteSelect();

        if (moved<=move)
        {
            for (int i=0; i<=3; i++)
            {
                switch(i)
                {
                    case 0://вверх
                        if (y != 0)
                        {
                            if (navigator.CurrentMapTiled[x][y - 1].ifTaken != 1 && !(navigator.CurrentMapTiled[x][y - 1] is ObjectTile))
                            {
                                MapAvailibleTiles(x, y - 1, move, moved + 1);
                            }
                        }

                                break;

                    case 1://вправо
                        if (x != navigator.CurrentMapTiled[x].Length)
                        {
                            if (navigator.CurrentMapTiled[x + 1][y].ifTaken != 1 && !(navigator.CurrentMapTiled[x + 1][y] is ObjectTile))
                            {
                                MapAvailibleTiles(x+1, y, move, moved + 1);
                            }
                        }
                                break;

                    case 2://вниз
                        if (y != navigator.CurrentMapTiled.Length)
                        {
                            if (navigator.CurrentMapTiled[x][y + 1].ifTaken != 1 && !(navigator.CurrentMapTiled[x][y + 1] is ObjectTile))
                            {
                                MapAvailibleTiles(x, y + 1, move, moved + 1);
                            }
                        }
                                break;

                    case 3://влево
                        if (x != 0)
                        {
                            if (navigator.CurrentMapTiled[x - 1][y].ifTaken != 1 && !(navigator.CurrentMapTiled[x - 1][y] is ObjectTile))
                            {
                                MapAvailibleTiles(x - 1, y, move, moved + 1);
                            }
                        }
                                break;
                }
            }
        }
        
    }
    protected void DeMapAvailibleTiles(int x, int y, int move, int moved)
    {
        SpriteSelection select = navigator.CurrentMapTiled[x][y].prefab.GetComponent<SpriteSelection>();
        select.SpriteDeSelect();

        if (moved <= move)
        {
            for (int i = 0; i <= 3; i++)
            {
                switch (i)
                {
                    case 0://вверх
                        if (y != 0)
                        {
                            if (navigator.CurrentMapTiled[x][y - 1].ifTaken != 1 && !(navigator.CurrentMapTiled[x][y - 1] is ObjectTile))
                            {
                                MapAvailibleTiles(x, y - 1, move, moved + 1);
                            }
                        }

                        break;

                    case 1://вправо
                        if (x != navigator.CurrentMapTiled[x].Length)
                        {
                            if (navigator.CurrentMapTiled[x + 1][y].ifTaken != 1 && !(navigator.CurrentMapTiled[x + 1][y] is ObjectTile))
                            {
                                MapAvailibleTiles(x + 1, y, move, moved + 1);
                            }
                        }
                        break;

                    case 2://вниз
                        if (y != navigator.CurrentMapTiled.Length)
                        {
                            if (navigator.CurrentMapTiled[x][y + 1].ifTaken != 1 && !(navigator.CurrentMapTiled[x][y + 1] is ObjectTile))
                            {
                                MapAvailibleTiles(x, y + 1, move, moved + 1);
                            }
                        }
                        break;

                    case 3://влево
                        if (x != 0)
                        {
                            if (navigator.CurrentMapTiled[x - 1][y].ifTaken != 1 && !(navigator.CurrentMapTiled[x - 1][y] is ObjectTile))
                            {
                                MapAvailibleTiles(x - 1, y, move, moved + 1);
                            }
                        }
                        break;
                }
            }
        }

    }
    protected void MoveTile(int x, int y)
    {

        //Vector2 start = transform.position;
       // Vector2 end = start + new Vector2(xDir, yDir);

    }
   

}
