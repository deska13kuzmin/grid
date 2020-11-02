using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.UI;
public class shareMoneyAmount : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject canvas;
    public int number = 0;
    void Start()
    {
        //canvas.Find("money_counter").GetChild(1).GetComponent<Text>().text;
    }

    // Update is called once per frame
    void Update()
    {
        canvas = GameObject.Find("money_counter");
        canvas.transform.GetChild(1).GetComponent<Text>().text = Convert.ToString(number);   
    }
}
