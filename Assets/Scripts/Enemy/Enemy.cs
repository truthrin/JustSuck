using UnityEngine;

namespace Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        private bool _isBeingSuckedIn;

        protected virtual void Update()
        {
            _isBeingSuckedIn = InputHandler.Instance.IsSucking;
            if (_isBeingSuckedIn) 
                BeingSuckedIn();
            else
                NotBeingSuckedIn();
        }

        // 玩家没吸时
        protected abstract void NotBeingSuckedIn();
        
        // 玩家正在吸时
        protected abstract void BeingSuckedIn();

        // 被吸入了
        protected abstract void Died();
        
        // 超出边界了
        protected abstract void Lost();
    }
}