using UnityEngine;

public class AmmoBonus : SelectableObject
{
    [SerializeField] private int ammoBonus = 1;
    public override void Use()
    {
        ProjectileGun.Instance.CurrentAmmo += ammoBonus;
        Destroy(gameObject);
    }
}
