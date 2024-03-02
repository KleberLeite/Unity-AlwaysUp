using System.Collections.Generic;
using System.Linq;
using AlwaysUp.Utils;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class ObstacleGenerator : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _sectorSize;
        [SerializeField] private Transform _ball;
        [SerializeField] private Transform _obstaclesHolder;
        [SerializeField] private Obstacle[] _obstaclesPrefab;
        [SerializeField] private Obstacle _colorObstacle;
        [SerializeField] private int _obstaclesPerSector;
        [SerializeField] private int _countToNewColorObstacle;

        private int _currentSectorIndex;
        private List<GameObject> _oldObstacles = new List<GameObject>();
        private int _maxObstacles;
        private int _currentCountToNewColorObstacle;

        private void Awake()
        {
            _maxObstacles = 3 * _obstaclesPerSector;
        }

        private void Start()
        {
            GenerateSector(0);
        }

        private void Update()
        {
            if (_ball.transform.position.y > _currentSectorIndex * _sectorSize)
            {
                _currentSectorIndex++;
                GenerateSector(_currentSectorIndex);
            }
        }

        private void GenerateSector(int sectorIndex)
        {
            Vector3 offset = new Vector3(0, sectorIndex * _sectorSize);
            List<Obstacle> obstacles = _obstaclesPrefab.ToList();
            float spaceBetweenObstacles = _sectorSize / _obstaclesPerSector;

            for (int i = 0; i < _obstaclesPerSector; i++)
            {
                Obstacle obstacle;
                if (_currentCountToNewColorObstacle == 0)
                {
                    obstacle = _colorObstacle;
                    _currentCountToNewColorObstacle = _countToNewColorObstacle;
                }
                else
                {
                    int rngIndex = Random.Range(0, obstacles.Count);
                    obstacle = obstacles[rngIndex];
                    obstacles.RemoveAt(rngIndex);
                }

                Vector3 pos = offset + obstacle.transform.position.With(y: 0, z: 0) + new Vector3(0, i * spaceBetweenObstacles);
                Obstacle newObstacle = Instantiate(obstacle, pos, Quaternion.identity, _obstaclesHolder);
                newObstacle.Init(GetRandomColorIndex());
                AddObstacle(newObstacle.gameObject);

                _currentCountToNewColorObstacle--;
            }
        }

        private int GetRandomColorIndex() => Random.Range(-1, GameColors.Count);

        private void AddObstacle(GameObject obstacle)
        {
            _oldObstacles.Add(obstacle);
            if (_oldObstacles.Count > _maxObstacles)
            {
                GameObject oldObstacle = _oldObstacles[0];
                Destroy(oldObstacle);
                _oldObstacles.RemoveAt(0);
            }
        }
    }
}
