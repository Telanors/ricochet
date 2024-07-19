using UnityEngine;
public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected LayerMask ignoreMask;
    public AudioSource audioSource { get; protected set; }

    public string projName = "noname";
    public float speed = 1.0f;
    public int shotsCount = -1;
    public virtual int LineCount => 2;

    protected IHitReaction hitReaction;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, transform.up, Time.deltaTime * speed + 0.1f, ~ignoreMask);
        if (raycastHit.collider != null)
        {
            hitReaction.Activate(this, raycastHit);
        }
        transform.Translate(transform.up * Time.deltaTime * speed, Space.World);
    }

    public bool DestructableCheck(Transform transform)
    {
        if(transform.TryGetComponent(out HitObject hitObject))
        {
            hitObject.Destract();
            return true;
        }
        return false;
    }
 
    public static Projectile Spawn(Projectile type, Vector2 position, Vector2 direction, Quaternion rotation)
    {
        Projectile projectile = Instantiate(type, position, Quaternion.identity);
        Rotate(projectile.transform, direction);
        return projectile;
    }

    public static void Rotate(Transform transform, Vector2 direction)
    {
        float angle = Vector2.SignedAngle(Vector2.up, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public static Projectile GetPrefab(string type) => Resources.Load<Projectile>($@"Prefabs\{type}");
}