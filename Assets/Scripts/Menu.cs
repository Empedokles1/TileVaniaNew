﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void StartGame()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
