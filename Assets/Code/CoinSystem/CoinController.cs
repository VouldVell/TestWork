using System.Collections.Generic;
using Configs;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.CoinSystem
{
    public class CoinController
    {
        private readonly CoinInfo _info;
        private readonly GameConfig _gameConfig;
        private readonly GameObject _coinObj;

        public CoinController(CoinInfo info, GameConfig gameConfig)
        {
            _info = info;
            _gameConfig = gameConfig;
        }
        
        public void UpCoin(Coin coin)
        {
            _info.ChangeCoin(1);
            coin.PhotonView.RPC("DestroyRpc", RpcTarget.All);
        }
    }
}