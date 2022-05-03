using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private AnimalBehaviour enemySheep;
    [SerializeField] private AnimalBehaviour enemyDeer;
    [SerializeField] private AnimalBehaviour enemyRoe;
    [SerializeField] private AnimalBehaviour enemyBoar;
    [SerializeField] private float timeIntervalSeconds;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject deadSheep;
    [SerializeField] private GameObject deadDeer;
    [SerializeField] private GameObject deadBoar;
    [SerializeField] private GameObject deadRoe;
    [SerializeField] private GameObject terrain;
    [SerializeField] private float landDivisionCoefficient;
    [SerializeField] private float maxAmmountOfAnimal;

    private float xLenghtTerrain;
    private float zLenghtTerrain;

    private float xFragmentLength;
    private float zFragmentLength;

    private float yRayCastPosition = 200;

    private float ammounOfAnimal = 0;
    
    
        
    void Start()
    {
        xLenghtTerrain = terrain.transform.localScale.x;
        zLenghtTerrain = terrain.transform.localScale.z;

        xFragmentLength = xLenghtTerrain / landDivisionCoefficient;
        zFragmentLength = zLenghtTerrain / landDivisionCoefficient;
        
        
        
        

        GenerateAnimalOnHoleTerrain();
        bool checkGenerateAnimal = true;
        while (checkGenerateAnimal)
        { 
            if (maxAmmountOfAnimal - ammounOfAnimal >= (Math.Pow(landDivisionCoefficient,2))) 
            {
                GenerateAnimalOnHoleTerrain();
            }
            else
            {
                checkGenerateAnimal = false;
            }
        }
        Debug.Log(ammounOfAnimal);
        
    }
    
    void Update()
    {
        generateMissingAnimal();
    }

    private void GenerateAnimalOnHoleTerrain()
    {
        float ix = xFragmentLength;
        while (ix <= xLenghtTerrain)
        {
            float iz = zFragmentLength;
            float repetitionCounterPlayer = 0;
            float repetitionCounterPlants = 0;
            while (iz <= zLenghtTerrain)
            {
                float x = Random.Range(ix - xFragmentLength, ix);
                float z = Random.Range(iz - zFragmentLength, iz);
                
                if (Math.Abs(x - playerController.transform.position.x) < 100 && Math.Abs(z - playerController.transform.position.z) < 100 )
                {
                    repetitionCounterPlayer += 1;
                    if (repetitionCounterPlayer > 50)
                    {
                        if (iz + zFragmentLength <= zLenghtTerrain)
                        {
                            iz += zFragmentLength;
                            continue;
                           
                        }
                        else
                        { 
                            if (ix + xFragmentLength <= xFragmentLength)
                            {
                                ix += xFragmentLength;
                                continue;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    continue;
                }
                
                
                Physics.Raycast(new Vector3(x, yRayCastPosition, z), Vector3.down, out RaycastHit hit);
                if (hit.transform.gameObject.layer.Equals("plants"))
                {
                    repetitionCounterPlants += 1;
                    if (repetitionCounterPlants > 50)
                    {
                        if (iz + zFragmentLength <= zLenghtTerrain)
                        {
                            iz += zFragmentLength;
                            continue;
                           
                        }
                        else
                        { 
                            if (ix + xFragmentLength <= xFragmentLength)
                            {
                                ix += xFragmentLength;
                                continue;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    continue;
                }
                else
                {
                    float y = yRayCastPosition - hit.distance + 2;
                    Vector3 newAnimalPosition = new Vector3(x, y, z);
                    int selectedAnimal = Random.Range(1, 5);
                    ammounOfAnimal += 1;
                    switch (selectedAnimal)
                    {
                        case 1:
                            GenerateSheep(newAnimalPosition);
                            break;
                        case 2:
                            GenerateBoar(newAnimalPosition);
                            break;
                        case 3:
                            GenerateRoe(newAnimalPosition);
                            break;
                        case 4:
                            GenerateDeer(newAnimalPosition);
                            break;
                        default:
                            break;
                    }
                }

                iz += zFragmentLength;
            }

            ix += xFragmentLength;
        }
    }

    public void generateMissingAnimal()
    {
        while (ammounOfAnimal < maxAmmountOfAnimal)
        {


            float x = Random.Range(0, xLenghtTerrain);
            float z = Random.Range(0, xLenghtTerrain);

            if (Math.Abs(x - playerController.transform.position.x) < 100 &&
                Math.Abs(z - playerController.transform.position.z) < 100)
            {
                continue;
            }
            
            Physics.Raycast(new Vector3(x, yRayCastPosition, z), Vector3.down, out RaycastHit hit);
           
            if (hit.transform.gameObject.layer.Equals("plants"))
            {
                continue;
            }
            else
            {
                float y = yRayCastPosition - hit.distance + 2;
                Vector3 newAnimalPosition = new Vector3(x, y, z);
                int selectedAnimal = Random.Range(1, 5);
                ammounOfAnimal += 1;
                switch (selectedAnimal)
                {
                    case 1:
                        GenerateSheep(newAnimalPosition);
                        break;
                    case 2:
                        GenerateBoar(newAnimalPosition);
                        break;
                    case 3:
                        GenerateRoe(newAnimalPosition);
                        break;
                    case 4:
                        GenerateDeer(newAnimalPosition);
                        break;
                    default:
                        break;
                }
            }
        }

    }


   

    void GenerateSheep(Vector3 newPosition)
    {
        AnimalBehaviour newSheep = Instantiate(enemySheep, newPosition, Quaternion.Euler(0,Random.Range(0,360),0));
        newSheep.setPlayerController(playerController);
        newSheep.deadSheep = deadSheep;
    }
    void GenerateDeer(Vector3 newPosition)
    {
        AnimalBehaviour newDeer = Instantiate(enemyDeer, newPosition, Quaternion.Euler(0,Random.Range(0,360),0));
        newDeer.setPlayerController(playerController);
        newDeer.deadDeer = deadDeer;
    }
    void GenerateRoe(Vector3 newPosition)
    {
        AnimalBehaviour newRoe = Instantiate(enemyRoe, newPosition, Quaternion.Euler(0,Random.Range(0,360),0));
        newRoe.setPlayerController(playerController);
        newRoe.deadRoe = deadRoe;
    }
    void GenerateBoar(Vector3 newPosition)
    {
        AnimalBehaviour newBoar = Instantiate(enemyBoar, newPosition, Quaternion.Euler(0,Random.Range(0,360),0));
        newBoar.setPlayerController(playerController);
        newBoar.deadBoar = deadBoar;
    }
    
    
}
