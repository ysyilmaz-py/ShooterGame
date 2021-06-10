using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDirection;

    [Header(" Settings ")]
    [SerializeField]
    private float speed = 5;

    void Awake(){
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Oyun oynamak için CursorLockMode.Locked olmalı!!!!!!
        if(Cursor.lockState == CursorLockMode.Locked) {
            MovePlayer();
        }
    }
    
    /// <summary>
    /// Oyuncunun W,A,S,D ve yön tuşları ile ilerlemesini sağlayan fonksiyon
    /// </summary>
    void MovePlayer(){
        moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed * Time.deltaTime;

        characterController.Move(moveDirection);
    }
}
