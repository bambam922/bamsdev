﻿using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Styx;
using Styx.WoWInternals.WoWObjects;

namespace MrItemRemover2
{
    public partial class MrItemRemover2Gui : Form
    {
        private readonly string _goldImangePathName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                                  string.Format(@"Plugins/MrItemRemover2DEV/MrItemRemover2/Gold2.bmp"));

        private readonly string _refreshImangePathName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                                  string.Format(@"Plugins/MrItemRemover2DEV/MrItemRemover2/ref.bmp"));

        public MrItemRemover2Gui(MrItemRemover2 controller)
        {
            Controller = controller;
            InitializeComponent();
        }
        
        public MrItemRemover2 Controller { get; private set; }

        public static void Slog(string format, params object[] args)
        {
            MrItemRemover2.Slog(format, args);
        }
        public static void Dlog(string format, params object[] args)
        {
            MrItemRemover2.Dlog(format, args);
        }

        private void MrItemRemover2GUI_Load(object sender, EventArgs e)
        {
            var refresh = new Bitmap(_refreshImangePathName);
            var goldImg = new Bitmap(_goldImangePathName);
            GoldBox.Image = goldImg;
            resf.Image = refresh;
            MrItemRemover2Settings.Instance.Load();
            SellList.Items.Clear();
            RemoveList.Items.Clear();
            ProtectedList.Items.Clear();
            SellDropDown.SelectedItem = MrItemRemover2Settings.Instance.EnableSell;
            RemoveDropDown.SelectedItem = MrItemRemover2Settings.Instance.EnableRemove;
            CombineDropDown.SelectedItem = MrItemRemover2Settings.Instance.CombineItems;
            OpeningDropDown.SelectedItem = MrItemRemover2Settings.Instance.EnableOpen;
            CheckAfterLootDropDown.SelectedItem = MrItemRemover2Settings.Instance.LootEnable; 
            GrayItems.Checked = MrItemRemover2Settings.Instance.DeleteAllGray;
            WhiteItems.Checked = MrItemRemover2Settings.Instance.DeleteAllWhite;
            GreenItems.Checked = MrItemRemover2Settings.Instance.DeleteAllGreen;
            BlueItems.Checked = MrItemRemover2Settings.Instance.DeleteAllBlue;
            SellGray.Checked = MrItemRemover2Settings.Instance.SellGray;
            //ApplyAll.Checked = MrItemRemover2Settings.Instance.ApplyAll;
            SellWhite.Checked = MrItemRemover2Settings.Instance.SellWhite;
            SellGreen.Checked = MrItemRemover2Settings.Instance.SellGreen;
            SellBlue.Checked = MrItemRemover2Settings.Instance.SellBlue;
            SellSoulbound.Checked = MrItemRemover2Settings.Instance.SellSoulbound;
            RemoveQItems.Checked = MrItemRemover2Settings.Instance.DeleteQuestItems;
            GoldGrays.Text = MrItemRemover2Settings.Instance.GoldGrays.ToString(CultureInfo.InvariantCulture);
            SilverGrays.Text = MrItemRemover2Settings.Instance.SilverGrays.ToString(CultureInfo.InvariantCulture);
            CopperGrays.Text = MrItemRemover2Settings.Instance.CopperGrays.ToString(CultureInfo.InvariantCulture);
            Time.Value = MrItemRemover2Settings.Instance.Time;
            foreach (string itm in Controller.ItemName)
            {
                RemoveList.Items.Add(itm);
            }
            foreach (string itm in Controller.ItemNameSell)
            {
                SellList.Items.Add(itm);
            }
            foreach (string itm in Controller.KeepList)
            {
                ProtectedList.Items.Add(itm);
            }

            foreach (WoWItem bagItem in StyxWoW.Me.BagItems)
            {
                if (bagItem.IsValid && bagItem.BagSlot != -1 && !MyBag.Items.Contains(bagItem.Name))
                {
                    MyBag.Items.Add(bagItem.Name);
                }
            }
        }

        private void AddToBagList_Click(object sender, EventArgs e)
        {
            if (InputAddToBagItem.Text != null)
            {
                MyBag.Items.Add(InputAddToBagItem.Text);
                Slog("Added {0} to Inventory List", InputAddToBagItem.Text);
            }
        }

        private void AddToSellList_Click(object sender, EventArgs e)
        {
            if (MyBag.SelectedItems[0] != null)
            {
                SellList.Items.Add(MyBag.SelectedItem);
                Controller.ItemNameSell.Add(MyBag.SelectedItem.ToString());
            }
        }

        private void AddToRemoveList_Click(object sender, EventArgs e)
        {
            if (MyBag.SelectedItems[0] != null)
            {
                RemoveList.Items.Add(MyBag.SelectedItem);
                Controller.ItemName.Add(MyBag.SelectedItem.ToString());
            }
        }

        private void AddToProtList_Click(object sender, EventArgs e)
        {
            if (MyBag.SelectedItems[0] != null)
            {
                ProtectedList.Items.Add(MyBag.SelectedItem);
                Controller.KeepList.Add(MyBag.SelectedItem.ToString());
            }
        }

        private void RemoveSellItem_Click(object sender, EventArgs e)
        {
            if (SellList.SelectedItem != null)
            {
                Slog("{0} Removed", SellList.SelectedItem.ToString());
                Controller.ItemNameSell.Remove(SellList.SelectedItem.ToString());
                SellList.Items.Remove(SellList.SelectedItem);
            }
        }

        private void RemoveRemoveItem_Click(object sender, EventArgs e)
        {
            if (RemoveList.SelectedItem != null)
            {
                Slog("{0} Removed", RemoveList.SelectedItem.ToString());
                Controller.ItemName.Remove(RemoveList.SelectedItem.ToString());
                RemoveList.Items.Remove(RemoveList.SelectedItem);
            }
        }

        private void RemoveProtectedItem_Click(object sender, EventArgs e)
        {
            if (ProtectedList.SelectedItem != null)
            {
                Slog("{0} Removed", ProtectedList.SelectedItem.ToString());
                Controller.KeepList.Remove(ProtectedList.SelectedItem.ToString());
                ProtectedList.Items.Remove(ProtectedList.SelectedItem);
            }
        }

        public void PrintSettings()
        {
            Dlog("Mr.ItemRemover2 Settings");
            Dlog("------------------------------------------");
            foreach (var setting in MrItemRemover2Settings.Instance.GetSettings())
            {
                var key = setting.Key;
                var value = setting.Value;
                Dlog(string.Format("{0} - {1}", key, value));
            }
            Dlog("------------------------------------------");
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Controller.MirSave();
            MrItemRemover2Settings.Instance.Save();
            PrintSettings();
            Close();
        }

        private void GrayItems_CheckedChanged(object sender, EventArgs e)
        {
            MrItemRemover2Settings.Instance.DeleteAllGray = GrayItems.Checked;
        }

        private void WhiteItems_CheckedChanged(object sender, EventArgs e)
        {
            MrItemRemover2Settings.Instance.DeleteAllWhite = WhiteItems.Checked;
        }

        private void GreenItems_CheckedChanged(object sender, EventArgs e)
        {
            MrItemRemover2Settings.Instance.DeleteAllGreen = GreenItems.Checked;
        }

        private void BlueItems_CheckedChanged(object sender, EventArgs e)
        {
            MrItemRemover2Settings.Instance.DeleteAllBlue = BlueItems.Checked;
        }

        private void RemoveQItems_CheckedChanged(object sender, EventArgs e)
        {
            MrItemRemover2Settings.Instance.DeleteQuestItems = RemoveQItems.Checked;
        }

        private void SellGray_CheckedChanged(object sender, EventArgs e)
        {
            MrItemRemover2Settings.Instance.SellGray = SellGray.Checked;
        }

        /* private void ApplyAll_CheckedChanged(object sender, EventArgs e)
        {
            MrItemRemover2Settings.Instance.ApplyAll = ApplyAll.Checked;
        }*/

        private void SellGreen_CheckedChanged(object sender, EventArgs e)
        {
            MrItemRemover2Settings.Instance.SellGreen = SellGreen.Checked;
        }

        private void SellWhite_CheckedChanged(object sender, EventArgs e)
        {
            MrItemRemover2Settings.Instance.SellWhite = SellWhite.Checked;
        }

        private void Time_ValueChanged(object sender, EventArgs e)
        {
            MrItemRemover2Settings.Instance.Time = int.Parse(Time.Value.ToString(CultureInfo.InvariantCulture));
        }

        private void resf_Click(object sender, EventArgs e)
        {
            MyBag.Items.Clear();
            foreach (WoWItem bagItem in StyxWoW.Me.BagItems)
            {
                if (bagItem.BagSlot != -1 && !MyBag.Items.Contains(bagItem.Name))
                {
                    MyBag.Items.Add(bagItem.Name);
                }
            }
        }

        private void Run_Click(object sender, EventArgs e)
        {
            Controller.ManualCheckRequested = true;
        }

        private void SellList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
        }

        private void SellBlue_CheckedChanged_1(object sender, EventArgs e)
        {
            MrItemRemover2Settings.Instance.SellBlue = SellBlue.Checked;
        }

        private void SellSoulbound_CheckedChanged(object sender, EventArgs e)
        {
            MrItemRemover2Settings.Instance.SellSoulbound = SellSoulbound.Checked;
        }

        private void RSDrinks_CheckedChanged(object sender, EventArgs e)
        {
        //   MrItemRemover2Settings.Instance.RSDrinks = CombineItems.Checked;
        }

        private void RSFood_CheckedChanged(object sender, EventArgs e)
        {
        //    MrItemRemover2Settings.Instance.RSFood = CombineItems.Checked;
        }


        private void SellDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

            MrItemRemover2Settings.Instance.EnableSell = SellDropDown.SelectedItem.ToString();
        }

        private void RemoveDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

            MrItemRemover2Settings.Instance.EnableRemove = RemoveDropDown.SelectedItem.ToString();
        }

        private void CombineDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            MrItemRemover2Settings.Instance.CombineItems = CombineDropDown.SelectedItem.ToString();
        }

        private void OpeningDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            MrItemRemover2Settings.Instance.EnableOpen = OpeningDropDown.SelectedItem.ToString();
        }

        private void CheckAfterLootDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

            MrItemRemover2Settings.Instance.LootEnable = CheckAfterLootDropDown.SelectedItem.ToString();
        }

        private void GoldGrays_TextChanged_1(object sender, EventArgs e)
        {
            MrItemRemover2Settings.Instance.GoldGrays = int.Parse(GoldGrays.Text);
        }

        private void SilverGrays_TextChanged_1(object sender, EventArgs e)
        {
            MrItemRemover2Settings.Instance.SilverGrays = int.Parse(SilverGrays.Text);
        }

        private void CopperGrays_TextChanged_1(object sender, EventArgs e)
        {
            MrItemRemover2Settings.Instance.CopperGrays = int.Parse(CopperGrays.Text);
        }
    }
}