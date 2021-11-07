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
    public GameObject Bonfire;
    public float slopeForce;
    public float slopeForceRayLength;

    private float gravity;
    private Vector3 direction;
    private bool isGrounded;
    private bool center = false;

    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        direction = transform.right * x + transform.forward * z;
        if(x != 0 && z != 0) direction /= 1.5f;
        // gravity
        groundCheck();
        gravity -= 9.81f * Time.deltaTime;
        if(isGrounded)
            gravity = 0;
        direction = new Vector3(direction.x, gravity, direction.z);

        if((x != 0 || z != 0) && onSlope()){
            controller.Move(Vector3.down * controller.height/2 * slopeForce * Time.deltaTime);
            Debug.Log("Working");
        }

        Vector3 move = direction;
        controller.Move(move * speed * Time.deltaTime);

        teleport();
        //if(teleport()){
            //Debug.Log("Teleported!");
            //if teleported do...
            //Instantiate(Bonfire, transform.position, Quaternion.identity);  //just an example
        //}
    }
    private void groundCheck(){
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
    public bool onSlope(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, (controller.height/2) * slopeForceRayLength)){
            if(hit.normal != Vector3.up) return true;
        }
        return false;
    }

    //currently i am still trying to figure out how to use collision 
    //to implement the teleportation
    private void OnTriggerEnter(Collider other){
        
        if(other.CompareTag("Marker") && center == false){
            Debug.Log("collider");
            //Instantiate(Bonfire, other.transform.position + other.transform.up * 23, Quaternion.identity);
            center = true;
        }

        if(other.CompareTag("Bonfire") && center == true){
            Debug.Log("bonfire lit");
            center = false;
            //Destroy(GameObject.FindWithTag("Bonfire"));
        }

    }

    //separate function for teleporting
    //once you near this side, you go to the opposite side. c.x
    public void teleport(){
        if(transform.position.x > East.transform.position.x){
            transform.position = new Vector3((transform.position.x - East.transform.position.x) + West.transform.position.x, 
                                             transform.position.y, 
                                             transform.position.z);
           // return true;
        }
        if(transform.position.x < West.transform.position.x){
            transform.position = new Vector3((transform.position.x - West.transform.position.x) + East.transform.position.x, 
                                             transform.position.y, 
                                             transform.position.z);
           // return true;
        }
        if(transform.position.z < South.transform.position.z ){
            transform.position = new Vector3(transform.position.x, transform.position.y, 
                                            (transform.position.z - South.transform.position.z) + North.transform.position.z);
            //return true;
        }
        if(transform.position.z > North.transform.position.z){
            transform.position = new Vector3(transform.position.x, transform.position.y, 
                                            (transform.position.z - North.transform.position.z) + South.transform.position.z);
           // return true;
        }
       // return false;
    }

}
