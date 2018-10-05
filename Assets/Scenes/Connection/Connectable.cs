using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connectable : MonoBehaviour
{
    public enum State
    {
        None,
        Connected,
        Fixed
    }

    public State state
    {
        get
        {
            return _currentState;
        }

        private set
        {
            if (_currentState != value)
            {
                State lastState = _currentState;
                _currentState = value;
                OnStateChanged(_currentState, lastState);
            }
        }
    }

    State _currentState = State.None;

    Connection[] connections;
    Transform rootJoint;

	void Start ()
    {
        connections = GetComponentsInChildren<Connection>();
	}
	
	void Update ()
    {
        CheckState();
	}

    private void LateUpdate()
    {
        if (state == State.Connected)
        {
            transform.position = rootJoint.transform.position;
        }
    }

    protected virtual void OnStateChanged(State newState, State oldState)
    {
    }

    void CheckState()
    {
        bool connected = true;

        foreach (Connection connection in connections)
        {
            if (connection.state != Connection.State.Connected ||
                connection.state != Connection.State.Hovered) 
            {
                connected = false;
                break;
            }
        }

        if (connected) {
            state = State.Connected;
        }
    }
}
