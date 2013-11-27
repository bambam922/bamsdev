using System;
using System.IO;
using Styx;
using Styx.Helpers;

namespace MrItemRemover2
{
    public class MrItemRemover2Settings : Settings
    {
        public static readonly MrItemRemover2Settings Instance = new MrItemRemover2Settings();

        private MrItemRemover2Settings()
            : base(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Settings/MrItemRemover2_Settings_{0}.xml",
                             StyxWoW.Me.Name))
        {
        }

        [Setting, Styx.Helpers.DefaultValue(5)]
        public int Time { get; set; }

        [Setting, Styx.Helpers.DefaultValue("False")]
        public string EnableRemove { get; set; }

        [Setting, Styx.Helpers.DefaultValue("False")]
        public string EnableOpen { get; set; }

        [Setting, Styx.Helpers.DefaultValue("False")]
        public string CombineItems { get; set; }

        [Setting, Styx.Helpers.DefaultValue(false)]
        public bool DeleteQuestItems { get; set; }

        [Setting, Styx.Helpers.DefaultValue("False")]
        public string EnableSell { get; set; }

        [Setting, Styx.Helpers.DefaultValue("False")]
        public string LootEnable { get; set; }

        [Setting, Styx.Helpers.DefaultValue(true)]
        public bool EnableGray { get; set; }

        [Setting, Styx.Helpers.DefaultValue(false)]
        public bool SellWhite { get; set; }

        [Setting, Styx.Helpers.DefaultValue(false)]
        public bool SellGreen { get; set; }

        [Setting, Styx.Helpers.DefaultValue(false)]
        public bool SellBlue { get; set; }

        [Setting, Styx.Helpers.DefaultValue(false)]
        public bool SellSoulbound { get; set; }

        [Setting, Styx.Helpers.DefaultValue(false)]
        public bool SellGray { get; set; }

        [Setting, Styx.Helpers.DefaultValue(true)]
        public bool ApplyAll { get; set; }

        [Setting, Styx.Helpers.DefaultValue(true)]
        public bool DeleteAllGray { get; set; }

        [Setting, Styx.Helpers.DefaultValue(false)]
        public bool DeleteAllWhite { get; set; }

        [Setting, Styx.Helpers.DefaultValue(false)]
        public bool DeleteAllGreen { get; set; }

        [Setting, Styx.Helpers.DefaultValue(false)]
        public bool DeleteAllBlue { get; set; }

        [Setting, Styx.Helpers.DefaultValue(1)]
        public int GoldGrays { get; set; }

        [Setting, Styx.Helpers.DefaultValue(53)]
        public int SilverGrays { get; set; }

        [Setting, Styx.Helpers.DefaultValue(41)]
        public int CopperGrays { get; set; }

        [Setting, Styx.Helpers.DefaultValue(false)]
        public bool RSFood { get; set; }

        [Setting, Styx.Helpers.DefaultValue(false)]
        public bool RSDrinks { get; set; }
    }
}