using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using MouseWorkflowAutomation.Controls;
using MouseWorkflowAutomation.Models;
using MouseWorkflowAutomation.Properties;
using Newtonsoft.Json;

namespace MouseWorkflowAutomation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            WorkflowCanvas.Children.Clear();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                // Load workflow from file
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                Workflow workflow = new Workflow();
                foreach (UIElement element in WorkflowCanvas.Children)
                {
                    if (element is WorkflowItem item)
                    {
                        workflow.Actions.Add(item.Action);
                    }
                }

                string json = JsonConvert.SerializeObject(workflow, Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
                File.WriteAllText(saveFileDialog.FileName, json);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Toolbox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Button button)
            {
                DragDrop.DoDragDrop(button, button.Content.ToString(), DragDropEffects.Copy);
            }
        }

        private void WorkflowCanvas_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        private void WorkflowCanvas_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        private void WorkflowCanvas_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.StringFormat) is string actionName)
            {
                Point position = e.GetPosition(WorkflowCanvas);

                WorkflowAction? action = actionName switch
                {
                    "Click" => new ClickAction(),
                    "Move" => new MoveAction(),
                    "Drag &amp; Drop" => new DragAndDropAction(),
                    "Image Click" => new ImageClickAction(),
                    "Right Click" => new RightClickAction(),
                    "Delay" => new DelayAction(),
                    _ => null
                };

                if (action != null)
                {
                    WorkflowItem newItem = new WorkflowItem
                    {
                        ItemName = actionName,
                        Action = action
                    };

                    newItem.Selected += WorkflowItemSelected;

                    Canvas.SetLeft(newItem, position.X);
                    Canvas.SetTop(newItem, position.Y);

                    WorkflowCanvas.Children.Add(newItem);
                }
            }
        }

        private void WorkflowItemSelected(object? sender, EventArgs e)
        {
            if (sender is WorkflowItem item)
            {
                switch (item.Action)
                {
                    case ClickAction clickAction:
                        PropertiesPanel.Content = new ClickProperties { Action = clickAction };
                        break;
                    case MoveAction moveAction:
                        PropertiesPanel.Content = new MoveProperties { Action = moveAction };
                        break;
                    case DragAndDropAction dragAndDropAction:
                        PropertiesPanel.Content = new DragAndDropProperties { Action = dragAndDropAction };
                        break;
                    case ImageClickAction imageClickAction:
                        PropertiesPanel.Content = new ImageClickProperties { Action = imageClickAction };
                        break;
                    case RightClickAction rightClickAction:
                        PropertiesPanel.Content = new RightClickProperties { Action = rightClickAction };
                        break;
                    case DelayAction delayAction:
                        PropertiesPanel.Content = new DelayProperties { Action = delayAction };
                        break;
                    default:
                        PropertiesPanel.Content = null;
                        break;
                }
            }
        }
    }
}
