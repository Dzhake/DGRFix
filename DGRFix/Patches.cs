using DuckGame;
using DGRFix.PatchSystem;

namespace DGRFix;

public class Patches
{
    [AutoPatch(typeof(DuckNetwork), nameof(DuckNetwork.MidGameJoiningLogic), MPatchType.Postfix)]
    public static void DuckNetwork_MidGameJoiningLog_Postfix()
    {
        if ((!Network.isActive || !Network.isServer) || Level.current is not GameLevel || !DGRSettings.MidGameJoining ||
            Network.activeNetwork?.core?.lobby == null) return;
        

        Network.activeNetwork.core.lobby.type = SteamLobbyType.Public;
        Network.activeNetwork.core.lobby.SetLobbyData("started", "false");
    }
}
