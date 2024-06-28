using UnityEngine;

namespace MultiJam
{
    /// <summary>
    /// 담당 업무
    /// Board(내 오브젝트) 생성
    /// 이후 Player와 Enemy의 아이콘 저장
    /// </summary>
    public class BoardManager : ManagerBase
    {
        private GameObject _player;
        private GameObject _enemy;
        private Transform _root;
        private BoardBase _currentBoard;

        public GameObject Player { get => _player; }
        public GameObject Enemy { get => _enemy; }

        public override void Init()
        {
            if(_root == null)
            {
                _root = new GameObject { name = "@Board_Root" }.transform;
            }
        }

        public override void Clear()
        {
            foreach(Transform child in _root)
            {
                Object.Destroy(child.gameObject);
            }
        }

        public void InitBoard(int _boardIndex)
        {
            switch (_boardIndex)
            {
                case 0:
                    _currentBoard = Managers.Resource.Instantiate<BoardBase>("Prefabs/Boards/BasicBoard", _root);

                    if(Managers.Game.PlayerType == Define.Player.Leader)
                    {
                        _currentBoard.Player.GetComponent<Renderer>().material.color = Color.red;
                        _currentBoard.Enemy.GetComponent<Renderer>().material.color = Color.blue;
                    }
                    else if(Managers.Game.PlayerType == Define.Player.Follower)
                    {
                        _currentBoard.Player.GetComponent<Renderer>().material.color = Color.blue;
                        _currentBoard.Enemy.GetComponent<Renderer>().material.color = Color.red;
                    }

                    break;
                default:
                    break;
            }
        }
    }
}