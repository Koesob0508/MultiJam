using Unity.Netcode;
using UnityEngine;

namespace MultiJam
{
    public class Managers : NetworkBehaviour
    {
        private static Managers s_instance;
        public static Managers Instance { get { Init(); return s_instance; } }

        SceneManagerEx _scene = new SceneManagerEx();
        PoolManager _pool = new PoolManager();
        ResourceManager _resource = new ResourceManager();
        UIManager _ui = new UIManager();
        GameManager _game = null;
        BoardManager _board = new BoardManager();

        public static SceneManagerEx Scene { get { return Instance._scene; } }
        public static PoolManager Pool { get { return Instance._pool; } }
        public static ResourceManager Resource { get { return Instance._resource; } }
        public static UIManager UI { get { return Instance._ui; } }
        public static GameManager Game { get { return Instance._game; } }
        public static BoardManager Board { get { return Instance._board; } }

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

            s_instance._scene.Init();
            s_instance._resource.Init();
            s_instance._pool.Init();
            s_instance._ui.Init();

            if (s_instance._game == null)
            {
                GameObject obj = GameObject.Find("@GameManager");
                if (obj == null)
                {
                    obj = new GameObject { name = "@GameManager" };
                }

                DontDestroyOnLoad(obj);
                s_instance._game = obj.GetComponent<GameManager>();
            }

            s_instance._game.Init();
            s_instance._board.Init();
        }

        public static void Clear()
        {
            s_instance._board.Clear();
            s_instance._game.Clear();
            s_instance._ui.Clear();
            s_instance._pool.Clear();
            s_instance._resource.Clear();
            s_instance._scene.Clear();
        }
    }
}