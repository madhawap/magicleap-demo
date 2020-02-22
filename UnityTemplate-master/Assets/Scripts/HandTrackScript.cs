using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class HandTrackScript : MonoBehaviour {
  public enum HandPoses { Ok, Finger, Thumb, OpenHand, Pinch, NoPose };
  public HandPoses pose = HandPoses.NoPose;
  public Vector3[] pos;
  public GameObject sphereThumb, sphereIndex, sphereWrist, towers, selectedGameObject;
  
  BaseBehaviour Base;
  
  bool canIPlace = false;
  bool canIGrab = false;

  private float timer = 0.0f;
  private float waitTime = 1.0f;

  private MLHandKeyPose[] _gestures;

  private void Start() {
    GameObject baseObject = GameObject.FindWithTag("Base");
    if (baseObject != null) Base = baseObject.GetComponent<BaseBehaviour>();
    
    MLHands.Start();
    _gestures = new MLHandKeyPose[5]; 
    _gestures[0] = MLHandKeyPose.Ok;
    _gestures[1] = MLHandKeyPose.Finger;
    _gestures[2] = MLHandKeyPose.OpenHand;
    _gestures[3] = MLHandKeyPose.Fist;
    _gestures[4] = MLHandKeyPose.Thumb;
    MLHands.KeyPoseManager.EnableKeyPoses(_gestures, true, false);
    pos = new Vector3[3];
  }
  private void OnDestroy() {
    MLHands.Stop();
  }

  private void OnTriggerEnter(Collider other){
    if (other.gameObject.tag == "Interactable")
      {
        if (canIGrab == false)
        {
            selectedGameObject = other.gameObject;
            canIGrab = true;
            other.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
      }
  }
  

  private void OnTriggerExit(Collider other){
    if (other.gameObject.tag == "Interactable")
      {
        canIGrab = false;
        other.gameObject.GetComponent<Renderer>().material.color = Color.white;
      }
  }

  private void Update() {

    ShowPoints(); 
    transform.position = pos[1];
    timer += Time.deltaTime;

    if (GetGesture(MLHands.Left, MLHandKeyPose.Ok)) {
      pose = HandPoses.Ok;
    }
    else if (GetGesture(MLHands.Left, MLHandKeyPose.Finger)) {
      pose = HandPoses.Finger;
    }
    else if (GetGesture(MLHands.Left, MLHandKeyPose.OpenHand)) {
      pose = HandPoses.OpenHand;
    }
    else if (GetGesture(MLHands.Left, MLHandKeyPose.Pinch)) {
      pose = HandPoses.Pinch;
      if(canIGrab)
      {
          selectedGameObject.transform.position = transform.position;
          gameObject.GetComponent<SphereCollider>().radius = 0.1f;
      }
    }
    else if (GetGesture(MLHands.Left, MLHandKeyPose.Thumb)) {
      pose = HandPoses.Thumb;
        // Check if we have reached beyond 2 seconds.
        // Subtracting two is more accurate over time than resetting to zero.
      if (timer > waitTime)
      {
        if (canIPlace == true){
          if (Base == null)
          {
            GameObject placedObject = Instantiate(towers, pos[0], transform.rotation);
            canIPlace = false;
          }
          else
          {
            if (Base.canPayForTower())
            {
              GameObject placedObject = Instantiate(towers, pos[0], transform.rotation);
              Base.payForTower(placedObject);
            }
          }


        }
        // Remove the recorded 2 seconds.
        timer = timer - waitTime;
      }
      
    }
    else {
      pose = HandPoses.NoPose;
      canIPlace = true;
      if (selectedGameObject && canIGrab == false)
      {
            selectedGameObject = null;
            gameObject.GetComponent<SphereCollider>().radius = 0.01f;
      }
    }
    // ShowPoints(); 
  }

  private void ShowPoints() {
    // Left Hand Thumb tip
    pos[0] = MLHands.Left.Thumb.KeyPoints[0].Position;
    // Left Hand Index finger tip 
    pos[1] = MLHands.Left.Index.KeyPoints[0].Position;
    // Left Hand Wrist 
    pos[2] = MLHands.Left.Wrist.KeyPoints[0].Position; 
    // sphereThumb.transform.position = pos[0];
    // sphereIndex.transform.position = pos[1];
    // sphereWrist.transform.position = pos[2];
  }

  private bool GetGesture(MLHand hand, MLHandKeyPose type) {
    if (hand != null) {				
      if (hand.KeyPose == type) {
        if (hand.KeyPoseConfidence > 0.9f) {                       
          return true;
        }
      }
    }
    return false;
  }

}

