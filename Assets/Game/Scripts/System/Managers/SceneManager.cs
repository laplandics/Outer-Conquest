using System.Collections;
using UnityEngine;

public abstract class SceneManager : MonoBehaviour
{
    public virtual IEnumerator Initialize() { yield break; }
    public virtual void Deinitialize() { }
}