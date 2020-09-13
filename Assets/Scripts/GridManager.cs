using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager : MonoBehaviour
{
    //0 paused
    //1 started
    private static int state = 0;
    public int width;
    public int height;
    private List<GameObject> cells = new List<GameObject>();
    private List<GameObject> newCells = new List<GameObject>();
    public GameObject cell;

    public  GameObject filled, empty;
    // Start is called before the first frame update
    private int front = 0;
    private int back = 1;
    void Start()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject tile = (GameObject) Instantiate(cell, transform);
                tile.transform.position = new Vector3(x, y, front);
                cells.Add(tile);
                GameObject tile1 = (GameObject) Instantiate(cell, transform);
                tile1.transform.position = new Vector3(x + width + 1, y, back);
                newCells.Add(tile1);
            }
        }
    }

    public static float nextUpdate = 10000f;
    // Update is called once per frame
    void Update()
    {
        if(GridManager.state == 1) {
            if(Time.time >= nextUpdate) {
                nextUpdate = Time.time + nextUpdate/1000f;
                UpdateEverySecond();
            }
        }
    }

    void UpdateEverySecond(){
        Debug.Log("UpDATE!!@!");
        string filledname = filled.GetComponent<SpriteRenderer>().sprite.name;
        string emptyname = empty.GetComponent<SpriteRenderer>().sprite.name;
        Sprite emptysprite = empty.GetComponent<SpriteRenderer>().sprite;
        Sprite filledsprite = filled.GetComponent<SpriteRenderer>().sprite;

        for(int i=0;i<cells.Count;i++) {
            List<int> n = getNeighbors(i);
            GameObject cur = cells.ElementAt(i);
            int ln = liveNeighbours(n, cells);
            //if filled
            
            if(cur.GetComponent<SpriteRenderer>().sprite.name.Contains(filledname)) {
                //more than 3 neighbours
                if(ln < 2 || ln > 3) {
                    newCells.ElementAt(i).GetComponent<SpriteRenderer>().sprite = emptysprite;
                }
            } else {
                //empty cell
                if(ln == 3) {
                    newCells.ElementAt(i).GetComponent<SpriteRenderer>().sprite = filledsprite;
                }
            }
        }

        List<GameObject> temp;
        temp = cells;
        cells = newCells;
        newCells = cells;
        
        //send new front
        foreach(GameObject o in cells) {
            o.transform.position = new Vector3(o.transform.position.x, o.transform.position.y, front);
        }
        //send old back
        foreach(GameObject o in newCells) {
            o.transform.position = new Vector3(o.transform.position.x, o.transform.position.y, back);            
            o.GetComponent<SpriteRenderer>().sprite = emptysprite;
        }
        //swap lists
        // for(int i=0;i<cells.Count;i++) {
        //     GameObject o = cells.ElementAt(i);
        //     cells[i] = newCells.ElementAt(i);
        //     newCells[i] = o;
        // }

        
        
    }

    private int liveNeighbours(List<int> n, List<GameObject> c) {
        int count = 0;
        foreach(int i in n) {
            if(c.ElementAt(i).GetComponent<SpriteRenderer>().sprite.name.Contains(filled.GetComponent<SpriteRenderer>().sprite.name)) {
                count++;
            }
        }
        return count;
    }
    private List<int> getNeighbors(int cur) {
        List<int> neighbours = new List<int>();
        //left
        if((cur-1 >= 0) && (cur-1)%width < cur%width){
            neighbours.Add(cur-1);
        }
        //right
        if((cur+1)%width > cur%width) {
            neighbours.Add(cur+1);
        }
        //topright
        if((cur - width + 1 >=0 )&&(cur - width + 1)%width > cur%width) {
            neighbours.Add(cur - width + 1);
        }
        //bottom left
        if((cur + width - 1 < cells.Count) && (cur + width - 1)%width < cur%width) {
            neighbours.Add(cur + width -1);
        }

        //top
        if(cur - width > 0) {
            neighbours.Add(cur - width);
        }
        //top left
        if((cur - width - 1) > 0 && (cur - width - 1)%width <  cur%width) {
            neighbours.Add(cur - width -1);
        }
        //bottom
        if(cur + width < cells.Count) {
            neighbours.Add(cur + width);
        }
        //bottom right
        if(((cur + width + 1)%width > cur%width) && (cur + width + 1) < cells.Count) {
            neighbours.Add(cur + width+1);
        }
    
        return neighbours;
    }
    public static void updateState(int newstate) {
        if(newstate == 1) {
            nextUpdate = Mathf.FloorToInt(Time.time+1);
        }
        state = newstate;
    }
    public static int getState() {
        return state;
    }
} 
