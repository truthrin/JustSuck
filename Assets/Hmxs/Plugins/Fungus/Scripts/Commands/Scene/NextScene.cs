using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus
{
    [CommandInfo("Scene",
        "NextScene",
        "Load Scene")]
    [AddComponentMenu("")]
    public class NextScene : Command
    {
        [SerializeField] protected string sceneName;

        public override void OnEnter()
        {
            SceneManager.LoadScene(sceneName);

            Continue();
        }
    }
}