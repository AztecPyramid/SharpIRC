﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using IRC;

namespace SharpIRC.ViewModel
{
    public class ChannelViewModel
    {
        public ObservableCollection<string> TimeStamps { get; private set; }
        public ObservableCollection<string> Messages { get; private set; }
        public ObservableCollection<User> Users { get; set; } // TODO could be userviewmodel
        public string InputText { get; set; }

        public ChannelViewModel(Channel channel)
            : this()
        {
            _channel = channel;

            //Application.Current.MainWindow.Dispatcher

            // todo pretty format message (colours)
            channel.Message += (sender, m) => Application.Current.Dispatcher.Invoke(
                new Action(() => Message(m)));

            channel.Users.CollectionChanged += (sender, args) =>
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    if (args.Action == NotifyCollectionChangedAction.Add)
                        Users.Add(((User) args.NewItems[0]));

                    if (args.Action == NotifyCollectionChangedAction.Remove)
                        Users.Remove(((User)args.OldItems[0]));
                }));
        }

        public void Clear()
        {
            TimeStamps.Clear();
            Messages.Clear();
        }

        public void AddUser(User user)
        {
        }

        public void Message(Message message)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (Action) (() =>
                {
                    TimeStamps.Add(String.Format("[{0:HH:mm:ss}]\t<{1}>", DateTime.Now, message.User.Nick));
                    Messages.Add(message.Text);
                }));
        }

        // todo maybe move me to serverviewmodel when needed
        public void SystemMessage(string message)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (Action) (() =>
                {
                    TimeStamps.Add(String.Format("[{0:HH:mm:ss}]\t\t*", DateTime.Now));
                    Messages.Add(message);
                }));
        }

        public ChannelViewModel()
        {
            TimeStamps = new ObservableCollection<string>();
            Messages = new ObservableCollection<string>();
            Users = new ObservableCollection<User>();
        }

        public string Header
        {
            get { return _channel.Name ?? string.Empty; }
        }

        public override string ToString()
        {
            return _channel.Name ?? string.Empty;
        }

        private readonly Channel _channel;
    }
}
