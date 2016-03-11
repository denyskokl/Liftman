using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

public class GameplayController : MonoBehaviour
{
  [SerializeField] private GameObject lift;
  [SerializeField] private Collider2D touchArea;
  private Vector3 MouseLastPosition;

  public float speed;

  void Update()
  {
    UpdateTouchInput();
    // Debug.Log(Input.touchCount);
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
    {
      Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
      touchDeltaPosition = touchDeltaPosition*speed;
      Debug.Log("TEST");

      lift.transform.Translate(0, -touchDeltaPosition.y*speed, 0);
    }
  }


  public void UpdateTouchInput()
  {
    float wheelSpeed = 10;
    var d = Input.GetAxis("Mouse ScrollWheel");
    if (d > 0f)
    {
     lift.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, speed*d), ForceMode2D.Force);
    }
    else if(d < 0f)
    {
      lift.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, speed*d), ForceMode2D.Force);
    }
  }

  void TouchMoved(Vector3 touch)
  {
    lift.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -touch.y), ForceMode2D.Force);
  }
}