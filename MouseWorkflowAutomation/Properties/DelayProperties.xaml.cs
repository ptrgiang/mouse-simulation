using System.Windows;
using System.Windows.Controls;
using MouseWorkflowAutomation.Models;

namespace MouseWorkflowAutomation.Properties
{
    public partial class DelayProperties : UserControl
    {
        public static readonly DependencyProperty ActionProperty =
            DependencyProperty.Register("Action", typeof(DelayAction), typeof(DelayProperties), new PropertyMetadata(null));

        public DelayAction Action
        {
            get { return (DelayAction)GetValue(ActionProperty); }
            set { SetValue(ActionProperty, value); }
        }

        public DelayProperties()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
