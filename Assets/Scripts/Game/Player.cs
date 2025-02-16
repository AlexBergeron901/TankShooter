using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _delaiCanon = 0.5f;
    [SerializeField] private float _delaiMissile = 0.5f;
    [SerializeField] private GameObject _missilePrefab;
    [SerializeField] private int _vies = 3;
    [SerializeField] private AudioClip _clip = default;
    [SerializeField] private GameObject _canonBallPrefab = default;

    private SpawnManager _spawnManager;
    private UIManager _UIManager = default;

    private float _canFire = -1;
    private float _canFireCanon = -1;
    private bool _immortalite = false;
    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>().GetComponent<SpawnManager>();
        _UIManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Tir();
        TirCanon();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0f);
        transform.Translate(direction * Time.deltaTime * _speed);
        
        //Gérer limite haut bas
        transform.position = new Vector3(transform.position.x,
        Mathf.Clamp(transform.position.y, -4f, 0f), 0f);

        //Gérer tp
        if (transform.position.x >= 8.5)
        {
            transform.position = new Vector3(-8.5f, transform.position.y, 0f);
        }
        else if (transform.position.x <= -8.5)
        {
            transform.position = new Vector3(8.5f, transform.position.y, 0f);
        }
    }

    private void Tir()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _delaiMissile;
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 0.04f);
            Instantiate(_missilePrefab, (transform.position), Quaternion.identity);
        }
    }

    private void TirCanon()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > _canFireCanon)
        {
            _canFireCanon = Time.time + _delaiCanon;
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 0.04f);
            Instantiate(_canonBallPrefab, (transform.position), Quaternion.identity);
        }
    }

    IEnumerator coroutineImmortalite()
    {
        yield return new WaitForSeconds(7f);
        _immortalite = false;
    }

    //   methode publique

    public void Dommage()
    {
        if (!_immortalite)
        {
            _vies--;
            _UIManager.ChangeLivesDisplayImage(_vies);
            if (_vies < 1)
            {
                _spawnManager.ChangerBooleen();
                Destroy(this.gameObject);
                PlayerPrefs.SetInt("score", _UIManager.GetScore());
                PlayerPrefs.Save();
                SceneManager.LoadScene(2);
            }
        }
    }

    public int getVies()
    {
        return _vies;
    }

    public void vieBonus()
    {
        if (_vies < 3 && _vies > 0 )
        {
            _vies++;
            _UIManager.ChangeLivesDisplayImage(_vies);
        }
    }

    public void immortalite()
    {
        _immortalite = true;
        StartCoroutine(coroutineImmortalite());
    }
}
