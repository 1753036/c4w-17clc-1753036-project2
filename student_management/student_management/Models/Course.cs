﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management.Models
{
    public class Course
    {
        public string ID { get; set; }
        public string Fullname { get; set; }
        public string Room { get; set; }

        public void Print()
        {
            Console.WriteLine("ID: {0}, Name: {1}, Room: {2}", ID, Fullname, Room);
        }
    }
}
