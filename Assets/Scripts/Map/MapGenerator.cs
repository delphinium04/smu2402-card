using System;
using System.Collections.Generic;
using System.Linq;
using Map;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class MapGenerator
{
    GameObject _normalPrefab;
    GameObject _eventPrefab;
    GameObject _battlePrefab;

    int _lineCount = 5;
    int _stageCount = 5;
    int _lineMargin = 2;
    int _stageMargin = 2;
    Vector3 _startPosition;
    List<Stage>[] _stages;
    Stage _startStage;
    Stage _endStage;

    public List<Stage>[] VisualizeMap(int lineCount, int stageCount, int lineMargin, int stageMargin,
        Vector3 startPosition, out Stage startStage, out Stage endStage)
    {
        if (!_normalPrefab) _normalPrefab = Managers.Resource.Load<GameObject>("Prefabs/MapNode/Normal");
        if (!_eventPrefab) _eventPrefab = Managers.Resource.Load<GameObject>("Prefabs/MapNode/Event");
        if (!_battlePrefab) _battlePrefab = Managers.Resource.Load<GameObject>("Prefabs/MapNode/Battle");
        
        _lineCount = lineCount;
        _stageCount = stageCount;
        _lineMargin = lineMargin;
        _stageMargin = stageMargin;
        _startPosition = startPosition;

        _stages = new List<Stage>[lineCount];
        for (int i = 0; i < lineCount; i++)
        {
            _stages[i] = new List<Stage>();
        }

        startStage = _startStage = GenerateStage(startPosition, setPrefab: _normalPrefab).GetComponent<Stage>();
        endStage = _endStage = GenerateStage(startPosition + Vector3.right * stageMargin * (stageCount + 1),
            setPrefab: _battlePrefab).GetComponent<Stage>();

        for (int i = 1; i <= lineCount; i++)
        {
            VisualizeLine(i);
        }

        SetEdge();
        startStage.Activate();

        return _stages;
    }

    void VisualizeLine(int currentLine)
    {
        Vector3 lineStartPos = _startPosition +
                               Vector3.up * (_lineMargin * (_lineCount - 1) / 2.0f) +
                               Vector3.down * (_lineMargin * (currentLine - 1));

        for (int i = 1; i <= _stageCount; i++)
        {
            var stage = GenerateStage(lineStartPos + Vector3.right * i * _stageMargin);
            stage.name = $"Stage {currentLine}-{i}";
            _stages[currentLine - 1].Add(stage.GetComponent<Stage>());
        }
    }

    private Stage GenerateStage(Vector3 position, GameObject setPrefab = null)
    {
        GameObject prefab = null;

        int r = Random.Range(0, 100);
        prefab = r switch
        {
            < 15 => _battlePrefab,
            < 40 => _eventPrefab,
            _ => _normalPrefab
        };

        if (setPrefab != null)
            prefab = setPrefab;

        var createdObject = Object.Instantiate(prefab);
        createdObject.transform.position = position;
        return createdObject.GetComponent<Stage>();
    }


    void SetEdge()
    {
        if (_stages.Length == 0) return;

        _startStage.AddNode(_stages.Select(s => s[0]).ToArray());

        for (int lineIndex = 0; lineIndex < _lineCount; lineIndex++)
        for (int columnIndex = 0; columnIndex < _stageCount - 1; columnIndex++)
        {
            var target = _stages[lineIndex][columnIndex];
            target.AddNode(_stages[lineIndex][columnIndex + 1]); // 일직선

            int r = Random.Range(0, 100);
            if (r <= 49) continue;

            if (lineIndex == 0)
                target.AddNode(_stages[lineIndex + 1][columnIndex + 1]);
            else if (lineIndex == _lineCount - 1)
                target.AddNode(_stages[lineIndex - 1][columnIndex + 1]);
            else
            {
                target.AddNode(r > 75
                    ? _stages[lineIndex + 1][columnIndex + 1]
                    : _stages[lineIndex - 1][columnIndex + 1]);
            }
        }

        for (int i = 0; i < _lineCount; i++)
        {
            _stages[i][_stageCount - 1].AddNode(_endStage);
        }
    }
}