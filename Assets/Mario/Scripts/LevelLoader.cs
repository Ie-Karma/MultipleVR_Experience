using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mario.Scripts
{
    public class LevelLoader : MonoBehaviour
    {
        public void LoadLevel(int levelIndex)
        {
            SceneManager.LoadScene(levelIndex);
        }
        
        public void SetCompletedLevel(int levelIndex)
        {
            GlobalTimer.instance.SetLevelCompletion(levelIndex);
        }

    }
    
}
