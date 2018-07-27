using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour {

    int startingSceneIndex;
    private void Awake()
    {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
        if(numScenePersists > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(startingSceneIndex != sceneIndex)
        {
            Destroy(this.gameObject);
        }
    }
}
