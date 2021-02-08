using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    static private HUD _instance;
	[SerializeField]private Text scoreLabel;
    [SerializeField]private Slider healthBar;
    [SerializeField]private GameObject inventoryWindow;
    [SerializeField]private GameObject levelWonWindow;
    [SerializeField]private GameObject levelLoseWindow;
    [SerializeField]private GameObject optionsWindow;
    [SerializeField]private InventoryUIButton inventoryItemPrefab;
    [SerializeField]private Transform inventoryContainer;
    [SerializeField]private Transform inGameMenu;
    public Text damageValue;
    public Text speedValue;
    public Text healthValue;

    public void Awake()
	{
       		_instance = this;
	}

    public void Start()
    {
      LoadInventory();
      GameController.Instance.OnUpdateHeroParameters +=
      HandleOnUpdateHeroParameters;
      GameController.Instance.StartNewLevel();
    }

    private void OnDestroy()
    {
     GameController.Instance.OnUpdateHeroParameters -=
     HandleOnUpdateHeroParameters;
    }

 
	public static HUD Instance
	{
     	get
    	{
         return _instance;
    	}
	}


     
    public Slider HealthBar
    {
      get
      {
        return healthBar;
      }

      set
      {
        healthBar = value;
      }
    }

    private void HandleOnUpdateHeroParameters(HeroParameters parameters)
    {
        HealthBar.maxValue = parameters.MaxHealth;

        HealthBar.value = parameters.MaxHealth;

        UpdateCharacterValues(parameters.MaxHealth, parameters.Speed, parameters.Damage);
    }

    public void SetSoundVolume(Slider slider)
	{
    		GameController.Instance.AudioManager.SfxVolume = slider.value;
	}
    public void SetMusicVolume(Slider slider)
	{
    		GameController.Instance.AudioManager.MusicVolume = slider.value;
}



    public void ButtonNext()
	{
    		GameController.Instance.LoadNextLevel();
            GameController.Instance.State = GameState.Play;
	}
    
    public void ButtonRestart()
	{
    		GameController.Instance.RestartLevel();
	}

    public void ButtonMainMenu()
	{
    		GameController.Instance.LoadMainMenu();
	}

    public void ShowLevelWonWindow()
	{
    	    ShowWindow(levelWonWindow);      	
    }

    public void ShowLevelLoseWindow() 
    {
            ShowWindow(levelLoseWindow);
	}




	public void SetScore(string scoreValue)
    {
      scoreLabel.text = scoreValue;
    }

    public void LoadInventory()
    {
     InventoryUsedCallback callback = new
     InventoryUsedCallback(GameController.Instance.InventoryItemUsed);

     for (int i = 0; i < GameController.Instance.Inventory.Count; i++)
     {
      InventoryUIButton newItem =
      AddNewInventoryItem(GameController.Instance.Inventory[i]);             	
      newItem.Callback = callback;
     }	
    }


    public InventoryUIButton AddNewInventoryItem(InventoryItem itemData)
    {
        InventoryUIButton newItem = Instantiate(inventoryItemPrefab) as InventoryUIButton;

        newItem.transform.SetParent(inventoryContainer);
        newItem.ItemData = itemData;
        return newItem;
    }

    public void UpdateCharacterValues(float newHealth, float newSpeed, float newDamage)
    {
     healthValue.text = newHealth.ToString();
     speedValue.text = newSpeed.ToString();
     damageValue.text = newDamage.ToString();
    }

    

    public void ShowWindow(GameObject window)
    {
     window.GetComponent<Animator>().SetBool("Open", true);

     GameController.Instance.State = GameState.Pause;
    }  

    public void HideWindow(GameObject window)
    {
     window.GetComponent<Animator>().SetBool("Open", false);
     GameController.Instance.State = GameState.Play;
    }

    
}
