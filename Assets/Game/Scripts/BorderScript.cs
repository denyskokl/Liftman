using System;
using UnityEngine;
using System.Collections;

public class BorderScript : MonoBehaviour
{
  [SerializeField] private Collider2D _botom;
  [SerializeField] private Collider2D top;
  private CameraScript _camera;

  void Awake()
  {
    _camera = FindObjectOfType<CameraScript>();
  }

  void OnTriggerEnter(Collider collider)
  {
      Debug.Log(collider.gameObject.name);
    _camera.isNeedMove = true;
  }
}
