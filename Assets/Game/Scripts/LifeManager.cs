using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class LifeManager : MonoBehaviour
{
  private List<GameObject> _life;
  public int Life = 0;
  public static Action OnGameOver;


  void Awake()
  {
    Guest.OnLifeUpdate += MinusLife;
    _life = new List<GameObject>();
    foreach (Transform child in transform)
    {
      if (child == null) return;
      var currentLife = child.GetComponent<Image>().gameObject;
      _life.Add(currentLife);
    }

    Life = _life.Count;
  }

  public void MinusLife()
  {
    var lifeObj = _life.FirstOrDefault(f => f.activeSelf);
    if (lifeObj != null) lifeObj.SetActive(false);
    Life --;

    if (Life <= 0)
    {
      OnGameOver();
    }
  }
}