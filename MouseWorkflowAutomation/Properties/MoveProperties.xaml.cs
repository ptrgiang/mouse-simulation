using System.Windows;
using System.Windows.Controls;
using MouseWorkflowAutomation.Models;

namespace MouseWorkflowAutomation.Properties
{
    public partial class MoveProperties : UserControl
    {
        public static readonly DependencyProperty ActionProperty =
            DependencyProperty.Register("Action", typeof(MoveAction), typeof(MoveProperties), new PropertyMetadata(null));

        public MoveAction Action
        {
            get { return (MoveAction)GetValue(ActionProperty); }
            set { SetValue(ActionProperty, value); }
        }

        public MoveProperties()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
