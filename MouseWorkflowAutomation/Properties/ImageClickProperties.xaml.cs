using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using MouseWorkflowAutomation.Models;

namespace MouseWorkflowAutomation.Properties
{
    public partial class ImageClickProperties : UserControl
    {
        public static readonly DependencyProperty ActionProperty =
            DependencyProperty.Register("Action", typeof(ImageClickAction), typeof(ImageClickProperties), new PropertyMetadata(null));

        public ImageClickAction Action
        {
            get { return (ImageClickAction)GetValue(ActionProperty); }
            set { SetValue(ActionProperty, value); }
        }

        public ImageClickProperties()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Action.ImagePath = openFileDialog.FileName;
            }
        }
    }
}
