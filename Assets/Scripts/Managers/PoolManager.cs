using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MultiJam
{
    public class PoolManager
    {
        #region Pool
        private class Pool
        {
            public GameObject Original { get; private set; }
            public Transform Root { get; set; }

            Stack<Poolable> _poolStack = new Stack<Poolable>();

            public void Init(GameObject _original, int _count = 5)
            {
                Original = _original;
                Root = new GameObject().transform;
                Root.name = $"{_original.name}_Root";

                for (int i = 0; i < _count; i++)
                {
                    Push(Create());
                }
            }

            private Poolable Create()
            {
                GameObject obj = Object.Instantiate(Original);
                obj.name = Original.name;

                return obj.GetOrAddComponent<Poolable>();
            }

            public void Push(Poolable _poolable)
            {
                if (_poolable = null) return;

                _poolable.transform.parent = Root;
                _poolable.gameObject.SetActive(false);
                _poolable.IsUsing = false;

                _poolStack.Push(_poolable);
            }

            public Poolable Pop(Transform _parent)
            {
                Poolable poolable;

                if (_poolStack.Count > 0)
                {
                    poolable = _poolStack.Pop();
                }
                else
                {
                    poolable = Create();
                }

                poolable.gameObject.SetActive(true);

                if (_parent == null)
                {
                    poolable.transform.parent = Managers.Scene.CurrentScene.transform;
                }

                poolable.transform.parent = _parent;
                poolable.IsUsing = true;

                return poolable;
            }
        }

        #endregion Pool

        Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
        Transform _root;

        public void Init()
        {
            if (_root == null)
            {
                _root = new GameObject { name = "@Pool_Root" }.transform;
                Object.DontDestroyOnLoad(_root);
            }
        }

        public void CreatePool(GameObject _original, int _count = 5)
        {
            Pool pool = new Pool();
            pool.Init(_original, _count);
            pool.Root.parent = _root;

            _pool.Add(_original.name, pool);
        }

        public void Push(Poolable _poolable)
        {
            string name = _poolable.gameObject.name;

            if(_pool.ContainsKey(name) == false)
            {
                Object.Destroy(_poolable.gameObject);
                return;
            }

            _pool[name].Push(_poolable);
        }

        public Poolable Pop(GameObject original, Transform parent = null)
        {
            if (_pool.ContainsKey(original.name) == false)
            {
                CreatePool(original);
            }

            return _pool[original.name].Pop(parent);
        }

        public GameObject GetOriginal(string name)
        {
            if (_pool.ContainsKey(name) == false) return null;

            return _pool[name].Original;
        }

        public void Clear()
        {
            foreach (Transform child in _root)
            {
                Object.Destroy(child.gameObject);
            }

            _pool.Clear();
        }
    }
}