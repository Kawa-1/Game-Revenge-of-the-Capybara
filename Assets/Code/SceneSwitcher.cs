using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private void Start()
    {
       
    }

    public void startTimeRaceLevel()
    {
        DateBetwenScene.points = 0;
        DateBetwenScene.time = 0;
        LevelSetting.isPointRaceMode = true;
        LevelSetting.isBigHuntMode = false;
        cursorLocked();
        SceneManager.LoadScene("Game");
       
    }

    public void dontClick()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=0D-DFJLR5e4");
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void endGame()
    {   
        cursorUNLocked();
        SceneManager.LoadScene("EndGame");
    }
    public void youLoose()
    {   
        cursorUNLocked();
        SceneManager.LoadScene("GameOver");
    }
    void cursorLocked()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void cursorUNLocked()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void loadMenu()
    {
        cursorUNLocked();
        SceneManager.LoadScene("Menu");
    }

    public void startBigHuntRace()
    {
        DateBetwenScene.points = 0;
        DateBetwenScene.time = 0;
        LevelSetting.isBigHuntMode = true;
        LevelSetting.isPointRaceMode = false;

        LevelSetting.isGenerate = false;
        cursorLocked();
        SceneManager.LoadScene("Game");
    }
    
    
}
