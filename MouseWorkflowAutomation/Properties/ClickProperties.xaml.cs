using System.Windows;
using System.Windows.Controls;
using MouseWorkflowAutomation.Models;

namespace MouseWorkflowAutomation.Properties
{
    public partial class ClickProperties : UserControl
    {
        public static readonly DependencyProperty ActionProperty =
            DependencyProperty.Register("Action", typeof(ClickAction), typeof(ClickProperties), new PropertyMetadata(null));

        public ClickAction Action
        {
            get { return (ClickAction)GetValue(ActionProperty); }
            set { SetValue(ActionProperty, value); }
        }

        public ClickProperties()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}