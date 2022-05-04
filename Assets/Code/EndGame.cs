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
        if (LevelSetting.timeRace)
        {
            if (playerController.getPoints() > pointsToEndGame)
           {
               DateBetwenScene.points = playerController.getPoints();
               DateBetwenScene.time = this.time;
               
               sceneSwitcher.endGame();
           } 
        }
       
    }

    
}
