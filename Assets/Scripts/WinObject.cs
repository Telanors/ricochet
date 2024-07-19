using UnityEngine;
public class WinObject : HitObject
{
    public override void Destract()
    {
        SoundBox.Spawn(transform.position, Resources.Load<AudioClip>($@"Sounds\Wall_Sound"));
        Menu.Instance.LoseTimerStop();
        Menu.Instance.WinText();
        Menu.Instance.SelectMenu();
        Menu.Instance.lose = true;
        Destroy(gameObject);
    }
}
