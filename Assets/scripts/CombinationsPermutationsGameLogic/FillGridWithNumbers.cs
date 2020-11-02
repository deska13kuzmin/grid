using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;
using UnityEngine.UI;
using System;


public struct Number
{
	public GameObject obj;
	public int number;
	public GameObject numberObj;
	public Number(GameObject objin, int numberin, GameObject numberobjin)
	{	
		number = numberin;
		obj = objin;
		numberObj =numberobjin;
	}
}

public class FillGridWithNumbers : MonoBehaviour
{
    public List<GameObject> numbers;
    public Transform startTransform;
    public GameObject obj;
    public GameObject canvasBox;
    public GameObject[,] grid;
    public Number[,] field;
    public int W = 10;
    public int H = 4;
    public int sizeOFGridElement = 100;
    public int leftRightDelta = 5;
    public int topButtonDelta = 1;
    public GameObject CObject;
    public GameObject AObject;
    public GameObject PObject;
    public GameObject currentDragObject;
    public float wait_time = 1f;
    public float delta_color = 0.2f;
    public Color c;
    private int startNumbersCount;
    private Vector2 startPosition;
    
    private bool isNeedToAnimate = false;
    private int ai;
    private int aj;

    void Start()
    {
    	startPosition = CObject.transform.position;
    	grid = new GameObject[H,W];
    	field = new Number[H,W];
    	int n = 0;
    	int n_id = 0;
    	setupNumbers();
    	for(int i = 0; i < H; i++)
    	{
	        for(int j = 0; j < W; j++)
	        {
	        	var clone = Instantiate(obj, startTransform.position + new Vector3((sizeOFGridElement + leftRightDelta) * j, (sizeOFGridElement + topButtonDelta) * i, 0), startTransform.rotation, canvasBox.transform);
                clone.transform.SetSiblingIndex(0);
                grid[i,j] = clone;
                numbers[n].transform.position = grid[i,j].transform.position;
    			n++;
    			n_id++;
    			if(n > numbers.Count-1)
    				n = 0;
    			if(n_id > startNumbersCount)
    				n_id = 0;
    			field[i,j] = new Number(grid[i,j], n_id, numbers[n]);
	        }
	    }
	    CObject.transform.SetSiblingIndex(1000);
	    AObject.transform.SetSiblingIndex(1000);
    }

    void Update()
    {
        if(isNeedToAnimate)
            StartCoroutine(RunAnimation());
    }

    IEnumerator RunAnimation()
    {
        isNeedToAnimate = false;
        
        var startC1 = field[ai+1,aj].numberObj.transform.GetChild(0).gameObject.GetComponent<Image>().color;
        var startC2 = field[ai-1,aj].numberObj.transform.GetChild(0).gameObject.GetComponent<Image>().color;
        var startC3 = field[ai,aj].numberObj.transform.GetChild(0).gameObject.GetComponent<Image>().color;
        field[ai+1,aj].numberObj.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(c.r, c.g, c.b, c.a);
        field[ai-1,aj].numberObj.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(c.r, c.g, c.b, c.a);
        field[ai,aj].numberObj.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(c.r, c.g, c.b, c.a + delta_color);;
        yield return new WaitForSeconds(0.5f);

        field[ai+1,aj].numberObj.transform.GetChild(0).gameObject.GetComponent<Image>().color = startC1;
        field[ai-1,aj].numberObj.transform.GetChild(0).gameObject.GetComponent<Image>().color = startC2;
        field[ai,aj].numberObj.transform.GetChild(0).gameObject.GetComponent<Image>().color = startC3;
    }

    void setupNumbers()
    {	
    	int delta = 0;
    	startNumbersCount = numbers.Count;
    	for(int i = numbers.Count; i < W*H; i++)
    	{
    		var clone = Instantiate(numbers[delta], startTransform.position, startTransform.rotation, canvasBox.transform);
    		numbers.Add(clone);
    		if(delta > startNumbersCount)
    			delta = 0;
    		else
    			delta++;
    	}
    }

    public GameObject GetDependedObject(GameObject obj)
    { 
		for(int i = 0; i < H; i++)
    	{
    		for(int j = 0; j < W; j++)
    		{
    			if(field[i,j].numberObj == obj || field[i,j].obj.transform.position == obj.transform.position)
    			{
    				return GetSumOfObjects(i,j, obj);
    			}	
    		}
    	}

    	return obj;
    }

    public GameObject GetSumOfObjects(int i, int j, GameObject obj)
    {	
    	if(i+1 < field.GetLength(0) && j < field.GetLength(1) && i-1 >= 0 && j < field.GetLength(1))
    	{
    		int number = 0;
    		if(currentDragObject.name == CObject.name)//obj.transform.position == CObject.transform.position)
            {
    			number = CombinationsValue(Int32.Parse(field[i+1, j].numberObj.GetComponent<Text>().text), Int32.Parse(field[i-1, j].numberObj.GetComponent<Text>().text));
            }
	    	else{
	    		number = KPermutationsValue(Int32.Parse(field[i+1, j].numberObj.GetComponent<Text>().text), Int32.Parse(field[i-1, j].numberObj.GetComponent<Text>().text));
            }
	    	
	    	field[i,j].numberObj.GetComponent<Text>().text = Convert.ToString(number);
		    field[i+1, j].numberObj.GetComponent<Text>().text = Convert.ToString(UnityEngine.Random.Range(1, 10));
			field[i-1, j].numberObj.GetComponent<Text>().text = Convert.ToString(UnityEngine.Random.Range(1, 10));

            ai = i;
            aj = j;
            isNeedToAnimate = true;
    	}

    	return field[i,j].numberObj;
    }

    
    public int KPermutationsValue(int a, int b)
    {
    	int n = a > b ? a : b;
    	int k = a < b ? a : b;

    	return factorial(n) / factorial(n-k);
    }

    public int CombinationsValue(int a, int b)
    {
    	int n = a > b ? a : b;
    	int k = a < b ? a : b;

    	return factorial(n) / (factorial(n-k) * factorial(k));
    }

    public int factorial(int n)
    {
    	int r = 1;
    	for(int i = 1; i <= n; i++)
    		r *= i;
    	return r;
    }
}
