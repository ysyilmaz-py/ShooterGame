using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevolverHandler : MonoBehaviour
{
    [Header(" Effects ")]
    [SerializeField]
    private ParticleSystem _gunFireEffect;

    [Header(" Game Objects ")]
    [SerializeField]
    private Text _bulletCountText;

    [SerializeField]
    private GameObject _bulletHole;

    private int _bulletCount;
    
    [Header(" Settings ")]
    [SerializeField]
    private Transform _firePosition;

    private Camera _mainCamera;
    private Animator _animator;
 
    private int _firedBulletCount = 0;
    private RaycastHit _hit;
    private Vector3 _noAngle;
    private Quaternion _spreadAngle;
    private Vector3 _newVector;


    void Awake()
    {
        _animator = GetComponent<Animator>();     
        _mainCamera = Camera.main;
        _firedBulletCount = 0;
        PlayerPrefs.SetInt(BulletChoiceConstants.BULLETCOUNT, 1);        
    }

    /// <summary>
    /// Silah ile ateş etmeden önce bkelemeyi sağlayan fonksiyon
    /// </summary>
    public void PreFire(){
        RunShootAnimation();
        PlayGunFireEffect(_firePosition.position);

        // 1 sn sonra ateş etme fonksiyonunu başlat
        if(PlayerPrefs.GetInt(BulletChoiceConstants.LATEFIREDBULLET) == 1){
            Invoke("Fire", 1f);
        }else{
            Fire();
        }
    }
    /// <summary>
    /// Silah ile ateş etmeyi sağlayan fonksiyon
    /// </summary>
    public void Fire(){
        _bulletCount = PlayerPrefs.GetInt(BulletChoiceConstants.BULLETCOUNT);
        _firedBulletCount += _bulletCount;
        _bulletCountText.text = _firedBulletCount.ToString();

        //Tek bir ateşlemede belirlenen miktara göre mermi atmak için
        for(int i = 0 ; i < _bulletCount; i ++){
            //Rastgele -32 ile 32 derece x,y boyutlarında merminin gitmesi için
            _noAngle = _mainCamera.transform.forward;
            _spreadAngle = Quaternion.AngleAxis(8 * (i%4) * (i%2 == 0 ? 1 : -1), new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0f));
            _newVector = _spreadAngle * _noAngle;

            if(Physics.Raycast(_mainCamera.transform.position, _newVector, out _hit)){
                //Eğer temas edilen object vurulabilen bir obje mi?
                if(_hit.collider.tag == TagConstants.SHOOTABLEOBJECT){                    
                    GameObject go = Instantiate(_bulletHole, _hit.point, Quaternion.FromToRotation (Vector3.up, _hit.normal));
                    if(PlayerPrefs.GetInt(BulletChoiceConstants.REDBULLET) == 1){
                        Renderer rend = go.GetComponent<Renderer>();
                        rend.material.color = Color.red;
                    }
                    
                    if(PlayerPrefs.GetInt(BulletChoiceConstants.BIGBULLET) == 1){
                        go.transform.localScale += new Vector3(0.04f, 0f, 0.04f);
                    }
                    
                    // PlayBulletHoleEffect(go.transform.position);                    
                    PlayBulletHoleEffect(go.GetComponentInChildren <ParticleSystem> ());                    
                }
            }        
        }
    }

    /// <summary>
    /// Silah ile ateş edince oyuncu eli ve silahın animasyonunu tetikle
    /// </summary>
    void RunShootAnimation()
    {
        _animator.SetTrigger(AnimationConstants.SHOOT);        
    }

    /// <summary>
    /// Silah ile ateş edince silahın ucunda ateş efektini oynat
    /// </summary>
    /// <param name="pos">Efektin oynatılacalığı Vector3 position </param>
    void PlayGunFireEffect(Vector3 pos){        
        if(_gunFireEffect != null)
        {
            _gunFireEffect.transform.position = pos;
            _gunFireEffect.Play();
        }
    }

    /// <summary>
    /// Silah ile ateş edince merminin değdiği yerde duman efektini oynat
    /// </summary>
    /// <param name="pos">Efektin oynatılacalığı Vector3 position </param>
    void PlayBulletHoleEffect(ParticleSystem bulletHoleEffect){        
        if(bulletHoleEffect != null){
            bulletHoleEffect.Play();
        }
    }
}
