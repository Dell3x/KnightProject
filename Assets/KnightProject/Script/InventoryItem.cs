﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
   [SerializeField]private CrystallType crystallType;
   [SerializeField]private float quantity;

   public CrystallType CrystallType
   {
     get
     {
      return  crystallType;
     }

     set
     {
      crystallType = value;
     }
   }

   public float Quantity
   {
     get
     {
      return quantity;
     }

     set
     {
      quantity = value;
     }
   }

}
