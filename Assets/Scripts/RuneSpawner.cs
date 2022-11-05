using UnityEngine;
using System.Collections.Generic;
using System;

public class RuneSpawner : MonoBehaviour
{
    [SerializeField] private Rune[] _runePrefabs;
    [SerializeField] private PlayableFeildsController _playableFeilds;
    private Dictionary<RuneType, int> _runeCounters;
    private int _runeTypeCount;
    private void Awake()
    {
        ResetRuneCounters();
    }
    private void Start()
    {
        _runeTypeCount = PlayableFeildsController.FEILD_SIZE / 2 + 1;
        SpanwRunes();
    }
    private void SpanwRunes()
    {
        SpawnTopRunes();
        foreach (var feild in _playableFeilds.FeildsForRunes)
        {
            var randomed = RandomizeRune();
            Instantiate(randomed.gameObject, feild.transform.position, Quaternion.identity, this.transform).
                        GetComponent<Rune>().Type = randomed.type;
        }
        ResetRuneCounters();
    }
    private void SpawnTopRunes(){
        int i = (PlayableFeildsController.FEILD_SIZE -1);
        int iterationCounter = 0;
        while(i < _playableFeilds.FeildsForRunes.Length){
            Instantiate(_runePrefabs[iterationCounter].gameObject, 
                        new Vector3 ((float)_playableFeilds.FeildsForRunes[i].X, 
                                     (float)_playableFeilds.FeildsForRunes[i].Y + 1, 1), 
                        Quaternion.identity, this.transform).
                        GetComponent<Rune>().Type = (RuneType)iterationCounter;
            i += PlayableFeildsController.FEILD_SIZE;
            iterationCounter++;
        }
    }

    private (GameObject gameObject, RuneType type) RandomizeRune()
    {
        int runePrefabIndex = UnityEngine.Random.Range(0, _runeTypeCount);
        while (!(_runeCounters[(RuneType)runePrefabIndex] < PlayableFeildsController.FEILD_SIZE))
        {
            runePrefabIndex = UnityEngine.Random.Range(0, _runeTypeCount);
        }

        _runeCounters[(RuneType)runePrefabIndex]++;
        return (_runePrefabs[runePrefabIndex].gameObject, (RuneType)runePrefabIndex);
    }

    private void ResetRuneCounters()
    {
        _runeCounters = new Dictionary<RuneType, int>();
        foreach (var type in Enum.GetValues(typeof(RuneType)))
        {
            _runeCounters.Add((RuneType)type, 0);
        }

    }
}
