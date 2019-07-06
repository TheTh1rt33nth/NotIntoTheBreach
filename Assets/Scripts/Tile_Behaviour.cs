using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EventType {Defend, Attack, Kill, Damage};


public  class Tile 
{
    
    public int x;
    public int y;
    public GameObject prefab;
    
    public int ifTaken; //0-пустой, 1-занят наземным, 2-занят летающим
    public int ifSelected;
    public int type;
    public List<NatureEvent> natureEvents;
    public Tile()
    {

    }

    public Tile(int x, int y, GameObject pref, int IsOccupied, int type)
    {
        this.x = x;
        this.y = y;
        this.prefab = pref;
        this.ifTaken = IsOccupied;
        this.type = type;
        ifSelected = 0;
        natureEvents = new List<NatureEvent>();
        
    }
   
    
}
public class ObjectTile : Tile
{
    public int health;
    public ObjectTile(int x, int y, GameObject pref, int IsOccupied, int type, int hp):base(x,y,pref,IsOccupied,type)
    {
        this.health = hp;
    }
}

public class NatureEvent 
{

    private int type;
    private int lifespan;
    private int effectiveness;
    
    public int Lifespan
    {
        get
        {
            return lifespan;
        }

        set
        {
            lifespan = value;
        }
    }

    public int Effectiveness
    {
        get
        {
            return effectiveness;
        }

        set
        {
            effectiveness = value;
        }
    }

    public int ModifyDamage(int damage)
    {
        int d = damage;
        if(type==(int)EventType.Defend)
        {
            d = d - Effectiveness;
        }
        if (type ==(int)EventType.Attack)
        {
            d = d + Effectiveness;
        }
        if (type == (int)EventType.Kill)
        {
            d = 10;
            Lifespan = 0;
        }
        
        return d;
    }
    public void NatureDamage()
    {

    }

}
public class TileBehaviour 
{

}