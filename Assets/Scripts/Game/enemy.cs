using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    [SerializeField] private float _speed = 6;
    [SerializeField] private GameObject _explosionPrefab = default;
    private UIManager _UIManager = default;

    [SerializeField] private float _delai = 1f;
    [SerializeField] private GameObject _canonPrefab;
    private float _canFire = -1;
    private float _deuxVie = 2;
    private float _vie = 1;

    // Start is called before the first frame update
    void Start()
    {
        _UIManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.tag == "tank")
        {
            transform.Translate(Vector3.down * Time.deltaTime * _speed);
            Tir();
            if (transform.position.y < -5f)
            {
                float randomX = Random.Range(-8.3f, 8.3f);
                transform.position = new Vector3(randomX, 7f, 0f);
            }
        }
        else if (this.tag == "armored")
        {
            transform.Translate(Vector3.down * Time.deltaTime * _speed);
            if (transform.position.y < -5f)
            {
                float randomX = Random.Range(-8.3f, 8.3f);
                transform.position = new Vector3(randomX, 7f, 0f);
            }
        }
        else if (this.tag == "humvee")
        {
            transform.Translate(Vector3.down * Time.deltaTime * _speed);
            if (transform.position.y < -5f)
            {
                float randomX = Random.Range(-8.3f, 8.3f);
                transform.position = new Vector3(randomX, 7f, 0f);
            }
        }
        else if (this.tag == "soldier")
        {
            transform.Translate(Vector3.down * Time.deltaTime * _speed);
            if (transform.position.y < -5f)
            {
                float randomX = Random.Range(-8.3f, 8.3f);
                transform.position = new Vector3(randomX, 7f, 0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            player.Dommage();
            Destroy(this.gameObject);
        }
        else if (other.tag == "Tir")
        {
            if (_vie > 0 && (this.tag == "humvee" || this.tag == "soldier"))
            {
                _vie--;
                if (_vie == 0)
                {
                    Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                    _UIManager.AjouterScore(100);
                    Destroy(this.gameObject);
                    Destroy(other.gameObject);
                }
            }
            else if (_deuxVie > 0 && (this.tag == "armored" || this.tag == "tank"))
            {
                _deuxVie--;
                Destroy(other.gameObject);
                if (_deuxVie == 0)
                {
                    Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                    _UIManager.AjouterScore(100);
                    Destroy(this.gameObject);
                }
            }
        }
        else if (other.tag == "missile")
        {
            _vie--;
            if (_vie == 0)
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _UIManager.AjouterScore(100);
                Destroy(this.gameObject);
            }
        }
    }

    private void Tir()
    {
        if (Time.time > _canFire)
        {
            _delai = Random.Range(1f, 3f);
            _canFire = Time.time + _delai;
            Instantiate(_canonPrefab, (transform.position), Quaternion.identity);
        }
    }
}
