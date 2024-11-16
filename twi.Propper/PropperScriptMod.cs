using GDWeave;
using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace twi.Propper;

public class PropperScriptMod(IModInterface modInterface, Config config) : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline after any reference to "_ready"
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken { Name: "prop_ids" },
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken { Name: "size" },
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.OpGreater,
            t => t is ConstantToken && ((ConstantToken) t).Value is IntVariant && ((ConstantToken) t).Value.Equals(new IntVariant( 4 )),
        ], allowPartialMatch: true);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {
                yield return new ConstantToken(new IntVariant(config.NewLimit - 1));
                modInterface.Logger.Information(String.Format("Propper: Found prop limit check and tried to change prop limit from {1} to {0}",
                    config.NewLimit, ((IntVariant)((ConstantToken)token).Value).Value + 1));
            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
