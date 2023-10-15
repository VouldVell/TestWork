using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace Code.CoinSystem
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class Coin : MonoBehaviour
    {
        public PhotonView PhotonView;

        [PunRPC]
        private IEnumerator DestroyRpc()
        {
            GameObject.Destroy(this.gameObject);
            yield return
                0; // if you allow 1 frame to pass, the object's OnDestroy() method gets called and cleans up references.
            PhotonNetwork.AllocateViewID(PhotonView.ViewID);
        }
    }
}