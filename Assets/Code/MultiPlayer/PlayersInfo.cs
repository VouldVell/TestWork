using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.MultiPlayer
{
    public class PlayersInfo : MenuManager
    {
        [SerializeField] private TMP_Text text;
        public Button Start;
        

        public void RegisteredPlayer(int countPlayers)
        {
            text.text = $"Players count: {countPlayers}/2. \nAt least 2 players are required to run";
        }
    }
}