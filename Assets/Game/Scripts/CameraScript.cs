using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
  [SerializeField] private GameObject target;



  void Update()
  {
      if (target.transform.position.y > 0 && target.transform.position.y < 3.69f)
      {
        transform.position = new Vector3(transform.position.x, target.transform.position.y, 0);
      }
  }
}