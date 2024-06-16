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
    /// Логика взаимодействия для GradesPage.xaml
    /// </summary>
    public partial class GradesPage : Page
    {
        public GradesPage()
        {
            InitializeComponent();


        }

        private void LoadSummaryData()
        {
            using (var context = new StudyGroupDiaryBDEntities())
            {
                var query = from u in context.Users
                            join g in context.Grade on u.ID equals g.UserIDGrade
                            join s in context.Subject on g.SubjectIDGrade equals s.SubjectID
                            select new
                            {
                                StudentName = u.Name,
                                SubjectName = s.SubjectName,
                                Grade = g.GradeValue
                            };

                SummaryDataGrid.ItemsSource = query.ToList();
            }
        }
    }
}
