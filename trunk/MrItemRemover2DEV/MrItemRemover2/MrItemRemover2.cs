/*
 * Mr.ItemRemover2 - Created by CodenameGamma - 1-31-11 - For WoW Version 4.0.3
 * 1.6 Update by Bambam922
 * www.thebuddyforum.com
 * This is a free plugin and should not be sold or repackaged.
 * Donations accepted.
 * Version 1.6 for WoW Version 5.4 +
 */

using System.Windows.Forms;
using MrItemRemover2.GUI;
using System;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System.IO;
using Styx.Plugins;
using Styx;
using Styx.Common.Helpers;
using Styx.Common;
using System.Collections.Generic;
using System.Windows.Media;


namespace MrItemRemover2
{

    public partial class MrItemRemover2 : HBPlugin
    {
        const string _name = "Mr.ItemRemover2DEV 1.6";
        const string _debug = "Mr.Itemremover2 DEBUG";

        //Normal Stuff.
        public override string Name { get { return _name; } }
        public override string Author { get { return "CnG & Bambam922"; } }
        public override Version Version { get { return new Version(1, 6); } }
        public override bool WantButton { get { return true; } }
        public override string ButtonText { get { return _name; } }


        public override void OnButtonPress()
        {
            if (!IsInitialized)
            {
                slog("Not finished initializing");
                return;
            }

            var form = new MrItemRemover2GUI(this);
            form.ShowDialog();
        }
    
        public static void slog(string format, params object[] args)
        {
            slog(Colors.SkyBlue, format, args);
        }

        public static void slog(Color color, string format, params object[] args)
        {
            Logging.Write(color, "[" + _name + "]: " + format, args);
        }

        public static void dlog(string format, params object[] args)
        {
            dlog(Colors.Yellow, format, args);
        }

        public static void dlog(Color color, string format, params object[] args)
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
            initialMIRLoad();
            MrItemRemover2Settings.Instance.Load();
            CheckTimer.Reset(); //should start the timer 

            IsInitialized = true;
        }

        public bool ManualCheckRequested { get; set; }
       
        private WaitTimer CheckTimer = new WaitTimer(TimeSpan.FromMinutes(MrItemRemover2Settings.Instance.Time));
        private bool EnableCheck { get; set; }
        private bool IsInitialized { get; set; }
        private static LocalPlayer Me { get { return StyxWoW.Me; } }
   
        public override void Pulse()
        {
            if (ManualCheckRequested)
            {
                EnableCheck = true;
                ManualCheckRequested = false;
                CheckTimer.Reset();

                slog("Checking Bags Manually & Reloading Item Lists.");
                CheckForItems();
            } 
            
            else if (!MrItemRemover2Settings.Instance.LootEnable)
            {
                if (CheckTimer.TimeLeft.Ticks <= 0)
                {
                    if (EnableCheck == false)
                    {
                        EnableCheck = true;
                        CheckTimer.Reset();

                        slog("Enabling Check at {0}", GetTime(DateTime.Now));
                        dlog("Checktimer has Finished its Total wait of {0} Minutes, Enabling Item Check for next Opportunity", MrItemRemover2Settings.Instance.Time.ToString());
                        slog("Will Run Next Check At {0}", GetTime(CheckTimer.EndTime));
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
        public List<string> _ItemName = new List<string>
        {

        };

        //Specific items from the TXT Doc are loaded here.
        public List<string> ItemNameSell = new List<string>
        {

        };

        public List<string> InventoryList = new List<string>
        {

        };

        public List<string> KeepList = new List<string>
        {

        };

        public List<string> OpnList = new List<string>
        {

        };

        public List<string> BagList = new List<string>
        {

        };

        //file Path for Saving and Loading. 
        private readonly string _removeListPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                           string.Format(@"Plugins/MrItemRemover2DEV/MrItemRemover2/ItemNameRemoveList.txt"));
        private readonly string _sellListPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                           string.Format(@"Plugins/MrItemRemover2DEV/MrItemRemover2/ItemNameSellList.txt"));
        private readonly string _keepListPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                           string.Format(@"Plugins/MrItemRemover2DEV/MrItemRemover2/ItemNameKeepList.txt"));
        private readonly string _opnListPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                           string.Format(@"Plugins/MrItemRemover2DEV/MrItemRemover2/ItemNameOpnList.txt"));
        private readonly string _bagListPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                           string.Format(@"Plugins/MrItemRemover2DEV/MrItemRemover2/ItemNameBagList.txt"));
        public void initialMIRLoad()
        {
            slog("Initial Loading of Items to Remove List.");
            LoadList(_ItemName, _removeListPath);
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

        public void MIRLoad()
        {
            LoadList(_ItemName, _removeListPath);
            LoadList(ItemNameSell, _sellListPath);
            LoadList(KeepList, _keepListPath);
            LoadList(OpnList, _opnListPath);
            LoadList(BagList, _bagListPath); 
        }

        public void LoadList(List<string> ListToLoad, string FilePath)
        {
            ListToLoad.Clear();
            try
            {
                StreamReader Read = new StreamReader(Convert.ToString(FilePath));
                while (Read.Peek() >= 0)
                {
                    ListToLoad.Add(Convert.ToString(Read.ReadLine()));
                }

                Read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.Message));
                return;
            }
        }

        public void MIRSave()
        {
            slog("Saving All Lists.");

            writeList(_ItemName, _removeListPath);

            writeList(ItemNameSell, _sellListPath);

            writeList(KeepList, _keepListPath);

            writeList(OpnList, _opnListPath);
        }




        public void writeList(List<string> ListName, string filePath)
        {
            StreamWriter Write;
            try
            {
                Write = new StreamWriter(filePath);
                for (int I = 0; I < ListName.Count; I++)
                {
                    Write.WriteLine(Convert.ToString(ListName[I]));
                }
                Write.Close(); //dakkon for this fix.
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.Message));
                return;
            }
        }
    }
}

