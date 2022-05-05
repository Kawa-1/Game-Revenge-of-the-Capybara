using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private SceneSwitcher sceneSwitcher;
    [SerializeField] private DateBetwenScene dateBetwenScene;
    [SerializeField] private float pointsToEndGame;


    public float time = 0;
    void Start()
    {
    }

    
    void Update()
    {
        time += Time.deltaTime;
        if (LevelSetting.isPointRaceMode)
        {
            Debug.Log("race");

            if (playerController.getPoints() > pointsToEndGame)
           {
               DateBetwenScene.points = playerController.getPoints();
               DateBetwenScene.time = this.time;
               
               sceneSwitcher.endGame();
           } 
        } 
        if (LevelSetting.isBigHuntMode)
        {
            Debug.Log("hunt");

            if (LevelSetting.deerKill > LevelSetting.deerToKillAmount)
            {
                LevelSetting.youLose = true;
                Debug.Log("Loseeeeeeeeeeee1");
                sceneSwitcher.youLoose();
            }
            if (LevelSetting.roeKill > LevelSetting.roeToKillAmount)
            {
                LevelSetting.youLose = true;
                Debug.Log("Loseeeeeeeeeeee2");

                sceneSwitcher.youLoose();

            }
            if (LevelSetting.sheepKill > LevelSetting.sheepToKillAmount)
            {
                LevelSetting.youLose = true;
                Debug.Log("Loseeeeeeeeeeee3");

                sceneSwitcher.youLoose();

            }
            if (LevelSetting.boarKill > LevelSetting.boarToKillAmount)
            {
                LevelSetting.youLose = true;
                Debug.Log("Loseeeeeeeeeeee4");

                sceneSwitcher.youLoose();

            }
            
            if (LevelSetting.deerKill == LevelSetting.deerToKillAmount)
            { Debug.Log("hunt1");

                if (LevelSetting.roeKill == LevelSetting.roeToKillAmount)
                { Debug.Log("hunt2");

                    if (LevelSetting.sheepKill == LevelSetting.sheepToKillAmount)
                    { Debug.Log("hunt3");

                        if (LevelSetting.boarKill == LevelSetting.boarToKillAmount)
                        { Debug.Log("hunt4");

                            DateBetwenScene.points = playerController.getPoints();
                            DateBetwenScene.time = this.time;
                            sceneSwitcher.endGame();
                        }
                    }
                }
            }
        }
       
    }

    
}
