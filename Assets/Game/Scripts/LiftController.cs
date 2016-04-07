using System;
using UnityEngine;
using System.Collections;

public class LiftController : MonoBehaviour
{
  public float Speed;
  public float SpeedWheel;
  public static Action<int> OnStageStay { get; set; } 
       
  private Rigidbody2D _rigidbody2D;
  private int _currentStage;
  private bool IsCanMove = true;
    

  void Awake()
  {
     CreditsManager.Instance.GiveCurrency(10);
    _rigidbody2D = GetComponent<Rigidbody2D>();
    OnStageStay += (int stage) =>
    {
        IsCanMove = false;
        StartCoroutine(StartTimer(4, () =>
        {
            IsCanMove = true;
        }));

    };
  }

  void Update()
  {
        if(IsCanMove)
        {
            UpdateLiftPosition();
        }
    
  }

    void UpdateLiftPosition()
    {
        if (Application.isEditor)
        {
            UpdateTouchInput();
        }

        // Debug.Log(Input.touchCount);
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            touchDeltaPosition = touchDeltaPosition * Speed;
            TouchMoved(touchDeltaPosition);
        }
    }

  public void UpdateTouchInput()
  {
    var d = Input.GetAxis("Mouse ScrollWheel");
    _rigidbody2D.AddForce(new Vector2(0, SpeedWheel*d), ForceMode2D.Force);
  }

  void TouchMoved(Vector2 touch)
  {
    _rigidbody2D.AddForce(new Vector2(0, touch.y), ForceMode2D.Force);
  }

  void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.name.Contains("Stage"))
    {
      CheckStageStay(collider);
    }
  }

  private void CheckStageStay(Collider2D collider)
  {
    Debug.Log(collider.name);
    StartCoroutine(StartTimer(1, () =>
    {
      if(_rigidbody2D.IsTouching(collider))
      {
        _rigidbody2D.velocity = Vector2.zero;
        AudioManager.CreatePlayAudioObject(AudioManager.ins.sfxLiftOpen);
        var stageNumber = collider.name.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries)[1];
       _currentStage = int.Parse(stageNumber);
         if(OnStageStay != null)
            OnStageStay(_currentStage);
        Debug.Log(_currentStage);

      }
    }));
  }

  private IEnumerator StartTimer(float time, Action callback)
  {
    yield return new WaitForSeconds(time);
    callback();
  }
}