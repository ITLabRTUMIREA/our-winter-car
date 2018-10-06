using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPart : MonoBehaviour
{
    public Material _highlightMaterial;

    public enum State
    {
        Disabled,
        Highlighted,
        Visible
    }

    MeshRenderer _meshRenderer;
    Material[] _materials;
    
    public State state
    {
        get
        {
            return _currentState;
        }
        set
        {
            if (_currentState != value)
            {
                switch(value)
                {
                    case State.Disabled:
                        _meshRenderer.enabled = false;
                        break;
                    case State.Highlighted:
                        Material[] materials = _meshRenderer.materials;
                        for (int i = 0; i < _meshRenderer.materials.Length; ++i)
                        {
                            materials[i] = _highlightMaterial;
                        }
                        _meshRenderer.materials = materials;
                        _meshRenderer.enabled = true;
                        break;
                    case State.Visible:
                        _meshRenderer.materials = _materials;
                        _meshRenderer.enabled = true;
                        break;
                }
            }
            _currentState = value;
        }
    }
    State _currentState = State.Visible;

    void Awake()
    {
        Debug.Log(_highlightMaterial == null);
    }

	void Start ()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _materials = _meshRenderer.materials;

        state = State.Highlighted;
	}
}
