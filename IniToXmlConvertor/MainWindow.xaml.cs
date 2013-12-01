using System;
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
using System.IO;
using Microsoft.Win32;
using System.Xml.Linq;
using IniToXmlModel;

namespace IniToXmlConvertor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonLoadIniFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialogLoad = new OpenFileDialog();
            openFileDialogLoad.Filter = "Ini files (.ini)|*.ini";
            openFileDialogLoad.FilterIndex = 1;
            openFileDialogLoad.Multiselect = false;

            if (openFileDialogLoad.ShowDialog() == true)
            {
                textBoxLoadIniFile.Text = openFileDialogLoad.FileName;
                textBoxIniContent.Text = IniToXmlModel.IniToXmlModel.ReadEntireFile(openFileDialogLoad.FileName);
            }
        }

        private void buttonLoadXmlFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialogLoad = new OpenFileDialog();
            openFileDialogLoad.Filter = "Xml files (.xml)|*.xml";
            openFileDialogLoad.FilterIndex = 1;
            openFileDialogLoad.Multiselect = false;

            if (openFileDialogLoad.ShowDialog() == true)
            {
                textBoxLoadXmlFile.Text = openFileDialogLoad.FileName;
                
            }
        }

        private void buttonConvert_Click(object sender, RoutedEventArgs e)
        {
            // check if the output xml file exists
            if (!File.Exists(textBoxLoadXmlFile.Text))
            {
                MessageBox.Show(Properties.Resources.MissingXmlFile, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var outputFile = textBoxLoadXmlFile.Text;

            // check if the input ini file exists
            if (!File.Exists(textBoxLoadIniFile.Text))
            {
                MessageBox.Show(Properties.Resources.MissingIniFile, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var inputFile = textBoxLoadIniFile.Text;
            string appRootEl = Properties.Resources.MyAppRoot;

            // check if root value is inserted and change if this is the case
            if (!textBoxAppRootElement.Text.Equals(Properties.Resources.RootElement))
            {
                appRootEl = textBoxAppRootElement.Text;
            }

            try
            {
                XDocument doc = IniToXmlModel.IniToXmlModel.ConvertIni(inputFile, appRootEl);
                doc.Save(outputFile);

                textBoxLoadXmlFile.Text = IniToXmlModel.IniToXmlModel.ReadEntireFile(outputFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
