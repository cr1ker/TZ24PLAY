using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class YellowBoxController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _yellowMovingBoxes = new List<GameObject>();
    [SerializeField] private GameObject _yellowBoxPrefab;
    [Header("Events")]
    [SerializeField] private UnityEvent _eventOnTakeYellowBox;
    private GameObject _player;
    private Vector3 _lastSpawnBoxPosition;
    private int _indexOfBoxes;

    private void Start()
    {
        _player = FindObjectOfType<PlayerMovingController>().gameObject;
    }
    
    public void OnTakeYellowBox()
    {
        _indexOfBoxes = _yellowMovingBoxes.Count - 1;
        _lastSpawnBoxPosition = _yellowMovingBoxes[_indexOfBoxes].transform.position;
        _eventOnTakeYellowBox.Invoke();
        Vector3 spawnBoxPosition = new Vector3(_lastSpawnBoxPosition.x, _lastSpawnBoxPosition.y, _lastSpawnBoxPosition.z);
        GameObject newBox = Instantiate(_yellowBoxPrefab, spawnBoxPosition, Quaternion.identity);
        newBox.transform.SetParent(_player.transform);
        _yellowMovingBoxes.Add(newBox);
    }
    
    public int GetIndexOfLastBoxListElement(int indexOfLastBoxListElement)
    {
        return indexOfLastBoxListElement = _yellowMovingBoxes.Count - 1;
    }
}
