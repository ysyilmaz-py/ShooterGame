using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [Header(" Menu Elements ")]

    [SerializeField]
    private GameObject _bulletMenu;

    [SerializeField]
    private Toggle _bigBullet;

    [SerializeField]
    private Toggle _lateFiredBullet;

    [SerializeField]
    private Toggle _redBullet;

    [SerializeField]
    private InputField _bulletCount;

    void Start(){
        //initial değerler
        _bulletMenu.SetActive(false);
        PlayerPrefs.SetInt(BulletChoiceConstants.BIGBULLET, 0);
        PlayerPrefs.SetInt(BulletChoiceConstants.LATEFIREDBULLET, 0);
        PlayerPrefs.SetInt(BulletChoiceConstants.REDBULLET, 0);
        _bulletCount.text = "1";
        SetBindings();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B)) {
            if(Cursor.lockState == CursorLockMode.Locked) {
                _bulletMenu.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            } else {
                _bulletMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

        }
    }

    /// <summary>
    /// Mermi menüsündeki ui element lerinin eventlerini dinlenilmesini set eden fonksiyon
    /// </summary>
    private void SetBindings()
    {
        _bigBullet.onValueChanged.AddListener(delegate {SetBulletChoice(BulletChoiceConstants.BIGBULLET, _bigBullet.isOn);});
        _lateFiredBullet.onValueChanged.AddListener(delegate {SetBulletChoice(BulletChoiceConstants.LATEFIREDBULLET, _lateFiredBullet.isOn);});
        _redBullet.onValueChanged.AddListener(delegate {SetBulletChoice(BulletChoiceConstants.REDBULLET, _redBullet.isOn);});
        _bulletCount.onEndEdit.AddListener(delegate {SetBulletCount();});
    }

    /// <summary>
    /// Silah ateş edince mermi tipini set eden fonksiyon
    /// </summary>
    private void SetBulletChoice(string key, bool value){
        PlayerPrefs.SetInt(key, (value == true ? 1 : 0));
    }

    /// <summary>
    /// Silah ateş edince ateş edilecek mermi sayısını set eden fonksiyon
    /// </summary>
    private void SetBulletCount(){
        int count = 1;
        if (_bulletCount.text.Length > 0){
            if (!int.TryParse(Regex.Replace(_bulletCount.text, @"[^\d]", ""), out count))
            {
                count = 1;
            }
            _bulletCount.text = count.ToString();
		}
		
        PlayerPrefs.SetInt(BulletChoiceConstants.BULLETCOUNT, count);
    }
}
