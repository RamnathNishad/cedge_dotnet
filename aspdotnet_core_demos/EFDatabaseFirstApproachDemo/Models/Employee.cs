using System;
using System.Collections.Generic;

namespace EFDatabaseFirstApproachDemo.Models;

public partial class Employee
{
    public int Ecode { get; set; }

    public string? Ename { get; set; }

    public int? Salary { get; set; }

    public int? Deptid { get; set; }
}
