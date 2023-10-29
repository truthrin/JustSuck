using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class FinishScene : MonoBehaviour
    {
        public void NextScene()
        {
            SceneManager.LoadScene("end");
        }
    }
}