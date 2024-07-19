using UnityEngine;
public interface IHitReaction
{
    public void Activate(Projectile projectile, RaycastHit2D raycastHit);
}
