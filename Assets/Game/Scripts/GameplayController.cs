using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameplayController : MonoBehaviour
{
  void Start()
  {
    InitGuest();
  }


  private void InitGuest()
  {
    GuestGenerate.ins.GuestCreated();
  }
}