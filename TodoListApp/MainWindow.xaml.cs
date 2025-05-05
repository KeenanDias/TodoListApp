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
                todoListbox.ItemsSource = _context.todoItems.Local.ToObservableCollection();
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
                AddItemTextBox.Clear();
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (todoListbox.SelectedItem is todoItems selectedItem)
            {
                _context.todoItems.Remove(selectedItem);
                _context.SaveChanges();
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // You can add logic here to handle selection changes if needed
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