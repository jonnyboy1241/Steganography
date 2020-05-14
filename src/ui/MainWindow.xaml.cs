using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Steganography
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

        // This function is run when the button is clicked. Gets the png file and creates a bitmap
        private void SelectPicture(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Image files (*.png) |*.png",
                RestoreDirectory = true
            };

            // Initialize the OpenFileDialog
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFileName = dlg.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                stegImage.Source = bitmap;
            }
        }

        private void EmbedFile(object sender, RoutedEventArgs e)
        {
            if(stegImage.Source == null)
            {
                System.Windows.MessageBox.Show("Select an image first!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            System.Windows.MessageBox.Show("Good job, you have an image!", "Yay", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}
