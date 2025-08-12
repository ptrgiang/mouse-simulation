using System.Windows;
using System.Windows.Controls;
using MouseWorkflowAutomation.Models;

namespace MouseWorkflowAutomation.Properties
{
    public partial class DragAndDropProperties : UserControl
    {
        public static readonly DependencyProperty ActionProperty =
            DependencyProperty.Register("Action", typeof(DragAndDropAction), typeof(DragAndDropProperties), new PropertyMetadata(null));

        public DragAndDropAction Action
        {
            get { return (DragAndDropAction)GetValue(ActionProperty); }
            set { SetValue(ActionProperty, value); }
        }

        public DragAndDropProperties()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
