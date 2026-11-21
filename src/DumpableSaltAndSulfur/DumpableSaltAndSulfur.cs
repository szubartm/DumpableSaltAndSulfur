using System;
using Mafi;
using Mafi.Base;
using Mafi.Core.Buildings.Farms;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;

namespace DumpableSaltAndSulfur;

internal static class DumpableSaltAndSulfur
{
    public static void Configure(ProtoRegistrator registrator)
    {
        try
        {
            var salt = registrator.PrototypesDb.GetOrThrow<LooseProductProto>(Ids.Products.Salt);
            var sulfur = registrator.PrototypesDb.GetOrThrow<LooseProductProto>(Ids.Products.Sulfur);

            var baseSaltTerrain =
                registrator.PrototypesDb.GetOrThrow<TerrainMaterialProto>(Ids.TerrainMaterials.LimestoneDisrupted);
            var baseSulfurTerrain =
                registrator.PrototypesDb.GetOrThrow<TerrainMaterialProto>(Ids.TerrainMaterials.SandDisrupted);

            var saltTerrainId =
                new Proto.ID("DumpableSaltAndSulfur.TerrainMaterial_Salt" + TerrainMaterialProto.SUFFIX);
            var sulfurTerrainId =
                new Proto.ID("DumpableSaltAndSulfur.TerrainMaterial_Sulfur" + TerrainMaterialProto.SUFFIX);

            var saltColor = new ColorRgba(235, 238, 242);
            var sulfurColor = new ColorRgba(232, 205, 58);

            var saltGfx = baseSaltTerrain.Graphics.WithReplaced();
            saltGfx.Color = saltColor.SetA(byte.MaxValue);
            saltGfx.ParticleColor = saltColor.SetA(byte.MaxValue);

            var sulfurGfx = baseSulfurTerrain.Graphics.WithReplaced();
            sulfurGfx.Color = sulfurColor.SetA(byte.MaxValue);
            sulfurGfx.ParticleColor = sulfurColor.SetA(byte.MaxValue);

            var saltTerrain = new TerrainMaterialProto(
                saltTerrainId,
                "Salt",
                salt,
                baseSaltTerrain.MinedQuantityMult,
                baseSaltTerrain.MinCollapseHeightDiff,
                baseSaltTerrain.MaxCollapseHeightDiff,
                saltGfx,
                ignoreInEditor: true,
                useSoftCollapseSound: baseSaltTerrain.UseSoftCollapseSound
            );

            var sulfurTerrain = new TerrainMaterialProto(
                sulfurTerrainId,
                "Sulfur",
                sulfur,
                baseSulfurTerrain.MinedQuantityMult,
                baseSulfurTerrain.MinCollapseHeightDiff,
                baseSulfurTerrain.MaxCollapseHeightDiff,
                sulfurGfx,
                ignoreInEditor: true,
                useSoftCollapseSound: baseSulfurTerrain.UseSoftCollapseSound
            );

            registrator.PrototypesDb.Add(saltTerrain);
            registrator.PrototypesDb.Add(sulfurTerrain);

            salt.AddOrReplaceParam(new LooseProductParam(saltTerrainId));
            sulfur.AddOrReplaceParam(new LooseProductParam(sulfurTerrainId));

            Log.Info("DumpableSaltAndSulfur: Configured terrain materials for Salt and Sulfur.");
        }
        catch (Exception ex)
        {
            Log.Exception(ex);
        }
    }
}