using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.SceneManagement;


public class ControllerBehaviour : MonoBehaviour
{

  BaseBehaviour Base;

   bool canIPlace = false;

  private float timer = 0.0f;
  private float waitTime = 1.0f;

  public GameObject towers;

   private GameObject _cube;
  private MLInputController _controller;
  private const float _rotationSpeed = 30.0f;
  private const float _distance = 2.0f;
  private const float _moveSpeed = 1.2f;
  private bool _bumper = false;

  private GameObject contactedTower;

  void Start() {
    GameObject baseObject = GameObject.FindWithTag("Base");
    if (baseObject != null) Base = baseObject.GetComponent<BaseBehaviour>();

    MLInput.Start();
    MLInput.OnControllerButtonDown += OnButtonDown;
    MLInput.OnControllerButtonUp += OnButtonUp;
    _controller = MLInput.GetController(MLInput.Hand.Left);
  }

  void OnDestroy () {
    MLInput.OnControllerButtonDown -= OnButtonDown;
    MLInput.OnControllerButtonUp -= OnButtonUp;
    MLInput.Stop();
  }

  void Update() {
    if (_controller.TriggerValue > 0.2f && contactedTower != null) {
        contactedTower.transform.position = transform.position;
    }

    timer += Time.deltaTime;
    
    if (_bumper && timer > waitTime)
      {
        
          if (Base == null)
          {
            GameObject placedObject = Instantiate(towers, transform.position, transform.rotation);
          }
          else
          {
            if (Base.canPayForTower())
            {
              GameObject placedObject = Instantiate(towers, transform.position, transform.rotation);
              Base.payForTower(placedObject);
            }
        }
        // Remove the recorded 2 seconds.
        timer = 0;
      }

  }

//   void CheckControl() {
//     if (_controller.TriggerValue > 0.2f && _enabled) {
//       _bumper = false;
//       _cube.transform.Rotate(Vector3.up, - _rotationSpeed * Time.deltaTime);
//     }
//     else if (_controller.Touch1PosAndForce.z > 0.0f && _enabled) {
//       float X = _controller.Touch1PosAndForce.x;
//       float Y = _controller.Touch1PosAndForce.y;
//       Vector3 forward = Vector3.Normalize(Vector3.ProjectOnPlane(transform.forward, Vector3.up));
//       Vector3 right = Vector3.Normalize(Vector3.ProjectOnPlane(transform.right, Vector3.up));
//       Vector3 force = Vector3.Normalize((X * right) + (Y * forward));
//       _cube.transform.position += force * Time.deltaTime * _moveSpeed;
//     }
//   }

  void OnButtonDown(byte controller_id, MLInputControllerButton button) {
    if ((button == MLInputControllerButton.Bumper)) {
      _bumper = true;
    }
  }

  void OnButtonUp(byte controller_id, MLInputControllerButton button) {
    if (button == MLInputControllerButton.HomeTap) {
       Scene scene = SceneManager.GetActiveScene(); 
       SceneManager.LoadScene(scene.name);
    }
    if ((button == MLInputControllerButton.Bumper)) {
      _bumper = false;
    }
  }

  void OnTriggerEnter (Collider other){
      if (contactedTower == null && other.gameObject.CompareTag("Tower")){
        contactedTower = other.gameObject;
      }
  }

  void OnTriggerExit (Collider other){
      if (contactedTower == other.gameObject && other.gameObject.CompareTag("Tower")){
        contactedTower = null;
      }
  }

  public bool holdingTower()
  {
    return _controller.TriggerValue > 0.2f && contactedTower != null;
  }
  
}
