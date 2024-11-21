using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using Story;
using UI;
public class GameManager
{
    readonly List<StoryEventData> _eventDatas = new();
    List<StoryEventData> _battleData = new();

    GameObject _eventUIPrefab;

    public void Start()
    {
        Managers.Background.SetImage(BgImageManager.ImageType.Map);
        if (_eventUIPrefab == null)
            _eventUIPrefab = Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/EventUI"));
        _eventUIPrefab.SetActive(false);

        LoadAllEventData();
        // LoadAllBattleData();

        // DEBUG
        Managers.Map.GenerateMap();
    }

    public void Update()
    {
    }

    public void LoadEvent()
    {
        if (_eventUIPrefab == null)
            _eventUIPrefab = Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/EventUI"));
        _eventUIPrefab.SetActive(true);
        _eventUIPrefab.GetComponent<StoryEventUI>().UpdateUI(GetRandomEventData());
    }

    public void LoadBattle()
    {
        Managers.Background.SetImage(BgImageManager.ImageType.Battle);
        Managers.RunCoroutine(LoadBattleCoroutine());
    }

    void LoadAllEventData()
    {
        var events = Resources.LoadAll<StoryEventData>("Event");
        _eventDatas.AddRange(events);
    }

    StoryEventData GetRandomEventData()
        => _eventDatas[Random.Range(0, _eventDatas.Count)];


    // AI 사용 (씬 동기화 대기)
    private IEnumerator LoadBattleCoroutine()
    {
        // 씬 비동기 로딩
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Battle", LoadSceneMode.Additive);

        // 씬이 완전히 로드될 때까지 대기
        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        // 씬이 로드된 후 작업 수행
        Managers.Map.DisableStage();
        Managers.Battle.StartBattle();
    }

    public void EndBattle()
    {        Managers.Background.SetImage(BgImageManager.ImageType.Map);

        Managers.Map.EnableStage();
        SceneManager.UnloadSceneAsync("Battle");
    }
    
    public void GameLose()
    {
        SceneManager.LoadScene("GameOver");
    }
}