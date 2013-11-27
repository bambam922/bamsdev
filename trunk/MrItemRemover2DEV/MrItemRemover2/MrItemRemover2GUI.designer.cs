using System.Windows.Forms;

namespace MrItemRemover2
{
    partial class MrItemRemover2Gui
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Time = new System.Windows.Forms.NumericUpDown();
            this.MyBag = new System.Windows.Forms.ListBox();
            this.RemoveList = new System.Windows.Forms.ListBox();
            this.SellList = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.resf = new System.Windows.Forms.PictureBox();
            this.AddToSellList = new System.Windows.Forms.Button();
            this.AddToProtList = new System.Windows.Forms.Button();
            this.AddToRemoveList = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RemoveRemoveItem = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.RemoveSellItem = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ProtectedList = new System.Windows.Forms.ListBox();
            this.RemoveProtectedItem = new System.Windows.Forms.Button();
            this.AddToBagList = new System.Windows.Forms.Button();
            this.InputAddToBagItem = new System.Windows.Forms.TextBox();
            this.EnableRemove = new System.Windows.Forms.CheckBox();
            this.EnableSell = new System.Windows.Forms.CheckBox();
            this.SellWhite = new System.Windows.Forms.CheckBox();
            this.SellGreen = new System.Windows.Forms.CheckBox();
            this.SellGray = new System.Windows.Forms.CheckBox();
            this.GrayItems = new System.Windows.Forms.CheckBox();
            this.SellSoulbound = new System.Windows.Forms.CheckBox();
            this.SellBlue = new System.Windows.Forms.CheckBox();
            this.RemoveQItems = new System.Windows.Forms.CheckBox();
            this.Save = new System.Windows.Forms.Button();
            this.Run = new System.Windows.Forms.Button();
            this.EnableOpen = new System.Windows.Forms.CheckBox();
            this.LootEnable = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BlueItems = new System.Windows.Forms.CheckBox();
            this.WhiteItems = new System.Windows.Forms.CheckBox();
            this.GreenItems = new System.Windows.Forms.CheckBox();
            this.CombineItems = new System.Windows.Forms.CheckBox();
            this.RSFood = new System.Windows.Forms.CheckBox();
            this.RSDrinks = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.OpeningDropDown = new System.Windows.Forms.ComboBox();
            this.CombineDropDown = new System.Windows.Forms.ComboBox();
            this.RemoveDropDown = new System.Windows.Forms.ComboBox();
            this.CheckAfterLootDropdown = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SellDropdown = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.GoldGrays = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CopperGrays = new System.Windows.Forms.TextBox();
            this.SilverGrays = new System.Windows.Forms.TextBox();
            this.GoldBox = new System.Windows.Forms.PictureBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            ((System.ComponentModel.ISupportInitialize)(this.Time)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resf)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GoldBox)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Time
            // 
            this.Time.Location = new System.Drawing.Point(331, 7);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(51, 21);
            this.Time.TabIndex = 0;
            this.Time.ValueChanged += new System.EventHandler(this.Time_ValueChanged);
            // 
            // MyBag
            // 
            this.MyBag.FormattingEnabled = true;
            this.MyBag.ItemHeight = 15;
            this.MyBag.Location = new System.Drawing.Point(8, 19);
            this.MyBag.Name = "MyBag";
            this.MyBag.Size = new System.Drawing.Size(150, 139);
            this.MyBag.TabIndex = 4;
            // 
            // RemoveList
            // 
            this.RemoveList.FormattingEnabled = true;
            this.RemoveList.ItemHeight = 15;
            this.RemoveList.Location = new System.Drawing.Point(6, 19);
            this.RemoveList.Name = "RemoveList";
            this.RemoveList.Size = new System.Drawing.Size(100, 139);
            this.RemoveList.TabIndex = 5;
            // 
            // SellList
            // 
            this.SellList.FormattingEnabled = true;
            this.SellList.ItemHeight = 15;
            this.SellList.Location = new System.Drawing.Point(6, 19);
            this.SellList.Name = "SellList";
            this.SellList.Size = new System.Drawing.Size(100, 139);
            this.SellList.TabIndex = 6;
            this.SellList.SelectedIndexChanged += new System.EventHandler(this.SellList_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.resf);
            this.groupBox1.Controls.Add(this.AddToSellList);
            this.groupBox1.Controls.Add(this.AddToProtList);
            this.groupBox1.Controls.Add(this.AddToRemoveList);
            this.groupBox1.Controls.Add(this.MyBag);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(165, 246);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "My Bag Items";
            // 
            // resf
            // 
            this.resf.Location = new System.Drawing.Point(87, 0);
            this.resf.Name = "resf";
            this.resf.Size = new System.Drawing.Size(16, 16);
            this.resf.TabIndex = 38;
            this.resf.TabStop = false;
            this.resf.Click += new System.EventHandler(this.resf_Click);
            // 
            // AddToSellList
            // 
            this.AddToSellList.Location = new System.Drawing.Point(8, 164);
            this.AddToSellList.Name = "AddToSellList";
            this.AddToSellList.Size = new System.Drawing.Size(150, 23);
            this.AddToSellList.TabIndex = 7;
            this.AddToSellList.Text = "Add to Sell List";
            this.AddToSellList.UseVisualStyleBackColor = true;
            this.AddToSellList.Click += new System.EventHandler(this.AddToSellList_Click);
            // 
            // AddToProtList
            // 
            this.AddToProtList.Location = new System.Drawing.Point(8, 216);
            this.AddToProtList.Name = "AddToProtList";
            this.AddToProtList.Size = new System.Drawing.Size(149, 23);
            this.AddToProtList.TabIndex = 6;
            this.AddToProtList.Text = "Add To Protected List";
            this.AddToProtList.UseVisualStyleBackColor = true;
            this.AddToProtList.Click += new System.EventHandler(this.AddToProtList_Click);
            // 
            // AddToRemoveList
            // 
            this.AddToRemoveList.Location = new System.Drawing.Point(8, 190);
            this.AddToRemoveList.Name = "AddToRemoveList";
            this.AddToRemoveList.Size = new System.Drawing.Size(149, 23);
            this.AddToRemoveList.TabIndex = 5;
            this.AddToRemoveList.Text = "Add to Remove List";
            this.AddToRemoveList.UseVisualStyleBackColor = true;
            this.AddToRemoveList.Click += new System.EventHandler(this.AddToRemoveList_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RemoveRemoveItem);
            this.groupBox2.Controls.Add(this.RemoveList);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(112, 200);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "My Remove List";
            // 
            // RemoveRemoveItem
            // 
            this.RemoveRemoveItem.Location = new System.Drawing.Point(5, 173);
            this.RemoveRemoveItem.Name = "RemoveRemoveItem";
            this.RemoveRemoveItem.Size = new System.Drawing.Size(102, 23);
            this.RemoveRemoveItem.TabIndex = 6;
            this.RemoveRemoveItem.Text = "Remove Selected";
            this.RemoveRemoveItem.UseVisualStyleBackColor = true;
            this.RemoveRemoveItem.Click += new System.EventHandler(this.RemoveRemoveItem_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.RemoveSellItem);
            this.groupBox3.Controls.Add(this.SellList);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(112, 200);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "My Sell List";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // RemoveSellItem
            // 
            this.RemoveSellItem.Location = new System.Drawing.Point(5, 173);
            this.RemoveSellItem.Name = "RemoveSellItem";
            this.RemoveSellItem.Size = new System.Drawing.Size(102, 23);
            this.RemoveSellItem.TabIndex = 7;
            this.RemoveSellItem.Text = "Remove Selected";
            this.RemoveSellItem.UseVisualStyleBackColor = true;
            this.RemoveSellItem.Click += new System.EventHandler(this.RemoveSellItem_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ProtectedList);
            this.groupBox4.Controls.Add(this.RemoveProtectedItem);
            this.groupBox4.Location = new System.Drawing.Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(112, 200);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "My Protected List";
            // 
            // ProtectedList
            // 
            this.ProtectedList.FormattingEnabled = true;
            this.ProtectedList.ItemHeight = 15;
            this.ProtectedList.Location = new System.Drawing.Point(6, 19);
            this.ProtectedList.Name = "ProtectedList";
            this.ProtectedList.Size = new System.Drawing.Size(100, 139);
            this.ProtectedList.TabIndex = 9;
            // 
            // RemoveProtectedItem
            // 
            this.RemoveProtectedItem.Location = new System.Drawing.Point(5, 172);
            this.RemoveProtectedItem.Name = "RemoveProtectedItem";
            this.RemoveProtectedItem.Size = new System.Drawing.Size(102, 23);
            this.RemoveProtectedItem.TabIndex = 8;
            this.RemoveProtectedItem.Text = "Remove Selected";
            this.RemoveProtectedItem.UseVisualStyleBackColor = true;
            this.RemoveProtectedItem.Click += new System.EventHandler(this.RemoveProtectedItem_Click);
            // 
            // AddToBagList
            // 
            this.AddToBagList.Location = new System.Drawing.Point(3, 279);
            this.AddToBagList.Name = "AddToBagList";
            this.AddToBagList.Size = new System.Drawing.Size(165, 23);
            this.AddToBagList.TabIndex = 17;
            this.AddToBagList.Text = "Add Item to My Bag Items";
            this.AddToBagList.UseVisualStyleBackColor = true;
            this.AddToBagList.Click += new System.EventHandler(this.AddToBagList_Click);
            // 
            // InputAddToBagItem
            // 
            this.InputAddToBagItem.Location = new System.Drawing.Point(3, 254);
            this.InputAddToBagItem.Name = "InputAddToBagItem";
            this.InputAddToBagItem.Size = new System.Drawing.Size(165, 21);
            this.InputAddToBagItem.TabIndex = 18;
            // 
            // EnableRemove
            // 
            this.EnableRemove.AutoSize = true;
            this.EnableRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.EnableRemove.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.EnableRemove.Location = new System.Drawing.Point(429, 78);
            this.EnableRemove.Name = "EnableRemove";
            this.EnableRemove.Size = new System.Drawing.Size(124, 19);
            this.EnableRemove.TabIndex = 19;
            this.EnableRemove.Text = "Enable Removing";
            this.EnableRemove.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.EnableRemove.UseVisualStyleBackColor = true;
            this.EnableRemove.CheckedChanged += new System.EventHandler(this.EnableRemove_CheckedChanged);
            // 
            // EnableSell
            // 
            this.EnableSell.AutoSize = true;
            this.EnableSell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.EnableSell.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.EnableSell.Location = new System.Drawing.Point(429, 48);
            this.EnableSell.Name = "EnableSell";
            this.EnableSell.Size = new System.Drawing.Size(106, 19);
            this.EnableSell.TabIndex = 20;
            this.EnableSell.Text = "Enable Selling";
            this.EnableSell.UseVisualStyleBackColor = true;
            this.EnableSell.CheckedChanged += new System.EventHandler(this.EnableSell_CheckedChanged);
            // 
            // SellWhite
            // 
            this.SellWhite.AutoSize = true;
            this.SellWhite.Location = new System.Drawing.Point(141, 43);
            this.SellWhite.Name = "SellWhite";
            this.SellWhite.Size = new System.Drawing.Size(96, 19);
            this.SellWhite.TabIndex = 30;
            this.SellWhite.Text = "   Sell Whites";
            this.SellWhite.UseVisualStyleBackColor = true;
            this.SellWhite.CheckedChanged += new System.EventHandler(this.SellWhite_CheckedChanged);
            // 
            // SellGreen
            // 
            this.SellGreen.AutoSize = true;
            this.SellGreen.Location = new System.Drawing.Point(141, 60);
            this.SellGreen.Name = "SellGreen";
            this.SellGreen.Size = new System.Drawing.Size(99, 19);
            this.SellGreen.TabIndex = 29;
            this.SellGreen.Text = "   Sell Greens";
            this.SellGreen.UseVisualStyleBackColor = true;
            this.SellGreen.CheckedChanged += new System.EventHandler(this.SellGreen_CheckedChanged);
            // 
            // SellGray
            // 
            this.SellGray.AutoSize = true;
            this.SellGray.Location = new System.Drawing.Point(141, 25);
            this.SellGray.Name = "SellGray";
            this.SellGray.Size = new System.Drawing.Size(90, 19);
            this.SellGray.TabIndex = 28;
            this.SellGray.Text = "   Sell Grays";
            this.SellGray.UseVisualStyleBackColor = true;
            this.SellGray.CheckedChanged += new System.EventHandler(this.SellGray_CheckedChanged);
            // 
            // GrayItems
            // 
            this.GrayItems.AutoSize = true;
            this.GrayItems.Location = new System.Drawing.Point(205, 52);
            this.GrayItems.Name = "GrayItems";
            this.GrayItems.Size = new System.Drawing.Size(115, 19);
            this.GrayItems.TabIndex = 27;
            this.GrayItems.Text = "   Remove Grays";
            this.GrayItems.UseVisualStyleBackColor = true;
            this.GrayItems.CheckedChanged += new System.EventHandler(this.GrayItems_CheckedChanged);
            // 
            // SellSoulbound
            // 
            this.SellSoulbound.AutoSize = true;
            this.SellSoulbound.Location = new System.Drawing.Point(141, 94);
            this.SellSoulbound.Name = "SellSoulbound";
            this.SellSoulbound.Size = new System.Drawing.Size(119, 19);
            this.SellSoulbound.TabIndex = 44;
            this.SellSoulbound.Text = "   Sell Soulbound";
            this.SellSoulbound.UseVisualStyleBackColor = true;
            this.SellSoulbound.CheckedChanged += new System.EventHandler(this.SellSoulbound_CheckedChanged);
            // 
            // SellBlue
            // 
            this.SellBlue.AutoSize = true;
            this.SellBlue.Location = new System.Drawing.Point(141, 77);
            this.SellBlue.Name = "SellBlue";
            this.SellBlue.Size = new System.Drawing.Size(90, 19);
            this.SellBlue.TabIndex = 39;
            this.SellBlue.Text = "   Sell Blues";
            this.SellBlue.UseVisualStyleBackColor = true;
            this.SellBlue.CheckedChanged += new System.EventHandler(this.SellBlue_CheckedChanged_1);
            // 
            // RemoveQItems
            // 
            this.RemoveQItems.AutoSize = true;
            this.RemoveQItems.Location = new System.Drawing.Point(205, 121);
            this.RemoveQItems.Name = "RemoveQItems";
            this.RemoveQItems.Size = new System.Drawing.Size(112, 19);
            this.RemoveQItems.TabIndex = 31;
            this.RemoveQItems.Text = "   Quest Starters";
            this.RemoveQItems.UseVisualStyleBackColor = true;
            this.RemoveQItems.CheckedChanged += new System.EventHandler(this.RemoveQItems_CheckedChanged);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(246, 254);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(131, 26);
            this.Save.TabIndex = 32;
            this.Save.Text = "Save and Close";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Run
            // 
            this.Run.Location = new System.Drawing.Point(239, 213);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(143, 23);
            this.Run.TabIndex = 33;
            this.Run.Text = "Check Bag Items Now";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // EnableOpen
            // 
            this.EnableOpen.AutoSize = true;
            this.EnableOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.EnableOpen.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.EnableOpen.Location = new System.Drawing.Point(429, 110);
            this.EnableOpen.Name = "EnableOpen";
            this.EnableOpen.Size = new System.Drawing.Size(115, 19);
            this.EnableOpen.TabIndex = 35;
            this.EnableOpen.Text = "Enable Opening";
            this.EnableOpen.UseVisualStyleBackColor = true;
            this.EnableOpen.CheckedChanged += new System.EventHandler(this.EnableOpen_CheckedChanged);
            // 
            // LootEnable
            // 
            this.LootEnable.AutoSize = true;
            this.LootEnable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.LootEnable.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.LootEnable.Location = new System.Drawing.Point(429, 160);
            this.LootEnable.Name = "LootEnable";
            this.LootEnable.Size = new System.Drawing.Size(114, 19);
            this.LootEnable.TabIndex = 38;
            this.LootEnable.Text = "Check After Loot";
            this.LootEnable.UseVisualStyleBackColor = true;
            this.LootEnable.CheckedChanged += new System.EventHandler(this.LootEnable_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(170, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 15);
            this.label1.TabIndex = 40;
            this.label1.Text = "Time Between Bag Checks -";
            // 
            // BlueItems
            // 
            this.BlueItems.AutoSize = true;
            this.BlueItems.Location = new System.Drawing.Point(205, 104);
            this.BlueItems.Name = "BlueItems";
            this.BlueItems.Size = new System.Drawing.Size(115, 19);
            this.BlueItems.TabIndex = 41;
            this.BlueItems.Text = "   Remove Blues";
            this.BlueItems.UseVisualStyleBackColor = true;
            this.BlueItems.CheckedChanged += new System.EventHandler(this.BlueItems_CheckedChanged);
            // 
            // WhiteItems
            // 
            this.WhiteItems.AutoSize = true;
            this.WhiteItems.Location = new System.Drawing.Point(205, 70);
            this.WhiteItems.Name = "WhiteItems";
            this.WhiteItems.Size = new System.Drawing.Size(121, 19);
            this.WhiteItems.TabIndex = 43;
            this.WhiteItems.Text = "   Remove Whites";
            this.WhiteItems.UseVisualStyleBackColor = true;
            this.WhiteItems.CheckedChanged += new System.EventHandler(this.WhiteItems_CheckedChanged);
            // 
            // GreenItems
            // 
            this.GreenItems.AutoSize = true;
            this.GreenItems.Location = new System.Drawing.Point(205, 87);
            this.GreenItems.Name = "GreenItems";
            this.GreenItems.Size = new System.Drawing.Size(124, 19);
            this.GreenItems.TabIndex = 42;
            this.GreenItems.Text = "   Remove Greens";
            this.GreenItems.UseVisualStyleBackColor = true;
            this.GreenItems.CheckedChanged += new System.EventHandler(this.GreenItems_CheckedChanged);
            // 
            // CombineItems
            // 
            this.CombineItems.AutoSize = true;
            this.CombineItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.CombineItems.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.CombineItems.Location = new System.Drawing.Point(429, 135);
            this.CombineItems.Name = "CombineItems";
            this.CombineItems.Size = new System.Drawing.Size(128, 19);
            this.CombineItems.TabIndex = 45;
            this.CombineItems.Text = "Enable Combining";
            this.CombineItems.UseVisualStyleBackColor = true;
            this.CombineItems.CheckedChanged += new System.EventHandler(this.CombineItems_CheckedChanged);
            // 
            // RSFood
            // 
            this.RSFood.AutoSize = true;
            this.RSFood.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.RSFood.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.RSFood.Location = new System.Drawing.Point(255, 212);
            this.RSFood.Name = "RSFood";
            this.RSFood.Size = new System.Drawing.Size(127, 19);
            this.RSFood.TabIndex = 46;
            this.RSFood.Text = "Remove/Sell Food";
            this.RSFood.UseVisualStyleBackColor = true;
            this.RSFood.CheckedChanged += new System.EventHandler(this.RSFood_CheckedChanged);
            // 
            // RSDrinks
            // 
            this.RSDrinks.AutoSize = true;
            this.RSDrinks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.RSDrinks.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.RSDrinks.Location = new System.Drawing.Point(248, 187);
            this.RSDrinks.Name = "RSDrinks";
            this.RSDrinks.Size = new System.Drawing.Size(134, 19);
            this.RSDrinks.TabIndex = 47;
            this.RSDrinks.Text = "Remove/Sell Drinks";
            this.RSDrinks.UseVisualStyleBackColor = true;
            this.RSDrinks.CheckedChanged += new System.EventHandler(this.RSDrinks_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.tabControl1.Location = new System.Drawing.Point(7, 7);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(700, 337);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 48;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.OpeningDropDown);
            this.tabPage1.Controls.Add(this.CombineItems);
            this.tabPage1.Controls.Add(this.CombineDropDown);
            this.tabPage1.Controls.Add(this.LootEnable);
            this.tabPage1.Controls.Add(this.RemoveDropDown);
            this.tabPage1.Controls.Add(this.EnableOpen);
            this.tabPage1.Controls.Add(this.Run);
            this.tabPage1.Controls.Add(this.EnableSell);
            this.tabPage1.Controls.Add(this.Save);
            this.tabPage1.Controls.Add(this.EnableRemove);
            this.tabPage1.Controls.Add(this.CheckAfterLootDropdown);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.SellDropdown);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.InputAddToBagItem);
            this.tabPage1.Controls.Add(this.AddToBagList);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.Time);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(692, 309);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // OpeningDropDown
            // 
            this.OpeningDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OpeningDropDown.FormattingEnabled = true;
            this.OpeningDropDown.Items.AddRange(new object[] {
            "False",
            "True"});
            this.OpeningDropDown.Location = new System.Drawing.Point(288, 129);
            this.OpeningDropDown.Name = "OpeningDropDown";
            this.OpeningDropDown.Size = new System.Drawing.Size(121, 23);
            this.OpeningDropDown.TabIndex = 59;
            // 
            // CombineDropDown
            // 
            this.CombineDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CombineDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CombineDropDown.FormattingEnabled = true;
            this.CombineDropDown.Items.AddRange(new object[] {
            "False",
            "True"});
            this.CombineDropDown.Location = new System.Drawing.Point(288, 100);
            this.CombineDropDown.Name = "CombineDropDown";
            this.CombineDropDown.Size = new System.Drawing.Size(121, 23);
            this.CombineDropDown.TabIndex = 58;
            // 
            // RemoveDropDown
            // 
            this.RemoveDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RemoveDropDown.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RemoveDropDown.FormattingEnabled = true;
            this.RemoveDropDown.Items.AddRange(new object[] {
            "False",
            "True"});
            this.RemoveDropDown.Location = new System.Drawing.Point(288, 70);
            this.RemoveDropDown.Name = "RemoveDropDown";
            this.RemoveDropDown.Size = new System.Drawing.Size(121, 23);
            this.RemoveDropDown.TabIndex = 57;
            // 
            // CheckAfterLootDropdown
            // 
            this.CheckAfterLootDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CheckAfterLootDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CheckAfterLootDropdown.FormattingEnabled = true;
            this.CheckAfterLootDropdown.Items.AddRange(new object[] {
            "False",
            "True"});
            this.CheckAfterLootDropdown.Location = new System.Drawing.Point(288, 158);
            this.CheckAfterLootDropdown.Name = "CheckAfterLootDropdown";
            this.CheckAfterLootDropdown.Size = new System.Drawing.Size(121, 23);
            this.CheckAfterLootDropdown.TabIndex = 56;
            this.CheckAfterLootDropdown.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(170, 161);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 15);
            this.label8.TabIndex = 55;
            this.label8.Text = "Check After Loot -";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(170, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 15);
            this.label7.TabIndex = 54;
            this.label7.Text = "Enable Combining -";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(170, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 15);
            this.label6.TabIndex = 53;
            this.label6.Text = "Enable Opening -";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(170, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 15);
            this.label4.TabIndex = 52;
            this.label4.Text = "Enable Removing -";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 15);
            this.label3.TabIndex = 51;
            this.label3.Text = "Enable Selling -";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(384, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 15);
            this.label2.TabIndex = 50;
            this.label2.Text = "mins";
            // 
            // SellDropdown
            // 
            this.SellDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SellDropdown.FormattingEnabled = true;
            this.SellDropdown.Items.AddRange(new object[] {
            "False",
            "True"});
            this.SellDropdown.Location = new System.Drawing.Point(288, 41);
            this.SellDropdown.Name = "SellDropdown";
            this.SellDropdown.Size = new System.Drawing.Size(121, 23);
            this.SellDropdown.TabIndex = 49;
            this.SellDropdown.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.SellGray);
            this.tabPage2.Controls.Add(this.SellGreen);
            this.tabPage2.Controls.Add(this.SellWhite);
            this.tabPage2.Controls.Add(this.SellSoulbound);
            this.tabPage2.Controls.Add(this.SellBlue);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(692, 309);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sell";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.GoldGrays);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.CopperGrays);
            this.tabPage3.Controls.Add(this.SilverGrays);
            this.tabPage3.Controls.Add(this.GoldBox);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.RSFood);
            this.tabPage3.Controls.Add(this.RSDrinks);
            this.tabPage3.Controls.Add(this.RemoveQItems);
            this.tabPage3.Controls.Add(this.GrayItems);
            this.tabPage3.Controls.Add(this.BlueItems);
            this.tabPage3.Controls.Add(this.GreenItems);
            this.tabPage3.Controls.Add(this.WhiteItems);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(692, 309);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Remove";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // GoldGrays
            // 
            this.GoldGrays.BackColor = System.Drawing.Color.Black;
            this.GoldGrays.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GoldGrays.ForeColor = System.Drawing.Color.White;
            this.GoldGrays.Location = new System.Drawing.Point(193, 271);
            this.GoldGrays.MaxLength = 4;
            this.GoldGrays.Name = "GoldGrays";
            this.GoldGrays.Size = new System.Drawing.Size(26, 14);
            this.GoldGrays.TabIndex = 48;
            this.GoldGrays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(126, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(226, 15);
            this.label5.TabIndex = 51;
            this.label5.Text = "Minimum Value Before Removing Grays";
            // 
            // CopperGrays
            // 
            this.CopperGrays.BackColor = System.Drawing.Color.Black;
            this.CopperGrays.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CopperGrays.ForeColor = System.Drawing.Color.White;
            this.CopperGrays.Location = new System.Drawing.Point(281, 271);
            this.CopperGrays.MaxLength = 2;
            this.CopperGrays.Name = "CopperGrays";
            this.CopperGrays.Size = new System.Drawing.Size(18, 14);
            this.CopperGrays.TabIndex = 50;
            this.CopperGrays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // SilverGrays
            // 
            this.SilverGrays.BackColor = System.Drawing.Color.Black;
            this.SilverGrays.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SilverGrays.ForeColor = System.Drawing.Color.White;
            this.SilverGrays.Location = new System.Drawing.Point(243, 271);
            this.SilverGrays.MaxLength = 2;
            this.SilverGrays.Name = "SilverGrays";
            this.SilverGrays.Size = new System.Drawing.Size(17, 14);
            this.SilverGrays.TabIndex = 49;
            this.SilverGrays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // GoldBox
            // 
            this.GoldBox.Location = new System.Drawing.Point(171, 267);
            this.GoldBox.Name = "GoldBox";
            this.GoldBox.Size = new System.Drawing.Size(151, 24);
            this.GoldBox.TabIndex = 52;
            this.GoldBox.TabStop = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox4);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(692, 309);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Protected";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 24);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(692, 309);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Misc";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // MrItemRemover2Gui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(715, 350);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(715, 350);
            this.MinimumSize = new System.Drawing.Size(715, 350);
            this.Name = "MrItemRemover2Gui";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mr.ItemRemover2 - Removing items from your bags.";
            this.Load += new System.EventHandler(this.MrItemRemover2GUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Time)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.resf)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GoldBox)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown Time;
        private System.Windows.Forms.ListBox MyBag;
        private System.Windows.Forms.ListBox RemoveList;
        private System.Windows.Forms.ListBox SellList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button AddToRemoveList;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button AddToProtList;
        private System.Windows.Forms.Button AddToSellList;
        private System.Windows.Forms.Button RemoveRemoveItem;
        private System.Windows.Forms.Button RemoveSellItem;
        private System.Windows.Forms.ListBox ProtectedList;
        private System.Windows.Forms.Button RemoveProtectedItem;
        private System.Windows.Forms.Button AddToBagList;
        private System.Windows.Forms.TextBox InputAddToBagItem;
        public System.Windows.Forms.CheckBox EnableRemove;
        private System.Windows.Forms.CheckBox EnableSell;
        private System.Windows.Forms.CheckBox SellWhite;
        private System.Windows.Forms.CheckBox SellGreen;
        private System.Windows.Forms.CheckBox SellGray;
        private System.Windows.Forms.CheckBox GrayItems;
        private System.Windows.Forms.CheckBox RemoveQItems;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.CheckBox EnableOpen;
        private System.Windows.Forms.PictureBox resf;
        private System.Windows.Forms.CheckBox LootEnable;
        private System.Windows.Forms.CheckBox SellBlue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox BlueItems;
        private System.Windows.Forms.CheckBox WhiteItems;
        private System.Windows.Forms.CheckBox GreenItems;
        private System.Windows.Forms.CheckBox SellSoulbound;
        private System.Windows.Forms.CheckBox CombineItems;
        private System.Windows.Forms.CheckBox RSFood;
        private System.Windows.Forms.CheckBox RSDrinks;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.ComboBox SellDropdown;
        private System.Windows.Forms.ComboBox OpeningDropDown;
        private System.Windows.Forms.ComboBox CombineDropDown;
        private System.Windows.Forms.ComboBox RemoveDropDown;
        private System.Windows.Forms.ComboBox CheckAfterLootDropdown;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox GoldGrays;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox CopperGrays;
        private System.Windows.Forms.TextBox SilverGrays;
        private System.Windows.Forms.PictureBox GoldBox;
    }
}