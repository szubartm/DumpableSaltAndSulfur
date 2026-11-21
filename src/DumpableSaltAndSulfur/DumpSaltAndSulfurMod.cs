using Mafi;
using Mafi.Collections;
using Mafi.Core.Mods;

namespace DumpableSaltAndSulfur;

public sealed class DumpSaltAndSulfurMod : DataOnlyMod
{
    public DumpSaltAndSulfurMod(ModManifest manifest) : base(manifest)
    {
        Log.Info("DumpableSaltAndSulfur: constructed");
    }

    public override void RegisterPrototypes(ProtoRegistrator registrator)
    {
        Log.Info("DumpableSaltAndSulfur: registering prototypes");
        DumpableSaltAndSulfur.Configure(registrator);
    }

    public override void MigrateJsonConfig(VersionSlim savedVersion, Dict<string, object> savedValues)
    {
    }
}