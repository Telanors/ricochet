using UnityEngine;
public class SimpleReaction : IHitReaction
{
    public void Activate(Projectile projectile, RaycastHit2D raycastHit)
    {
        Object.Instantiate(Resources.Load<ParticleSystem>($@"Prefabs\SimpleProjectile_Partical"), projectile.transform.position, Quaternion.identity);
        SoundBox.Spawn(projectile.transform.position, Resources.Load<AudioClip>($@"Sounds\Hit_Sound"));
        projectile.DestructableCheck(raycastHit.transform);
        Object.Destroy(projectile.gameObject);
    }
}
