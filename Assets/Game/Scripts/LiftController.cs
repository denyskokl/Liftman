﻿using UnityEngine;
using System.Collections;

public class LiftController : MonoBehaviour {

 private Vector3 MouseLastPosition;

   public float speed;
  [SerializeField] private CameraScript camera;
  private Rigidbody2D _rigidbody2D;

  void Awake()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
  }

   void Update()
   {
     if (Application.isEditor)
     {
       UpdateTouchInput();
     }

     // Debug.Log(Input.touchCount);
     if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
     {
       Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
       touchDeltaPosition = touchDeltaPosition*speed;
       TouchMoved(touchDeltaPosition);
     }
   }


   public void UpdateTouchInput()
   {
     var d = Input.GetAxis("Mouse ScrollWheel");
       _rigidbody2D.AddForce(new Vector2(0, speed*d), ForceMode2D.Force);
   }

   void TouchMoved(Vector2 touch)
   {
    _rigidbody2D.AddForce(new Vector2(0, -touch.y), ForceMode2D.Force);
   }

  void OnTriggerEnter2D(Collider2D collider)
  {
    Debug.Log("OnTriggerEnter" + collider.name);
  }
}
