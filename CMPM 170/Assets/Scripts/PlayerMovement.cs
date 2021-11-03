using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 20f;

    public float distToGround;

    public GameObject North;
    public GameObject South;
    public GameObject East;
    public GameObject West;

    private float gravity;
    private Vector3 direction;
    private bool isGrounded;

    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        direction = transform.right * x + transform.forward * z;
        // gravity
        groundCheck();
        gravity -= 9.81f * Time.deltaTime;
        if(isGrounded)
            gravity = 0;
        direction = new Vector3(direction.x, gravity, direction.z);

        Vector3 move = direction;
        controller.Move(move * speed * Time.deltaTime);
        teleport();
    }
    private void groundCheck(){
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    //currently i am still trying to figure out how to use collision 
    //to implement the teleportation
    private void OnTriggerEnter(Collider other){
        
    }

    //separate function for teleporting
    //once you near this side, you go to the opposite side. c.x
    void teleport(){
        if(transform.position.x > East.transform.position.x){
            transform.position = new Vector3(West.transform.position.x, transform.position.y, transform.position.z);
        }
        if(transform.position.x < West.transform.position.x){
            transform.position = new Vector3(East.transform.position.x, transform.position.y, transform.position.z);
        }
        if(transform.position.z < South.transform.position.z){
            transform.position = new Vector3(transform.position.x, transform.position.y, North.transform.position.z);
        }
        if(transform.position.z > North.transform.position.z){
            transform.position = new Vector3(transform.position.x, transform.position.y, South.transform.position.z);
        }
    }

}
