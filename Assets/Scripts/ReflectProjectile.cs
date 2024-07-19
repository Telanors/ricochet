using UnityEngine;
public class ReflectProjectile : Projectile
{
    [SerializeField] public int ricochetCount = 10;
    public override int LineCount => 3;
    private void Start()
    {
        hitReaction = new ReflectReaction(ricochetCount);
    }
}
