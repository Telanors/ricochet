public class SimpleProjectile : Projectile
{
    private void Start()
    {
        hitReaction = new SimpleReaction();
    }
}
