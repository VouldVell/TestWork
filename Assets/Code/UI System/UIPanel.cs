using System.Collections.Generic;
using Code.CoinSystem;
using PlayerManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI_System
{
    public class UIPanel : MonoBehaviour
    {
        [Header("Objects")]
        public GameObject Managment;
        public GameObject VictoryPanel;
        public TMP_Text VictoryPaneCountCoins;
        public TMP_Text VictoryPanePlayerName;
        [Header("Buttons")]
        public Button Fire;
        public Button ButtonsExitToLobby;
        [Header("Scripts")]
        public VariableJoystick JoyStick;
        public CoinInfo CoinInfo;
        public HealthBar healthBar;
        [Space]
        [SerializeField]private List<Player> _players = new ();
        
        public void AddPlayer(Player player)
        {
            _players.Add(player);
            Debug.Log(_players.Count);
        }
        
        public void RemovePlayer(Player player)
        {
            _players.Remove(player);
            Debug.Log(_players.Count);
        }
    }
}