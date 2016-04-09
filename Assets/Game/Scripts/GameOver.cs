using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{


  void Awake()
  {
    LifeManager.OnGameOver += ShowGameOver;

  }

  private void ShowGameOver()
  {
    Time.timeScale=0;
    transform.GetChild(0).gameObject.SetActive(true);
  }
}