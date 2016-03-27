using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Guest : MonoBehaviour
{
  public int StageNumber { get; set; }
  public int Destination { get; set; }
  public float LifeTime { get; set; }

  public bool IsClaimed;
  private float lifeDuration;
    private Rigidbody2D _rigidbody;  

  public Guest(int stageNumber, int destination, float lifeTime)
  {
    StageNumber = stageNumber;
    Destination = destination;
    LifeTime = lifeTime;
  }

  void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }  

  void Start()
  {
    MoveGuest();
  }


  // Temporal coordinates. Fix it
  public void MoveIn()
  {
    transform.DOMoveX(2, 2f);
    IsClaimed = true;
  }

  // Temporal coordinates Fix it
  public void MoveOut()
  {
    transform.DOMoveX(-2, 2f);
  }

  private void MoveGuest()
  {
        _rigidbody.AddForce(new Vector2(-4, 0), ForceMode2D.Force);  
  }

  private void StopMoving()
  {
    Invoke("MoveBack", 1f);
  }


  private void MoveBack()
  {
    transform.rotation = Quaternion.Euler(Vector3.back);
    transform.DOLocalMoveX(160f, 8f).OnComplete(Destroy);
  }


  public void Destroy()
  {
//    GameplayController.RemoveGuest(gameObject);
    Destroy(gameObject);
  }
}