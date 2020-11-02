using System.Linq;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlSequance : MonoBehaviour
{
    public List<GameObject> spots;
    public List<GameObject> badges;
    public GameObject victoryMessage;
    public GameObject grid_basement;
    private bool check_ = true;
    private bool victory_message_clicked = true;
    private bool wasVictory = false;
    private bool moneyWasDelivered = false;
    private GameObject clonedVictoryMessage;

    void Update()
    {
        if(check_)
            Check();
        
        if(!check_ && Input.GetMouseButtonDown(0)){
        	victory_message_clicked = true;
        	Destroy(clonedVictoryMessage);
        	StartCoroutine(Waiter());
        }
        if(wasVictory && !moneyWasDelivered){
        	GameObject canvas_parent = GameObject.Find("Canvas");
        	shareMoneyAmount src = canvas_parent.GetComponent<shareMoneyAmount>();
        	for(int i = 0; i < 100; i++)
        		src.number += 1;
        	moneyWasDelivered = true;
        }

    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);

        check_ = true;
    }

    void Check()
    {
    	bool same = true;
        
    	for(int i = 0; i < spots.Count; i++)
    	{
    		if(spots[i].transform.position != badges[i].transform.position)
    			same = false;
    	}
    	
    	if(same)
    	{ 
    		clonedVictoryMessage = Instantiate(victoryMessage, grid_basement.transform);
    		wasVictory = true;
    		check_ = false;
		}

    }
}
