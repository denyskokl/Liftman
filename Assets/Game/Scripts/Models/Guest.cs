using DG.Tweening;
using UnityEngine;

public class Guest : MonoBehaviour
{
  public int StageNumber { get; set; }
  public int Destination { get; set; }
  public float LifeTime { get; set; }

  public bool IsClaimed;
  private float lifeDuration;
  private Rigidbody2D _rigidbody;
  private GameObject _lift;
  private GameplayController _gamePlayController;

  public Guest(int stageNumber, int destination, float lifeTime)
  {
    StageNumber = stageNumber;
    Destination = destination;
    LifeTime = lifeTime;
  }

  void Awake()
  {
    _rigidbody = GetComponent<Rigidbody2D>();
    _lift = FindObjectOfType<LiftController>().gameObject;
    _gamePlayController = FindObjectOfType<GameplayController>();
  }

  void Start()
  {
    MoveGuest();
  }


  // Temporal coordinates. Fix it
  public void MoveIn()
  {
    transform.SetParent(_lift.transform);
    _rigidbody.isKinematic = true;
    GetComponent<Animator>().Play("Moving");
    transform.DOMoveX(-0.3f, 2f).OnComplete(StopAnimation);
    IsClaimed = true;
  }

  // Temporal coordinates Fix it
  public void MoveOut()
  {
    transform.SetParent(_gamePlayController.gameObject.transform);
    GetComponent<SpriteRenderer>().flipX = false;
    GetComponent<Animator>().Play("Moving");
    transform.DOMoveX(0.6f, 3).OnComplete(StopAnimation).OnComplete(Destroy);
  }

  private void MoveGuest()
  {
    GetComponent<Animator>().Play("Moving");
    transform.DOLocalMoveX(-0.1f, 3).OnComplete(StopAnimation);
  }


  private void StopAnimation()
  {
    GetComponent<Animator>().Play("Idle");
  }

  public void Destroy()
  {
    GameplayController.RemoveGuest(gameObject);
    Destroy(gameObject);
  }
}