using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInterface : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private RawImage aimingGun;
    [SerializeField] private TextMeshProUGUI amountOfAmmo;
    [SerializeField] private EndGame endGame;

    [SerializeField] private RawImage deerToKillImage;
    [SerializeField] private RawImage roeToKillImage;
    [SerializeField] private RawImage sheepToKillImage;
    [SerializeField] private RawImage boarToKillImage;

    [SerializeField] private TextMeshProUGUI amountDeerToKill;
    [SerializeField] private TextMeshProUGUI amountRoeToKill;
    [SerializeField] private TextMeshProUGUI amountSheepToKill;
    [SerializeField] private TextMeshProUGUI amountBoarToKill;
    
    [SerializeField] private TextMeshProUGUI deerKillAmount;
    [SerializeField] private TextMeshProUGUI roeKillAmount;
    [SerializeField] private TextMeshProUGUI sheepKillAmount;
    [SerializeField] private TextMeshProUGUI boarKillAmount;
    void Start()
    {
        SetStartInfoToBigHuntMode();
        
    }

    
    void Update()
    {
        setPointOnPlayerScreen();
        showAmountOfAmmo();
        
        deerKillAmount.SetText("x" + LevelSetting.deerKill.ToString());
        roeKillAmount.SetText("x" + LevelSetting.roeKill.ToString());
        sheepKillAmount.SetText("x" + LevelSetting.sheepKill.ToString());
        boarKillAmount.SetText("x" + LevelSetting.boarKill.ToString());
    }

    private void SetStartInfoToBigHuntMode()
    {
        if (LevelSetting.isBigHuntMode)
        {
            deerToKillImage.gameObject.SetActive(true);
            roeToKillImage.gameObject.SetActive(true);
            sheepToKillImage.gameObject.SetActive(true);
            boarToKillImage.gameObject.SetActive(true);
            amountDeerToKill.gameObject.SetActive(true);
            amountRoeToKill.gameObject.SetActive(true);
            amountSheepToKill.gameObject.SetActive(true);
            amountBoarToKill.gameObject.SetActive(true);
            deerKillAmount.gameObject.SetActive(true);
            roeKillAmount.gameObject.SetActive(true);
            sheepKillAmount.gameObject.SetActive(true);
            boarKillAmount.gameObject.SetActive(true);
         
            amountDeerToKill.SetText("x" + LevelSetting.deerToKillAmount.ToString());
            amountRoeToKill.SetText("x" + LevelSetting.roeToKillAmount.ToString());
            amountSheepToKill.SetText("x" + LevelSetting.sheepToKillAmount.ToString());
            amountBoarToKill.SetText("x" + LevelSetting.boarToKillAmount.ToString());
            
            
            
        }
    }
    void setPointOnPlayerScreen()
    {
        pointsText.SetText("Points: " + playerController.getPoints() + "\n Time: " + endGame.time);
    }

    public void showAimingGun()
    {
        aimingGun.gameObject.SetActive(true);
    }

    public void hideAimingGun()
    {
        aimingGun.gameObject.SetActive(false);
    }

    private void showAmountOfAmmo()
    {
        amountOfAmmo.SetText("Ammo: " + playerController.getAmountOfAmmo());
    }
}
