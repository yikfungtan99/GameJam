using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentLogger : MonoBehaviour
{
    [SerializeField] private bool debug = true;
    [SerializeField] private bool debug_gameObject_name = false;

    public void Log(object msg)
    {
        if (!debug) return;

        string logMessage = msg.ToString();

        if (debug_gameObject_name) logMessage = $"{gameObject}: {msg}";

        Debug.Log(logMessage);
    }
}
