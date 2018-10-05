using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{
    public enum State
    {
        None,
        Hovered,
        Connected,
        Fixed
    }

    public string type;
    public State state {
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

    public bool Connect()
    {
        if (state == State.Hovered)
        {
            state = State.Connected;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Fix()
    {
        if (state == State.Connected)
        {
            state = State.Fixed;
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual void OnStateChanged(State newState, State oldState)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CheckSocketType(other.GetComponent<Socket>()))
            return;

        if (state == State.None)
            state = State.Hovered;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!CheckSocketType(other.GetComponent<Socket>()))
            return;

        if (state == State.Hovered)
            state = State.None;
    }

    private bool CheckSocketType(Socket socket)
    {
        return socket && socket.type == type;
    }
}
