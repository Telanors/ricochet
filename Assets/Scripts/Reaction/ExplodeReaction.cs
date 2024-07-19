using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//Класс для взрывного поведения пули
public class ExplodeReaction : IHitReaction
{
    private float spread;
    public ExplodeReaction(float spread)
    {
        this.spread = spread;
    }

    public void Activate(Projectile projectile, RaycastHit2D raycastHit)
    {
        Vector2 direction = Vector2.Reflect(projectile.transform.up, raycastHit.normal).normalized;
        direction.x -= spread * 2.0f;
        for (int i = 0; i < 3; i++)
        {
            direction.x += spread;
            Projectile.Spawn(Projectile.GetPrefab(typeof(ReflectProjectile).ToString()), projectile.transform.position, direction.normalized, Quaternion.identity);
        }
        SoundBox.Spawn(projectile.transform.position, Resources.Load<AudioClip>($@"Sounds\Hit_Sound"));
        projectile.DestructableCheck(raycastHit.transform);
        Object.Instantiate(Resources.Load<ParticleSystem>($@"Prefabs\ExplodeProjectile_Partical"), projectile.transform.position, Quaternion.identity);
        Object.Destroy(projectile.gameObject);
    }
}
