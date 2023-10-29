using System;
using Hmxs.Toolkit.Flow.FungusTools;
using Hmxs.Toolkit.Module.Audios;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using AudioType = Hmxs.Toolkit.Module.Audios.AudioType;

namespace DefaultNamespace
{
    public class StartScene : MonoBehaviour
    {
        private void Start()
        {
            AudioCenter.Instance.AudioPlaySync(new AudioAsset(AudioType.BGM, "BGM", true));
        }

        public void GameStart()
        {
            FlowchartManager.ExecuteBlock("GameStart");
        }

        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                GameStart();
            }
        }

        public void NextScene()
        {
            SceneManager.LoadScene("main");
        }
    }
}