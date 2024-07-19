using UnityEngine;
public class SoundBox : MonoBehaviour
{
    public AudioSource audioSource { get; private set; }
    public float timeToDestroy = 3.0f;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke(nameof(Delete), timeToDestroy);
    }
  
    private void Delete()
    {
        Destroy(gameObject);
    }

    public static void Spawn(Vector3 position, AudioClip clip)
    {
        var soundBox = Instantiate(Resources.Load<SoundBox>($@"Prefabs\Soundbox"), position, Quaternion.identity);
        soundBox.audioSource.clip = clip;
        soundBox.audioSource.Play();
    }
}
