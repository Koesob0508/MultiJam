namespace MultiJam
{
    public class StartScene : BaseScene
    {
        protected override void Init()
        {
            SceneType = Define.Scene.Title;

            Managers.UI.ShowSceneUI<UI_StartButtons>();
        }

        public override void Clear()
        {

        }
    }
}
