using UnityEngine;
using UnityEngine.UI;

public class PistolSeting : MonoBehaviour
{
    [SerializeField] private float maxDistanceWhereMaxDamage = 20;
    [SerializeField] private float damageReductionFactor = 1;
    [SerializeField] private float damage = 100;
    [SerializeField] private RawImage aimingGunImage;

    public float MaxDistanceWhereMaxDamage
    {
        get => maxDistanceWhereMaxDamage;
        set => maxDistanceWhereMaxDamage = value;
    }

    public float DamageReductionFactor
    {
        get => damageReductionFactor;
        set => damageReductionFactor = value;
    }

    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    public RawImage AimingGunImage
    {
        get => aimingGunImage;
        set => aimingGunImage = value;
    }
}