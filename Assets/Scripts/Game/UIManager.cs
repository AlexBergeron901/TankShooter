using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    private int _score = 0;
    [SerializeField] private TextMeshProUGUI _txtScore = default;
    [SerializeField] private Image _livesDisplayImage = default;
    [SerializeField] private Sprite[] _liveSprites = default;
    [SerializeField] private GameObject _pausePanel = default;
    [SerializeField] private TextMeshProUGUI _txtTime = default;
    private bool _pauseOn = false;
    private float _timeDebut;
    private float _timeFin;

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        ChangeLivesDisplayImage(3);
        UpdateScore();
        _timeDebut = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_pauseOn)  {
            _pausePanel.SetActive(true);
            Time.timeScale = 0;
            _pauseOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _pauseOn) {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
            _pauseOn = false;
        }
            _timeFin = Time.time;
            _txtTime.text = "Durée de la partie : " + (_timeFin - _timeDebut).ToString("F2");
    }

    public void AjouterScore(int points)
    {
        _score += points;
        UpdateScore();
    }

    private void UpdateScore()
    {
        _txtScore.text = "Score : " + _score.ToString();
    }

    public void ChangeLivesDisplayImage(int noImage)
    {
        if (noImage < 0)
        {
            noImage = 0;
        }
        _livesDisplayImage.sprite = _liveSprites[noImage];
    }

    public void ResumeGame()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
        _pauseOn = false;
    }

    public void Quitter()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    
    public int GetScore()
    {
        return _score;
    }
}
