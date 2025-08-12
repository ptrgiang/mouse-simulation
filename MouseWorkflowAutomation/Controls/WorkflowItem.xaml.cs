using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MouseWorkflowAutomation.Models;

namespace MouseWorkflowAutomation.Controls
{
    public partial class WorkflowItem : UserControl
    {
        public static readonly DependencyProperty ItemNameProperty =
            DependencyProperty.Register("ItemName", typeof(string), typeof(WorkflowItem), new PropertyMetadata("Workflow Item"));

        public string ItemName
        {
            get { return (string)GetValue(ItemNameProperty); }
            set { SetValue(ItemNameProperty, value); }
        }

        public WorkflowAction? Action { get; set; }

        public event EventHandler<EventArgs>? Selected;

        private bool isDragging = false;
        private Point startPoint;

        public WorkflowItem()
        {
            InitializeComponent();
            this.DataContext = this;

            this.PreviewMouseLeftButtonDown += WorkflowItem_PreviewMouseLeftButtonDown;
            this.PreviewMouseMove += WorkflowItem_PreviewMouseMove;
            this.PreviewMouseLeftButtonUp += WorkflowItem_PreviewMouseLeftButtonUp;
            this.GotFocus += WorkflowItem_GotFocus;
        }

        private void WorkflowItem_GotFocus(object sender, RoutedEventArgs e)
        {
            Selected?.Invoke(this, EventArgs.Empty);
        }

        private void WorkflowItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            startPoint = e.GetPosition(this.Parent as IInputElement);
            this.CaptureMouse();
            this.Focus();
        }

        private void WorkflowItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPoint = e.GetPosition(this.Parent as IInputElement);
                double newLeft = currentPoint.X - startPoint.X + Canvas.GetLeft(this);
                double newTop = currentPoint.Y - startPoint.Y + Canvas.GetTop(this);
                Canvas.SetLeft(this, newLeft);
                Canvas.SetTop(this, newTop);
                startPoint = currentPoint;
            }
        }

        private void WorkflowItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            this.ReleaseMouseCapture();
        }
    }
}