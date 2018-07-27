using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Invoke("LoadNextLevel",1f);
            var player = GameObject.FindObjectOfType<Player>();
            player.CanMove = false;
            player.anim.SetTrigger("freeze");          
        }
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        
        
    }
}
