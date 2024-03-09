using System.Collections.Generic;
using AlwaysUp.Events;
using AlwaysUp.Utils;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class ColorObstacle : Obstacle
    {
        [Header("Settings")]
        [SerializeField] private GameObject[] _obstacles;
        [SerializeField] private Animation _onHitColorAnim;

        [Header("Broadcasting")]
        [SerializeField] private IntEventChannelSO _onHitColor;

        // Warning: _objectIDs and _obstaclesSRs is set on OnValidate.
        // The order of then is important, does not change one without other!
        [HideInInspector][SerializeField] private ObjectID[] _objectIDs;
        [HideInInspector][SerializeField] private SpriteRenderer[] _obstaclesSRs;
        private bool _initializedObjectsIDs;
        private List<int> _obstaclesColorIndex;

        private EdgeCollider2D[] _colliders;

        protected override void Awake()
        {
            base.Awake();
            _colliders = GetComponentsInChildren<EdgeCollider2D>();
        }

        private void OnValidate()
        {
            if (_obstacles == null)
                return;

            _objectIDs = new ObjectID[_obstacles.Length];
            _obstaclesSRs = new SpriteRenderer[_obstacles.Length];
            for (int i = 0; i < _obstacles.Length; i++)
            {
                if (_obstacles[i] == null)
                    continue;

                _objectIDs[i] = _obstacles[i].GetComponent<ObjectID>();
                _obstaclesSRs[i] = _obstacles[i].GetComponent<SpriteRenderer>();
            }
        }

        private void OnEnable()
        {
            if (!_initializedObjectsIDs)
                InitializeObjectsIDs();

            SetObstaclesColors();
            SetCollidersState(enabled: true);
        }

        private void OnDisable()
        {
            // Safe to use again
            _onHitColorAnim.Stop();
            transform.localScale = Vector3.one;
        }

        private void InitializeObjectsIDs()
        {
            for (int i = 0; i < _objectIDs.Length; i++)
                _objectIDs[i].SetID(i);

            _initializedObjectsIDs = true;
        }

        private void SetObstaclesColors()
        {
            _obstaclesColorIndex = new List<int>();
            int j = 0;
            for (int i = 0; j < _obstacles.Length; i++)
            {
                if (i == GameColorController.CurrentColorIndex)
                    continue;

                _obstaclesSRs[j].color = GameColors.GetColorByIndex(i);
                _obstaclesColorIndex.Add(i);

                j++;
            }
        }

        override protected void OnCollision(GameObject obstacle)
        {
            int colorIndex = _obstaclesColorIndex[obstacle.GetComponent<ObjectID>().ID];
            _onHitColor.RaiseEvent(colorIndex);

            _onHitColorAnim.Play();
            SetCollidersState(enabled: false);
        }

        private void OnEndHitColorAnim()
        {
            gameObject.SetActive(false);
        }

        private void SetCollidersState(bool enabled)
        {
            foreach (EdgeCollider2D collider in _colliders)
                collider.enabled = enabled;
        }
    }
}
