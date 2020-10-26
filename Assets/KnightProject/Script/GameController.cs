using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Play, Pause }
public delegate void UpdateHeroParametersHandler(HeroParameters parameters);
public delegate void InventoryUsedCallback(InventoryUIButton item);

public class GameController : MonoBehaviour
{
    static private GameController _instance;
    private GameState state;
    private int score;
    [SerializeField]private List<InventoryItem> inventory;
    [SerializeField]private Knight knight;
    [SerializeField]private int  dragonHitScore;
    [SerializeField]private int dragonKillScore;
    [SerializeField]private Audio audioManager;
    [SerializeField]private HeroParameters hero;
    [SerializeField] private int dragonKillExperience;

    public event UpdateHeroParametersHandler OnUpdateHeroParameters;


    public static GameController Instance
    {
     get
     {
      if (_instance == null)
      {
       GameObject gameController =
       Instantiate(Resources.Load("Prefabs/GameController")) as GameObject;
       _instance = gameController.GetComponent<GameController>();
      }
      return _instance;
     }
    }

    public List<InventoryItem> Inventory
    {
     get
     {
          return inventory;
     }

     set
     {
      inventory = value;
     }


    }
    
    public Audio AudioManager
    {
        get
        {
          return audioManager;
        }

        set
        {
          audioManager = value;
        }
    }

    private void InitializeAudioManager()
    {
	 audioManager.SourceSFX = gameObject.AddComponent<AudioSource>();
     audioManager.SourceMusic = gameObject.AddComponent<AudioSource>();
     audioManager.SourceRandomPitchSFX = gameObject.AddComponent<AudioSource>();
     gameObject.AddComponent<AudioListener>();
    }


    private void Awake()
	{
     if (_instance == null)
     {
      _instance = this;
     }
     else
     {
      if (_instance != this)
      {
       Destroy(gameObject);
      }
     }

     DontDestroyOnLoad(gameObject);
 	 State = GameState.Play;
     inventory = new List<InventoryItem>();
     GetComponent<GameController>().InitializeAudioManager();
    }


    public void StartNewLevel()
	{
     HUD.Instance.SetScore(Score.ToString());
     	
     if (OnUpdateHeroParameters != null)
     {
      OnUpdateHeroParameters(hero);
     }
 
      State = GameState.Play;
	}

    public void LevelUp()
	{
     if (OnUpdateHeroParameters != null)
     {
        			OnUpdateHeroParameters(hero);
     }
	}



    public void GameOver() 
    {
    		HUD.Instance.ShowLevelLoseWindow();
	}


    public void PrincessFound()
	{
    		HUD.Instance.ShowLevelWonWindow();
	}


    public void LoadNextLevel()
	{
    		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1,
            LoadSceneMode.Single);
            
           
	}

    public void RestartLevel()
	{
    		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,
            LoadSceneMode.Single);
            GameController.Instance.State = GameState.Play;
            
	}

    public void LoadMainMenu()
	{
    SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    AudioManager.PlayMusic(true);
    
	}


    public void InventoryItemUsed(InventoryUIButton item)
    {
     switch (item.ItemData.CrystallType)
     {
      case CrystallType.speed:
      hero.Speed += item.ItemData.Quantity / 10f;
      break;             	
      case CrystallType.damage:
      hero.Damage += item.ItemData.Quantity / 10f;
      break;
      case CrystallType.heal:
      hero.MaxHealth += item.ItemData.Quantity / 10f;
      break;
      default:
      Debug.LogError("Wrong crystall type!");
      break;
     }
     Inventory.Remove(item.ItemData);
     Destroy(item.gameObject);
     GameController.Instance.AudioManager.PlaySound("DM-CGS-28");
     if (OnUpdateHeroParameters != null)
     {
            	OnUpdateHeroParameters(hero);
     }
    }

    public void AddNewInventoryItem(InventoryItem itemData)
    {
      InventoryUIButton newUiButton = HUD.Instance.AddNewInventoryItem(itemData);
      InventoryUsedCallback callback = new InventoryUsedCallback(InventoryItemUsed);
      newUiButton.Callback = callback;
      inventory.Add(itemData);
    }

    public GameState State
    {
      get
      {
       return state;
      }
      set
      {
       if (value == GameState.Play)
       {
        Time.timeScale = 1.0f;
       }

       else
       {
        Time.timeScale = 0.0f;
       }

       state = value;
      }
    }

    public HeroParameters Hero
    {
      get
      {
        return hero;
      }

      set
      {
       hero = value;
      }
    }

    public Knight Knight
    {
     get
     {
      return knight;
     }

     set
     {
      knight = value;
     }
    }
    
    
    public void Hit(IDestructable victim)
	{
     if (victim.GetType() == typeof(Dragon))
     {
        			//дракон получил урон
            		Score += dragonHitScore;
     }

     if (victim.GetType() == typeof(Knight))
     {
        		HUD.Instance.HealthBar.value = victim.Health;
     }
    }

    public void Killed(IDestructable victim)
	{
     if (victim.GetType() == typeof(Dragon))
     {
      Score += dragonKillScore;
      hero.Experience += dragonKillExperience;
      Destroy((victim as MonoBehaviour).gameObject);
      }

       if (victim.GetType() == typeof(Knight))
        {   		
         GameOver();
        }
	}

    private int Score
	{
      get
      {
        return score;
      }

      set
	  {
        if (value != score)
        {
          score = value;
          HUD.Instance.SetScore(score.ToString());
        }
      }
    }
}
