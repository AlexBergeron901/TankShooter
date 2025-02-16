using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{

    [SerializeField] private float _speed = 3.0f;
    // _powerUpID  0=vieBonus   1=pointBonus    2=immortalite    3=nuke
    [SerializeField] private int _powerUpID = default;
    [SerializeField] private AudioClip _powerUpSound = default;
    UIManager _UIManager;
    SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _UIManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();
        _spawnManager = FindObjectOfType<SpawnManager>().GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y <= -5.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            Destroy(this.gameObject);
            AudioSource.PlayClipAtPoint(_powerUpSound, Camera.main.transform.position, 0.15f);
            if (player != null)
            {
                switch (_powerUpID)
                {
                    case 0:
                        player.vieBonus();
                        break;
                    case 1:
                        _UIManager.AjouterScore(500);
                        break;
                    case 2:
                        player.immortalite();
                        break;
                    case 3:
                        _spawnManager.nuke();
                        break;
                }
            }

        }
    }
}
