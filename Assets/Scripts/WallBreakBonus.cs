using UnityEngine;
public class WallBreakBonus : SelectableObject
{
    public override void Use()
    {
        SoundBox.Spawn(transform.position, Resources.Load<AudioClip>($@"Sounds\Dinamite_Sound"));
        var walls =  FindObjectsOfType<WeakWall>();
        if (walls.Length != 0)
        {
            walls[Random.Range(0, walls.Length)].Destract();
        }
        Destroy(gameObject);
    }
}
