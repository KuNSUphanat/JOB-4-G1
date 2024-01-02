using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterMovement : MonoBehaviour
{
    Animator animator;
    CharacterController characterController;
    public float speed = 6.0f;
    public float roarationSpeed = 25.0f;
    public float jumpSpeed = 7.5f;
    public float gravity = 20.0f;
    Vector3 InputVec;
    Vector3 targetDirection;
    private Vector3 moveDirection = Vector3.zero;



    void Start()
    {
        Time.timeScale = 1;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        float x = -(Input.GetAxisRaw("Vertical"));
        float z = Input.GetAxisRaw("Horizontal");
        InputVec = new Vector3(x,0,z);

        animator.SetFloat("input X",z);
        animator.SetFloat("input Z",-(x));

        if(x != 0 || z != 0){
            animator.SetBool("Moving",true);
            animator.SetBool("Running",true);
        }else{
            animator.SetBool("Moving",false);
            animator.SetBool("Running",false);
        }

        //jump
        if(characterController.isGrounded){
            moveDirection= new Vector3(Input.GetAxis("Horizontal"),0.0f,Input.GetAxis("Vertical"));
            moveDirection *= speed;
        }
        characterController.Move(moveDirection * Time.deltaTime);
        UpdateMovement();
    }
    void UpdateMovement(){
        Vector3 motion = InputVec;
        motion *= (Mathf.Abs(InputVec.x) == 1 && Mathf.Abs(InputVec.z) ==1)?.7f:1;
        RotateTowardMovementDirection();
        getCameraRealtive();
    }
    void RotateTowardMovementDirection(){
        if(InputVec != Vector3.zero){
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(targetDirection),Time.deltaTime * roarationSpeed);
        }
    }
    void getCameraRealtive(){
        Transform cameraTransform = Camera.main.transform;
        Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z,0,-forward.x);
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        targetDirection = (h* right) + (v * forward);


    
    }
}
