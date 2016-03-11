using UnityEngine;
using System.Collections;

public class LiftController : MonoBehaviour {

 [SerializeField] private Collider2D _botom;
  [SerializeField] private Collider2D top;
  private CameraScript _camera;

  void Awake()
  {
    _camera = FindObjectOfType<CameraScript>();
  }

  void OnTriggerEnter(Collider collider)
  {
      Debug.Log("ON");
    _camera.isNeedMove = true;
  }
}
