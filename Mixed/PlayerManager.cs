



using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IPlayerManager {

    [SerializeField] private GameData gameData;

    private Player player1;
    private Player player2;

    public Player Player1 => this.player1;
    public Player Player2 => this.player2;

    private List<Player> playerList = new();
    public  List<Player> PlayerList => this.playerList;

    public Player GetPlayerById(int id) {
        return playerList.Where(x => x.ID == id).FirstOrDefault();
    }
    public void Init (Player player1, Player player2) {

        playerList.Add(player1);
        playerList.Add(player2);

        this.player2 = player2;
        this.player1 = player1;

        this.player1.Init(1, this.gameData.runtimeGameData.GetTestDeck(), gameData.TestVillain);
        this.player2.Init(2, this.gameData.runtimeGameData.GetTestDeck(), gameData.TestVillain);
    }

    public List<CreatureRuntimeCardData> GetCreaturesInPlayByPlayer (int playerId) {

        return GetPlayerById(playerId).GetCreaturesInPlay();
    }
}