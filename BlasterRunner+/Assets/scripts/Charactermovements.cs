using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    CharacterController characterController;
    public float speed = 6.0f;
    public float roatationSpeed = 25;
    public float jumpSpeed = 7.5f;
    public float gravity = 20.0f;
    Vector3 inputVec;
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
        inputVec = new Vector3(x,0,z);

        animator.SetFloat("input X",z);
        animator.SetFloat("input Z",-(x));

        if(x != 0 || z != 0){
            animator.SetBool("Moving",true);
            animator.SetBool("Running",true);

        }else{
            animator.SetBool("Moving",false);
            animator.SetBool("Running",false);
        }
        
        if(characterController.isGrounded){
            moveDirection = new Vector3(Input.GetAxis("Horizontal"),0.0f,Input.GetAxis("Vertical"));
            moveDirection *= speed;
        }
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
