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
            if (playerController.getPoints() > pointsToEndGame)
           {
               DateBetwenScene.points = playerController.getPoints();
               DateBetwenScene.time = this.time;
               LevelSetting.youLose = false;
               sceneSwitcher.endGame();
           } 
        } 
        if (LevelSetting.isBigHuntMode)
        {
            LevelSetting.youLose = false;

            if (LevelSetting.deerKill > LevelSetting.deerToKillAmount)
            {
                LevelSetting.youLose = true;
                sceneSwitcher.youLoose();
            }
            if (LevelSetting.roeKill > LevelSetting.roeToKillAmount)
            {
                LevelSetting.youLose = true;
                sceneSwitcher.youLoose();

            }
            if (LevelSetting.sheepKill > LevelSetting.sheepToKillAmount)
            {
                LevelSetting.youLose = true;
                sceneSwitcher.youLoose();

            }
            if (LevelSetting.boarKill > LevelSetting.boarToKillAmount)
            {
                LevelSetting.youLose = true;
                sceneSwitcher.youLoose();

            }
            
            if (LevelSetting.deerKill == LevelSetting.deerToKillAmount)
            {
                if (LevelSetting.roeKill == LevelSetting.roeToKillAmount)
                {
                    if (LevelSetting.sheepKill == LevelSetting.sheepToKillAmount)
                    {
                        if (LevelSetting.boarKill == LevelSetting.boarToKillAmount)
                        {
                            DateBetwenScene.points = playerController.getPoints();
                            DateBetwenScene.time = this.time;
                            LevelSetting.youLose = false;
                            sceneSwitcher.endGame();
                        }
                    }
                }
            }
        }
       
    }

    
}
