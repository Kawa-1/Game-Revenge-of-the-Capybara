using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneInterface : MonoBehaviour
{
  
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TMP_InputField playerNameInputField;

    [SerializeField] private Transform entryContainer;
    [SerializeField] private Transform entryTamplate;

    [SerializeField] private SceneSwitcher sceneSwitcher;

    [SerializeField] private Button newBigHuntGame;
    [SerializeField] private Button newRaceTimeGame;
 
    
    List<string> bestPlayers = new List<string>();
    List<float> bestTimeList = new List<float>();

    private bool dd = false;
    private string sole;
    void Start()
    {

        if (LevelSetting.youLose)
        {
            return;
        }
        
        if (LevelSetting.isBigHuntMode)
        {
            sole = "hunt";
        }

        if (LevelSetting.isPointRaceMode)
        {
            sole = "race";
        }

        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetString("playersTable" + sole) == "")
        {
            PlayerPrefs.SetString("playersTable"+ sole,"name1" + sole+ ":name2" + sole+ ":name3" + sole+ ":name4" + sole + ":name5" + sole+ ":name6" + sole+ ":name7" + sole+ ":name8" + sole+ ":name9" + sole+ ":name10" + sole+ ":");
            PlayerPrefs.SetFloat("name1" + sole,110000);
            PlayerPrefs.SetFloat("name2"+ sole,110000);
            PlayerPrefs.SetFloat("name3"+ sole,110000);
            PlayerPrefs.SetFloat("name4"+ sole,110000);
            PlayerPrefs.SetFloat("name5"+ sole,110000);
            PlayerPrefs.SetFloat("name6"+ sole,110000);
            PlayerPrefs.SetFloat("name7"+ sole,110000);
            PlayerPrefs.SetFloat("name8"+ sole,110000);
            PlayerPrefs.SetFloat("name9"+ sole,110000);
            PlayerPrefs.SetFloat("name10"+ sole,110000);
        }
        
        
        /*
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("playersTable"+ "race","name1" + "race"+ ":name2" + "race"+ ":name3" + "race"+ ":name4" + "race"+ ":name5" + "race"+ ":name6" + "race"+ ":name7" + "race"+ ":name8" + "race"+ ":name9" + "race"+ ":name10" + "race"+ ":");
        PlayerPrefs.SetFloat("name1" + "race",110000);
        PlayerPrefs.SetFloat("name2"+ "race",110000);
        PlayerPrefs.SetFloat("name3"+ "race",110000);
        PlayerPrefs.SetFloat("name4"+ "race",110000);
        PlayerPrefs.SetFloat("name5"+ "race",110000);
        PlayerPrefs.SetFloat("name6"+ "race",110000);
        PlayerPrefs.SetFloat("name7"+ "race",110000);
        PlayerPrefs.SetFloat("name8"+ "race",110000);
        PlayerPrefs.SetFloat("name9"+ "race",110000);
        PlayerPrefs.SetFloat("name10"+ "race",110000);
        PlayerPrefs.SetString("playersTable"+ "hunt","name11" +"hunt"+ ":name22" +"hunt"+ ":name33" +"hunt"+ ":name44" +"hunt"+ ":name55" +"hunt"+ ":name66" +"hunt"+ ":name77" +"hunt"+ ":name88" +"hunt"+ ":name99" +"hunt"+ ":name100" +"hunt"+ ":");
        PlayerPrefs.SetFloat("name11"+ "hunt",10000);
        PlayerPrefs.SetFloat("name22"+ "hunt",10000);
        PlayerPrefs.SetFloat("name33"+ "hunt",10000);
        PlayerPrefs.SetFloat("name44"+ "hunt",10000);
        PlayerPrefs.SetFloat("name55"+ "hunt",10000);
        PlayerPrefs.SetFloat("name66"+ "hunt",10000);
        PlayerPrefs.SetFloat("name77"+ "hunt",10000);
        PlayerPrefs.SetFloat("name88"+ "hunt",10000);
        PlayerPrefs.SetFloat("name99"+ "hunt",10000);
        PlayerPrefs.SetFloat("name100"+ "hunt",10000);
        //*/
        newBigHuntGame.gameObject.SetActive(false);
        newRaceTimeGame.gameObject.SetActive(false);
        if (LevelSetting.isBigHuntMode)
        {
            newBigHuntGame.gameObject.SetActive(true);
        }
        if (LevelSetting.isPointRaceMode)
        {
            newRaceTimeGame.gameObject.SetActive(true);
        }

        playerNameInputField.characterLimit = 8;
        
        ReadSavedResult();
        DisplayInformation();
        DisplayHighScoreTable();
    }

    private void DisplayHighScoreTable()
    {
        float templateHight = 35f;
        for (int i = 0; i < 10; i++)
        {
            Transform entryTransform = Instantiate(entryTamplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHight * i);
            entryTransform.gameObject.SetActive(true);

            entryTransform.Find("Position-Text").GetComponent<TextMeshProUGUI>().SetText((i + 1).ToString());
            entryTransform.Find("Name-Text").GetComponent<TextMeshProUGUI>().SetText(bestPlayers[i]);
            entryTransform.Find("Time-Text").GetComponent<TextMeshProUGUI>().SetText(bestTimeList[i].ToString());
        }
    }

    private void DisplayInformation()
    {
        float bestTimeForEver = PlayerPrefs.GetFloat("bestTime");

        if (PlayerPrefs.GetFloat("bestTime") == null)
        {
            bestTimeForEver = 10000;
        }


        text.SetText("Your points: " + DateBetwenScene.points + "\n Your time: " + DateBetwenScene.time);
        if (DateBetwenScene.time < bestTimeForEver)
        {
            PlayerPrefs.SetFloat("bestTime", DateBetwenScene.time);
            
        }
        else
        {
            
        }
    }

    private void ReadSavedResult()
    {
        string playerTable = PlayerPrefs.GetString("playersTable"+ sole);
        Debug.Log("=========");
        Debug.Log(playerTable);
        playerTable= playerTable.Replace(sole, "");
        Debug.Log("**********");
        Debug.Log(playerTable);
        string tmp = null;
        foreach (var oneChar in playerTable)
        {
            if (!oneChar.Equals(':'))
            {
                tmp = tmp + oneChar;
            }
            else
            {
                bestPlayers.Add(tmp);
                tmp = null;
            }
        }

        foreach (var player in bestPlayers)
        {
            bestTimeList.Add(PlayerPrefs.GetFloat(player + sole));
        }
    }

    public void SavePlayerTime()
    {
        
        float playerTime = DateBetwenScene.time;
        if (playerTime == 0)
        {
            return;
        }

        if (playerNameInputField.text == "")
        {
            return;
        }
        int bestTimeIndex = -1;
        int index = 0;
        foreach (var time in bestTimeList)
        {
            if (time == playerTime)
            {
                
                index += 1;
                continue;
            }
            if (time > playerTime)
            {
                bestTimeIndex = index;
                break;
            }
            index += 1;
        }

        if (bestTimeIndex == -1)
        {
            return;
        }

        foreach (var letter in playerNameInputField.text)
        {
            if (letter == ':') 
            {
                return;
            }
        }
        
        bestPlayers.Insert(bestTimeIndex,playerNameInputField.text);
        bestTimeList.Insert(bestTimeIndex,playerTime);
        
        bestPlayers.RemoveAt(bestPlayers.Count - 1);
        bestTimeList.RemoveAt(bestTimeList.Count - 1);
        
        string stringToSave = null;
        foreach (var player in bestPlayers)
        {
            stringToSave += player+ sole + ":";
        }
        PlayerPrefs.SetString("playersTable"+ sole,stringToSave);
        PlayerPrefs.SetFloat(playerNameInputField.text + sole,playerTime);

        DateBetwenScene.points = 0;
        DateBetwenScene.time = 0;

        playerNameInputField.text = "";
        ReadSavedResult();
        DisplayHighScoreTable();
        sceneSwitcher.endGame();
        

    }
}
