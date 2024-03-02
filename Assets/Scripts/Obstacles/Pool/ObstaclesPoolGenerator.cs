using System;
using System.Linq;
using System.Reflection;
using AlwaysUp.Utils;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class ObstaclesPoolGenerator : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Transform _objectPoolsHolder;
        [SerializeField] private ObstaclePool _obstaclePoolPrefab;

        private void Awake()
        {
            Type[] obstacles = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(Obstacle)) && !t.IsAbstract).ToArray();
            for (int i = 0; i < obstacles.Length; i++)
                Instantiate(_obstaclePoolPrefab, _objectPoolsHolder);
        }
    }
}
