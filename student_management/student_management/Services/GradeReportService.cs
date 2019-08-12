using student_management.DataAccess;
using student_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.Services
{
    public class GradeReportService
    {
        GradeReportRepo repo = new GradeReportRepo();
        public GradeReport AddGradeReport(int sectionID, string studentID, float mid, float fin, float other, float total)
        {
            return repo.AddGradeReport(sectionID, studentID, mid, fin, other, total);
        }

        public GradeReport GetGradeReport(int sectionID, string studentID)
        {
            return repo.GetGradeReport(sectionID, studentID);
        }

        public GradeReport UpdateGradeReport(int sectionID, string studentID, float mid, float fin, float other, float total)
        {
            return repo.UpdateGradeReport(sectionID, studentID, mid, fin, other, total);
        }
    }
}
