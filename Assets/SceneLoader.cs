using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Scene] public string environmentScene;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(environmentScene, LoadSceneMode.Additive);
    }
}
