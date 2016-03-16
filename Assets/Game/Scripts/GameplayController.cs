using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameplayController : MonoBehaviour
{
  [SerializeField] private GameObject lift;
  [SerializeField] private Collider2D touchArea;
  private Vector3 MouseLastPosition;

  void Start()
  {
    InitGuest();
  }


  private void InitGuest()
  {
    GuestGenerate.ins.GuestCreated();
  }
}