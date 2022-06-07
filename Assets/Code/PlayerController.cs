using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2;
    [SerializeField] private Transform gun;
    [SerializeField] private Camera camera;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private PlayerInterface playerInterface;
    [SerializeField] private float maxAmmo;
    [SerializeField] private PistolSeting pistolSeting;
    [SerializeField] private AudioSource shootAudioControler;
    [SerializeField] private AudioSource reloadAudiController;
    [SerializeField] private ParticleSystem muzzleParticleSystem;
    [SerializeField] private ParticleSystem aimingMuzzlePArticalSystem;
    [SerializeField] private Transform muzzleTransform;
    [SerializeField] private Transform muzzleAimingTransform;
    [SerializeField] private float defultZoomInCamera;
    [SerializeField] private float aimingZoomInCamera;

   // [SerializeField] private Transform posUsunac;
   // [SerializeField] private NavMeshAgent navUsunac;
    
    
    private float damageReductionFactor;
    private float maxDistanceWhereMaxDamage;
    private float damageGun;
    private RawImage aimingGun;
    

    List<ParticleSystem> muzzleList = new List<ParticleSystem>();
    
    



    private float points = 0;
    private float ammo = 0 ;

    private bool justReload = false;
    private bool canYouShootAfterRelod = true;

    private float timeAfterReload = 0;
    
    void Start()
    {
      
        ammo = maxAmmo;
        damageReductionFactor = pistolSeting.DamageReductionFactor;
        maxDistanceWhereMaxDamage = pistolSeting.MaxDistanceWhereMaxDamage;
        damageGun = pistolSeting.Damage;
        aimingGun = pistolSeting.AimingGunImage;
        


        
        
      
        
        
    }
    
    int howManyTimesPlayerCanJump;
    void Update()
    {
        //navUsunac.SetDestination(posUsunac.position);
        
        Walking();
        HorizontalRotation();
        VerticalRotation();
        Jump();
        
        GunShoot();
        relodAmmo();
        countTimeAfterReload();

        
    }
    
    //void Awake() 
    //{
     ///   DontDestroyOnLoad(transform.gameObject);
    //}

    private void Jump()
    {
        if (IsGrounded())
            howManyTimesPlayerCanJump = 1;

        if (howManyTimesPlayerCanJump > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            howManyTimesPlayerCanJump--;
        }
    }

    void VerticalRotation() {
        var mouseVerticalRotation = Input.GetAxis("Mouse Y");
        var camRot = camera.transform.rotation.eulerAngles;
        camRot.x = camRot.x - mouseVerticalRotation;
        camera.transform.rotation = Quaternion.Euler(camRot);
    }

    void HorizontalRotation() {
        var mouseHorizontalRotation = Input.GetAxis("Mouse X");
        var newRotation = transform.localRotation.eulerAngles;
        newRotation.y = newRotation.y + mouseHorizontalRotation;
        transform.localRotation = Quaternion.Euler(newRotation);
    }

    void Walking() {
        var userKeyboardInput = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );
        var velocity = transform.rotation * userKeyboardInput * movementSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) velocity = velocity * 2.5f;
        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f) && rigidbody.velocity.y <= 0;
    }
    

    void GunShoot()
    {
        camera.fieldOfView = defultZoomInCamera;
        if (Input.GetButton("Fire2"))
        {
            camera.fieldOfView = aimingZoomInCamera;
         if (gun.gameObject.activeSelf)
         {
           gun.gameObject.SetActive(false);  
           playerInterface.showAimingGun();
         }
         if (!canYouShootAfterRelod)
         {
             return;
         }
         if (Input.GetButtonDown("Fire1") && ammo > 0)
         {
             removeAmmoAndPlaySound();
             //aimingMuzzlePArticalSystem.Play();
             ParticleSystem newAimingMuzzle = Instantiate(aimingMuzzlePArticalSystem , muzzleAimingTransform.position, muzzleAimingTransform.rotation);
             newAimingMuzzle.Play();
             muzzleList.Add(newAimingMuzzle);
             Invoke("deleteMuzzle" , 2);
             if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hit))
             {
                 
                 ShotOnPartForBody enemy = hit.collider.GetComponent<ShotOnPartForBody>();
                 if (enemy)
                 {
                     float giveDamage = getGiveDamage(hit.distance);
                     enemy.Shot( hit.collider.tag, giveDamage);
                   
                 }
             }
         }
        }
        else
        {
         if (!gun.gameObject.activeSelf)
         {
             gun.gameObject.SetActive(true);
             playerInterface.hideAimingGun();
         }
         if (!canYouShootAfterRelod)
         {
             return;
         }
         
         if (Input.GetButtonDown("Fire1") && ammo > 0)
         {
             removeAmmoAndPlaySound();
            // muzzleParticleSystem.Play();
             ParticleSystem newMuzzle = Instantiate(aimingMuzzlePArticalSystem , muzzleTransform.position, muzzleTransform.rotation);
             newMuzzle.Play(); 
             muzzleList.Add(newMuzzle);
             Invoke("deleteMuzzle" , 2);
            
             if (Physics.Raycast(gun.position, gun.forward, out RaycastHit hit))
             {
                 ShotOnPartForBody enemy = hit.collider.GetComponent<ShotOnPartForBody>();
                 if (enemy)
                 {
                     float giveDamage = getGiveDamage(hit.distance);
                     enemy.Shot( hit.collider.tag, giveDamage);
                    
                 }
             }
         }
        }
       
        
    }
    void deleteMuzzle()
    {
        if (muzzleList.Count != 0)
        {
            Destroy(muzzleList[0]);
            muzzleList.RemoveAt(0);
        }
        
    }
    

   
   


    float getGiveDamage(float distance)
    {
        float damage = 0;

        if (distance <= maxDistanceWhereMaxDamage)
        {
            damage = damageGun;
            return damage;
        }
        else
        {
            
            damage = damageGun - ((distance - maxDistanceWhereMaxDamage) * damageReductionFactor);
            if (damage <= 0)
            {
                return 0;
            }
            else
            { 
                return damage;   
            }
        }
    }

    public void setPoints(float point)
    {
        points = point;
    }
    public float getPoints()
    {
        return points;
    }

    public void addPoints(float _points)
    { 
        points = points + _points;
    }

    private void relodAmmo()
    {
        if (Input.GetKey(KeyCode.R) && !justReload)
        {
            justReload = true;
            canYouShootAfterRelod = false;
            reloadAudiController.Play();
            ammo = maxAmmo; 
        }
    }

    public float getAmountOfAmmo()
    {
        return ammo;
    }

    private void removeAmmoAndPlaySound()
    {
        shootAudioControler.Play();
        ammo -= 1;
        justReload = false;
       
    }

    private void countTimeAfterReload()
    {
        if (justReload)
        {
            timeAfterReload += Time.deltaTime;
            if (timeAfterReload > 1)
            {
                canYouShootAfterRelod = true;
                timeAfterReload = 0;
            }
            
        }
    }
    
    
}
