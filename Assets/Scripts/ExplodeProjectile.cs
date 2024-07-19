using UnityEngine;
public class ExplodeProjectile : Projectile
{
    [SerializeField] private float spread;
    private void Start()
    {
        hitReaction = new ExplodeReaction(spread);
    }
}
