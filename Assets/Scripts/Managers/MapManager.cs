using System;
using System.Collections.Generic;
using UnityEngine;
using Map;
using UnityEditor.SceneManagement;
using Stage = Map.Stage;

public class MapManager
{
    MapGenerator _mapGenerator = new();
    List<Stage>[] _stages = null; // stages[line][column]

    Stage _startStage = null;
    Stage _endStage = null;

    Transform _stageRoot = null;

    public void GenerateMap()
    {
        Vector3 startPos = new Vector3(-6, 0, 0);
        if (!_stageRoot)
        {
            var go = GameObject.Find("@StageRoot");
            if (!go) go = new GameObject("@StageRoot");
            _stageRoot = go.transform;
            _stageRoot.position = startPos;
        }

        _stages = _mapGenerator.VisualizeMap(5, 5, 2, 2, startPos, out _startStage, out _endStage);

        _startStage.transform.SetParent(_stageRoot);
        _startStage.OnStageClicked += OnStageClicked;
        _endStage.transform.SetParent(_stageRoot);
        _endStage.OnStageClicked += OnStageClicked;

        foreach (var t in _stages)
            t.ForEach(s =>
            {
                s.transform.SetParent(_stageRoot);
                s.OnStageClicked += OnStageClicked;
            });
    }

    public void DisableStage()
    {
        _stageRoot.gameObject.SetActive(false);
    }

    public void EnableStage()
    {
        _stageRoot.gameObject.SetActive(true);
    }


    void OnStageClicked(Stage stage)
    {
        // Activate target stages
        if (stage == _startStage)
        {
            _startStage.Deactivate();
            return;
        }

        if (stage == _endStage)
        {
            _endStage.Deactivate();
            // Do something
            return;
        }

        foreach (var t in _stages)
        {
            int idx = t.FindIndex(s => s == stage);
            if (idx == -1) continue;
            foreach (var stages in _stages)
            {
                stages[idx].Deactivate();
            }

            break;
        }

        switch (stage.type)
        {
            case StageType.Normal:
                break;
            case StageType.Event:
                Managers.Game.LoadEvent();
                break;
            case StageType.Battle:
                Managers.Game.LoadBattle();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(stage.type), stage.type, null);
        }

        // When battle stage clicked
    }
}