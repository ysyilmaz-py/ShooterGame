using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField]
    private RevolverHandler _revolver;

    void Update()
    {
        ShootRevolver();    
    }

    /// <summary>
    /// Mouse sol tuşa basılınca silah ateş etme fonksiyonlarını çağıran fonksiyon
    /// </summary>
    void ShootRevolver(){

        if(Input.GetMouseButtonDown(0)){
            if(Cursor.lockState == CursorLockMode.Locked) {
                _revolver.PreFire();
            }
        }
    }
}