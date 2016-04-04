using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    GameObject _enemyPrefab;
    public int _spawnNumber = 3;
    public float _spawnDelay = 2.5f;
    public float _expulsionPower;
    private bool _spawnEnemies = true;

    void Start () {
        _enemyPrefab = Resources.Load<GameObject>("Prefabs/Entity/Enemy");
        StartCoroutine(Spawner());
	}

    private IEnumerator Spawner()
    {
        while(_spawnEnemies)
        {
            for (int i = 0; i < _spawnNumber; i++)
            {
                GameObject go = Instantiate(_enemyPrefab, transform.position + new Vector3(Random.Range(-1.0f, 1.0f), 3.0f, Random.Range(-1.0f, 1.0f)), Quaternion.identity) as GameObject;
                go.GetComponent<Rigidbody>().AddForce(transform.up * _expulsionPower);
            }
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
