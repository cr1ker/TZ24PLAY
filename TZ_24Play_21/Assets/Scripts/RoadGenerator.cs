using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _roadPrefab;
    [SerializeField] private float _maxSpeed;
    private float _speed = 0;
    [SerializeField] private int _maxRoadCount;
    private Vector3 _lengthOfRoad;
    
    private List<GameObject> _currentRoads = new List<GameObject>();
    
    
    private void Start()
    {
        float roadLength = _roadPrefab[0].transform.localScale.z;
        _lengthOfRoad = new Vector3(0,0, roadLength);
        ResetLevel();
        StartLevel();
    }

    private void Update()
    {
        if (_speed != 0)
        {
            foreach (GameObject road in _currentRoads)
            {
                road.transform.position -= new Vector3(0,0,_speed * Time.deltaTime);
            }
        }

        if (_currentRoads[0].transform.position.z < -25)
        {
            Destroy(_currentRoads[0]);
            _currentRoads.RemoveAt(0);
            CreateNextRoad();
        }
    }

    private void CreateNextRoad()
    {
        Vector3 position = Vector3.zero;
        if (_currentRoads.Count > 0)
        {
            position = _currentRoads[_currentRoads.Count - 1].transform.position + _lengthOfRoad;
        }
        int numberOfRoadForSpawn = Random.Range(0, _roadPrefab.Length);
        GameObject newRoad = Instantiate(_roadPrefab[numberOfRoadForSpawn], position, Quaternion.identity);
        newRoad.transform.SetParent(transform);
        _currentRoads.Add(newRoad);
    }

    private void StartLevel() => _speed = _maxSpeed;

    private void ResetLevel()
    {
        _speed = 0;
        while (_currentRoads.Count > 0)
        {
            Destroy(_currentRoads[0]);
            _currentRoads.RemoveAt(0);
        }
        for (int i = 0; i < _maxRoadCount; i++)
        {
            CreateNextRoad();
        }
    }
}
