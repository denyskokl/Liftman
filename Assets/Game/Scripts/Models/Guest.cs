
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


    private void MoveGuest()
    {
        ChageFlip(true);
        transform.DOMoveX(-7.8f, 6f, false).OnComplete(StopMoving);
    }

    private void StopMoving()
    {
        Invoke("MoveBack", 1f);
    }



    private void MoveBack()
    {
        ChageFlip(false);
        transform.DOMoveX(-2f, 6f, false).OnComplete(Destroy);
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
