using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mario.Scripts
{
    public class GlobalTimer : MonoBehaviour
    {
        public static GlobalTimer instance { get; } = new();

        private float _startTime;
        private float _elapsedTime;
        private readonly Dictionary<int, bool> _levelCompletion = new Dictionary<int, bool>();

        private void Start()
        {
            _startTime = Time.time;
            DontDestroyOnLoad(gameObject);
            
            // Pistola
            _levelCompletion.Add(0, false);
            // Mini Coche
            _levelCompletion.Add(1, false);
            // Conducir
            _levelCompletion.Add(2, false);
            // Cerdos
            _levelCompletion.Add(3, false);
            // Plantas
            _levelCompletion.Add(4, false);
            // Cubos
            _levelCompletion.Add(5, false);
            // Gancho
            _levelCompletion.Add(6, false);
            // Bolas
            _levelCompletion.Add(7, false);
            // Laberinto
            _levelCompletion.Add(8, false);

        }

        private void Update()
        {
            _elapsedTime = Time.time - _startTime;
            
            if (!HasFinished()) return;
            
            if (PlayerPrefs.GetFloat("TimeScore", 0) > _elapsedTime || PlayerPrefs.GetFloat("TimeScore", 0) == 0)
            {
                PlayerPrefs.SetFloat("TimeScore", _elapsedTime);
            }
        }
        
        private bool HasFinished()
        {
            return _levelCompletion.All(level => level.Value);
        }
        
        public void SetLevelCompletion(int levelIndex)
        {
            if (_levelCompletion.ContainsKey(levelIndex))
            {
                _levelCompletion[levelIndex] = true;
            }
        }
        
    }
}
