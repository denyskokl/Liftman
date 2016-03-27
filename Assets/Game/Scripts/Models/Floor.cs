using System;
using UnityEngine;

[Serializable]
public class Floor
{
  [SerializeField] public int Number;
  [SerializeField] public float PositionY;
  public int GuestNumber { get; set; }
}