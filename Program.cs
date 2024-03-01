using System.Text.Json;
using EmployeeManager;

bool appRunning = true;
string fileName = "employeeData.json";
Employee[]? employees = [];

Console.WriteLine("...Loading");
if (!File.Exists(fileName))
{
    try
    {
        using (FileStream fs = File.Create(fileName))
        {
            Console.WriteLine("...Complete");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        Console.ReadLine();
        appRunning = false;
    }
}
else
{
    try
    {
        string json = File.ReadAllText(fileName);
        employees = JsonSerializer.Deserialize<Employee[]>(json);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        Console.ReadLine();
        appRunning = false;
    }
}
Console.WriteLine("...Complete");

do
{
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("--- Welcome to the Employee Management System ---");
    Console.WriteLine("-------- Created By: Naveed Mahmoudian ----------");
    Console.WriteLine("-------------------------------------------------");

    Console.WriteLine("Choose from the following options:");
    Console.WriteLine("1. View all Employees");
    Console.WriteLine("2. Add an Employee");
    Console.WriteLine("3. Update an Employee");
    Console.WriteLine("4, Remove an employee");
    Console.WriteLine("5. Exit the application\n");

    string? userOption = Console.ReadLine();
    bool userChoosingOptions = true;

    while (userChoosingOptions)
    {
        if (userOption != null)
        {
            switch (userOption)
            {
                case "1":
                    Console.WriteLine("View all employees\n");
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        if (reader.ReadLine() == null)
                        {
                            Console.WriteLine("No Employees...");
                        }
                    }
                    Console.ReadLine();
                    userChoosingOptions = false;
                    break;

                case "2":
                    bool addAnEmployee = true;
                    do
                    {
                        Console.WriteLine("Add an employee\n");

                        int numberOfEmployees;
                        if (employees != null)
                        {
                            numberOfEmployees = employees.Length;
                        }
                        else
                        {
                            numberOfEmployees = 0;
                        }

                        int employeeID = numberOfEmployees + 1;

                        Console.WriteLine("First Name:");
                        string? employeeFirstName = Console.ReadLine();
                        Console.WriteLine("Last Name:");
                        string? employeeLastName = Console.ReadLine();
                        Console.WriteLine("Email:");
                        string? employeeEmail = Console.ReadLine();
                        Console.WriteLine("Department:");
                        string? employeeDepartment = Console.ReadLine();
                        Console.WriteLine("Title:");
                        string? employeeTitle = Console.ReadLine();
                        Console.WriteLine("Salary");
                        string? employeeSalaryString = Console.ReadLine();
                        int employeeSalary = int.Parse(employeeSalaryString);

                        Employee employee = new Employee(
                            id: employeeID,
                            firstName: employeeFirstName,
                            lastName: employeeLastName,
                            email: employeeEmail,
                            department: employeeDepartment,
                            title: employeeTitle,
                            salary: employeeSalary
                        );

                        string json = JsonSerializer.Serialize(employee, new JsonSerializerOptions { WriteIndented = true });
                        File.WriteAllText(fileName, json);
                        Console.WriteLine("Employee added!");
                        Console.WriteLine("Add another y/n?");
                        string? userAddAnotherEmployee = Console.ReadLine();
                        if (userAddAnotherEmployee != null)
                        {
                            string userChoice = userAddAnotherEmployee.ToLower().Trim();
                            if (userChoice == "y")
                            {
                                break;
                            }
                            else if (userChoice == "n")
                            {
                                addAnEmployee = false;
                            }
                        }
                    }
                    while (addAnEmployee);
                    break;


                case "3":
                    Console.WriteLine("Under Construction");
                    break;

                case "4":
                    Console.WriteLine("Under construction");
                    break;

                case "5":
                    userChoosingOptions = false;
                    appRunning = false;
                    break;
            }
        }
    }
} while (appRunning);
