using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClosedXML.Excel;

namespace StudyGroupDiary
{
    /// <summary>
    /// Логика взаимодействия для PersonalBusinessPage.xaml
    /// </summary>
    public partial class PersonalBusinessPage : Page
    {
        private Users _currentUsers = new Users();
        private Passports _currentPassports = new Passports();
        private Enrollment _currentEnrollment = new Enrollment();
        private PersonalBusiness _currentPersonalBusiness = new PersonalBusiness();
        private BitmapImage photoImage;

        public PersonalBusinessPage(Users selectedUsers)
        {

            InitializeComponent();

            selectedUsers.Passport = StudyGroupDiaryBDEntities.GetContext().Passports.ToList().FirstOrDefault(x => x.UIDP == selectedUsers.ID);
            selectedUsers.Enrollments = StudyGroupDiaryBDEntities.GetContext().Enrollment.ToList().FirstOrDefault(x => x.EID == selectedUsers.ID);
            selectedUsers.PersonalBusines = StudyGroupDiaryBDEntities.GetContext().PersonalBusiness.ToList().FirstOrDefault(x => x.PBID == selectedUsers.ID);
            if (selectedUsers != null) //Таблицы users
            {
                _currentUsers = selectedUsers;
            }
            DataContext = _currentUsers;

            var allGender = StudyGroupDiaryBDEntities.GetContext().Gender.ToList();
            ComboGender.ItemsSource = allGender;
            ComboGender.DisplayMemberPath = "Gender1";
            ComboGender.SelectedValuePath = "IDGender";

            var allSpec = StudyGroupDiaryBDEntities.GetContext().Spevialisition.ToList();
            ComboSpec.ItemsSource = allSpec;
            ComboSpec.DisplayMemberPath = "NameSpecialisation";
            ComboSpec.SelectedValuePath = "IDS";

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

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentUsers.ID == 0)
                StudyGroupDiaryBDEntities.GetContext().Users.Add(_currentUsers);
                StudyGroupDiaryBDEntities.GetContext().Passports.Add(_currentPassports);
                StudyGroupDiaryBDEntities.GetContext().Enrollment.Add(_currentEnrollment);
            try
            {
                StudyGroupDiaryBDEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                MessageBox.Show(ex.InnerException.ToString());
                MessageBox.Show(ex.HResult.ToString());
            }

            if (photoImage != null) //СОХРАНЕНИЕ ФОТО ПОКА НЕ ТРОГАТЬ!
            {
                using (StudyGroupDiaryBDEntities entities = new StudyGroupDiaryBDEntities())
                {
                    byte[] photoData;

                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(photoImage));

                    using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                    {
                        encoder.Save(stream);
                        photoData = stream.ToArray();
                    }

                    SqlParameter param = new SqlParameter("@Photo", photoData);
                    entities.Database.ExecuteSqlCommand("INSERT INTO Users (Photo) VALUES (@Photo)", param);
                }
            }
        }

        
        private void UpdateUsers()
        {
            var currentUser = StudyGroupDiaryBDEntities.GetContext().Users.ToList();

            if (ComboGender.SelectedIndex > 0)
                currentUser = currentUser.Where(p => p.Gender.Contains(ComboGender.SelectedItem as Gender)).ToList();        
        }


        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog p = new PrintDialog();
            if (p.ShowDialog() == true)
            {
                p.PrintVisual(GridPB, "Печатать");
            }
        }

        private void BtnDosti_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new DostiPage());
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Image files (*.jpg,*.jpeg,*.png,*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                photoImage = new BitmapImage(new Uri(filename));
                imgPhoto.Source = photoImage;
            }
        }

        private void ComboGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUsers();
        }
    }       
}
