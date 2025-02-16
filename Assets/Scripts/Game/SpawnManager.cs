using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _tankEnemyPrefab = default;
    [SerializeField] private GameObject _humveeEnemyPrefab = default;
    [SerializeField] private GameObject _soldierEnemyPrefab = default;
    [SerializeField] private GameObject _armoredPrefab = default;
    [SerializeField] private GameObject _container = default;
    [SerializeField] private GameObject[] _powerUpPrefab = default;
    [SerializeField] private GameObject _explosionPrefab = default;
    private bool _stopSpawning = false;
    private UIManager _UIManager;

    private void Awake()
    {
        _UIManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RoutineSpawn());
        StartCoroutine(RoutinePowerUp());
    }

    IEnumerator RoutineSpawn()
    {
        yield return new WaitForSeconds(2f);
        while (!_stopSpawning)
        {
            if (_container.transform.childCount < 10)
            {
                int i = _UIManager.GetScore();
                if (i > 5000)
                {
                    Vector3 positionSpawn = new Vector3(Random.Range(-8.3f, 8.3f), 7f, 0f);
                    GameObject newEnemy = Instantiate(_tankEnemyPrefab, positionSpawn, Quaternion.identity);
                    newEnemy.transform.parent = _container.transform;
                    yield return new WaitForSeconds(1.7f);
                }
                else if (i > 2500)
                {
                    Vector3 positionSpawn = new Vector3(Random.Range(-8.3f, 8.3f), 7f, 0f);
                    GameObject newEnemy = Instantiate(_armoredPrefab, positionSpawn, Quaternion.identity);
                    newEnemy.transform.parent = _container.transform;
                    yield return new WaitForSeconds(1.7f);
                }
                else if (i > 1000)
                {
                    Vector3 positionSpawn = new Vector3(Random.Range(-8.3f, 8.3f), 7f, 0f);
                    GameObject newEnemy = Instantiate(_humveeEnemyPrefab, positionSpawn, Quaternion.identity);
                    newEnemy.transform.parent = _container.transform;
                    yield return new WaitForSeconds(1.3f);
                }
                else
                {
                    Vector3 positionSpawn = new Vector3(Random.Range(-8.3f, 8.3f), 7f, 0f);
                    GameObject newEnemy = Instantiate(_soldierEnemyPrefab, positionSpawn, Quaternion.identity);
                    newEnemy.transform.parent = _container.transform;
                    yield return new WaitForSeconds(1f);
                }
            }
            else
            {
                yield return new WaitForSeconds(5f);
            }
        }
        
    }

    IEnumerator RoutinePowerUp()
    {
        yield return new WaitForSeconds(5f);
        while (!_stopSpawning)
        {
            Vector3 positionSpawn = new Vector3(Random.Range(-8.3f, 8.3f), 7f, 0f);
            int randomPU = Random.Range(0, _powerUpPrefab.Length);
            GameObject newPU = Instantiate(_powerUpPrefab[randomPU], positionSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(7f, 15f));
        }
    }

    public void ChangerBooleen()
    {
        _stopSpawning = true;
    }

    public void nuke() 
    {
        var allChild = _container.GetComponentsInChildren<Transform>();
        for (int i = 1; i < allChild.Length; i++)
        {
            Instantiate(_explosionPrefab, allChild[i].transform.position, Quaternion.identity);
            _UIManager.AjouterScore(100);
            Destroy(allChild[i].gameObject);
        }
    }
}
