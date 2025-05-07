using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;

namespace TodoListApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private todoContext _context = new todoContext();

        public MainWindow()
        {
            InitializeComponent();
            LoadTodoItems();
        }

        private void LoadTodoItems()
        {
            try
            {
                _context.Database.EnsureCreated();
                _context.todoItems.Load(); // Load data into the local cache
                todoPanel.Children.Clear(); // Clear any existing checkboxes
                foreach (var item in _context.todoItems.Local)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.Content = item.description;
                    checkBox.IsChecked = false; // Initialize as not completed
                    checkBox.Margin = new Thickness(5);
                    checkBox.Tag = item; // Store the todoItem object in the Tag
                    checkBox.Checked += CheckBox_Checked; // Attach event handler
                    checkBox.Unchecked += CheckBox_Unchecked;
                    todoPanel.Children.Add(checkBox);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching items: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(AddItemTextBox.Text))
            {
                var newItem = new todoItems { description = AddItemTextBox.Text };
                _context.todoItems.Add(newItem);
                _context.SaveChanges();

                CheckBox checkBox = new CheckBox();
                checkBox.Content = newItem.description;
                checkBox.IsChecked = false;
                checkBox.Margin = new Thickness(5);
                checkBox.Tag = newItem; // Store the new item
                checkBox.Checked += CheckBox_Checked;
                checkBox.Unchecked += CheckBox_Unchecked;
                todoPanel.Children.Add(checkBox);
                AddItemTextBox.Clear();
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            List<CheckBox> checkedBoxes = new List<CheckBox>();
            foreach (UIElement element in todoPanel.Children)
            {
                if (element is CheckBox checkBox && checkBox.IsChecked == true)
                {
                    checkedBoxes.Add(checkBox);
                }
            }

            foreach (CheckBox checkBox in checkedBoxes)
            {
                if (checkBox.Tag is todoItems itemToRemove)
                {
                    _context.todoItems.Remove(itemToRemove);
                    todoPanel.Children.Remove(checkBox); // Remove the checkbox
                }
            }
            _context.SaveChanges();
            LoadTodoItems(); //refresh
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                checkBox.Foreground = Brushes.Gray;
                TextBlock textBlock = new TextBlock
                {
                    Text = checkBox.Content.ToString(),
                    TextDecorations = TextDecorations.Strikethrough
                };
                checkBox.Content = textBlock;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                checkBox.Foreground = Brushes.Black;
                if (checkBox.Content is TextBlock textBlock)
                {
                    checkBox.Content = textBlock.Text;
                }
            }
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}