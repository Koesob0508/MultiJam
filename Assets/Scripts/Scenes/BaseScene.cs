using UnityEngine;

namespace MultiJam
{
    public abstract class BaseScene : MonoBehaviour
    {
        public Define.Scene SceneType { get; protected set; }

        private void Start()
        {
            Init();
        }

        protected abstract void Init();

        public abstract void Clear();
    }
}