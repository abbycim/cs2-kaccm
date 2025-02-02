/*
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <https://www.gnu.org/licenses/>.
 */


using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;

namespace Kaccm;

public class Main : BasePlugin
{
    public override string ModuleName => "Kaccm";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "Abby";

    private readonly List<CCSPlayerController> _cooldowns = new();

    public override void Load(bool hotReload)
    {
        AddCommand("kaccm", "Kaç CM? Eklentisi", Kaccm);
    }

    public override void Unload(bool hotReload)
    {
        RemoveCommand("kaccm", Kaccm);
    }

    public void Kaccm(CCSPlayerController? player, CommandInfo command)
    {
        if (_cooldowns.Contains(player))
        {
            command.ReplyToCommand(
                $" {ChatColors.Blue}[{ChatColors.Gold}ABBY{ChatColors.Blue}] {ChatColors.White}Lütfen {ChatColors.Red}3 {ChatColors.White}saniye sonra tekrar deneyin!");
            return;
        }

        _cooldowns.Add(player);
        AddTimer(3, () => { _cooldowns.Remove(player); });
        command.ReplyToCommand(
            $" {ChatColors.Blue}[{ChatColors.Gold}ABBY{ChatColors.Blue}] {ChatColors.Green}{player?.PlayerName} " +
            $"{ChatColors.White}Adlı oyuncunun malafatı {ChatColors.Red}{SayiUretici.SayiOlustur()}" +
            $"{ChatColors.White} cm!");
    }
}

public class SayiUretici
{
    private static readonly Random _random = new();
    private const double SPECIAL_FRACTION = 0.31;
    private const double SPECIAL_PROBABILITY = 0.1;

    public static double SayiOlustur()
    {
        int integerPart = _random.Next(0, 101);
        if (integerPart == 100) return 100.0;
        bool useSpecialFraction = _random.NextDouble() < SPECIAL_PROBABILITY;
        double fractional = useSpecialFraction ? SPECIAL_FRACTION : _random.NextDouble();

        return Math.Round(integerPart + fractional, 5);
    }
}