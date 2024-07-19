using UnityEngine;
public class ReflectReaction : IHitReaction
{
    private int ricochetCount;

    private int currentRicochetCount;
    public ReflectReaction(int ricochetCount)
    {
        this.ricochetCount = ricochetCount;
        currentRicochetCount = 0;
    }
    public void Activate(Projectile projectile, RaycastHit2D raycastHit)
    {
        currentRicochetCount++;
        if (currentRicochetCount > ricochetCount)
        {
            Object.Destroy(projectile.gameObject);
        }
        SoundBox.Spawn(projectile.transform.position, Resources.Load<AudioClip>($@"Sounds\Hit_Sound"));
        projectile.DestructableCheck(raycastHit.transform);
        Vector2 direction = Vector2.Reflect(projectile.transform.up, raycastHit.normal).normalized;
        Projectile.Rotate(projectile.transform, direction);
        Object.Instantiate(Resources.Load<ParticleSystem>($@"Prefabs\ReflectProjectile_Partical"), projectile.transform.position, Quaternion.identity);
    }
}