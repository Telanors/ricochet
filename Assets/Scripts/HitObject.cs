using UnityEngine;
public abstract class HitObject : MonoBehaviour
{
    [SerializeField] protected GameObject[] destroyObjects;
    public abstract void Destract();
}
