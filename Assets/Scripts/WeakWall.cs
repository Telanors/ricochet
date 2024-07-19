using System.Collections;
using UnityEngine;
public class WeakWall : HitObject
{
    public float animationSpeed = 2.5f;
    private Collider2D _collider;
    private SpriteRenderer sprite;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();  
        _collider = GetComponent<Collider2D>();
    }

    public override void Destract()
    {
        _collider.enabled = false;
        StartCoroutine(AlphaAnimation());
        Instantiate(Resources.Load<ParticleSystem>($@"Prefabs\WeakWall_Partical"), transform.position, Quaternion.identity);
        SoundBox.Spawn(transform.position, Resources.Load<AudioClip>($@"Sounds\Wall_Sound"));
    }

    private IEnumerator AlphaAnimation()
    {
        float alpha = sprite.color.a;
        float timer = 0.0f;
        Color color = sprite.color;
        while(timer < 1.0f)
        {
            timer += Time.deltaTime * animationSpeed;
            color.a = Mathf.Lerp(alpha, 0.0f, timer);
            sprite.color = color;
            yield return null;
        }
        color.a = 0.0f;
        sprite.color = color;
        if(destroyObjects.Length != 0)
        {
            foreach (var item in destroyObjects)
            {
                Instantiate(item, transform.position, transform.rotation);
            }
        }
        Destroy(gameObject);
    }
}
