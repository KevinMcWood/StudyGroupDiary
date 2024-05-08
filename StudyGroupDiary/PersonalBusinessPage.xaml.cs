using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    /// Логика взаимодействия для PersonalBusinessPage.xaml
    /// </summary>
    public partial class PersonalBusinessPage : Page
    {
        private Users _currentUsers = new Users();
        private Passports _currentPassports = new Passports();

        public PersonalBusinessPage(Users selectedUsers)
        {
            InitializeComponent();

            selectedUsers.Passport = StudyGroupDiaryBDEntities.GetContext().Passports.ToList().FirstOrDefault(x=>x.UIDP==selectedUsers.ID);
            if (selectedUsers != null) //Таблицы users
            {
                _currentUsers = selectedUsers;
            }
            DataContext = _currentUsers;

            /*if (selectedPassports != null) //таблица passports
            {
                _currentPassports = selectedPassports;
            }
            DataContext = _currentPassports;*/

        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentUsers.Name))
                errors.AppendLine("Укажите Имя!");
            if (string.IsNullOrWhiteSpace(_currentUsers.Surname))
                errors.AppendLine("Укажите Фамилию!");
            if (string.IsNullOrWhiteSpace(_currentUsers.MiddleName))
                errors.AppendLine("Укажите Отчество!");
            if (string.IsNullOrWhiteSpace(_currentUsers.Role))
                errors.AppendLine("Укажите Роль!");
            if (string.IsNullOrWhiteSpace(_currentUsers.DateOfBirth))
                errors.AppendLine("Укажите дату рождения!");
            if (string.IsNullOrWhiteSpace(_currentUsers.Gender))
                errors.AppendLine("Укажите пол!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentUsers.ID == 0)
                StudyGroupDiaryBDEntities.GetContext().Users.Add(_currentUsers);
                StudyGroupDiaryBDEntities.GetContext().Passports.Add(_currentPassports);
            try
            {
                StudyGroupDiaryBDEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog p = new PrintDialog();
            if(p.ShowDialog() == true)
            {
                p.PrintVisual(GridPB, "Печатать");
            }
        }
    }
}
