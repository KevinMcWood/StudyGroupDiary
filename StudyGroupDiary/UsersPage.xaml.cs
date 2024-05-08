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
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();

            DGridUsers.ItemsSource = StudyGroupDiaryBDEntities.GetContext().Users.ToList();
        }


        private void BtnOpenPB_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var parameter = button.Tag as Users;
            Manager.MainFrame.Navigate(new PersonalBusinessPage(parameter));

        }

        private void BtnOpenAnket_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AnketsPage());
        }


        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PersonalBusinessPage(new Users()));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                StudyGroupDiaryBDEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(action: p => p.Reload());
                DGridUsers.ItemsSource = StudyGroupDiaryBDEntities.GetContext().Users.ToList();
            }
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var usersForRemoving = DGridUsers.SelectedItems.Cast<Users>().ToList();

            if (MessageBox.Show($"Вы действительно хотите удалить следующие {usersForRemoving.Count()} элементов?", "ВНИМАНИЕ!", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    StudyGroupDiaryBDEntities.GetContext().Users.RemoveRange(usersForRemoving);
                    StudyGroupDiaryBDEntities.GetContext().SaveChanges();
                    MessageBox.Show("ДАННЫЕ УДАЛЕНЫ!");

                    DGridUsers.ItemsSource = StudyGroupDiaryBDEntities.GetContext().Users.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
