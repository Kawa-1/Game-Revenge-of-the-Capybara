using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AnimalBehaviour : MonoBehaviour
{

    [SerializeField] private PlayerController playerController;
    [SerializeField] private float headShootMultiplier = 2;
    
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
    
    //AI
    public float lookRadius = 60f;
    public float walkRadius;
    Transform target; 
    NavMeshAgent agent;
    
    //patrolling
    Vector3 pointOfPatrol;
    public Vector3 walkPoint;
    bool walkPointSet;

    public float walkPointRange;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pointOfPatrol = gameObject.transform.position;
        walkRadius = 100f;
        SelectedAnimal();
    }
    
    void Update()
    {
        // float distance = Vector3.Distance(target.position, transform.position);
        if (playerController.transform.position.x <= lookRadius && !gameObject.CompareTag("Dead"))
        {
            // Need to introduce some kind of Coroutines and involved with it things to keep Runaway and other functions running without interruptions 
            float timeLeft = 6f;
            Runaway();
        }
        else
        {
            Patrol();
        }
    }

    private void Runaway()
    {
        float xPlayer = playerController.transform.position.x;
        float yPlayerRotation = playerController.transform.rotation.y;
        
        float directionRange = Random.Range(-60f, 60f);
        float directionOfRunaway = yPlayerRotation + directionRange;

        transform.position += transform.forward * Time.deltaTime * agent.speed;
        


        // Vector3 randomPointInCircle = Random.insideUnitSphere * walkRadius;
        // randomPointInCircle += transform.position;
        // NavMeshHit hit;
        // if (NavMesh.SamplePosition(randomPointInCircle, out hit, walkRadius, 0))
        // {
        //     Vector3 finalPosition = hit.position;
        //     walkPoint = finalPosition;
        //     walkPointSet = true;
        // }

    }
    
    private void Patrol()
    {
        if (!walkPointSet)
            WalkPointInCircle();
        
        if (walkPointSet)
        {
            float distanceToMid = Vector3.Distance(pointOfPatrol, walkPoint);

            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToDest = transform.position - walkPoint;
        float magnitude = distanceToDest.magnitude;
        if (magnitude < 2.5f)
        {
            walkPointSet = false;
        }
    }
    
    private void WalkPointInCircle()
    {
        Vector3 randomPointInCircle = Random.insideUnitSphere * walkRadius;
        randomPointInCircle += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPointInCircle, out hit, walkRadius, 0))
        {
            Vector3 finalPosition = hit.position;
            walkPoint = finalPosition;
            walkPointSet = true;
        }
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
        creatDeadAnimal();
        
    
        addAmountKillAnimal();
        Destroy(gameObject);
        
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
