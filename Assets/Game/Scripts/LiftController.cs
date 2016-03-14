using UnityEngine;
using System.Collections;

public class LiftController : MonoBehaviour {

 private Vector3 MouseLastPosition;

   public float speed;
  [SerializeField] private Collider2D touchArea;

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
       GetComponent<Rigidbody2D>().AddForce(new Vector2(0, speed*d), ForceMode2D.Force);
   }

   void TouchMoved(Vector2 touch)
   {
     GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -touch.y), ForceMode2D.Force);
   }

  void OnTriggerEnter(Collider collider)
  {
    Debug.Log("OnTriggerEnter");
  }

  void OnTriggerExit()
  {
    Debug.Log("OnTriggerExit");
  }

  void OnTriggerStay()
  {
      Debug.Log("OnTriggerStay");
  }


}
