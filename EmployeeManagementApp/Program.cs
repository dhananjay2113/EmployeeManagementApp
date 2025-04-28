using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

class Employee
{
    public int ID;
    public string Name;
    public string Department;

}
class EmployeeManagement
{
    static List<Employee> employees = new List<Employee>();
    static string filePath = "employee.txt";
    static void Main()
    {
        bool EmpProg = true;
        LoadEmployee();

        while (EmpProg)
        {
            Console.WriteLine("--------KallWik Employee Management--------");
            Console.WriteLine("1.Add Employee");
            Console.WriteLine("2.View Employee");
            Console.WriteLine("3.Delete Employee");
            Console.WriteLine("4.Edit Employee");

            int choice = Convert.ToInt16(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddEmployee();
                    break;

                case 2:
                    ViewEmployees();
                    break;

                case 3:
                    DeleteEmployee();
                    break;

                case 4:
                    EditEmployee();
                    break;

                default:
                    Console.WriteLine("Enter Valid Input");
                    break;


            }
        }
    }
    static void AddEmployee()
    {
        Employee emp = new Employee();

        Console.Write("Enter ID: ");
        emp.ID = Convert.ToInt16(Console.ReadLine());

        Console.Write("Enter Name: ");
        emp.Name = Console.ReadLine();
        

        Console.Write("Enter Department: ");
        emp.Department = Console.ReadLine();

        employees.Add(emp);
        Console.WriteLine("Employee added succesfully");

        SaveEmployees();

    }

    static void SaveEmployees()
    {
        List<string> lines = new List<string>();
        foreach (Employee emp in employees)
        {
            string line = $"{emp.ID},{emp.Name},{emp.Department}";    //string interpolation
            lines.Add(line);
        }
        File.WriteAllLines(filePath, lines);
        
    }


    static void LoadEmployee()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                var parts = line.Split(',');   //why not strings? because the list had different data types

                if (parts.Length == 3)
                {
                    Employee emp = new Employee();
                    emp.ID = int.Parse(parts[0]);
                    emp.Name = parts[1];
                    emp.Department = parts[2];
                    
                    employees.Add(emp);
                        
                    
                    Console.WriteLine("Employee data found");
                }
                
            }
        }
    }

    static void ViewEmployees()
    {
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            Employee emp = new Employee
            {
                ID = int.Parse(parts[0]),
                Name = parts[1],
                Department = parts[2]
            };


            Console.WriteLine($"ID: {emp.ID}, Name: {emp.Name}, Department: {emp.Department}");
        }
       
    } 

    static void DeleteEmployee()
    {
        Console.WriteLine("Enter ID to remove Employee:");
        int id = Convert.ToInt32(Console.ReadLine());
        
        Employee empDelete = null;
        foreach (Employee emp in employees)
        {
            if (emp.ID == id)
            {
                empDelete = emp;
            }
            

        }
        if (empDelete != null)
        {
            employees.Remove(empDelete);
            SaveEmployees();
        }
        else
        {
            Console.WriteLine("Employee was not found");
        }
    }

    static void EditEmployee()
    {
        Console.Write("Enter Employee ID to edit:");
        int id = Convert.ToInt16(Console.ReadLine());

        Employee empEdit = null;
        foreach (Employee employee in employees)
        {
            if (employee.ID == id)
            {
                empEdit = employee;
                break;
            }
            
        }
        if (empEdit != null)
        {
            Console.WriteLine("Enter new Name: ");
            empEdit.Name = Console.ReadLine();

            Console.WriteLine("Enter new department: ");
            empEdit.Department = Console.ReadLine();
            SaveEmployees();
        }
        else
        {
            Console.WriteLine("No Employees Found");
        }
    }
}
