using System.Windows;
using System.Windows.Controls;
using MouseWorkflowAutomation.Models;

namespace MouseWorkflowAutomation.Properties
{
    public partial class RightClickProperties : UserControl
    {
        public static readonly DependencyProperty ActionProperty =
            DependencyProperty.Register("Action", typeof(RightClickAction), typeof(RightClickProperties), new PropertyMetadata(null));

        public RightClickAction Action
        {
            get { return (RightClickAction)GetValue(ActionProperty); }
            set { SetValue(ActionProperty, value); }
        }

        public RightClickProperties()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
