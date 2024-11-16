



using System.Collections.Generic;

public interface IPlayerManager {

    public Player Player1 { get; }
    public Player Player2 { get; }
    public List<Player> PlayerList { get; }

    void Init (Player player1, Player player2);
    Player GetPlayerById (int id);
}