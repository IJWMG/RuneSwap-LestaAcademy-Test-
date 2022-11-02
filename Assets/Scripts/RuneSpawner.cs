using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class RuneSpawner : MonoBehaviour
{
    [SerializeField] private Rune[] _runePrefabs;
    [SerializeField] private PlayableFeildsController _playableFeilds;
    private int _collumnToFill1 = -2, _collumnToFill2 = 0, _collumnToFill3 = 2, _runeTypeCount;
    private Dictionary<RuneType, int> _runeCounters;
    private void Awake()
    {
        ResetRuneCounters();
    }
    private void Start()
    {
        _runeTypeCount = Enum.GetValues(typeof(RuneType)).Length;
        SpanwRunes();
    }
    private void SpanwRunes()
    {
        var freeFeilds = (from f in _playableFeilds.ActiveFeilds
                          where f.X == _collumnToFill1 || f.X == _collumnToFill2 || f.X == _collumnToFill3
                          select f);

        foreach (var feild in freeFeilds)
        {
            var randomed = RandomizeRune();
            Instantiate(randomed.gameObject, feild.transform.position, Quaternion.identity, this.transform).
                        GetComponent<Rune>().Type = randomed.type;
        }
        ResetRuneCounters();
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
