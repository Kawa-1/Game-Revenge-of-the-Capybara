
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2;
    [SerializeField] private Transform gun;
    [SerializeField] private Camera camera;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private PlayerInterface playerInterface;
    [SerializeField] private float maxAmmo;
    [SerializeField] private PistolSeting pistolSeting;

    private float damageReductionFactor;
    private float maxDistanceWhereMaxDamage;
    private float damageGun;
    private RawImage aimingGun;



    private float points = 0;
    private float ammo = 0 ;
    
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
        Walking();
        HorizontalRotation();
        VerticalRotation();
        Jump();
        
        GunShoot();
        relodAmmo();
        
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
        if (Input.GetKey(KeyCode.LeftShift)) velocity = velocity * 2;
        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f) && rigidbody.velocity.y <= 0;
    }
    

    void GunShoot()
    {
        if (Input.GetButton("Fire2"))
     {
         if (gun.gameObject.activeSelf)
         {
           gun.gameObject.SetActive(false);  
           playerInterface.showAimingGun();
         }
         if (Input.GetButtonDown("Fire1") && ammo > 0)
         {
             ammo -= 1;
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
         
         if (Input.GetButtonDown("Fire1") && ammo > 0)
         {
             ammo -= 1;
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
        if (Input.GetKey(KeyCode.R))
        {
           ammo = maxAmmo; 
        }
    }

    public float getAmountOfAmmo()
    {
        return ammo;
    }
    
}
