using GDWeave;

namespace twi.Propper;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        modInterface.RegisterScriptMod(new PropperScriptMod(modInterface,Config));
        modInterface.Logger.Information($"Propper: Prop limit is {Config.NewLimit}");
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
