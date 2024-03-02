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
        [SerializeField] private ObstaclePool[] _obstaclesPool;
        [SerializeField] private ObstaclePool _colorObstaclePool;
        [SerializeField] private int _obstaclesPerSector;
        [SerializeField] private int _countToNewColorObstacle;

        private int _currentSectorIndex;
        private List<Obstacle> _oldObstacles = new List<Obstacle>();
        private List<ObstaclePool> _oldObstaclesPools = new List<ObstaclePool>();
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
            List<ObstaclePool> obstacles = _obstaclesPool.ToList();
            float spaceBetweenObstacles = _sectorSize / _obstaclesPerSector;

            for (int i = 0; i < _obstaclesPerSector; i++)
            {
                Obstacle obstacle;
                ObstaclePool obstaclePool;
                if (_currentCountToNewColorObstacle == 0)
                {
                    obstacle = _colorObstaclePool.Get();
                    obstaclePool = _colorObstaclePool;

                    _currentCountToNewColorObstacle = _countToNewColorObstacle;
                }
                else
                {
                    int rngIndex = Random.Range(0, obstacles.Count);
                    obstacle = obstacles[rngIndex].Get();
                    obstacles.RemoveAt(rngIndex);

                    obstaclePool = _obstaclesPool[rngIndex];
                }

                Vector3 pos = offset + obstacle.transform.position.With(y: 0, z: 0) + new Vector3(0, i * spaceBetweenObstacles);
                obstacle.transform.position = pos;
                obstacle.gameObject.SetActive(true);
                obstacle.transform.parent = _obstaclesHolder;
                obstacle.Init(GetRandomColorIndex());
                AddObstacle(obstacle, obstaclePool);

                _currentCountToNewColorObstacle--;
            }
        }

        private int GetRandomColorIndex() => Random.Range(-1, GameColors.Count);

        private void AddObstacle(Obstacle obstacle, ObstaclePool pool)
        {
            _oldObstacles.Add(obstacle);
            _oldObstaclesPools.Add(pool);
            if (_oldObstacles.Count > _maxObstacles)
            {
                _oldObstaclesPools[0].GiveBack(_oldObstacles[0]);

                _oldObstaclesPools.RemoveAt(0);
                _oldObstacles.RemoveAt(0);
            }
        }
    }
}
