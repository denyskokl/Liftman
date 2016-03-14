using UnityEngine;
using DG.Tweening;

public class GameplayController : MonoBehaviour
{
  [SerializeField] private GameObject lift;
  [SerializeField] private Collider2D touchArea;
  private Vector3 MouseLastPosition;
  [SerializeField] private GameObject _gues;


  void Start()
  {
    var gues = Instantiate(_gues);
    gues.transform.SetParent(gameObject.transform);
    gues.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
  }
}