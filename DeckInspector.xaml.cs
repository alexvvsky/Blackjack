using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Blackjack
{
    /// <summary>
    /// Interaction logic for DeckInspector.xaml
    /// </summary>
    public partial class DeckInspector : Window
    {
        public DeckInspector()
        {
            InitializeComponent();
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Application.Current.MainWindow;
            window.MoveItems(DeckInspectorListBox.SelectedIndex, DeckInspectorListBox.SelectedIndex - 1);
        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Application.Current.MainWindow; // probably a better way to do this
            window.MoveItems(DeckInspectorListBox.SelectedIndex, DeckInspectorListBox.SelectedIndex + 1);
        }
    }
}
