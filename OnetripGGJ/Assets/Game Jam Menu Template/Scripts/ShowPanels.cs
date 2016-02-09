using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowPanels : MonoBehaviour {

    public StartOptions startOptions;
    public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject optionsTint;							//Store a reference to the Game Object OptionsTint 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject pausePanel;							//Store a reference to the Game Object PausePanel 
    public GameObject gameOverPanel;
    public Text scoreText;

	//Call this function to activate and display the Options panel during the main menu
	public void ShowOptionsPanel()
	{
		optionsPanel.SetActive(true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideOptionsPanel()
	{
		optionsPanel.SetActive(false);
		optionsTint.SetActive(false);
	}

	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
		menuPanel.SetActive (true);
	}

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		menuPanel.SetActive (false);
	}
	
	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
		optionsTint.SetActive(false);
	}

    public void CloseEndScreen()
    {
        optionsTint.SetActive(false);
        gameOverPanel.SetActive(false);

        Time.timeScale = 1;
    }

    public void EndScreen(bool active, int score)
    {
        optionsTint.SetActive(active);
        gameOverPanel.SetActive(active);
        scoreText.text = "You scored: " + score.ToString() + " points!";

        if (active)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    void Update()
    {
        if (gameOverPanel.activeSelf)
        {
            if (Input.GetButtonDown("Drop"))
            {
                // Restart
                CloseEndScreen();
                startOptions.StartButtonClicked(2);
            }
            else if (Input.GetButtonDown("Back"))
            {
                // Menu
                CloseEndScreen();
                startOptions.StartButtonClicked(1);
            }
        }
    }
}
