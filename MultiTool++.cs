using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using Il2CppAssets.Scripts.Unity;
using MelonLoader;
using UnityEngine;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using MultiTool;


[assembly: MelonInfo(typeof(MultiTool.MultiTool), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace MultiTool
{
    public class MultiTool : BloonsTD6Mod
    {
        public override void OnApplicationStart()
        {
            MelonLogger.Msg("MultiTool++ loaded!");
        }

        private static readonly ModSettingHotkey RoundChange = new(KeyCode.F6)
        {
            displayName = "Set Round"
        };
        private static readonly ModSettingHotkey CashChange = new(KeyCode.F7)
        {
            displayName = "Set in game cash"
        };
        private static readonly ModSettingHotkey HealthChange = new(KeyCode.F8)
        {
            displayName = "Set in game round"
        };
        private static readonly ModSettingHotkey addtowerxp = new(KeyCode.F9)
        {
            displayName = "Sets Monkey xp to all towers"
        };
        public static ModSettingInt SetsxpAmmount = new ModSettingInt(2)
        {
            slider = false,
            displayName = "Sets Monkey XP Ammount",
            max = 9999999,
            min = 0,
        };
        private static readonly ModSettingHotkey MonkeyMoney = new(KeyCode.F10)
        {
            displayName = "Sets to your current Monkey Money"
        };
        public static ModSettingInt SetMonkeyMoney = new ModSettingInt(2)
        {
            slider = false,
            displayName = "  Monkey Ammount",
            max = 9999999,
            min = 0,
        };
        private static readonly ModSettingHotkey KnowledgePoints = new(KeyCode.F11)
        {
            displayName = "Sets Monkey Knowledge"
        };
        public static ModSettingInt KnowledgeAmmount = new ModSettingInt(2)
        {
            slider = false,
            displayName = "Sets Monkey Knowledge Ammount",
            max = 9999999,
            min = 0,
        };
        private static readonly ModSettingHotkey TrophiesAdder = new(KeyCode.F12)
        {
            displayName = "Add Trophies"
        };
        public static ModSettingInt TrophiesAmmount = new ModSettingInt(2)
        {
            slider = false,
            displayName = "Add Trophies Ammount",
            max = 9999999,
            min = 0,
        };
        public override void OnUpdate()
        {
            if (RoundChange.JustPressed())
            {
                Il2CppSystem.Action<int> wantedRound = (Il2CppSystem.Action<int>)delegate (int newRound)
                { if (newRound > 0) { InGame.instance.bridge.SetRound(newRound - 1); } };

                PopupScreen.instance.ShowSetValuePopup("Round",
                    "What would you like to set the round to?", wantedRound, 3);
            }

            if (CashChange.JustPressed())
            {
                Il2CppSystem.Action<int> wantedMoney = (Il2CppSystem.Action<int>)delegate (int newMoney)
                { InGame.instance.bridge.SetCash(newMoney); };

                PopupScreen.instance.ShowSetValuePopup("Cash",
                    "What would you like to set your cash to?", wantedMoney, 650);
            }

            if (HealthChange.JustPressed())
            {
                Il2CppSystem.Action<int> wantedHealth = (Il2CppSystem.Action<int>)delegate (int newHealth)
                { InGame.instance.bridge.SetSandboxHealth(newHealth); };

                PopupScreen.instance.ShowSetValuePopup("Lives",
                    "What would you like to set your health to?", wantedHealth, 100);
            }
            if (addtowerxp.JustPressed())
            {
                foreach (var item in Game.instance.playerService.Player.Data.towerXp)
                {
                    Game.instance.playerService.Player.Data.towerXp[item.key].Value = SetsxpAmmount;
                }
            }
            if (MonkeyMoney.JustPressed())
            {
                Game.instance.playerService.Player.Data.monkeyMoney.Value = SetMonkeyMoney;
            }
            if (KnowledgePoints.JustPressed())
            {
                Game.instance.playerService.Player.Data.KnowledgePoints = KnowledgeAmmount;
            }
            if(TrophiesAdder.JustPressed())
            {
                GameExt.GetBtd6Player(Game.instance).GainTrophies(TrophiesAmmount, "event", null);
            }
        }
    }
}