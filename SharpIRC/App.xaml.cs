﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using IRC;
using ViewModel;


namespace SharpIRC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// A list of all IRC connections.
        /// </summary>
        public Client IRCConnection;
        /// <summary>
        /// An event handler for all user commands
        /// </summary>

        public App()
        {

            // Default connection
            IRCConnection = new Client(); // todo populate from config file
        }

        protected override void OnLoadCompleted(NavigationEventArgs e)
        {
            base.OnLoadCompleted(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            // close IRC connections
            if(IRCConnection.IsConnected)
                IRCConnection.Disconnect();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
    }
}