using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
  void Awake()
  {
    LifeManager.OnGameOver += ShowGameOver;
  }

  private void ShowGameOver()
  {
    var panel = transform.GetChild(0).gameObject;
    panel.transform.localScale = Vector3.zero;
    panel.transform.DOScale(Vector3.one, 3f);
    panel.SetActive(true);
    StartCoroutine(Pause());
  }

  IEnumerator Pause()
  {
    yield return new WaitForSeconds(3.1f);
    Time.timeScale = 0;
  }
}