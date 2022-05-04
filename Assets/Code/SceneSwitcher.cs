using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
   
    public void startTimeRaceLevel()
    {
        LevelSetting.timeRace = true;
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
}
