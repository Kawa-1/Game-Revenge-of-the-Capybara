using System;

using UnityEngine;
using Random = UnityEngine.Random;


public class LevelSetting : MonoBehaviour
{
    private float ammountAnimalToKill = 25;

    static public bool isPointRaceMode;
    static public bool isBigHuntMode;
    static public bool youLose;

    static public float deerToKillAmount ;
    static public float deerKill;
    static public float roeToKillAmount;
    static public float roeKill;
    static public float sheepToKillAmount;
    static public float sheepKill;
    static public float boarToKillAmount;
    static public float boarKill;

    static public bool isGenerate;
    private void Update()
    {   
        if (!isGenerate)
        {
            GenerateAnimalToKillInBigHuntMode();
            isGenerate = true;
        }
        
    }

    void GenerateAnimalToKillInBigHuntMode()
    {
        
        deerToKillAmount = 0;
        roeToKillAmount = 0;
        sheepToKillAmount = 0;
        boarToKillAmount = 0;
        deerKill = 0;
        roeKill = 0;
        sheepKill = 0;
        boarKill = 0;
        
        for (int i = 0; i <= ammountAnimalToKill; i++)
        {
            int selectedAnimal = Random.Range(1, 5);
            switch (selectedAnimal)
            {
                case 1 :
                    deerToKillAmount += 1;
                    break;
                case 2 :
                    roeToKillAmount += 1;
                    break;
                case 3 :
                    sheepToKillAmount += 1;
                    break;
                case 4 :
                    boarToKillAmount += 1;
                    break;
            }
        }
    }
}
