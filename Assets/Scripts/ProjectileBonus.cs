using UnityEngine;
public class ProjectileBonus : SelectableObject
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private int bonusShots = 2;
    private void Awake()
    {
        projectile.shotsCount = bonusShots;
    }
   
    public override void Use()
    {
        if (ProjectileGun.Instance.CurrentProjectile.GetType() == projectile.GetType())
        {
            ProjectileGun.Instance.CurrentAmmo += bonusShots;
        }
        else
        {
            ProjectileGun.Instance.defaultAmmo = ProjectileGun.Instance.CurrentAmmo;
            ProjectileGun.Instance.CurrentProjectile = projectile;
        }
        Destroy(gameObject);
    }
}
