using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    public Hint hint;

    public void SetHintEnabled(bool enabled)
    {
        if (hint)
        {
            hint.enabled = enabled;
        }
    }
}
