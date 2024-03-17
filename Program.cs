using System.Text.Json;
using EmployeeManager;

const string fileName = "employeeData.json";

if (!File.Exists(fileName))
{
    FileStream fs = File.Create(fileName);
    fs.Close();
}

List<Employee> importedEmployees = ImportEmployees();
bool appRunning = true;
string? userMenuOption;

do
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("--- Welcome to the Employee Management System ---");
    Console.WriteLine("--------- Created By: Naveed Mahmoudian ---------");
    Console.WriteLine("-------------------------------------------------");

    Console.WriteLine("Choose from the following options:");
    Console.WriteLine("1. View all Employees");
    Console.WriteLine("2. Add an Employee");
    Console.WriteLine("3. Update an Employee");
    Console.WriteLine("4. Remove an employee");
    Console.WriteLine("5. Exit the application\n");

    userMenuOption = Console.ReadLine();

    switch (userMenuOption)
    {
        case "1":
            ViewAllEmployees();
            break;
        case "2":
            AddEmployee();
            break;
        case "3":
            UpdateEmployee();
            break;
        case "4":
            DeleteEmployee();
            break;
        case "5":
            ExitApplication();
            break;
        default:
            break;
    }
}
while (appRunning);

List<Employee> ImportEmployees()
{
    string employeeJson = File.ReadAllText(fileName);
    try
    {
        return JsonSerializer.Deserialize<List<Employee>>(employeeJson) ?? new List<Employee>();
    }
    catch
    {
        return new List<Employee>();
    }
}

void ViewAllEmployees()
{
    Console.WriteLine("View All Employees\n");

    StreamReader sr = new StreamReader(fileName);
    if (sr.ReadLine() == null)
    {
        Console.WriteLine("No Employees...");
    }
    else
    {
        foreach (Employee employee in importedEmployees)
        {
            Console.WriteLine($"ID: {employee.ID}");
            Console.WriteLine($"First Name: {employee.FirstName}");
            Console.WriteLine($"Last Name: {employee.LastName}");
            Console.WriteLine($"Email: {employee.Email}");
            Console.WriteLine($"Department: {employee.Department}");
            Console.WriteLine($"Title: {employee.Title}");
            Console.WriteLine($"Salary: {employee.Salary:C2}");
            Console.WriteLine();
        }
    }
    Console.ReadLine();
}

void AddEmployee()
{
    bool addAnEmployee = true;
    string? userAddEmployee;

    do
    {
        int id;
        if (importedEmployees.Count > 0)
        {
            id = importedEmployees.Count + 1;
        }
        else
        {
            id = 1;
        }
        string firstName = "";
        string lastName = "";
        string email = "";
        string department = "";
        string title = "";
        int salary;
        bool validFirstName = false;
        bool validLastName = false;
        bool validEmail = false;
        bool validDepartment = false;
        bool validTitle = false;
        bool validSalary = false;

        Console.WriteLine("Add an Employee:\n");

        do
        {
            Console.WriteLine("First Name:");
            string? userFirstName = Console.ReadLine();
            if (userFirstName != null && userFirstName.Length >= 2)
            {
                firstName = userFirstName;
                validFirstName = true;
            }
            else
            {
                Console.WriteLine("Please enter a valid first name");
            }
        } while (validFirstName == false);

        do
        {
            Console.WriteLine("Last Name:");
            string? userLastName = Console.ReadLine();
            if (userLastName != null && userLastName.Length >= 2)
            {
                lastName = userLastName;
                validLastName = true;
            }
            else
            {
                Console.WriteLine("Please enter a valid last name");
            }
        } while (validLastName == false);

        do
        {
            Console.WriteLine("Email:");
            string? userEmail = Console.ReadLine();
            if (userEmail != null && userEmail.Length >= 6 && userEmail.Contains('@'))
            {
                email = userEmail;
                validEmail = true;
            }
            else
            {
                Console.WriteLine("Please enter a valid email");
            }
        }
        while (validEmail == false);

        do
        {
            Console.WriteLine("Department:");
            string? userDepartment = Console.ReadLine();
            if (userDepartment != null && userDepartment.Length >= 2)
            {
                department = userDepartment;
                validDepartment = true;
            }
            else
            {
                Console.WriteLine("Please enter a valid department");
            }
        }
        while (validDepartment == false);

        do
        {
            Console.WriteLine("Title:");
            string? userTitle = Console.ReadLine();
            if (userTitle != null && userTitle.Length >= 2)
            {
                title = userTitle;
                validTitle = true;
            }
            else
            {
                Console.WriteLine("Please enter a valid title");
            }
        }
        while (validTitle == false);

        do
        {
            Console.WriteLine("Salary (numbers only):");
            string? salaryString = Console.ReadLine();
            if (int.TryParse(salaryString, out salary))
            {
                validSalary = true;
            }
            else
            {
                Console.WriteLine("Please enter a valid salary");
            }
        }
        while (validSalary == false);

        Employee employee = new Employee(id, firstName, lastName, email, department, title, salary);

        importedEmployees.Add(employee);
        string employeeJson = JsonSerializer.Serialize(importedEmployees, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, employeeJson);
        Console.WriteLine("\nEmployee Added!");
        Console.WriteLine("Would you like to add another employee? (y/n)");
        userAddEmployee = Console.ReadLine();

        if (userAddEmployee == null || userAddEmployee.ToLower().Trim() != "y")
        {
            addAnEmployee = false;
        }
    }
    while (addAnEmployee);
}

void UpdateEmployee()
{
    int employeeId = -1;
    bool editedEmployee = false;

    Console.WriteLine("Update an Employee\n");

    do
    {
        Console.Write("Enter Employee ID: ");
        string? userEmployeeId = Console.ReadLine();

        if (userEmployeeId == null || !int.TryParse(userEmployeeId, out employeeId) || employeeId < 0)
        {
            Console.WriteLine("Invalid Employee ID\n");
        }
        else
        {
            Employee? employeeToEdit = importedEmployees.FirstOrDefault(e => e.ID == employeeId);
            if (employeeToEdit != null)
            {
                Console.WriteLine("Edit First Name? (y/n)");
                Console.WriteLine($"CURRENT: {employeeToEdit.FirstName}");
                string? editFirstName = Console.ReadLine();
                if (editFirstName != null && editFirstName.ToLower().Trim() == "y")
                {
                    bool validFirstName = false;
                    do
                    {
                        Console.WriteLine("Enter new First Name:");
                        string? newFirstName = Console.ReadLine();
                        if (newFirstName != null && newFirstName.Length >= 2)
                        {
                            employeeToEdit.FirstName = newFirstName;
                            validFirstName = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid First Name");
                        }
                    }
                    while (validFirstName == false);
                }

                Console.WriteLine("Edit Last Name? (y/n)");
                Console.WriteLine($"CURRENT: {employeeToEdit.LastName}");
                string? editLastName = Console.ReadLine();
                if (editLastName != null && editLastName.ToLower().Trim() == "y")
                {
                    bool validLastName = false;
                    do
                    {
                        Console.WriteLine("Enter new Last Name:");
                        string? newLastName = Console.ReadLine();
                        if (newLastName != null && newLastName.Length >= 2)
                        {
                            employeeToEdit.LastName = newLastName;
                            validLastName = true;
                        }
                    }
                    while (validLastName == false);
                }

                Console.WriteLine("Edit Email? (y/n)");
                Console.WriteLine($"CURRENT: {employeeToEdit.Email}");
                string? editEmail = Console.ReadLine();
                if (editEmail != null && editEmail.ToLower().Trim() == "y")
                {
                    bool validEmail = false;
                    do
                    {
                        Console.WriteLine("Enter new Email:");
                        string? newEmail = Console.ReadLine();
                        if (newEmail != null && newEmail.Length >= 6 && newEmail.Contains('@'))
                        {
                            employeeToEdit.Email = newEmail;
                            validEmail = true;
                        }
                    }
                    while (validEmail == false);
                }

                Console.WriteLine("Edit Department? (y/n)");
                Console.WriteLine($"CURRENT: {employeeToEdit.Department}");
                string? editDepartment = Console.ReadLine();
                if (editDepartment != null && editDepartment.ToLower().Trim() == "y")
                {
                    bool validDepartment = false;
                    do
                    {
                        Console.WriteLine("Enter new Department:");
                        string? newDepartment = Console.ReadLine();
                        if (newDepartment != null && newDepartment.Length >= 2)
                        {
                            employeeToEdit.Department = newDepartment;
                            validDepartment = true;
                        }

                    }
                    while (validDepartment == false);
                }

                Console.WriteLine("Edit Title? (y/n)");
                Console.WriteLine($"CURRENT: {employeeToEdit.Title}");
                string? editTitle = Console.ReadLine();
                if (editTitle != null && editTitle.ToLower().Trim() == "y")
                {
                    bool validTitle = false;
                    do
                    {
                        Console.WriteLine("Enter new Title:");
                        string? newTitle = Console.ReadLine();
                        if (newTitle != null && newTitle.Length >= 2)
                        {
                            employeeToEdit.Title = newTitle;
                            validTitle = true;
                        }
                    }
                    while (validTitle == false);
                }

                Console.WriteLine("Edit Salary? (y/n)");
                Console.WriteLine($"CURRENT: {employeeToEdit.Salary:C2}");
                string? editSalary = Console.ReadLine();
                if (editSalary != null && editSalary.ToLower().Trim() == "y")
                {
                    bool validSalary = false;
                    do
                    {
                        Console.WriteLine("Enter new Salary (numbers only):");
                        string? newUserSalary = Console.ReadLine();
                        if (newUserSalary != null && int.TryParse(editSalary, out int newSalary))
                        {
                            employeeToEdit.Salary = newSalary;
                            validSalary = true;
                        }
                    }
                    while (validSalary == false);
                }

                string employeeJson = JsonSerializer.Serialize(importedEmployees, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fileName, employeeJson);
                Console.WriteLine("Employee Updated!");
                Console.WriteLine("Press Enter to return to the main menu");
                Console.ReadLine();
                editedEmployee = true;
            }
            else
            {
                Console.WriteLine("Employee not found");
            }
        }

    }
    while (editedEmployee == false);

}

void DeleteEmployee()
{
    int employeeId = -1;
    bool deletingEmployee = false;

    Console.WriteLine("Delete an Employee\n");

    do
    {
        Console.WriteLine("WARNING: This cannot be undone!");
        Console.Write("Enter Employee ID: ");
        string? userEmployeeId = Console.ReadLine();

        if (userEmployeeId == null || !int.TryParse(userEmployeeId, out employeeId) || employeeId < 0)
        {
            Console.WriteLine("Invalid Employee ID\n");
        }
        else
        {
            Employee? employeeToRemove = importedEmployees.SingleOrDefault(e => e.ID == employeeId);
            if (employeeToRemove != null)
            {
                Console.WriteLine("Employee:");
                Console.WriteLine($"ID: {employeeToRemove.ID}");
                Console.WriteLine($"First Name: {employeeToRemove.FirstName}");
                Console.WriteLine($"Last Name: {employeeToRemove.LastName}");
                Console.WriteLine($"Email: {employeeToRemove.Email}");
                Console.WriteLine($"Department: {employeeToRemove.Department}");
                Console.WriteLine($"Title: {employeeToRemove.Title}");
                Console.WriteLine($"Salary: {employeeToRemove.Salary:C2}");
                Console.WriteLine("Are you sure you want to delete this Employee? THIS CANNOT BE UNDONE. (y/n):");

                string? userConfirmDelete = Console.ReadLine();
                if (userConfirmDelete != null && userConfirmDelete.ToLower().Trim() == "y")
                {
                    importedEmployees.Remove(employeeToRemove);
                    string employeeJson = JsonSerializer.Serialize(importedEmployees, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(fileName, employeeJson);
                    Console.WriteLine("Employee Removed!");
                    Console.WriteLine("Press Enter to return to the main menu");
                    Console.ReadLine();
                    deletingEmployee = true;
                }
                else
                {
                    Console.WriteLine("Operation cancelled");
                    Console.WriteLine("Press Enter to return to the main menu");
                    Console.ReadLine();
                    deletingEmployee = true;
                }

            }
            else
            {
                Console.WriteLine("Employee not found");
            }
        }

    } while (deletingEmployee == false);
}

void ExitApplication()
{
    appRunning = false;
}
