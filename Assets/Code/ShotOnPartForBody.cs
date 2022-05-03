using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotOnPartForBody : MonoBehaviour
{

  [SerializeField] private AnimalBehaviour animalBehaviour;
  
  
  public void Shot(string bodyPart, float damage)
  {
    animalBehaviour.ShotOnAnimal(bodyPart, damage);
  }
}
