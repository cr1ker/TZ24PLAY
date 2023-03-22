using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [Header("Road Settings")]
    [SerializeField] private GameObject[] _roadPrefab;
    [SerializeField] private GameObject _startRoad;
    [SerializeField] private float _speedOfRoad;
    [SerializeField] private int _maxRoadCount;
    [Header("UI and Effects")] 
    [SerializeField] private GameObject _textForStartLevel;
    [SerializeField] private GameObject _warpEffect;
    [SerializeField] private GameObject _joystick;
    private float _currentSpeedOfRoad = 0;
    private Vector3 _lengthOfRoad;
    private GameObject _newRoad;
    private bool _isGameRunning = false;
    private bool _isLevelCreationStart;
    private List<GameObject> _currentRoads = new List<GameObject>();
    
    public void StartLevel()
    {
        _currentSpeedOfRoad = _speedOfRoad;
        _isGameRunning = true;
        _textForStartLevel.SetActive(false);
        _warpEffect.SetActive(true);
    }

    public void FinishLevel()
    {
        _currentSpeedOfRoad = 0;
        _warpEffect.SetActive(false);
        _joystick.SetActive(false);
        _isGameRunning = false;
    }
    
    private void Start()
    {
        float roadLength = _roadPrefab[0].transform.localScale.z;
        _lengthOfRoad = new Vector3(0,0, roadLength);
        ResetLevel();
    }

    private void Update()
    {
        if (_isGameRunning)
        {
            foreach (GameObject road in _currentRoads)
            {
                road.transform.position -= new Vector3(0,0,_currentSpeedOfRoad * Time.deltaTime);
            }
            if (_currentRoads[0].transform.position.z < -_currentRoads[0].transform.localScale.z)
            {
                RemoveLastRoad();
                CreateNextRoad();
            }
        }
    }

    private void RemoveLastRoad()
    {
        Destroy(_currentRoads[0]);
        _currentRoads.RemoveAt(0);
    }
    
    private void CreateNextRoad()
    {
        Vector3 position = Vector3.zero;
        if (_currentRoads.Count > 0)
        {
            position = _currentRoads[^1].transform.position + _lengthOfRoad;
        }
        int numberOfRoadForSpawn = Random.Range(0, _roadPrefab.Length);
        if (_isLevelCreationStart)
        {
            _newRoad = Instantiate(_roadPrefab[numberOfRoadForSpawn], position, Quaternion.identity);
        }
        else
        {
            _newRoad = Instantiate(_startRoad, position, Quaternion.identity);
        }
        _newRoad.transform.SetParent(transform);
        _currentRoads.Add(_newRoad);
    }

    private void ResetLevel()
    {
        _currentSpeedOfRoad = 0;
        while (_currentRoads.Count > 0)
        {
            Destroy(_currentRoads[0]);
            _currentRoads.RemoveAt(0);
        }
        _isLevelCreationStart = false;
        for (int i = 0; i < _maxRoadCount; i++)
        {
            CreateNextRoad();
            _isLevelCreationStart = true;
        }
    }
}
