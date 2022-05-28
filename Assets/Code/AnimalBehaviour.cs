using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalBehaviour : MonoBehaviour
{

    [SerializeField] private PlayerController playerController;
    [SerializeField] private float headShootMultiplier = 2;
    [SerializeField] private Animator deadAnimator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject body;
    [SerializeField] private Material redDeadMaterial;

    [SerializeField] private NavMeshAgent navMeshAgent;

    public GameObject deadSheep;
    public GameObject deadDeer;
    public GameObject deadRoe;
    public GameObject deadBoar;
    
    private bool isDeer = false;
    private bool isRoe = false;
    private bool isBoar = false;
    private bool isSheep = false;

    private float pointsForHeadShoot = 4;
    private float pointsForBodyNextHeadShoot = 2;
    private float pointsForBodyNextBodyShoot = 1;
    private float negativePointsForLegShoot = 1;
    
    private float sheepLive = 80;
    private float deerLive = 150;
    private float boarLive = 120;
    private float roeLive = 100;


    private float allNegativePoints = 0;

    private float multiplier = 1;
    private float live;

    private bool isShotOnBody = false;

    private bool isLive = true;
    
    
    
    void Start()
    {
        SelectedAnimal();
    }

   
    void Update()
    {
        navMeshAgent.SetDestination(new Vector3(0,0,0));

    }

    private void SelectedAnimal()
    {
        
        if (gameObject.tag == "Deer")
        {
            isDeer = true;
            multiplier = 4;
            live = deerLive;
        }
        if (gameObject.tag == "Roe")
        {
            isRoe = true;
            multiplier = 3;
            live = roeLive;
        }
        if (gameObject.tag == "Boar")
        {
            isBoar = true;
            multiplier = 2;
            live = boarLive;
        }
        if (gameObject.tag == "Sheep")
        {
            isSheep = true;
            multiplier = 1;
            live = sheepLive;
        }
    }

    public void ShotOnAnimal(string shotPartsOfAnimal, float damage)
    {
        if (!isLive)
        {
            return;
        }
       
        if (shotPartsOfAnimal.Equals("Head"))
        {
            ShotOnHead(damage);
        }
        if (shotPartsOfAnimal.Equals("Body"))
        {
            ShotOnBody(damage);
        }
        if (shotPartsOfAnimal.Equals("Leg"))
        {
            ShotOnLeg(damage);
        }
        
    }

    private void ShotOnHead(float damage)
    {
        if (isShotOnBody)
        {
            live -= damage * headShootMultiplier;
            if (live <= 0)
            {
              killAnimalAndAddPoints(pointsForBodyNextHeadShoot);  
            }
        }
        else
        {
            live -= damage * headShootMultiplier;
            
            if (live <= 0)
            {
                killAnimalAndAddPoints(pointsForHeadShoot); 
            }
            
        }
        
    }

    private void ShotOnBody(float damage)
    {   
        isShotOnBody = true;
        live -= damage;
        if (live <= 0)
        {
            killAnimalAndAddPoints(pointsForBodyNextBodyShoot);
        }
       
        
        

    }

    private void ShotOnLeg(float damage)
    {
        if (allNegativePoints < 4)
        {
            allNegativePoints += negativePointsForLegShoot;
        }
    }

    private void killAnimalAndAddPoints(float pointsForShot)
    {
        float recivedPoints = (multiplier * pointsForShot) - allNegativePoints;
        if (recivedPoints < 0)
        {
            recivedPoints = 0;
        }
        playerController.addPoints(recivedPoints);

     
        isLive = false;
        addAmountKillAnimal();
       
        StartCoroutine(deadAnimationRotation());
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        //Destroy(gameObject);
        //creatDeadAnimal();
    }

    private IEnumerator  deadAnimationRotation()
    {
        float duration = 1;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(Vector3.forward * 90);
        bool changeColorOfBody = true;
        for(float t = 0f; t < duration; t += Time.deltaTime)
        {

            if (t > duration / 2 && changeColorOfBody)
            {
                body.GetComponent<Renderer>().material = redDeadMaterial;
                changeColorOfBody = false;
            }
            gameObject.transform.rotation = Quaternion.Lerp( startRotation, endRotation, t / duration ) ;
            yield return null;
        }
        
        gameObject.transform.rotation = endRotation;
       
    }

    private void addAmountKillAnimal()
    {
        if (isDeer)
        {
            LevelSetting.deerKill += 1;
        }
        if (isRoe)
        {
            LevelSetting.roeKill += 1;
        }
        if (isSheep)
        {
            LevelSetting.sheepKill += 1;
        }
        if (isBoar)
        {
            LevelSetting.boarKill += 1;
        }
    }

    public void setPlayerController(PlayerController player)
    {
        playerController = player;
    }

    private void creatDeadAnimal()
    {
        if (isSheep)
        {
            GameObject newSheep = Instantiate(deadSheep , transform.position, Quaternion.identity);
                    newSheep.transform.Rotate(new Vector3(0,0,90));
        }
        if (isDeer)
        {
            GameObject newDeer = Instantiate(deadDeer , transform.position, Quaternion.identity);
            newDeer.transform.Rotate(new Vector3(0,0,90));
        }
        if (isRoe)
        {
            GameObject newRoe = Instantiate(deadRoe , transform.position, Quaternion.identity);
            newRoe.transform.Rotate(new Vector3(0,0,90));
        }
        if (isBoar)
        {
            GameObject newBoar = Instantiate(deadBoar , transform.position, Quaternion.identity);
            newBoar.transform.Rotate(new Vector3(0,0,90));
        }
        
        

    }
}
