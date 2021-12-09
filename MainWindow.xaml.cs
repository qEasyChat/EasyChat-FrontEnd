using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using Microsoft.Win32;
using System.Drawing;
using System.IO;
using System.Security;


namespace EasyChat_FrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("EasyChat-DLLs.dll", CallingConvention = CallingConvention.Cdecl)]
        extern static int new_server(int x);
        [DllImport("EasyChat-DLLs.dll", CallingConvention = CallingConvention.Cdecl)]
        extern static int get_driver_type(string db_type);
        [DllImport("EasyChat-DLLs.dll", CallingConvention = CallingConvention.Cdecl)]
        extern static int connect_to_database(IntPtr server,int driver_type, string file_path);

        [DllImport("EasyChat-DLLs.dll", CallingConvention = CallingConvention.Cdecl)]
        extern static int start(IntPtr server);

        private OpenFileDialog openFileDialog;

        public MainWindow()
        {
            InitializeComponent();
            openFileDialog = new OpenFileDialog();
        }

        private void runServerButton_Click(object sender, RoutedEventArgs e)
        {
            welcomeGrid.Visibility = Visibility.Hidden;
            startServerGrid.Visibility = Visibility.Visible;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/qEasyChat");
        }

        private void startServerButton_Click(object sender, RoutedEventArgs e)
        {
            string serverName = serverNameTextBox.Text;
            string portString = portNumberTextBox.Text;
            int port = Int32.Parse(portString)
            IntPtr server = new_server();
        }

        private void selectDBFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string filepath = openFileDialog.FileName;
                    MessageBox.Show(filepath);
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                                    $"Details:\n\n{ex.StackTrace}");
                }
            }

        }
    }
}
