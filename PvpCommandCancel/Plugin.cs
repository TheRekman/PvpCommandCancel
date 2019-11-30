using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
namespace PvpCommandCancel
{
    public class Plugin : TerrariaPlugin
    {
        public override string Name => "PvpCommandCancel";
        public override Version Version => System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        public override string Author => "Rekman";
        public override string Description => "Cancel command use in PvP.";

        public Plugin(Main game) : base(game)
        {

        }
        public override void Initialize()
        {
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
