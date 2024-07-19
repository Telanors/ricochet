using UnityEngine;
public class ProjectileGun : MonoBehaviour
{
    public static ProjectileGun Instance { get; private set; }
    public Projectile defaultProjectile;
    public int defaultAmmo;

    private Projectile currentProjectile;
    private AudioSource audioSource;
    public Projectile CurrentProjectile 
    {
        get => currentProjectile;
        set
        {
            currentProjectile = value;
            CurrentAmmo = currentProjectile.shotsCount;
        }
    }

    private int currentAmmo;
    public int CurrentAmmo
    {
        get => currentAmmo;
        set
        {
            currentAmmo = value;
            if (currentAmmo <= 0)
            {
                if (currentProjectile.GetType() != defaultProjectile.GetType())
                {
                    currentProjectile = defaultProjectile;
                    CurrentAmmo = defaultAmmo;
                }
                else
                {
                    Menu.Instance.LoseTimerStart();
                }
            }
            else
            {
                Menu.Instance.LoseTimerStop();
            }
            Menu.Instance.AmmoUpdate(currentProjectile.projName, currentAmmo.ToString());
        }
    }

    [SerializeField] private Transform gunner;
    [SerializeField] private DrawLine line;
    [SerializeField] private LayerMask ignoreMask;
    [SerializeField] private LayerMask selectMask;
    private bool readyToShoot = true;
    private Vector2 currentMouseWorldPosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private void Awake()
    {
        if (Instance != null)
        {
            Instance = this;
        }
        audioSource = GetComponent<AudioSource>();
        line = GetComponent<DrawLine>();
    }

    private void Start()
    {
        currentProjectile = defaultProjectile;
        CurrentAmmo = defaultAmmo;
    }

    private void FixedUpdate()
    {
        if (Menu.Instance.Pause) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D raycastHit = Physics2D.Raycast(ray.origin, ray.direction, 100.0f, selectMask);
        if (raycastHit.collider != null)
        {
            audioSource.PlayOneShot(Resources.Load<AudioClip>($@"Sounds\Collect_Sound"));
            raycastHit.transform.GetComponent<SelectableObject>().Use();
        }
    }

    private void Update()
    {
        if (Menu.Instance.Pause) return;
        if (Input.GetMouseButtonDown(0))
        {
            line.ToggleLine(true);
            readyToShoot = true;
        }
        if (Input.GetMouseButtonUp(0))
        {               
            line.ToggleLine(false);
            if (readyToShoot)
            {
                if (CurrentAmmo <= 0)
                {
                    Menu.Instance.DisplayeAlert("ÍÅÒ ÇÀÐßÄÎÂ", Color.red);
                }
                else
                {
                    audioSource.PlayOneShot(Resources.Load<AudioClip>($@"Sounds\Shot_Sound"));
                    Projectile.Spawn(currentProjectile, gunner.position, (currentMouseWorldPosition - (Vector2)gunner.position).normalized, Quaternion.identity);
                    CurrentAmmo--;
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(1))
            {
                readyToShoot = false;
                line.ToggleLine(false);
            }
            else
            {
                line.positionCount = currentProjectile.LineCount;
                line.DrawToMouse(gunner.position, ignoreMask);
                gunner.transform.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.right, (currentMouseWorldPosition - (Vector2)gunner.position).normalized));
            }
        }
    }
}
