using Unity.VisualScripting;
using UnityEngine;

public interface IThrowable
{
    public virtual void OnTriggerEnter(Collider other) { }
}
