using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Triggerable : MonoBehaviour
{

    /// <summary>
    /// Should be called whenever this object is interacted with.
    /// </summary>
    public abstract void Interact();

}
