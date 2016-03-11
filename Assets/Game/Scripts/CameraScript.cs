using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
  [SerializeField] private GameObject target;
  [SerializeField] private Collider2D touchArea;

  public bool isNeedMove = false;

  [SerializeField] private Collider2D _bottomTrigger;
  [SerializeField] private Collider2D _topTrigger;
  public float xOffset = 1.45f;
  public float yOffset = 1.9f;
  public float zOffset = 0;


  void LateUpdate()
  {
    if (isNeedMove)
    {
      this.transform.position = new Vector3(target.transform.position.x + xOffset, target.transform.position.y + yOffset,
        target.transform.position.z + zOffset);
      touchArea.transform.position = new Vector3(target.transform.position.x + xOffset,
        target.transform.position.y + yOffset,
        target.transform.position.z + zOffset);
    }
  }


  void OnTriggerEnter(Collider collider)
  {
    Debug.Log(collider.gameObject.name);
    if (collider.gameObject.name == _bottomTrigger.name) isNeedMove = true;
    if (collider.gameObject.name == _topTrigger.name) isNeedMove = false;
  }
}