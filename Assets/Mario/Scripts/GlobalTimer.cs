using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Mario.Scripts
{
    public class GlobalTimer : MonoBehaviour
    {
        public static GlobalTimer instance { get; private set; }

        private float _startTime;
        private float _elapsedTime;
        private readonly Dictionary<int, bool> _levelCompletion = new Dictionary<int, bool>();
        private TextMeshProUGUI _text;
        private string _bestTime;
        private bool _hasFinished = false;
        
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
        private void Start()
        {
            _startTime = Time.time;
            DontDestroyOnLoad(gameObject);
            _text = GetComponentInChildren<TextMeshProUGUI>();
            _bestTime = "Best time: " + PlayerPrefs.GetFloat("TimeScore", 0).ToString("F2") + "s";
            
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
            var levelIndex = _levelCompletion.Count(level => level.Value);
            var actualLevelText = "Actual time: " + _elapsedTime.ToString("F2") + "s";
            _text.text = actualLevelText + "\n" + _bestTime + "\n" + "Levels completed: " + levelIndex + "/9";
            if (Camera.main != null)
            {
                var camTrans = Camera.main.transform;
                this.transform.position = new Vector3(camTrans.position.x, camTrans.position.y + 15, camTrans.position.z);
                this.transform.LookAt(camTrans.position);
            }

            if(!_hasFinished) _elapsedTime = Time.time - _startTime;
            
            if (!HasFinished()) return;

            if (!(PlayerPrefs.GetFloat("TimeScore", 0) > _elapsedTime) &&
                PlayerPrefs.GetFloat("TimeScore", 0) != 0) return;
            PlayerPrefs.SetFloat("TimeScore", _elapsedTime);
            _bestTime = "Best time: " + PlayerPrefs.GetFloat("TimeScore", 0).ToString("F2") + "s";
            _hasFinished = true;
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
