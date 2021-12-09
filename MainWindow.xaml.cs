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
        extern static IntPtr new_server(string server_name, int port);
        [DllImport("EasyChat-DLLs.dll", CallingConvention = CallingConvention.Cdecl)]
        extern static int get_driver_type(string db_type);
        [DllImport("EasyChat-DLLs.dll",
            EntryPoint = "?connect_to_database@Server@@QEAAXW4Database_Driver_Type@@V?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@@Z",
            CallingConvention = CallingConvention.ThisCall)]
        extern static void connect_to_database(IntPtr server, int db_type, string file_path);

        [DllImport("EasyChat-DLLs.dll",
            EntryPoint = "?start@Server@@QEAAXXZ",
            CallingConvention = CallingConvention.ThisCall)]
        extern static void start(IntPtr server);

        private OpenFileDialog openFileDialog;
        private string db_file_path;
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
            int port = Int32.Parse(portString);
            try
            {
                IntPtr server = new_server(serverName, port);
                string database_type = databaseTypeComboBox.Text;
                connect_to_database(server, 1, db_file_path);
                start(server);
                MessageBox.Show("Server started successfully.");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void selectDBFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    db_file_path = openFileDialog.FileName;
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                                    $"Details:\n\n{ex.StackTrace}");
                }
            }

        }

        private void serverNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            serverNameTextBox.Text = "";
        }

        private void portNumberTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            portNumberTextBox.Text = "";
        }
    }
}
