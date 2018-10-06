using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    public static ConstructionManager instance = null;

    public Part[] parts;
    public int partIndex = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ActivatePart(partIndex);
    }

    public void NextHint()
    {
        ActivatePart(++partIndex);
    }

    void ActivatePart(int index)
    {
        if (index < 0 || index >= parts.Length)
        {
            return;
        }

        for (int i = 0; i < parts.Length; ++i)
        {
            if (parts[i] == null)
            {
                continue;
            }

            if (i == index)
            {
                parts[i].SetHintEnabled(true);
                GameObject[] otherParts = GameObject.FindGameObjectsWithTag(parts[i].tag);

                foreach (GameObject part in otherParts)
                {
                    FixedPart fixedPart = part.GetComponent<FixedPart>();
                    if (fixedPart && fixedPart.state == FixedPart.State.Disabled)
                    {
                        fixedPart.state = FixedPart.State.Highlighted;
                    }
                }
            }
            else
            {
                parts[i].SetHintEnabled(false);
                GameObject[] otherParts = GameObject.FindGameObjectsWithTag(parts[i].tag);
                foreach (GameObject part in otherParts)
                {
                    FixedPart fixedPart = part.GetComponent<FixedPart>();
                    if (fixedPart && fixedPart.state != FixedPart.State.Visible)
                    {
                        fixedPart.state = FixedPart.State.Disabled;
                    }
                }
            }
        }
    }
}
