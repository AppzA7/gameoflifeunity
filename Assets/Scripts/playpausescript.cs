using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playpausescript : MonoBehaviour
{
    public GameObject start, pause;

    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<SpriteRenderer>().sprite = getStatePrefab().GetComponent<SpriteRenderer>().sprite; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() {
        int newstate = GridManager.getState() + 1;
        newstate = newstate%2;
        GridManager.updateState(newstate);
        GetComponent<SpriteRenderer>().sprite = getStatePrefab().GetComponent<SpriteRenderer>().sprite; 
    }

    private GameObject getStatePrefab() {
        if(GridManager.getState() == 1) {
            return pause;
        } else {
            return start;
        }
    }
}
