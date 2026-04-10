using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace ShearsRebalanced
{
    public class LeavesDropHandler
    {
        private readonly ICoreServerAPI api;

        public LeavesDropHandler(ICoreServerAPI api)
        {
            this.api = api;
            api.Event.BreakBlock += OnBreakBlock;
        }

        private void OnBreakBlock(IServerPlayer byPlayer, BlockSelection blockSel, ref float dropQuantityMultiplier, ref EnumHandling handling)
        {
            if (byPlayer.InventoryManager.ActiveTool != EnumTool.Shears)
                return;

            // Iterar el cubo 3x3x3 alrededor del bloque roto
            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                    for (int z = -1; z <= 1; z++)
                    {
                        BlockPos neighborPos = blockSel.Position.AddCopy(x, y, z);
                        Block neighborBlock = api.World.BlockAccessor.GetBlock(neighborPos);

                        if (neighborBlock == null || neighborBlock.BlockMaterial != EnumBlockMaterial.Leaves)
                            continue;

                        string blockCode = neighborBlock.Code.ToString();
                        bool isBranchy = blockCode.Contains("branchy") || blockCode.Contains("narrow");

                        if (isBranchy && api.World.Rand.NextDouble() < 0.75)
                        {
                            SpawnSticks(neighborPos, 1);
                        }
                        else if (api.World.Rand.NextDouble() < 0.25)
                        {
                            SpawnSticks(neighborPos, 1);
                        }
                    }
        }

        private void SpawnSticks(BlockPos blockPos, int amount)
        {
            ItemStack stickStack = new ItemStack(api.World.GetItem(new AssetLocation("stick")), amount);
            if (stickStack.Item == null)
            {
                return;
            }
            api.World.SpawnItemEntity(stickStack, blockPos);
        }
    }
}