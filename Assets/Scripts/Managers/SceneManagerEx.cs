using UnityEngine;
using UnityEngine.SceneManagement;

namespace MultiJam
{
    public class SceneManagerEx
    {
        public BaseScene CurrentScene
        {
            get { return Object.FindFirstObjectByType<BaseScene>(); }
        }

        public void LoadSccene(Define.Scene _type)
        {
            Clear();
            SceneManager.LoadScene(GetSceneName(_type));
        }

        private string GetSceneName(Define.Scene _type)
        {
            string name = System.Enum.GetName(typeof(Define.Scene), _type);

            return name;
        }

        public void Clear()
        {
            CurrentScene.Clear();
        }
    }
}