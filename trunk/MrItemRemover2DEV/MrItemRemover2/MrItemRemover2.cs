/*
 * Mr.ItemRemover2 - Created by CodenameGamma - 1-31-11 - For WoW Version 4.0.3
 * 1.6 Update by Bambam922
 * www.thebuddyforum.com
 * This is a free plugin and should not be sold or repackaged.
 * Donations accepted.
 * Version 1.6 for WoW Version 5.4 +
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;
using Styx;
using Styx.Common;
using Styx.Common.Helpers;
using Styx.Plugins;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;

namespace MrItemRemover2
{
    public partial class MrItemRemover2 : HBPlugin
    {
// ReSharper disable InconsistentNaming
        private const string _debug = "Mr.Itemremover2 DEBUG";
        private const string _name = "Mr.ItemRemover2DEV 1.6";
// ReSharper restore InconsistentNaming
        private readonly WaitTimer _checkTimer = new WaitTimer(TimeSpan.FromMinutes(MrItemRemover2Settings.Instance.Time));

        private readonly string _bagListPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                            string.Format(
                                                                @"Plugins/MrItemRemover2DEV/MrItemRemover2/ItemNameBagList.txt"));
        private readonly string _keepListPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                             string.Format(
                                                                @"Plugins/MrItemRemover2DEV/MrItemRemover2/ItemNameKeepList.txt"));
        private readonly string _opnListPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                            string.Format(
                                                                @"Plugins/MrItemRemover2DEV/MrItemRemover2/ItemNameOpnList.txt"));
        private readonly string _removeListPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                               string.Format(
                                                                @"Plugins/MrItemRemover2DEV/MrItemRemover2/ItemNameRemoveList.txt"));
        private readonly string _sellListPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                             string.Format(
                                                                @"Plugins/MrItemRemover2DEV/MrItemRemover2/ItemNameSellList.txt"));

        public List<string> BagList = new List<string>();
        public List<string> InventoryList = new List<string>();
        public List<string> ItemNameSell = new List<string>();
        public List<string> KeepList = new List<string>();
        public List<string> OpnList = new List<string>();
        public List<string> ItemName = new List<string>();

        //Normal Stuff.
        public override string Name
        {
            get { return _name; }
        }

        public override string Author
        {
            get { return "CnG & Bambam922"; }
        }

        public override Version Version
        {
            get { return new Version(1, 6); }
        }

        public override bool WantButton
        {
            get { return true; }
        }

        public override string ButtonText
        {
            get { return _name; }
        }

        public bool ManualCheckRequested { get; set; }
        private bool EnableCheck { get; set; }
        private bool IsInitialized { get; set; }

        private static LocalPlayer Me
        {
            get { return StyxWoW.Me; }
        }
        
        public override void OnButtonPress()
        {
            if (!IsInitialized)
            {
                slog("Not finished initializing");
                return;
            }

            var form = new MrItemRemover2Gui(this);
            form.ShowDialog();
        }

// ReSharper disable InconsistentNaming
        public static void slog(string format, params object[] args)
// ReSharper restore InconsistentNaming
        {
            slog(Colors.SkyBlue, format, args);
        }
// ReSharper disable InconsistentNaming
        public static void slog(Color color, string format, params object[] args)
// ReSharper restore InconsistentNaming
        {
            Logging.Write(color, "[" + _name + "]: " + format, args);
        }
// ReSharper disable InconsistentNaming
        public static void dlog(string format, params object[] args)
// ReSharper restore InconsistentNaming
        {
            dlog(Colors.Yellow, format, args);
        }
// ReSharper disable InconsistentNaming
        public static void dlog(Color color, string format, params object[] args)
// ReSharper restore InconsistentNaming
        {
            Logging.WriteDiagnostic(color, "[" + _debug + "]: " + format, args);
        }

        //My Crappy Initalise.
        public override void Initialize()
        {
            Lua.Events.AttachEvent("DELETE_ITEM_CONFIRM", DeleteItemConfirmPopup);
            Lua.Events.AttachEvent("MERCHANT_SHOW", SellVenderItems);
            Lua.Events.AttachEvent("LOOT_CLOSED", LootEnded);

            slog("Initial Loading of Item names.");
            InitialMirLoad();
            MrItemRemover2Settings.Instance.Load();
            _checkTimer.Reset(); //should start the timer 

            IsInitialized = true;
        }

        public override void Pulse()
        {
            if (ManualCheckRequested)
            {
                EnableCheck = true;
                ManualCheckRequested = false;
                _checkTimer.Reset();

                slog("Checking Bags Manually & Reloading Item Lists.");
                CheckForItems();
            }

            else if (!MrItemRemover2Settings.Instance.LootEnable)
            {
                if (_checkTimer.TimeLeft.Ticks <= 0)
                {
                    if (EnableCheck == false)
                    {
                        EnableCheck = true;
                        _checkTimer.Reset();

                        slog("Enabling Check at {0}", GetTime(DateTime.Now));
                        dlog(
                            "Checktimer has Finished its Total wait of {0} Minutes, Enabling Item Check for next Opportunity",
                            MrItemRemover2Settings.Instance.Time.ToString(CultureInfo.InvariantCulture));
                        slog("Will Run Next Check At {0}", GetTime(_checkTimer.EndTime));
                    }
                }
            }


            if (!Me.Combat && !Me.IsCasting && !Me.IsDead && !Me.IsGhost && EnableCheck)
            {
                slog("EnableCheck was Passed!");
                if (MrItemRemover2Settings.Instance.EnableOpen)
                {
                    OpenBagItems();
                }
                if (MrItemRemover2Settings.Instance.EnableRemove)
                {
                    CheckForItems();
                }
                EnableCheck = false;
                slog("Turning off Check Since Done!");
            }
        }

        private void LootEnded(object sender, LuaEventArgs args)
        {
            if (MrItemRemover2Settings.Instance.LootEnable)
            {
                if (EnableCheck == false)
                {
                    EnableCheck = true;
                    slog("Enabling Check because Loot Ended");
                }
            }
        }

        //All items from the TXT Doc are loaded here.

        public void InitialMirLoad()
        {
            slog("Initial Loading of Items to Remove List.");
            LoadList(ItemName, _removeListPath);
            slog("Initial Loading of Items to Sell List.");
            LoadList(ItemNameSell, _sellListPath);
            slog("Initial Loading of Items to Keep List.");
            LoadList(KeepList, _keepListPath);
            slog("Initial Loading of Items to Open List.");
            LoadList(OpnList, _opnListPath);
            slog("Initial Loading of Items to Bag List.");
            LoadList(BagList, _bagListPath);
            slog("Initial Loading Complete!");
        }

        public void MirLoad()
        {
            LoadList(ItemName, _removeListPath);
            LoadList(ItemNameSell, _sellListPath);
            LoadList(KeepList, _keepListPath);
            LoadList(OpnList, _opnListPath);
            LoadList(BagList, _bagListPath);
        }

        public void LoadList(List<string> listToLoad, string filePath)
        {
            listToLoad.Clear();
            try
            {
                var read = new StreamReader(Convert.ToString(filePath));
                while (read.Peek() >= 0)
                {
                    listToLoad.Add(Convert.ToString(read.ReadLine()));
                }

                read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.Message));
            }
        }

        public void MirSave()
        {
            slog("Saving All Lists.");

            WriteList(ItemName, _removeListPath);
            WriteList(ItemNameSell, _sellListPath);
            WriteList(KeepList, _keepListPath);
            WriteList(OpnList, _opnListPath);
        }

        public void WriteList(List<string> listName, string filePath)
        {
// ReSharper disable TooWideLocalVariableScope
            StreamWriter write;
// ReSharper restore TooWideLocalVariableScope
            try
            {
                write = new StreamWriter(filePath);
// ReSharper disable ForCanBeConvertedToForeach
                for (int I = 0; I < listName.Count; I++)
// ReSharper restore ForCanBeConvertedToForeach
                {
                    write.WriteLine(Convert.ToString(listName[I]));
                }
                write.Close(); //dakkon for this fix.
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.Message));
            }
        }
    }
}