using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    private float _positionInitX;
    private float _positionInitY;
    private float _positionFinalX;
    private float _positionFinalY;
    private float _x;
    private float _y;
    private float _vFinal;
    private float _angle;
    Vector3 angle;
    // Start is called before the first frame update
    void Start()
    {
        _positionInitX = transform.position.x;
        _positionInitY = transform.position.y;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _positionFinalX = worldPosition.x;
        _positionFinalY = worldPosition.y;
        _x = _positionFinalX - _positionInitX;
        _y = _positionFinalY - _positionInitY;
        _vFinal = Mathf.Sqrt(Mathf.Pow(_x, 2) + Mathf.Pow(_y, 2));
        _angle = Mathf.Asin(_y / _vFinal) * Mathf.Rad2Deg;
        angle = new Vector3(0f, 0f, _angle);
        if (_x >= 0 && _y >= 0)
        {
            transform.Rotate(angle - new Vector3(0f, 0f, 90f));
        }
        else if (_x >= 0 && _y < 0)
        {
            transform.Rotate(angle + new Vector3(0f, 0f, 270f));
        }
        else if (_x < 0 && _y >= 0)
        {
            transform.Rotate(new Vector3(0f,0f,90f) - angle);
        }
        else if (_x < 0 && _y < 0)
        {
            transform.Rotate(new Vector3(0f, 0f, 90f) - angle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(this.gameObject);
        }
    }
}
