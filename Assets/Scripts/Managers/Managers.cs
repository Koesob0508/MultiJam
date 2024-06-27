using UnityEngine;

namespace MultiJam
{
    public class Managers : MonoBehaviour
    {
        private static Managers s_instance;
        public static Managers Instance { get { Init(); return s_instance; } }

        SceneManagerEx _scene = new SceneManagerEx();
        PoolManager _pool = new PoolManager();
        ResourceManager _resource = new ResourceManager();

        public static SceneManagerEx Scene { get { return Instance._scene; } }
        public static PoolManager Pool { get { return Instance._pool; } }
        public static ResourceManager ResourceManager { get { return Instance._resource;} }

        private void Start()
        {
            Init();
        }

        private static void Init()
        {
            if (s_instance == null)
            {
                GameObject obj = GameObject.Find("@Managers");

                if (obj == null)
                {
                    obj = new GameObject { name = "@Managers" };
                    obj.AddComponent<Managers>();
                }

                DontDestroyOnLoad(obj);
                s_instance = obj.GetComponent<Managers>();
            }

            s_instance._pool.Init();
        }

        public static void Clear()
        {
            s_instance._pool.Clear();
        }
    }
}