using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIButton : MonoBehaviour
{
   private InventoryItem itemData;
   private InventoryUsedCallback callback;
   [SerializeField]private Image image;
   [SerializeField]private Text label;
   [SerializeField]private Text count;
   [SerializeField]private List<Sprite> sprites;

   public InventoryUsedCallback Callback
   {
     get
     {
      return callback;
     }

     set
     {
      callback = value;
     }
   }

    public InventoryItem ItemData
   {
     get
     {
      return itemData;
     }

     set
     {
      itemData = value;
     }
   }


   void Start()
   {
   label.text = itemData.CrystallType.ToString(); 
   count.text = itemData.Quantity.ToString();
   string spriteNameToSearch = itemData.CrystallType.ToString().ToLower();
   image.sprite = sprites.Find ( x => x.name.Contains(spriteNameToSearch) );
   label.text = spriteNameToSearch;
   count.text = itemData.Quantity.ToString();
   gameObject.GetComponent<Button>().onClick.AddListener( () => callback(this) );
   }
}
