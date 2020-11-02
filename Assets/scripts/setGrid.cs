using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class setGrid : MonoBehaviour
{
    
    public List<GameObject> numbers;
    public Transform startTransform;
    public GameObject obj;
    public GameObject canvasBox;
    public static GameObject[,] grid;
    public Number[,] field;
    Vector3 delta = new Vector3(50,0,0);
    public int W = 10;
    public int H = 4;
    public int sizeOFGridElement = 100;
    public int leftRightDelta = 5;
    public int topButtonDelta = 1;

    public bool ceck_ = false;

    private int startNumbersCount;
    void Start()
    {
        grid = new GameObject[H,W];
        startNumbersCount = numbers.Count;
        field = new Number[H,W];
        int n = 0;

    	for(int i = 0; i < H; i++)
    	{
	        for(int j = 0; j < W; j++)
	        {
	        	var clone = Instantiate(obj, startTransform.position + new Vector3((sizeOFGridElement + leftRightDelta) * j, (sizeOFGridElement + topButtonDelta) * i, 0), startTransform.rotation, canvasBox.transform);
                clone.transform.SetSiblingIndex(0);
                grid[i,j] = clone;

                n++;
                if(n < numbers.Count-1)
                    field[i,j] = new Number(grid[i,j], 0, new GameObject());
	        }
	    }
    }

    void Update()
    {
        if(ceck_)
            Check();
    }


    void Check()
    {
        List<GameObject> sequnce = new List<GameObject>();
        for(int i = 0; i < H; i++)
        {
            for(int j = 0 ; j < W; j++)
            {
                for(int k = 0; k < numbers.Count; k++)
                {
                    if(numbers[k].transform.position == grid[i,j].transform.position)
                    {
                        sequnce.Add(numbers[k]);
                    }
                }
            }
        }

        //foreach(var i in sequnce)
            //print(i.name);

        bool same = true;
        for(int i = 0; i < sequnce.Count; i++)
        {
            if(sequnce[i] != numbers[i])
                same = false;
        }

        print(same);
    }
}
