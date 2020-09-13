using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeCellState : MonoBehaviour
{
    public GameObject filled;
    public GameObject empty;

    public bool mouseover = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver() {
        if(!mouseover) {
            mouseover = true;
            Debug.Log("mouse entered " + this.transform.position);
            if(Input.GetMouseButton(0)) {
                this.flipColour();
            }
        }
    }

    void OnMouseExit() {
        if(mouseover) {
            mouseover = false;
            print("mouse left " + this.transform.position);
        }
    }
    void OnMouseDown() {
        Debug.Log("Clicked :" + this.transform.position);
        Debug.Log(GetComponent<SpriteRenderer>().sprite.name);
        Debug.Log(filled.GetComponent<SpriteRenderer>().sprite.name);
        Debug.Log(empty.GetComponent<SpriteRenderer>().sprite.name);
        this.flipColour();
    }
    private void flipColour() {
        if(GetComponent<SpriteRenderer>().sprite.name.Contains(filled.GetComponent<SpriteRenderer>().sprite.name)) {
            GetComponent<SpriteRenderer>().sprite = empty.GetComponent<SpriteRenderer>().sprite;
        } else {
            GetComponent<SpriteRenderer>().sprite = filled.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
