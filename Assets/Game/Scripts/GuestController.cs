using System;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class GuestController : MonoBehaviour
{

  void Start()
  {
    MoveGues();
  }


  private void MoveGues()
  {
    ChageFlip(true);
    transform.DOMoveX(-7.8f, 6f, false).OnComplete(Pause);
  }

  private void Pause()
  {
    Invoke("MoveBack", 1f);
  }

  private void MoveBack()
  {
    ChageFlip(false);
    transform.DOMoveX(-2f, 6f, false).OnComplete(Destroy);
  }


  public void Destroy()
  {
    Destroy(gameObject);
  }

  private void ChageFlip(bool status)
  {
    transform.GetComponent<SpriteRenderer>().flipX = status;
  }
}