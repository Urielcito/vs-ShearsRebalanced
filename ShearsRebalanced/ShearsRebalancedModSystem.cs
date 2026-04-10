using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;

namespace ShearsRebalanced
{
    public class ShearsRebalancedModSystem : ModSystem
    {
        public override void Start(ICoreAPI api)
        {
            Mod.Logger.Notification("ShearsRebalanced loaded on: " + api.Side);
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
            new LeavesDropHandler(api);
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            Mod.Logger.Notification("ShearsRebalanced client side loaded.");
        }
    }
}