﻿using System;
using System.Collections.Generic;
using System.IO;
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
using gearXmlViewer.LanguageManager;

namespace gearXmlViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ListView mainView;

        public MainWindow()
        {
            InitializeComponent();
            mainView = (ListView)FindName("MainList");
        }

        private void LoadText(object sender, RoutedEventArgs e)
        {
            const string filePath = @"D:\Dev\WelcomeRoom\Assets\Scenes\WelcomeRoom.unity";
            StreamReader file;
            try
            {
                file = new System.IO.StreamReader(filePath);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return;
            }
            
            var translations = new List<Translation>();
            var m_text = new Dictionary<string, string>();
            var m_key = new Dictionary<string, string>();

            string line;
            string fileId = null;
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains("m_GameObject:"))
                {
                    fileId = line;
                }
                else if (fileId != null && line.Contains("m_text:"))
                {
                    m_text[fileId] = line.Replace(" m_text: ", "");
                    fileId = null;
                }
                else if (fileId != null && line.Contains(" key:"))
                {
                    m_key[fileId] = line.Replace("key: ", "");
                    fileId = null;
                }
            }

            foreach (var objectId in m_text.Keys)
            {
                if (m_key.ContainsKey(objectId))
                {
                    translations.Add(new Translation(){Key = m_key[objectId], English = m_text[objectId], German = objectId});
                }
            }


            MainDataGrid.ItemsSource = translations;

        }

        private void MainDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
        }
    }
}
