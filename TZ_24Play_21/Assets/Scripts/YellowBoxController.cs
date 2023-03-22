using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class YellowBoxController : MonoBehaviour
{
    [Header("Boxes Prefabs")]
    [SerializeField] private List<GameObject> _yellowMovingBoxes = new List<GameObject>();
    [SerializeField] private GameObject _yellowBoxPrefab;
    [SerializeField] private GameObject _creationEffect;
    [Header("Appearing Text")] 
    [SerializeField] private GameObject _textWhileTakingBox;
    [SerializeField] private Transform _targetForText;
    private Tween _textTween;
    [Header("Events")] [SerializeField] private UnityEvent _eventOnTakeYellowBox;
    private GameObject _player;
    private Vector3 _lastSpawnBoxPosition;
    private int _indexOfBoxes;
    
    private void Start()
    {
        _player = FindObjectOfType<PlayerMovingController>().gameObject;
        RefreshBoxTrail();
    }
    
    public void OnTakeYellowBox()
    {
        _indexOfBoxes = _yellowMovingBoxes.Count - 1;
        _lastSpawnBoxPosition = _yellowMovingBoxes[_indexOfBoxes].transform.position;
        _eventOnTakeYellowBox.Invoke();
        StartCoroutine(nameof(ShowAppearingText));
        StartCoroutine(nameof(SetBoxCreationEffect));
        Vector3 spawnBoxPosition = new Vector3(_lastSpawnBoxPosition.x, _lastSpawnBoxPosition.y, _lastSpawnBoxPosition.z);
        GameObject newBox = Instantiate(_yellowBoxPrefab, spawnBoxPosition, Quaternion.identity);
        newBox.transform.SetParent(_player.transform);
        _yellowMovingBoxes.Add(newBox);
        RefreshBoxTrail();
    }

    private IEnumerator ShowAppearingText()
    {
        GameObject newText = Instantiate(_textWhileTakingBox, _lastSpawnBoxPosition, Quaternion.identity);
        _textTween = newText.transform.DOMove(_targetForText.position, 3);
        yield return new WaitForSeconds(1);
        Destroy(newText);
        _textTween?.Kill();
    }

    private IEnumerator SetBoxCreationEffect()
    {
        GameObject boxCreationEffect = Instantiate(_creationEffect, _lastSpawnBoxPosition, Quaternion.identity);
        yield return new WaitForSeconds(2);
        Destroy(boxCreationEffect);
    }
    
    public void RefreshBoxTrail()
    {
        if (_yellowMovingBoxes.Count != 0)
        {
            for (int i = 0; i < _yellowMovingBoxes.Count; i++)
            {
                if (i < _yellowMovingBoxes.Count - 1)
                {
                    _yellowMovingBoxes[i].GetComponent<ParticleSystem>().Stop();
                }
                else
                {
                    _yellowMovingBoxes[i].GetComponent<ParticleSystem>().Play();
                }
            }
        }
    }
    
    public void RemoveLostBox(GameObject box) => _yellowMovingBoxes.Remove(box);
}
