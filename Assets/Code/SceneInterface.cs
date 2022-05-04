using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneInterface : MonoBehaviour
{
  
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI bestTime;
    

    
    void Update()
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
        
        bestTime.SetText("BestTime: " + bestTimeForEver);
    }
}
