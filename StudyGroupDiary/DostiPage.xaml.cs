using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudyGroupDiary
{
    /// <summary>
    /// Логика взаимодействия для DostiPage.xaml
    /// </summary>
    public partial class DostiPage : Page
    {
        public DostiPage()
        {
            InitializeComponent();
        }

        private void UpdateDosti()
        {
            var currentDosti = StudyGroupDiaryBDEntities.GetContext().Dostigenia.ToList();

            if (ComboType.SelectedIndex > 0)
                currentDosti = currentDosti.Where(p => p.Type.Contains(ComboType.SelectedItem as Type)).ToList();

            currentDosti = currentDosti.Where(p => p.NameDosti.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();


            LViewTours.ItemsSource = currentDosti.OrderBy(p => p.Mesto).ToList();

        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDosti();
        }

        private void AddDosti_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DelDosti_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
