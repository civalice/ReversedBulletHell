using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public enum UpgradeOption
    {
        Enable,
        AddBurnEffect,
        AddFreezeEffect,
        AddKnockEffect,
        AddDamageModifier,
        AddProjectSpeedModifier,
        AddAoEModifier,
        AddFireRateModifier,
        AddPieceModifier,
        AddSubSpawnModifier
    }

    public class UpgradeInfo
    {
        public BaseWeapon Weapon;
        public UpgradeOption Option;

        public string GetStringUpgrade()
        {
            string str = "";
            switch (Option)
            {
                case UpgradeOption.Enable:
                {
                    str += "Unlock ";
                }
                    break;
                case UpgradeOption.AddBurnEffect:
                {
                    str += "Add BurnEffect to ";
                }
                    break;
                case UpgradeOption.AddFreezeEffect:
                {
                    str += "Add FreezeEffect to ";
                }
                    break;
                case UpgradeOption.AddKnockEffect:
                {
                    str += "Add KnockEffect to ";
                }
                    break;
                case UpgradeOption.AddDamageModifier:
                {
                    str += "Increase damage to ";
                }
                    break;
                case UpgradeOption.AddFireRateModifier:
                {
                    str += "Increase fire rate to ";
                }
                    break;
                case UpgradeOption.AddProjectSpeedModifier:
                {
                    str += "Increase Projectile Speed to ";
                }
                    break;
                case UpgradeOption.AddPieceModifier:
                {
                    str += "Piece 1 more enemy to ";
                }
                    break;
                case UpgradeOption.AddAoEModifier:
                {
                    str += "Add AoE radius to ";
                }
                    break;
                case UpgradeOption.AddSubSpawnModifier:
                {
                    str += "Spawn more bullet to ";
                }
                    break;
            }
            str += Weapon.name;
            return str;
        }
    }

    public static class UpgradeHelper
    {
        public static List<UpgradeInfo> GetUpgradeList(Player player)
        {
            List<UpgradeInfo> upgradeList = new List<UpgradeInfo>();
            {
                BaseWeapon weapon = player.HolmingWeapon;
                if (!TryAddList(upgradeList, weapon, UpgradeOption.Enable))
                {
                    TryAddList(upgradeList, weapon, UpgradeOption.AddBurnEffect);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddFreezeEffect);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddKnockEffect);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddDamageModifier, 4);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddProjectSpeedModifier, 5);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddAoEModifier, 3);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddFireRateModifier, 5);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddPieceModifier, 5);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddSubSpawnModifier);
                }
            }
            {
                BaseWeapon weapon = player.DiagonalWeapon;
                if (!TryAddList(upgradeList, weapon, UpgradeOption.Enable))
                {
                    TryAddList(upgradeList, weapon, UpgradeOption.AddBurnEffect);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddFreezeEffect);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddKnockEffect);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddDamageModifier, 4);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddProjectSpeedModifier, 5);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddAoEModifier, 3);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddFireRateModifier, 5);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddPieceModifier, 5);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddSubSpawnModifier);
                }
            }
            {
                BaseWeapon weapon = player.ParabolaWeapon;
                if (!TryAddList(upgradeList, weapon, UpgradeOption.Enable))
                {
                    TryAddList(upgradeList, weapon, UpgradeOption.AddBurnEffect);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddFreezeEffect);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddKnockEffect);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddDamageModifier, 4);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddProjectSpeedModifier, 5);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddAoEModifier, 3);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddFireRateModifier, 5);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddPieceModifier, 5);
                    TryAddList(upgradeList, weapon, UpgradeOption.AddSubSpawnModifier);
                }
            }

            return upgradeList;
        }

        private static bool TryAddList(List<UpgradeInfo> upgradeList, BaseWeapon weapon, UpgradeOption option, int count = 1)
        {
            if (weapon.UpgradeList.FindAll(x => x == option).Count < count)
            {
                upgradeList.Add(new UpgradeInfo()
                {
                    Weapon = weapon,
                    Option = option
                });
                return true;
            }

            return false;
        }
    }
}