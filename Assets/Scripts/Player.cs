using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransForm = null;
    [SerializeField] private LayerMask playerMask;


    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidBodyComponent;
    private int superJumpsRemaining;
    // private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // make the player jump
        if(Input.GetKeyDown(KeyCode.Space)){
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");

    }
    // Fixed update is called once every physics update
    private void FixedUpdate() {
        // if(!isGrounded){
        //     return;
        // }
        rigidBodyComponent.velocity = new Vector3(horizontalInput, rigidBodyComponent.velocity.y, 0);

        if(Physics.OverlapSphere(groundCheckTransForm.position, 0.1f, playerMask).Length == 0){
            return;
        }
        if(jumpKeyWasPressed == true){
            float jumpPower = 6f;
            if(superJumpsRemaining > 0){
                jumpPower *= 2;
                superJumpsRemaining--;
            }
            Debug.Log("Key Pressed");
            rigidBodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed=false;
        }

    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 10){
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }

    // private void OnCollisionEnter(Collision collision) {
    //     isGrounded = true;
    // }
    // private void OnCollisionExit(Collision collision) {
    //     isGrounded=false;
    // }
}
