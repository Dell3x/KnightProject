using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    
	private void OnTriggerEnter2D (Collider2D collider)
    { 
	  Knight knight = collider.gameObject.GetComponent<Knight>();
	 
	 if (knight != null)
	 {
         	knight.OnStair = true;
     } 

    }
 
    private void OnTriggerExit2D(Collider2D collider)
    {
	  Knight knight = collider.gameObject.GetComponent<Knight>();
	 
	 if (knight != null)
	 {
         	knight.OnStair = false;
     } 
    }


}
