using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_Bob : MonoBehaviour
{
    public bool enable;
    public float amplitude;
    public float frequency;
    public Transform playerCamera;
    public Transform cameraHolder;

    private float toggleSpeed = 3.0f;
    Vector3 startPosition;
    private CharacterController playerController;

    private void Start() {
        playerController = GetComponent<CharacterController>();
        startPosition = playerCamera.localPosition;
    }
    private void Update() {
        if(!enable) return;
        checkMotion();
        resetPosition();
        playerCamera.LookAt(focusTarget());
    }

    private Vector3 footStepMotion(){
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude;
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;
        return pos;
    }
    private void checkMotion(){
        float speed = new Vector3(playerController.velocity.x, 0, playerController.velocity.z).magnitude;
        if(speed < toggleSpeed) return;
        playerCamera.localPosition += footStepMotion();
    }
    private void resetPosition(){
        if(playerCamera.localPosition == startPosition) return;
        cameraHolder.localPosition = Vector3.Lerp(cameraHolder.localPosition, startPosition, 1*Time.deltaTime);
    }
    private Vector3 focusTarget(){
        Vector3 pos = new Vector3(transform.position.x,
                                  transform.position.y + cameraHolder.localPosition.y,
                                  transform.position.z);
        pos += cameraHolder.forward * 15.0f;
        return pos;
    }
}
