using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]private GameObject optionsWindow;
    private void Start()
	{
    		Time.timeScale = 1f;
	}

    public void PlayButton()
    {
     SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void OptionButton()
    {
     
    }

    public void ExitButton()
    {
     Application.Quit();
    }

}
