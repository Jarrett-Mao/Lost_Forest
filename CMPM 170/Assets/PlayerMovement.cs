using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Start is called before the first frame update
    int x_limit = 50;
    int z_limit = 50;
    public CharacterController controller;
    public float speed = 20f; 

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        teleport();
    }

    //currently i am still trying to figure out how to use collision 
    //to implement the teleportation
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Cube")){ 
            Debug.Log("LOLLOLOLLOL");
        }
    }

    //separate function for teleporting
    //once you near this side, you go to the opposite side. c.x
    void teleport(){
        if(transform.position.x > 50){
            transform.position = new Vector3(-1 * x_limit, transform.position.y, transform.position.z);
        }
        if(transform.position.x < -50){
            transform.position = new Vector3(x_limit, transform.position.y, transform.position.z);
        }
        if(transform.position.z < -50){
            transform.position = new Vector3(transform.position.x, transform.position.y, z_limit);
        }
        if(transform.position.z > 50){
            transform.position = new Vector3(transform.position.x, transform.position.y, -1 * z_limit);
        }
    }

}
