using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehaviour : NetworkBehaviour
{
    private ComponentLogger log;

    private void Awake()
    {
        log = GetComponent<ComponentLogger>();
    }

    // Start is called before the first frame update
    void Start()
    {
        log.Log("Begin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
