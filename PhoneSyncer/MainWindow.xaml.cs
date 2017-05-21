﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhoneSyncer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            button.Click += Button_Click;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EnumerateWPDs();
        }

        private void EnumerateWPDs()
        {
            var collection = new PortableDeviceCollection();
            collection.Refresh();
            StringBuilder devicelist = new StringBuilder();
            foreach (var device in collection)
            {
                device.Connect();
                devicelist.AppendLine(device.DeviceId);
                device.Disconnect();
            }

            textBlock.Text = devicelist.ToString();
        }
    }
}
