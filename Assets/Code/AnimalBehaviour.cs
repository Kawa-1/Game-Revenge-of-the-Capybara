using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class AnimalBehaviour : MonoBehaviour
{

    [SerializeField] private PlayerController playerController;
    [SerializeField] private float headShootMultiplier = 2;
    [SerializeField] private Animator deadAnimator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject body;
    [SerializeField] private Material redDeadMaterial;

    [SerializeField] private NavMeshAgent navMeshAgent;
    public Transform playerTransform;

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
    
    
    private Vector3 spawnPoint;
    private bool walkNowToPointSet = false;
    private float patrolRadius = 50f;
    private Vector3 destinationToPatrolVector3;
    private float yRayCastPosition = 200f;
    private Vector3 destinationPoint;
    private float maxRange = 100f;
    private bool escapeBeforeThePlayer = true;
    public Terrain terrain;
    private float xTerrein;
    private float zTerrein;
    
    void Start()
    { 
        zTerrein = terrain.terrainData.size.z;
        xTerrein = terrain.terrainData.size.x;
        spawnPoint = gameObject.transform.position;
        SelectedAnimal();
        //Debug.Log("pozycja " + playerTransform.position + "kurwa ");
       
        
    }

   
    void Update()
    {
        //Debug.Log("kurwica  = " +  spawnPoint);
        //navMeshAgent.SetDestination(new Vector3(0,0,0));
        if (isLive)
        {
            patrol();
            runningAwayFromThePalyer(); 
        }
        
     //   Debug.Log("kurwa");
       // Debug.Log(playerTransform.position);


    }

    private void patrol()
    {   
        if (walkNowToPointSet)
        {
            navMeshAgent.SetDestination(destinationPoint);
            float distanceToDestination = Vector3.Distance(gameObject.transform.position, destinationPoint);
            if (distanceToDestination < 2f)
            {
                walkNowToPointSet = false;
            }
        }
        else
        {
            float x;
            float z;
            RaycastHit hit;
            while (true)
            {
                x = Random.Range(spawnPoint.x - patrolRadius,spawnPoint.x + patrolRadius);
                z = Random.Range(spawnPoint.z - patrolRadius,spawnPoint.z + patrolRadius);
                Physics.Raycast(new Vector3(x, yRayCastPosition, z), Vector3.down, out hit);
                if (hit.transform == null || hit.transform.gameObject.layer == 17 || hit.transform.gameObject.layer == 6)
                    {

                        continue;
                    }
                    destinationPoint = new Vector3(x, hit.point.y, z);
                    //Debug.Log("dest = " + destinationPoint + transform.gameObject.tag);
                    break;
               
            }
            walkNowToPointSet = true;
        }
    }

    private void runningAwayFromThePalyer()
    {
        float distanceAnimalToPlayer = Vector3.Distance(gameObject.transform.position, playerTransform.position);
        if (distanceAnimalToPlayer > maxRange )
        {
            escapeBeforeThePlayer = true;
        }
        if (escapeBeforeThePlayer)
        {



            if (distanceAnimalToPlayer < maxRange)
            {
                Debug.Log("1111111111111111111111111111111111111");

                RaycastHit hit;

                Debug.DrawLine(transform.position, playerTransform.position, Color.red, Mathf.Infinity);
                //Debug.Log(playerController.transform.position - transform.position);

                if (Physics.Raycast(transform.position, (playerTransform.position - transform.position).normalized,
                        out hit))
                {
                    Debug.Log("222222222222222222222222222222");
                   // Debug.Log(gameObject.tag);
                    if (hit.transform.tag.Equals("Player"))
                    { 
                        /*float x = Random.Range(playerTransform.position.x - maxRange, playerTransform.position.x + maxRange);
                        float z;
                        float zSign = Random.Range(0,2);
                        if (zSign == 0)
                        {
                            z = playerTransform.position.z + maxRange;
                        }

                        if (zSign == 1)
                        {
                            z = playerTransform.position.z - maxRange;

                        }*/
                        

                        float x;
                        float z;

                        x = transform.position.x + transform.position.x - playerTransform.position.x;
                        z = transform.position.z + transform.position.z - playerTransform.position.z;
                        if (x <= 0)
                        {
                            x = 1;
                        }
                        if (z <= 0)
                        {
                            z = 1;
                        }
                        if (x >= xTerrein)
                        {
                            x = xTerrein - 1;
                        }
                        if (z >= zTerrein)
                        {
                            z = zTerrein - 1;
                        }
                        
                        Vector3 tmpSpawnPoint = new Vector3(x,0, z);
                        Debug.Log("first spawn" + tmpSpawnPoint);
                        while (true)
                        {
                            Physics.Raycast(new Vector3(tmpSpawnPoint.x, yRayCastPosition, tmpSpawnPoint.z), Vector3.down, out  RaycastHit hit1);
                            if (hit1.transform.gameObject.layer == 17 || hit1.transform.gameObject.layer == 6)
                            {
                                Debug.Log("x = " + z + " z + " + z);
                                float x1 = Random.Range(x - 5, x+5);
                                float z1 = Random.Range(z-5, z+5);
                                tmpSpawnPoint = new Vector3(x1, 0, z1);
                                
                                continue;
                            }
                            else
                            {
                                spawnPoint = new Vector3(tmpSpawnPoint.x, hit1.point.y, tmpSpawnPoint.z);
                                break;
                            }   
                    
                        }
                        Debug.Log("secend spawn" + spawnPoint);
                        
                        walkNowToPointSet = false;
                        escapeBeforeThePlayer = false;
                    }
                }
                //Debug.Log(hit.transform.position);
                //Debug.Log(hit.transform.gameObject.tag);
            }
        }

    }
    private Vector3 RandomPointOnCircleEdge(float radius)
    {
        var vector2 = Random.insideUnitCircle.normalized * radius;
        return new Vector3(vector2.x, 0, vector2.y);
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
        rb.isKinematic = true;
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
    
    public void setTerrain(Terrain terrain)
    {
        this.terrain = terrain;
    }
    public void setPlayerTransform(Transform transform)
    {
        this.playerTransform = transform;
    }
}
