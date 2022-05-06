using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    
    [SerializeField] private AnimalBehaviour enemySheep;
    [SerializeField] private AnimalBehaviour enemyDeer;
    [SerializeField] private AnimalBehaviour enemyRoe;
    [SerializeField] private AnimalBehaviour enemyBoar;
    [SerializeField] private GameObject deadSheep;
    [SerializeField] private GameObject deadDeer;
    [SerializeField] private GameObject deadBoar;
    [SerializeField] private GameObject deadRoe;
    
    [SerializeField] private GameObject tree1;
    [SerializeField] private GameObject tree2;
    [SerializeField] private GameObject tree3;
    [SerializeField] private GameObject tree4;
    [SerializeField] private GameObject tree5;
    [SerializeField] private GameObject tree6;
    [SerializeField] private GameObject tree7;
    [SerializeField] private GameObject tree8;
    [SerializeField] private GameObject bush1;
    [SerializeField] private GameObject bush2;
    [SerializeField] private GameObject rock1;
    [SerializeField] private GameObject rock2;
 
    
    [SerializeField] private Terrain terrain;
    [SerializeField] private float landDivisionCoefficient;
    
    [SerializeField] private float maxAmmountOfAnimal;
    [SerializeField] private float maxAmmountOfPlants;
    
  

    private float xLenghtTerrain;
    private float zLenghtTerrain;

    private float xFragmentLength;
    private float zFragmentLength;

    private float yRayCastPosition = 200;

    private float ammounOfAnimal = 0;
    private float ammountOfPlants = 0;
    
    
        
    void Start()
    {
        startInformationAboutTerrain();
        
        
        GeneratePlantsAndRockOnHoleTerrain();
        GenerateAnimalOnHoleTerrain();
        
    }

    private void startInformationAboutTerrain()
    {
        
        xLenghtTerrain = terrain.terrainData.size.x;
        zLenghtTerrain = terrain.terrainData.size.z;
        xFragmentLength = xLenghtTerrain / landDivisionCoefficient;
        zFragmentLength = zLenghtTerrain / landDivisionCoefficient;
    }

    
    private void GeneratePlantsAndRockOnHoleTerrain()
    {
        float maxAmmountPlants =(int) (maxAmmountOfPlants / Math.Pow(landDivisionCoefficient,2));
        if (maxAmmountPlants <= 0)
        {
            maxAmmountPlants = 1;
        }
        float ix = xFragmentLength;
            while (ix <= xLenghtTerrain)
            {
                float iz = zFragmentLength;
                float repetitionCounterPlayer = 0;
                float repetitionCounterPlants = 0;
                while (iz <= zLenghtTerrain)
                {
                    float amountPlantsInOneSquare = 0;
                    while (maxAmmountPlants >= amountPlantsInOneSquare)
                    {
                        float x = Random.Range(ix - xFragmentLength, ix);
                        float z = Random.Range(iz - zFragmentLength, iz);
                        
                        Physics.Raycast(new Vector3(x, yRayCastPosition, z), Vector3.down, out RaycastHit hit);
                        if (hit.transform.gameObject.layer.Equals("plants") && hit.transform.gameObject.layer.Equals("Player"))
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
                                        iz = zFragmentLength;
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
                        float y = yRayCastPosition - hit.distance;
                        Vector3 newPlantsPosition = new Vector3(x, y, z);
                        int selectedPlants = Random.Range(1, 13);
                        ammountOfPlants += 1;
                        switch (selectedPlants)
                        {
                            case 1:
                                GameObject newTree1 = Instantiate(tree1, newPlantsPosition, Quaternion.Euler(0,Random.Range(0,360),0));
                                checkPositionNewObject(newTree1, newPlantsPosition);
                                break;
                            case 2:
                                GameObject newTree2 = Instantiate(tree2, newPlantsPosition, Quaternion.Euler(0,Random.Range(0,360),0));
                                checkPositionNewObject(newTree2, newPlantsPosition);
                                break;
                            case 3:
                                GameObject newTree3 = Instantiate(tree3, newPlantsPosition, Quaternion.Euler(0,Random.Range(0,360),0));
                                checkPositionNewObject(newTree3, newPlantsPosition);
                                break;
                            case 4:
                                GameObject newTree4 = Instantiate(tree4, newPlantsPosition, Quaternion.Euler(0,Random.Range(0,360),0));
                                checkPositionNewObject(newTree4, newPlantsPosition);
                                break;
                            case 5:
                                GameObject newTree5 = Instantiate(tree5, newPlantsPosition, Quaternion.Euler(0,Random.Range(0,360),0));
                                checkPositionNewObject(newTree5, newPlantsPosition);
                                break;
                            case 6:
                                GameObject newTree6 = Instantiate(tree6, newPlantsPosition, Quaternion.Euler(0,Random.Range(0,360),0));
                                checkPositionNewObject(newTree6, newPlantsPosition);
                                break;
                            case 7:
                                GameObject newTree7 = Instantiate(tree7, newPlantsPosition, Quaternion.Euler(0,Random.Range(0,360),0));
                                checkPositionNewObject(newTree7, newPlantsPosition);
                                break;
                            case 8:
                                GameObject newTree8 = Instantiate(tree8, newPlantsPosition, Quaternion.Euler(0,Random.Range(0,360),0));
                                checkPositionNewObject(newTree8, newPlantsPosition);
                                break;
                            case 9:
                                GameObject newbush1 = Instantiate(bush1, newPlantsPosition, Quaternion.Euler(0,Random.Range(0,360),0));
                                checkPositionNewObject(newbush1, newPlantsPosition);
                                break;
                            case 10:
                                GameObject newbush2 = Instantiate(bush2, newPlantsPosition, Quaternion.Euler(0,Random.Range(0,360),0));
                                checkPositionNewObject(newbush2, newPlantsPosition);
                                break;
                            case 11:
                                GameObject newrock1 = Instantiate(rock1, newPlantsPosition, Quaternion.Euler(0,Random.Range(0,360),0));
                                checkPositionNewObject(newrock1, newPlantsPosition);
                                break;
                            case 12:
                                GameObject newrock2 = Instantiate(rock2, newPlantsPosition, Quaternion.Euler(0,Random.Range(0,360),0));
                                checkPositionNewObject(newrock2, newPlantsPosition);
                                break;
                           
                            default:
                                break;
                        }
                        amountPlantsInOneSquare += 1;
                    }
                    iz += zFragmentLength;
                    
                }
                ix += xFragmentLength;
            }
        
    }

    private void checkPositionNewObject(GameObject newObject, Vector3 selectPosition)
    {
        if (newObject.transform.position.y != selectPosition.y)
        {
            Destroy(newObject);
            ammountOfPlants -= 1;
            
        }
        
    }

    private void SelectAnimalAndGenerate(int selectedAnimal, Vector3 newAnimalPosition)
    {
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

    private void GenerateAnimalOnHoleTerrain()
    {
        float maxAmmountAnimalInOneSquare =(int) (maxAmmountOfAnimal / Math.Pow(landDivisionCoefficient,2));
        if (maxAmmountAnimalInOneSquare <= 0)
        {
            maxAmmountAnimalInOneSquare = 1;
        }
        float ix = xFragmentLength;
            while (ix <= xLenghtTerrain)
            {
                float iz = zFragmentLength;
                float repetitionCounterPlayer = 0;
                float repetitionCounterPlants = 0;
                while (iz <= zLenghtTerrain)
                {
                    float amountAnimalInOneSquare = 0;
                    while (maxAmmountAnimalInOneSquare >= amountAnimalInOneSquare)
                    {
                        float x = Random.Range(ix - xFragmentLength, ix);
                        float z = Random.Range(iz - zFragmentLength, iz);

                        if (Math.Abs(x - playerController.transform.position.x) < 100 &&
                            Math.Abs(z - playerController.transform.position.z) < 100)
                        {
                            repetitionCounterPlayer += 1;
                            if (repetitionCounterPlayer > 50)
                            {
                                if (iz + zFragmentLength <= zLenghtTerrain)
                                {
                                    iz += zFragmentLength;
                                    repetitionCounterPlayer = 0;
                                    continue;

                                }
                                else
                                {
                                    if (ix + xFragmentLength <= xFragmentLength)
                                    {
                                        ix += xFragmentLength;
                                        iz = zFragmentLength;
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
                                        iz = zFragmentLength;
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
                        float y = yRayCastPosition - hit.distance + 2;
                        Vector3 newAnimalPosition = new Vector3(x, y, z);
                        int selectedAnimal = Random.Range(1, 5);
                        ammounOfAnimal += 1;
                        SelectAnimalAndGenerate(selectedAnimal, newAnimalPosition);
                        amountAnimalInOneSquare += 1;
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
