using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Look : MonoBehaviour
{
    [Header(" Menu Elements ")]
    [SerializeField]
    private GameObject _infoMenu;
    [SerializeField]
    private Button _btnStart;

    [Header(" Game Objects ")]
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform look;

    [Header(" Settings ")]
    [SerializeField]
    private float sensitivity = 5f;
    
    [SerializeField]
    private Vector2 lookLimits = new Vector2(-70f, 80f);
    
    private Vector2 lookAngles;
    
    private Vector2 currentLooks;
    
    private Vector2 move;

    private float rollAngle;

    private int lookFrame;

    void Start()
    {        
        SetBindings();
    }

    void Update()
    {
        //Oyun oynamak için CursorLockMode.Locked olmalı!!!!!!
        if(Cursor.lockState == CursorLockMode.Locked) {
            LookAround();
        }
    }   


    /// <summary>
    /// Oyuncunun mouse ile sağa sola ve yukarı aşağı bakmasını sağlayan fonksiyon
    /// </summary>
    void LookAround(){
        currentLooks = new Vector2(Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X));

        lookAngles.x += currentLooks.x * sensitivity * -1;
        lookAngles.y += currentLooks.y * sensitivity;
        lookAngles.x = Mathf.Clamp(lookAngles.x, lookLimits.x, lookLimits.y);

        look.localRotation = Quaternion.Euler(lookAngles.x, 0f, 0f);
        player.localRotation = Quaternion.Euler(0f, lookAngles.y, 0f);
    }

    /// <summary>
    /// Oyun başlangında gösterilen message menüsünde ki başla buttonunu event listener set eden fonksiyon
    /// </summary>
    void SetBindings()
    {
        _btnStart.onClick.AddListener(() => StartGame());
    }

    /// <summary>
    /// Oyun başlangında gösterilen message menüsünde oyunu başlata tıkladık
    /// </summary>
    void StartGame(){
        _infoMenu.SetActive(false);

        //Oyun oynamak için CursorLockMode.Locked olmalı!!!!!!
        Cursor.lockState = CursorLockMode.Locked;   
    }
}
