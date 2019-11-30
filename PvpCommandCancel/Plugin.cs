using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace PvpCommandCancel
{
    public class Plugin : TerrariaPlugin
    {
        public override string Name => "PvpCommandCancel";
        public override Version Version => System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        public override string Author => "Rekman";
        public override string Description => "Cancel command use in PvP.";

        Config Config;
        
        public Plugin(Main game) : base(game)
        {

        }

        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
            PlayerHooks.PlayerCommand += OnPlayerCommand;
        }

        private void OnInitialize(EventArgs args)
        {
            Config = Config.Read();
            Commands.ChatCommands.Add(new Command("pcc.configcmd", ConfigCmd, "/pcc", "/pvpcmdcancel"));
            
        }

        private void OnPlayerCommand(PlayerCommandEventArgs args)
        {
            if (!args.Player.TPlayer.hostile || args.Player.HasPermission("pcc.ignore")) 
                return;
            if (Config.UnallowedCommands.First(cmd => cmd == args.CommandName) != null)
            {
                args.Player.SendErrorMessage("This command does not allowed in PvP!");
                args.Handled = true;
            }
        }

        private void ConfigCmd(CommandArgs args)
        {
            if (args.Parameters.Count < 1) args.Parameters.Add("help");

            switch (args.Parameters[0])
            {
                case "add":
                    if(args.Parameters.Count == 2)
                    {
                        Command cmd = Commands.ChatCommands.Find(c => c.Names.First(n => n == args.Parameters[1]) != null);
                        if(cmd != null)
                        {
                            Config.WriteUnallowedCommand(cmd.Name);
                            args.Player.SendSuccessMessage($"Succesfully unallow {cmd.Name} command.");
                        }
                        else
                        {
                            args.Player.SendErrorMessage($"Invalid command {args.Parameters[1]}!");
                        }
                    }
                    else
                    {
                        args.Player.SendErrorMessage("Invalid syntax! Proper syntax: {0}/pcc add <commandName>");
                    }
                    break;
                case "reset":
                    Config = Config.Create();
                    args.Player.SendSuccessMessage($"Succesfully reset config file.");
                    break;
            }
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
