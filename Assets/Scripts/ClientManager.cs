using Unity.Netcode;
using UnityEngine;

namespace MultiJam
{
    public class ClientManager : NetworkBehaviour
    {
        [ClientRpc] 
        public void InitWorldClientRpc(int _worldNumber)
        {
            Debug.Log($"{_worldNumber} 구현");
        }

        [ClientRpc]
        public void InitPlayerClientRpc()
        {
            // local Player는 언제나 아래로
            // 상대는 항상 위로
            if(IsServer) // 빨간색 유지
            {
                // 상대를 파란색으로 변경
            }
            else // 자신의 local Player를 파란색으로 변경
            {

            }
        }

        [ClientRpc]
        public void InitHandClientRpc()
        {
            // Server가 선공이고, Client가 후공
            // 선공은 3장의 카드를 뽑고, 후공은 4장의 카드 + 동전을 갖고 시작한다.
            if(IsServer)
            {

            }
            else
            {

            }
        }

        // 멀리건 진행

        // 멀리건 결과는 Server에 보고

    }
}