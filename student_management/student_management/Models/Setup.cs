using student_management.Models;
using student_management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.Views
{
    public class Setup
    {
        public Auth Auth { get; set; }
        public ClassService ClassSvc = new ClassService();
    }
}
