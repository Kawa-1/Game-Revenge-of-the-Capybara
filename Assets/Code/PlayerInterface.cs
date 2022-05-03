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
    
    void Start()
    {
        
    }

    
    void Update()
    {
        setPointOnPlayerScreen();
        showAmountOfAmmo();
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
