using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public enum StageType
    {
        Normal, // 아무 작동하지 않는 일반
        Event, // 이벤트 팝업을 띄우는 이벤트
        Battle // 전투 씬을 로딩하는 전투
    }

    [RequireComponent(typeof(SpriteRenderer))]
    public class Stage : MonoBehaviour
    {
        public StageType type = StageType.Normal;
        public Action <Stage> OnStageClicked;
        private List<Stage> NextStages { get; set; }
        
        public int StageNumber { get; set; }

        bool _isActive;
        bool _isClicked;

        void Awake()
        {
            Deactivate();
            OnStageClicked = null;
            NextStages = new List<Stage>();
        }

        public void Activate()
        {
            _isActive = true;
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        public void Deactivate()
        {
            _isActive = false;
            GetComponent<SpriteRenderer>().color = !_isClicked ? Color.grey : Color.blue;
        }

        public void AddNode(params Stage[] s)
        {
            NextStages.AddRange(s);
            foreach (var t in s)
            {
                GameObject go = new GameObject(name: t.name);
                go.transform.SetParent(transform);
                var lineRenderer = go.AddComponent<LineRenderer>();
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, t.transform.position);
                lineRenderer.startWidth = 0.05f;
                lineRenderer.endWidth = 0.05f;
                lineRenderer.material = new Material(Shader.Find("Sprites/Default")) { color = Color.black };
                lineRenderer.sortingLayerName = "UI";
            }
        }

        void Interact()
        {
            NextStages.ForEach(e => e.Activate());
        }

        void OnMouseDown()
        {
            if (!_isActive) return;
            _isClicked = true;

            Interact();
            OnStageClicked?.Invoke(this);
            // 본인 Column이 맞다면
        }
    }
}