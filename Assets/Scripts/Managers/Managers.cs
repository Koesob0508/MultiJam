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

        public static NetworkManager Network { get { return NetworkManager.Singleton; } }
        public static SceneManagerEx Scene { get { return Instance._scene; } }
        public static PoolManager Pool { get { return Instance._pool; } }
        public static ResourceManager Resource { get { return Instance._resource; } }
        public static UIManager UI { get { return Instance._ui; } }
        public static GameManager Game { get { return Instance._game; } }
        public static BoardManager Board { get { return Instance._board; } }

        Logger _logger = new Logger();

        public static Logger Logger { get { return Instance._logger; } }

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

                s_instance._scene.Init();
                s_instance._resource.Init();
                s_instance._pool.Init();
                s_instance._ui.Init();

                if (s_instance._game == null)
                {
                    GameObject gm = GameObject.Find("@GameManager");
                    if (gm == null)
                    {
                        GameObject original = Resources.Load<GameObject>("Prefabs/@GameManager");
                        gm = Instantiate(original);

                        int index = gm.name.IndexOf("(Clone)");
                        if (index > 0)
                        {
                            gm.name = gm.name.Substring(0, index);
                        }

                        gm.GetComponent<NetworkObject>().Spawn();
                    }

                    s_instance._game = gm.GetComponent<GameManager>();
                }

                s_instance._game.Init();
                s_instance._board.Init();
            }
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