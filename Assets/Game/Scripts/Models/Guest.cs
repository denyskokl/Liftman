using DG.Tweening;
using UnityEngine;

public class Guest : MonoBehaviour
{
  public int StageNumber { get; set; }
  public int Destination { get; set; }
  public float LifeTime { get; set; }

  private float lifeDuration;

  public Guest(int stageNumber, int destination, float lifeTime)
  {
    StageNumber = stageNumber;
    Destination = destination;
    LifeTime = lifeTime;
  }

  void Start()
  {
    MoveGuest();
  }



    public void MoveIn()
    {
      transform.DOMoveX(2, 2f);
    }

     // Temporal coordinates Fix it
    public void MoveOut()
    {
       transform.DOMoveX(-2, 2f);
    }


  public void MoveGuest()
  {
    ChageFlip(true);
    transform.DOMoveX(-0.11f, 6f, false).OnComplete(StopMoving);
  }

  private void StopMoving()
  {
    Invoke("MoveBack", 1f);
  }


  public void MoveBack()
  {
    ChageFlip(false);
    transform.DOMoveX(0.9f, 6f, false).OnComplete(Destroy);
  }


  public void Destroy()
  {
    GameplayController.RemoveGuest(gameObject);
    Destroy(gameObject);
  }

  private void ChageFlip(bool status)
  {
    transform.GetComponent<SpriteRenderer>().flipX = status;
  }
}