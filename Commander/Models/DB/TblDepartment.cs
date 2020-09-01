// User
//  Scaffold-DbContext "Server=LAPTOP-K1N85LM7;Database=CommanderDB;Trusted_Connection=True;user id=TestUser;password=pa55word;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/DB 
// to create Models/DB/TbsDepartment etc from DB


using System;
using System.Collections.Generic;

namespace Commander.Models.DB
{
    public partial class TblDepartment
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
    }
}
