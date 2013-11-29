﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AutoEquip2;
using Styx.Common;
using Styx.Common.Helpers;
using Styx.CommonBot;
using Styx.CommonBot.Inventory;
using Styx.Localization;
using Styx.Plugins;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;


namespace Styx.Bot.Plugins.AutoEquip2
{
    public partial class AutoEquip : HBPlugin
    {
        #region Overrides of HBPlugin

        /// <summary>Initializes this plugin after it has been properly loaded.</summary>
        public override void Initialize()
        {
            Lua.Events.AttachEvent("UNIT_INVENTORY_CHANGED", DoCheck);
            Lua.Events.AttachEvent("LOOT_CLOSED", DoCheck);
            Lua.Events.AttachEvent("START_LOOT_ROLL", HandleLootRoll);
            Lua.Events.AttachEvent("CONFIRM_LOOT_ROLL", HandleConfirmLootRoll);
            Lua.Events.AttachEvent("CONFIRM_DISENCHANT_ROLL", HandleConfirmLootRoll);
        }

        /// <summary>Dispose of this plugin, cleaning up any resources it uses.</summary>
        public override void Dispose()
        {
            Lua.Events.DetachEvent("UNIT_INVENTORY_CHANGED", DoCheck);
            Lua.Events.DetachEvent("LOOT_CLOSED", DoCheck);
            Lua.Events.DetachEvent("START_LOOT_ROLL", HandleLootRoll);
            Lua.Events.DetachEvent("CONFIRM_LOOT_ROLL", HandleConfirmLootRoll);
            Lua.Events.DetachEvent("CONFIRM_DISENCHANT_ROLL", HandleConfirmLootRoll);
        }

        /// <summary>
        /// The text of the button if the plugin wants it. [Default: "Settings"]
        /// </summary>
        public override string ButtonText { get { return "Configuration"; } }

        /// <summary>
        /// Does this plugin want a button? If set to true, the button in the plugin manager will be enabled. [Default: false]
        /// </summary>
        public override bool WantButton { get { return true; } }

        /// <summary>
        /// Called when the user presses the button while having this plugin selected. The plugin can start a thread, show a form, or just do what the hell it wants.
        /// </summary>
        public override void OnButtonPress()
        {
            new FormSettings().ShowDialog();
        }

        private readonly WaitTimer _itemCheckTimer = WaitTimer.TenSeconds;
        /// <summary>
        /// Called everytime the engine pulses.
        /// </summary>
        public override void Pulse()
        {
            if (!_itemCheckTimer.IsFinished)
                return;

            _itemCheckTimer.Reset();

            if (AutoEquipSettings.Instance.WeaponStyle == WeaponStyle.None)
            {
                Log("You have not selected a weapon style yet. Please open the configuration and check your settings.");
                return;
            }

            DoCheck(null, null);
        }

        /// <summary>
        /// The name of this plugin.
        /// </summary>
        public override string Name { get { return Globalization.AutoEquip_Name; } }

        /// <summary>
        /// The author of this plugin.
        /// </summary>
        public override string Author { get { return "bambam922 AEDEV"; } }

        /// <summary>
        /// The version of the plugin.
        /// </summary>
        public override Version Version { get { return new Version(2, 0, 0); } }

        #endregion

        private readonly WeightSetEx _weightSet = WeightSetEx.CurrentWeightSet;
        private bool ClassCanUseWeapon(WoWItem item)
        {
            return true;
        }

        private void DoCheck(object sender, LuaEventArgs e)
        {
            // Don't run anything if AutoEquip is not enabled
            var plugin = PluginManager.Plugins.FirstOrDefault(p => p.Name == Name);
            if (plugin != null && !plugin.Enabled)
            {
                return;
            }

            if (_weightSet == null)
            {
                LogDebug("No weight set was found for your character.{0}Ensure that the 'Data\\Weight Sets\\' folder exists and has valid weight sets.", Environment.NewLine);
                return;
            }

            if (!TreeRoot.IsRunning || StyxWoW.Me.Combat || StyxWoW.Me.IsDead || StyxWoW.Me.IsGhost || Battlegrounds.IsInsideBattleground)
            {
                return;
            }

            // So.. beautiful!
            var equippableItems = (from item in StyxWoW.Me.BagItems
                                   where item != null
                                   where EquipQualities.Contains(item.Quality)
                                   let info = item.ItemInfo
                                   where
                                        // BoE Items check here. 
                                       info.Bond != WoWItemBondType.OnEquip
                                       || info.Bond == WoWItemBondType.OnEquip && item.IsSoulbound
                                       || EquipBoEQualities.Contains(item.Quality)
                                   let inventoryType = info.InventoryType
                                   let slots = InventoryManager.GetInventorySlotsByEquipSlot(inventoryType)
                                   where
                                        // Ignored Types checks here. Don't check items with ignore types unless the slot is empty.
                                       !IgnoreTypes.Contains(inventoryType) ||
                                       slots.Any(s => EquippedItems[s] == null)
                                   where 
                                       ((AutoEquipSettings.Instance.WeaponStyle == WeaponStyle.TwoHander &&
                                         inventoryType == InventoryType.TwoHandWeapon) ||

                                        (AutoEquipSettings.Instance.WeaponStyle == WeaponStyle.OneHanderAndShield &&
                                        (EquippedItems[InventorySlot.MainHandSlot] == null || 
                                        EquippedItems[InventorySlot.MainHandSlot].ItemInfo.InventoryType != InventoryType.TwoHandWeapon || 
                                        inventoryType != InventoryType.Holdable && inventoryType != InventoryType.Shield) &&
                                         (inventoryType == InventoryType.Shield ||
                                          inventoryType == InventoryType.Holdable ||
                                          inventoryType == InventoryType.WeaponMainHand ||
                                          inventoryType == InventoryType.Weapon)) ||

                                        (AutoEquipSettings.Instance.WeaponStyle == WeaponStyle.DualWield
                                        && (StyxWoW.Me.Class != WoWClass.Shaman || StyxWoW.Me.Level >= 10)
                                        && (StyxWoW.Me.Class != WoWClass.Warrior || StyxWoW.Me.Level >= 10)
                                        && (StyxWoW.Me.Class != WoWClass.Monk || StyxWoW.Me.Level >= 10)
                                        && (inventoryType == InventoryType.Weapon ||
                                          inventoryType == InventoryType.WeaponMainHand ||
                                          inventoryType == InventoryType.WeaponOffHand)) ||

                                        (AutoEquipSettings.Instance.WeaponStyle == WeaponStyle.Ranged &&
                                         (inventoryType == InventoryType.RangedRight ||
                                          inventoryType == InventoryType.Ranged))

                                        ) && StyxWoW.Me.CanEquipItem(item)
                                       // After we get what items to use make sure we can even use it

                                       ||

                                       (inventoryType != InventoryType.Shield &&
                                        inventoryType != InventoryType.Holdable &&
                                        inventoryType != InventoryType.Ranged &&
                                        inventoryType != InventoryType.RangedRight &&
                                        inventoryType != InventoryType.TwoHandWeapon &&
                                        inventoryType != InventoryType.Weapon &&
                                        inventoryType != InventoryType.WeaponMainHand &&
                                        inventoryType != InventoryType.WeaponOffHand &&
                                        StyxWoW.Me.CanEquipItem(item))
                                   // Checks everything but weapons now

                                   select item).ToList();

            if (equippableItems.Count == 0)
                return;

            if (AutoEquipSettings.Instance.AutoEquipItems)
            {
                CheckForItems(equippableItems);
            }
            if (AutoEquipSettings.Instance.AutoEquipBags)
            {
                CheckForBag(equippableItems);
            }
        }




        /// <summary> </summary>
        private void HandleLootRoll(object sender, LuaEventArgs e)
        {
            // Don't run anything if AutoEquip is not enabled
            var plugin = PluginManager.Plugins.FirstOrDefault(p => p.Name == Name);
            if (plugin != null && !plugin.Enabled)
            {
                return;
            }

            if (!AutoEquipSettings.Instance.RollForLootInDungeons)
                return;

            Log("Loot roll in progress");

            string rollId = e.Args[0].ToString();
            string itemLink = Lua.GetReturnVal<string>("return GetLootRollItemLink(" + rollId + ")", 0);
            string[] splitted = itemLink.Split(':');

            uint itemId;
            if (string.IsNullOrEmpty(itemLink) || (splitted.Length == 0 || splitted.Length < 2) || (!uint.TryParse(splitted[1], out itemId) || itemId == 0))
            {
                Log("Parsing ItemLink for lootroll failed!");
                Log("ItemLink:{0}", itemLink);
                return;
            }

            ItemInfo rollItemInfo = ItemInfo.FromId(itemId);
            if (rollItemInfo == null)
            {
                Log("Retrieving item info for roll item failed");
                Log("Item Id:{0} ItemLink:{1}", itemId, itemLink);
                return;
            }



            // The name of the roll item.
            string rollItemName = rollItemInfo.Name;
            // Score of the item being rolled for.
            var newstats = new ItemStats(itemLink);

            float rollItemScore = _weightSet.EvaluateItem(rollItemInfo, newstats);
            // Score the equipped item if any. otherwise 0
            float bestEquipItemScore = float.MaxValue;
            // The best slot
            InventorySlot bestSlot = InventorySlot.None;

            var inventorySlots = InventoryManager.GetInventorySlotsByEquipSlot(rollItemInfo.EquipSlot);
            foreach (InventorySlot slot in inventorySlots)
            {
                WoWItem equipped = EquippedItems[slot];

                if (equipped != null)
                {

                    var newscore = _weightSet.EvaluateItem(equipped, AutoEquipSettings.Instance.IncludeEnchants);
                    if (newscore < bestEquipItemScore)
                    {
                        bestSlot = slot;
                        bestEquipItemScore = newscore;
                    }
                }
                else
                {
                    bestSlot = slot;
                    bestEquipItemScore = -1;
                }
            }

            if (bestEquipItemScore != float.MaxValue)
                Log("Equipped item in slot:{0} scored {1} while loot-roll item scored:{2}", bestSlot, bestEquipItemScore, rollItemScore);


            //Make sure item we are rolling on contains our primary stat

            //Find my primary stat
            float str, intel, agi;
            var primary = StatTypes.Agility;
            var other1 = StatTypes.Strength;
            var other2 = StatTypes.Intellect;
            str = _weightSet.GetStatScore(Stat.Strength, 1);
            intel = _weightSet.GetStatScore(Stat.Intellect, 1);
            agi = _weightSet.GetStatScore(Stat.Agility, 1);

            if (str > intel && str > agi)
            {
                primary = StatTypes.Strength;
                other1 = StatTypes.Intellect;
                other2 = StatTypes.Agility;
            }
            else if (intel > str && intel > agi)
            {
                primary = StatTypes.Intellect;
                other1 = StatTypes.Strength;
                other2 = StatTypes.Agility;
            }


            //Now check and make sure the item has our stat on it.
            if (!newstats.Stats.ContainsKey(primary) && (newstats.Stats.ContainsKey(other1) || newstats.Stats.ContainsKey(other2)) && rollItemInfo.EquipSlot != InventoryType.Ranged)
            {
                Log("New item did not contain our primary stat of:{0} so we are not rolling need.", primary);
                bestSlot = InventorySlot.None;
            }


            // Check if the item is better than the currently equipped item. 
            if (bestEquipItemScore < rollItemScore && bestSlot != InventorySlot.None)
            {
                var inventoryTypes = GetInventoryTypesForWeaponStyle(AutoEquipSettings.Instance.WeaponStyle);

                var miscArmorType = new[]
                {
                    InventoryType.Cloak,
                    InventoryType.Trinket,
                    InventoryType.Neck,
                    InventoryType.Finger,
                };


                //If we are less then level 50, we can wear pretty much whatever armor type we want if it has the right stats
                // Make sure we only roll need if the item is of the wanted armor class for this player (or if it's a cloak, trinket, ring or neck).
                bool needRollForArmor =
                    rollItemInfo.ItemClass == WoWItemClass.Armor &&
                    (rollItemInfo.ArmorClass == _weightSet.GetWantedArmorClass() || miscArmorType.Contains(rollItemInfo.InventoryType));

                // Make sure we only roll need if the item is a weapon we might use.
                bool needRollForWeapon =
                    rollItemInfo.ItemClass == WoWItemClass.Weapon &&
                    inventoryTypes.Contains(rollItemInfo.EquipSlot);

                bool canNeed = Lua.GetReturnVal<bool>("return GetLootRollItemInfo(" + rollId + ")", 5);
                if ((needRollForArmor || needRollForWeapon) && canNeed)
                {
                    Log("{0} scored {1} while your equiped item only scored {2} - Rolling Need", rollItemName, rollItemScore, bestEquipItemScore);
                    Lua.DoString("RollOnLoot(" + rollId + ", 1)");
                    return;
                }
            }

            bool canDisenchant = Lua.GetReturnVal<bool>("return GetLootRollItemInfo(" + rollId + ")", 7);
            if (AutoEquipSettings.Instance.RollForLootDE && canDisenchant && (((rollItemInfo.Bond == WoWItemBondType.OnPickup) && DisenchantBoPQualities.Contains(rollItemInfo.Quality)) ||
                ((rollItemInfo.Bond == WoWItemBondType.OnEquip) && DisenchantBoEQualities.Contains(rollItemInfo.Quality))))
            {
                Log("Rolling for disenchant");
                Lua.DoString("RollOnLoot(" + rollId + ", 3)");
                return;
            }

            Log("Primary stat: " + primary );
            Log("bestEquipItemScore: " + bestEquipItemScore);
            Log("rollItemScore: " + rollItemScore);
            Log("bestSlot: " + bestSlot);

            Log("Rolling Greed");
            Lua.DoString("RollOnLoot(" + rollId + ", 2)");
        }
        List<WoWItemArmorClass> Types = new List<WoWItemArmorClass>() { WoWItemArmorClass.Cloth, WoWItemArmorClass.Leather, WoWItemArmorClass.Mail, WoWItemArmorClass.Plate };
        private bool CanWear(WoWItemArmorClass item)
        {
            if (StyxWoW.Me.Level < 50)
            {
                int x = -1;
                for (int i = 0; i < 3; i++)
                {
                    if (Types[i] == _weightSet.GetWantedArmorClass())
                    {
                        x = i;
                        break;
                    }
                }
                if (x > 0)
                {
                    for (int i = 0; i < x; i++)
                    {
                        if (Types[i] == item)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary> Handles the 'CONFIRM_LOOT_ROLL' and 'CONFIRM_DISENCHANT_ROLL' event. Fires when you try to roll "need" or "greed" for an item which Binds on Pickup. </summary>
        private void HandleConfirmLootRoll(object sender, LuaEventArgs e)
        {
            // Don't run anything if AutoEquip is not enabled
            var plugin = PluginManager.Plugins.FirstOrDefault(p => p.Name == Name);
            if (plugin != null && !plugin.Enabled)
            {
                return;
            }

            if (!AutoEquipSettings.Instance.RollForLootInDungeons)
                return;

            //Log("Confirming Loot Roll");

            double rollId = (double)e.Args[0];
            double rollType = (double)e.Args[1];
            //Log("RollId:{0} RollType:{1}", rollId, rollType);

            Lua.DoString("ConfirmLootRoll({0},{1})", rollId, rollType);
        }

        /// <summary>
        /// Retrives the item id in a item-link using regex.
        /// </summary>
        /// <param name="itemLink">itemlink to parse</param>
        private static uint? GetItemIdFromItemLink(string itemLink)
        {
            var regex = new Regex(@"([0-9]\d\d+)", RegexOptions.IgnoreCase);
            var match = regex.Match(itemLink);

            uint itemId;
            if (uint.TryParse(match.Value, out itemId))
            {
                return itemId;
            }

            return null;
        }

        private static List<InventoryType> GetInventoryTypesForWeaponStyle(WeaponStyle style)
        {
            var slots = new List<InventoryType>();
            switch (style)
            {
                case WeaponStyle.DualWield:
                    slots.Add(InventoryType.WeaponMainHand);
                    slots.Add(InventoryType.WeaponOffHand);
                    slots.Add(InventoryType.Weapon);
                    break;

                case WeaponStyle.OneHanderAndShield:
                    slots.Add(InventoryType.WeaponMainHand);
                    slots.Add(InventoryType.Weapon);
                    slots.Add(InventoryType.Shield);
                    break;

                case WeaponStyle.TwoHander:
                    slots.Add(InventoryType.TwoHandWeapon);
                    break;
            }

            return slots;
        }

        #region Check for items

        private void CheckForItems(IEnumerable<WoWItem> equippableItems)
        {
            ILookup<InventoryType, WoWItem> categorizedItems = equippableItems.ToLookup(item => item.ItemInfo.InventoryType);

            foreach (var grouping in categorizedItems.Where(grouping => grouping.Key != InventoryType.Bag))
            {
                float bestEquipItemScore;
                WoWItem bestEquipItem = FindBestItem(grouping.OrderByDescending(i => i.ItemInfo.Level), out bestEquipItemScore);
                if (bestEquipItem == null)
                    continue;

                var equipSlots = DecideEquipmentSlots(bestEquipItem).ToList();

                if (equipSlots.Any(ret => ret == InventorySlot.Trinket0Slot || ret == InventorySlot.Trinket1Slot) && StyxWoW.Me.Level < 90)
                {
                    bestEquipItemScore = 0;
                    bestEquipItemScore += bestEquipItem.ItemInfo.Level;
                }

                float lowestItemScore;
                InventorySlot bestSlot = FindBestEquipmentSlot(equipSlots, out lowestItemScore);
                if (bestSlot == InventorySlot.None)
                {
                    //LogDebug("I'm not equipping item {0} of inventory type {1} as there are no slots to equip it into", bestEquipItem.Name, bestEquipItem.ItemInfo.InventoryType);
                    continue;
                }

                // Ignore the item if the best slot is offhand and it isn't a shield if we have a one hand weapon + shield weapon style.
                if (AutoEquipSettings.Instance.WeaponStyle == WeaponStyle.OneHanderAndShield && bestSlot == InventorySlot.SecondaryHandSlot && 
                    bestEquipItem.ItemInfo.InventoryType != InventoryType.Shield && bestEquipItem.ItemInfo.InventoryType != InventoryType.Holdable)
                    continue;

                WoWItem equippedItem = EquippedItems[equipSlots.FirstOrDefault()];

                if (equippedItem != null && equippedItem.ItemInfo.InventoryType == InventoryType.TwoHandWeapon &&
                    (bestEquipItem.ItemInfo.InventoryType == InventoryType.Shield || bestEquipItem.ItemInfo.InventoryType == InventoryType.Holdable))
                {
                    continue;
                }
                
                if (bestEquipItemScore <= lowestItemScore)
                    continue;

                if (lowestItemScore == float.MinValue)
                    Log("Equipping {2} \"{0}\" into empty slot {1}", bestEquipItem.Name, bestSlot, bestEquipItem.ItemInfo.InventoryType);
                else
                    Log("Equipping {4} \"{0}\" instead of \"{1}\" - it scored {2} while the old scored {3}", bestEquipItem.Name, EquippedItems[bestSlot].Name, bestEquipItemScore, lowestItemScore, bestEquipItem.ItemInfo.InventoryType);

                EquipItemIntoSlot(bestEquipItem, bestSlot);
                StyxWoW.Sleep(500);
                return;
            }
        }

        private InventorySlot FindBestEquipmentSlot(IEnumerable<InventorySlot> equipSlots, out float lowestItemScore)
        {
            InventorySlot bestSlot = InventorySlot.None;

            float lowestEquippedItemScore = float.MaxValue;
            foreach (InventorySlot inventorySlot in equipSlots)
            {
                WoWItem equippedItem = EquippedItems[inventorySlot];
                if (equippedItem == null)
                {
                    lowestItemScore = float.MinValue;
                    return inventorySlot;
                }

                if (AutoEquipSettings.Instance.ProtectedSlots.Contains(inventorySlot))
                {
                    //Log(true, "I'm not equipping into equipment slot {0} as it is protected", inventorySlot);
                    continue;
                }

                if (!EquippedItems.ContainsKey(inventorySlot))
                {
                    Log(true, "InventorySlot {0} is unknown! Please report this to MaiN.", inventorySlot);
                    continue;
                }

                if (!AutoEquipSettings.Instance.ReplaceHeirlooms && equippedItem.Quality == WoWItemQuality.Heirloom)
                {
                    //Log(false, "I'm not equipping anything into {0} as I can't replace heirloom items!", inventorySlot);
                    continue;
                }

                if (equippedItem.ItemInfo.Bond == WoWItemBondType.Quest)
                {
                    //Log(false, "I'm not equipping anything into {0} as I shouldn't replace quest items!", inventorySlot);
                    continue;
                }


                // Compute the score for the current equipped item.
                float itemScore = _weightSet.EvaluateItem(equippedItem, AutoEquipSettings.Instance.IncludeEnchants);

                //Trinkets should be based off ilvl under lvl 90
                if ((inventorySlot == InventorySlot.Trinket0Slot || inventorySlot == InventorySlot.Trinket1Slot) &&
                    StyxWoW.Me.Level < 90)
                {
                    itemScore += equippedItem.ItemInfo.Level;
                }

                if (equippedItem.ItemInfo.ItemClass == WoWItemClass.Weapon 
                    || equippedItem.ItemInfo.InventoryType == InventoryType.Shield 
                    || equippedItem.ItemInfo.InventoryType == InventoryType.Ranged 
                    || equippedItem.ItemInfo.InventoryType == InventoryType.RangedRight)
                {

                    // Set the score to zero if the item is a two hand weapon and the weapon style doesn't match, kinda hackish but it works ;p.
                    if (AutoEquipSettings.Instance.WeaponStyle != WeaponStyle.TwoHander &&
                        equippedItem.ItemInfo.InventoryType == InventoryType.TwoHandWeapon)
                        itemScore = -1;

                    // Set the score to zero if the item is a shield and the weapon style doesn't match, kinda hackish but it works ;p.
                    if (AutoEquipSettings.Instance.WeaponStyle != WeaponStyle.OneHanderAndShield &&
                        equippedItem.ItemInfo.InventoryType == InventoryType.Shield)
                        itemScore = -1;

                    // Set the score to zero if the item is not a two hand weapon and the weapon style doesn't match, kinda hackish but it works ;p.
                    if (AutoEquipSettings.Instance.WeaponStyle == WeaponStyle.TwoHander &&
                        equippedItem.ItemInfo.InventoryType != InventoryType.TwoHandWeapon)
                        itemScore = -1;


                    if (AutoEquipSettings.Instance.WeaponStyle == WeaponStyle.OneHanderAndShield &&
                        equippedItem.ItemInfo.InventoryType != InventoryType.Weapon &&
                        equippedItem.ItemInfo.InventoryType != InventoryType.WeaponMainHand &&
                        equippedItem.ItemInfo.InventoryType != InventoryType.WeaponOffHand &&
                        equippedItem.ItemInfo.InventoryType != InventoryType.Shield)
                        itemScore = -1;

                    if (AutoEquipSettings.Instance.WeaponStyle == WeaponStyle.DualWield &&
                        equippedItem.ItemInfo.InventoryType != InventoryType.Weapon &&
                        equippedItem.ItemInfo.InventoryType != InventoryType.WeaponMainHand &&
                        equippedItem.ItemInfo.InventoryType != InventoryType.WeaponOffHand)
                        itemScore = -1;

                    if (AutoEquipSettings.Instance.WeaponStyle == WeaponStyle.Ranged &&
                        equippedItem.ItemInfo.InventoryType != InventoryType.TwoHandWeapon &&
                        equippedItem.ItemInfo.InventoryType != InventoryType.Ranged &&
                        equippedItem.ItemInfo.InventoryType != InventoryType.RangedRight)
                        itemScore = -1;
                }


                var inventoryType = equippedItem.ItemInfo.InventoryType;
                if (inventoryType == InventoryType.Weapon || inventoryType == InventoryType.TwoHandWeapon ||
                    inventoryType == InventoryType.WeaponMainHand || inventoryType == InventoryType.WeaponOffHand)
                {
                    if ((FindWeaponType(equippedItem) & AutoEquipSettings.Instance.WeaponType) == 0)
                        itemScore = -1;
                }

                if (itemScore < lowestEquippedItemScore)
                {
                    bestSlot = inventorySlot;
                    lowestEquippedItemScore = itemScore;
                }
            }

            lowestItemScore = lowestEquippedItemScore;
            return bestSlot;
        }

        private IEnumerable<InventorySlot> DecideEquipmentSlots(WoWItem item)
        {
            List<InventorySlot> slots = InventoryManager.GetInventorySlotsByEquipSlot(item.ItemInfo.InventoryType);

            if (slots.Contains(InventorySlot.SecondaryHandSlot))
            {
                if(item.ItemInfo.ItemClass == WoWItemClass.Weapon && AutoEquipSettings.Instance.WeaponStyle == WeaponStyle.OneHanderAndShield)
                {
                    slots.Clear();
                    slots.Add(InventorySlot.MainHandSlot);
                }

                WoWItem mainHand = EquippedItems[InventorySlot.MainHandSlot];
                if (mainHand != null)
                {
                    InventoryType type = mainHand.ItemInfo.InventoryType;
                    if (type == InventoryType.TwoHandWeapon)
                    {
                        if (item.ItemInfo.InventoryType == InventoryType.Shield)
                        {
                            slots.Clear();
                            slots.Add(InventorySlot.SecondaryHandSlot);
                        }
                        else
                        {
                            // Fury can use 2x 2H weps by default.
                            var hasTitansGrip = StyxWoW.Me.Specialization == WoWSpec.WarriorFury && StyxWoW.Me.Level >= 38;


                            // This item takes up two slots - we have to ensure that the specific item will be checked against the main-hand.
                            if (!hasTitansGrip)
                            {
                                slots.Clear();
                                slots.Add(InventorySlot.MainHandSlot);
                            }
                        }
                    }
                }
            }

            return slots;
        }


        private WoWItem FindBestItem(IEnumerable<WoWItem> items, out float bestScore)
        {
            WoWItem bestEquipItem = null;
            float bestEquipItemScore = float.MinValue;
            foreach (WoWItem item in items)
            {
                var inventorySlots = InventoryManager.GetInventorySlotsByEquipSlot(item.ItemInfo.InventoryType);
                bool shouldIgnore = false;

                if (!AutoEquipSettings.Instance.ReplaceHeirlooms)
                {
                    foreach (InventorySlot slot in inventorySlots)
                    {
                        if (EquippedItems[slot] != null)
                        {
                            if (EquippedItems[slot].Quality == WoWItemQuality.Heirloom)
                                shouldIgnore = true;
                        }
                    }
                }

                var inventoryType = item.ItemInfo.InventoryType;
                if (inventoryType == InventoryType.Weapon || inventoryType == InventoryType.TwoHandWeapon ||
                    inventoryType == InventoryType.WeaponMainHand || inventoryType == InventoryType.WeaponOffHand)
                {
                    // WeaponClass checks for the setting
                    if ((AutoEquipSettings.Instance.WeaponType & FindWeaponType(item)) == 0)
                    {
                        if (inventorySlots.All(s => EquippedItems[s] != null))
                            shouldIgnore = true;
                    }
                }

                if (shouldIgnore)
                    continue;

                float itemScore = _weightSet.EvaluateItem(item, AutoEquipSettings.Instance.IncludeEnchants);
                if (itemScore <= bestEquipItemScore)
                    continue;

                bestEquipItemScore = itemScore;
                bestEquipItem = item;
            }

            bestScore = bestEquipItemScore == float.MinValue ? 0f : bestEquipItemScore;
            return bestEquipItem;
        }

        private static WeaponType FindWeaponType(WoWItem item)
        {
            var weaponClass = item.ItemInfo.WeaponClass;

            switch (weaponClass)
            {
                case WoWItemWeaponClass.Axe:
                case WoWItemWeaponClass.AxeTwoHand:
                    return WeaponType.Axe;
                case WoWItemWeaponClass.Mace:
                case WoWItemWeaponClass.MaceTwoHand:
                    return WeaponType.Mace;
                case WoWItemWeaponClass.Polearm:
                    return WeaponType.Polearm;
                case WoWItemWeaponClass.Sword:
                case WoWItemWeaponClass.SwordTwoHand:
                    return WeaponType.Sword;
                case WoWItemWeaponClass.Staff:
                    return WeaponType.Staff;
                case WoWItemWeaponClass.Fist:
                    return WeaponType.Fist;
                case WoWItemWeaponClass.Dagger:
                    return WeaponType.Dagger;
                case WoWItemWeaponClass.Spear:
                    return WeaponType.Spear;
                case WoWItemWeaponClass.Bow:
                case WoWItemWeaponClass.Gun:
                case WoWItemWeaponClass.Wand:
                    return WeaponType.Ranged;
            }

            return WeaponType.None;
        }

        private void CheckForBag(IList<WoWItem> items)
        {
            WoWItem bestBag = FindBestBag(items);
            if (bestBag == null)
                return;

            for (uint b = 0; b < 4; b++)
            {
                if (StyxWoW.Me.GetBagAtIndex(b) != null)
                    continue;

                Log("Equipping bag {0}", bestBag.Name);
                EquipItem(bestBag);
                break;
            }
        }

        /// <summary>
        /// Returns the best bag we have, null if none was found
        /// </summary>
        /// <param name="items">items to compare with</param>
        public WoWItem FindBestBag(IList<WoWItem> items)
        {
            WoWItem bestBag = null;
            int mostSlots = int.MinValue;
            for (int i = 0; i < items.Count; i++)
            {
                WoWItem item = items[i];

                ItemInfo itemInfo = item.ItemInfo;

                if (itemInfo == null || itemInfo.InternalInfo.BagSlots <= 0 || itemInfo.InternalInfo.BagFamilyId != 0 || itemInfo.InventoryType != InventoryType.Bag)
                    continue;

                if (itemInfo.InternalInfo.BagSlots <= mostSlots)
                    continue;

                bestBag = item;
                mostSlots = itemInfo.InternalInfo.BagSlots;
            }

            return bestBag;
        }

        /// <summary>
        /// Returns a decitionary containing all equipped items.
        /// </summary>
        public Dictionary<InventorySlot, WoWItem> EquippedItems
        {
            get
            {
                var equipped = new Dictionary<InventorySlot, WoWItem>();
                WoWItem[] items = StyxWoW.Me.Inventory.Equipped.Items;

                equipped.Clear();
                for (int i = 0; i < 23; i++)
                    equipped.Add((InventorySlot)(i + 1), items[i]);

                return equipped;
            }
        }

        #endregion

        #region Equip Item

        private static void EquipItem(int bagIndex, int bagSlot, int targetSlot)
        {
            Lua.DoString(
                "ClearCursor(); PickupContainerItem({0}, {1}); EquipCursorItem({2}); if StaticPopup1Button1 and StaticPopup1Button1:IsVisible() then StaticPopup1Button1:Click(); end;",
                bagIndex + 1, bagSlot + 1, targetSlot);
        }

        private static void EquipItemIntoSlot(WoWItem item, InventorySlot slot)
        {
            EquipItem(item.BagIndex, item.BagSlot, (int)slot);
        }

        private static void EquipItem(WoWItem item)
        {
            EquipItem(item.BagIndex, item.BagSlot, (int)item.ItemInfo.EquipSlot);
        }

        #endregion


        #region Settings

        /// <summary>
        /// Returns a HashSet with qualities we want to equip.
        /// </summary>
        public HashSet<WoWItemQuality> EquipQualities
        {
            get
            {
                var settings = AutoEquipSettings.Instance;
                var temp = new HashSet<WoWItemQuality>();

                if (settings.EquipPurples)
                    temp.Add(WoWItemQuality.Epic);

                if (settings.EquipBlues)
                    temp.Add(WoWItemQuality.Rare);

                if (settings.EquipGreens)
                    temp.Add(WoWItemQuality.Uncommon);

                if (settings.EquipWhites)
                    temp.Add(WoWItemQuality.Common);

                if (settings.EquipGrays)
                    temp.Add(WoWItemQuality.Poor);

                if (settings.EquipHeirlooms)
                    temp.Add(WoWItemQuality.Heirloom);

                return temp;
            }
        }

        /// <summary>
        /// Returns a HashSet with BoE qualities we want to equip.
        /// </summary>
        public HashSet<WoWItemQuality> EquipBoEQualities
        {
            get
            {
                var settings = AutoEquipSettings.Instance;
                var temp = new HashSet<WoWItemQuality>();

                if (settings.EquipBoEPurples)
                    temp.Add(WoWItemQuality.Epic);

                if (settings.EquipBoEBlues)
                    temp.Add(WoWItemQuality.Rare);

                if (settings.EquipBoEGreens)
                    temp.Add(WoWItemQuality.Uncommon);

                if (settings.EquipWhites)
                    temp.Add(WoWItemQuality.Common);

                if (settings.EquipGrays)
                    temp.Add(WoWItemQuality.Poor);

                if (settings.EquipHeirlooms)
                    temp.Add(WoWItemQuality.Heirloom);

                return temp;
            }
        }

        /// <summary>
        /// Returns a HashSet with BoE quality types we want to disenchant.
        /// </summary>
        public HashSet<WoWItemQuality> DisenchantBoEQualities
        {
            get
            {
                var settings = AutoEquipSettings.Instance;
                var temp = new HashSet<WoWItemQuality>();

                if (settings.DisenchantBoEGreens)
                    temp.Add(WoWItemQuality.Uncommon);

                if (settings.DisenchantBoEBlues)
                    temp.Add(WoWItemQuality.Rare);

                if (settings.DisenchantBoEPurples)
                    temp.Add(WoWItemQuality.Epic);

                return temp;
            }
        }

        /// <summary>
        /// Returns a HashSet with BoP quality types we want to disenchant.
        /// </summary>
        public HashSet<WoWItemQuality> DisenchantBoPQualities
        {
            get
            {
                var settings = AutoEquipSettings.Instance;
                var temp = new HashSet<WoWItemQuality>();

                if (settings.DisenchantBoPBlues)
                    temp.Add(WoWItemQuality.Rare);

                if (settings.DisenchantBoPPurples)
                    temp.Add(WoWItemQuality.Epic);

                return temp;
            }
        }


        /// <summary>
        /// Returns a HashSet with inventory types we want to ignore.
        /// </summary>
        public HashSet<InventoryType> IgnoreTypes
        {
            get
            {
                var settings = AutoEquipSettings.Instance;

                return settings.IgnoreInvTypes == null ?
                    new HashSet<InventoryType>() :
                    new HashSet<InventoryType>(settings.IgnoreInvTypes);
            }

            set { AutoEquipSettings.Instance.IgnoreInvTypes = value.ToArray(); }
        }

        #endregion
    }

    public partial class AutoEquip
    {
        private static void Log(bool isDebug, string format, params object[] args)
        {
            if (isDebug)
                Logging.Write(LogLevel.Diagnostic, "[AutoEquip]: {0}", string.Format(format, args));
            else
                Logging.Write("[AutoEquip]: {0}", string.Format(format, args));
        }

        private void Log(string format, params object[] args)
        {
            Log(false, format, args);
        }

        private void Log(string message)
        {
            Log(false, message);
        }

        private void LogDebug(string format, params object[] args)
        {
            Log(true, format, args);
        }

        private void LogDebug(string message)
        {
            Log(true, message);
        }

        //private IEnumerable<WoWItem> GetEquippableItems()
        //{
        //    var equippableItems = new List<WoWItem>();
        //    foreach (WoWItem item in StyxWoW.Me.BagItems)
        //    {
        //        if (item == null)
        //            continue;

        //        if (!EquipQualities.Contains(item.Quality))
        //            continue;

        //        ItemInfo info = item.ItemInfo;

        //        if(info.Bond == WoWItemBondType.OnEquip && !EquipBoEQualities.Contains(item.Quality))
        //            continue;

        //        InventoryType inventoryType = info.InventoryType;
        //        if (IgnoreTypes.Contains(inventoryType))
        //            continue;

        //        if (inventoryType == InventoryType.TwoHandWeapon && AutoEquipSettings.Instance.WeaponStyle != WeaponStyle.TwoHander)
        //            continue;

        //        if (inventoryType == InventoryType.Shield && AutoEquipSettings.Instance.WeaponStyle != WeaponStyle.OneHanderAndShield)
        //            continue;

        //        if (!StyxWoW.Me.CanEquipItem(item))
        //            continue;

        //        equippableItems.Add(item);
        //    }

        //    return equippableItems;
        //}
    }
}