using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

public class GameplayController : MonoBehaviour
{
  [SerializeField] private GameObject lift;
  [SerializeField] private Collider2D touchArea;
  private Vector3 MouseLastPosition;

  public float speed;


}