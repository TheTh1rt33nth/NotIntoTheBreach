using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class SpriteSelection : MonoBehaviour {
    private SpriteRenderer sprite;
    public GameObject Outliner;
    private GameObject OutlinerExample;
    private SpriteRenderer OutlinerSprite;
    Transform trans;


    void Start () {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        
        OutlinerSprite=Outliner.GetComponent<SpriteRenderer>();
        OutlinerExample = Instantiate(Outliner, gameObject.transform);
        
        OutlinerExample.SetActive(false);
    }
	public void Select()
    {
       float desiredScale = 1.1f;
        sprite.sortingOrder += 1;
       
        transform.localScale = new Vector3(desiredScale, desiredScale, desiredScale);
        

    }
    public void DeSelect()
    {
        float desiredScale = 1f; 
        transform.localScale = new Vector3(desiredScale, desiredScale, desiredScale);
      
        sprite.sortingOrder -= 1;
       
    }
    public void SpriteSelect()
    {
        OutlinerExample.SetActive(true);
    }
    public void SpriteDeSelect()
    {
        OutlinerExample.SetActive(false);
    }
    // Update is called once per frame
    /*void OnMouseOver()
    {
        SpriteSelect();
        
    }
    void OnMouseExit()
    {
        SpriteDeSelect();
     
    }
    */
    void Update ()
    {
    }

}
