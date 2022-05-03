using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneInterface : MonoBehaviour
{
  
    [SerializeField] private TextMeshProUGUI text;
    

    
    void Update()
    {
        text.SetText("Your points: " + DateBetwenScene.points + "\n Your time: " + DateBetwenScene.time);
    }
}
