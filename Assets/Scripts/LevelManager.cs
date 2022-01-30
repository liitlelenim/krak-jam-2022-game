using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{
   public static void RestartLevel()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

   public static void LoadNextLevel()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
   }

   public static void LoadLevel(int buildIndex)
   {
      SceneManager.LoadScene(buildIndex);
   }
   public static void LoadMenu()
   {
      throw new NotImplementedException("Implement when menu is added");
   }  
}
