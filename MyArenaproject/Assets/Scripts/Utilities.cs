using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public static class Utilities
{
    public static int playerDeaths = 0;

    public static string UpdateDeathCount(ref int countReference)
    {
        // 2
        countReference += 1;
        return "Next time you'll be at number " + countReference;
    }

    public static void RestartLevel(int sceneIndex)
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;

        Debug.Log("Player deaths: " +playerDeaths);
        string message = UpdateDeathCount(ref playerDeaths);
        Debug.Log("Player deaths: "  +playerDeaths);
        if (sceneIndex < 0)
        {
            // 2
            throw new System.ArgumentException("Scene index cannot be negative");
         }
    }
    public static bool RestartLevel(int sceneIndex)
    {
        // 2
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1.0f;

        // 3
        return true;
    }
}
