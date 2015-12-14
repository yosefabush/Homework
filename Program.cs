using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Homework_Project
{
    class MainClass
    {
        static List<SimpleUser> Users = new List<SimpleUser>(); 
        static AdminUser admin = new AdminUser();
        static TeacherUser teacher = new TeacherUser();
        static SimpleUser currentUser = new SimpleUser();
        public static void Main(string[] args)
        {
            while (true)
            {
                string input;
                while (true)
                {
                    Console.WriteLine("\t**********************Welcome to Share Homework!***********************");
                    Console.WriteLine("\t*\t\t\t\t\t\t\t\t      *");
                    Console.WriteLine("\t*\t\t\t\t\t\t\t\t      *");
                    Console.WriteLine("\t*\t\t\t\t\t\t\t\t      *");
                    Console.WriteLine("\t*\t\t\t\t\t\t\t\t      *");
                    Console.WriteLine("\t*\t\t\t\t\t\t\t\t      *");
                    Console.WriteLine("\t*********2015 All rights reserved : Lior,Meitr,Lidor,Yosef ************");
                    Console.WriteLine("1. Login\n2. Sign Up\n\n0. Exit");
                    input = Console.ReadLine();
                    if (input != "1" || input != "0" || input != "2")
                        break;
                }
                switch (input)
                {
                    case "1":
                        Console.Clear();
                        LoginMenu();
                        break;
                    case "2":
                        Console.Clear();
                        SignupMenu();
                        break;
                    case "0":
                        Console.Clear();
                        return;
                       
                    default:
                        Console.Clear();
                        break;
                }
            }
        }

        private static void LoginMenu() { 
            Console.WriteLine("Please Login:");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.Clear();
            int userType = 0;
            DataBase.Instance.CreateUsers();        //temp - using this function only because database isn't live yet
            DataBase.Instance.CreateTasks();        //temp - using this function only because database isn't live yet
            if (DataBase.Instance.isAdmin(username, password)!="")
            {
                admin = admin.getAdmin(username,password);
                userType = 1;
            }
            else if (DataBase.Instance.isTeacher(username, password)!="")
            {
                teacher = teacher.getTeacher(DataBase.Instance.isTeacher(username, password));
                userType = 2;
            }
            else if (DataBase.Instance.isSimpleUser(username, password)!="")
            {
                currentUser = currentUser.getUser(DataBase.Instance.isSimpleUser(username, password));
                userType = 3;
            }
            switch (userType)
            {
                case 0:
                    Console.WriteLine("Username/Password was wrong, try again");
                    break;
                case 1:
                    AdminMenu();
                    break;
                case 2:
                    TeacherMenu();
                    break;
                case 3:
                    SimpleUserMenu();
                    break;
                default:
                    break;

            }

        }

        private static void SignupMenu()
        {
            int userType = 0;
            string input;
            bool end = false;
            while (!end)
            {
                Console.WriteLine("Choose Account Type:\n" +
                    "1. Admin\n2. Teacher\n3. User\n\n0. Back");
                input = Console.ReadLine();


                switch (input)
                {
                    case "0":
                        Console.Clear();
                        return;
                    case "1":
                        userType = 1;
                        end = true;
                        break;
                    case "2":
                        userType = 2;
                        end = true;
                        break;
                    case "3":
                        userType = 3;
                        end = true;
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
            
            Console.Clear();
            string username="";
            string password="";
            string email="";
            bool exit = false;
            Users = DataBase.Instance.getAllUsers(-1);
            while (!exit)
            {
                InputValidation iv = new InputValidation();
                Console.Write("Enter -1 Any Time To Cancel\n");
                while (true)
                {
                    Console.WriteLine("Username must contain at least 4 characters\n" +
                        "Choose Username: ");
                    username = Console.ReadLine();
                    if (username.Equals("-1"))
                    {
                        Console.Clear();
                        return;
                    }
                    if (!iv.isValidUsername(username))
                    {
                        Console.Clear();
                        Console.WriteLine("Not a valid Username");
                        continue;
                    }
                    break;
                }
                while (true)
                {
                    Console.WriteLine("Password must contain a combination of at least 8 letters and numbers\nChoose Password: ");
                    password = Console.ReadLine();
                    if (password.Equals("-1"))
                    {
                        Console.Clear();
                        return;
                    }
                    if (!(iv.isValidPassword(password)))
                    {
                        Console.Clear();
                        Console.WriteLine("Not a valid Password");
                        Console.WriteLine("Username must contain at least 4 characters\n" +
                        "Choose Username:\n"+username);
                        continue;
                    }
                    break;
                }
                while (true)
                {
                    Console.WriteLine("Re-Enter Password: ");
                    string passwordAgain = Console.ReadLine();
                    if (passwordAgain.Equals("-1"))
                    {
                        Console.Clear();
                        return;
                    }
                    if (!(password.Equals(passwordAgain)))
                    {
                        Console.Clear();
                        Console.WriteLine("Passwords do not match");
                        Console.WriteLine("Username must contain at least 4 characters\n" +
                        "Choose Username:\n" + username);
                        Console.WriteLine("Password must contain a combination of at least 8 letters and numbers\nChoose Password:\n"+password);
                        continue;
                    }
                    break;
                }
                while (true)
                {
                    Console.WriteLine("Enter Email: ");
                    email = Console.ReadLine();
                    if (email.Equals("-1"))
                    {
                        Console.Clear();
                        return;
                    }
                    
                    if (!iv.isValidEmail(email))
                    {
                        Console.Clear();
                        Console.WriteLine("Not a Valid Email");
                        Console.WriteLine("Username must contain at least 4 characters\n" +
                            "Choose Username:\n" + username);
                        Console.WriteLine("Password must contain a combination of at least 8 letters and numbers\nChoose Password:\n" + password);
                        Console.WriteLine("Re-Enter Password:\n"+password);
                        continue;
                    }

                    foreach (SimpleUser su in Users)
                    {
                        if (su.Email.Equals(email))
                        {
                            Console.Clear();
                            Console.WriteLine("User with that Email already exists");
                            Console.WriteLine("Username must contain at least 4 characters\n" +
                                "Choose Username:\n" + username);
                            Console.WriteLine("Password must contain a combination of at least 8 letters and numbers\nChoose Password:\n" + password);
                            Console.WriteLine("Re-Enter Password:\n" + password);
                            continue;
                        }
                        break;
                    }
                    break;
                }
               
                exit = true;
                Console.Clear();
            }
            switch (userType)
            {
              
                case 1:
                    admin = new AdminUser(username,password,email);
                    //admin.createAdmin();
                    AdminMenu();
                    break;
                case 2:
                    teacher = new TeacherUser(username, password, email);
                    TeacherMenu();
                    break;
                case 3:
                    currentUser = new SimpleUser(username, password, email);
                    SimpleUserMenu();
                    break;
                default:
                    break;

            }
        }

        private static void AdminMenu()
        {
            DataBase.Instance.CreateUsers();        //temp - using this function only because database isn't live yet
            DataBase.Instance.CreateTasks();        //temp - using this function only because database isn't live yet
            bool exit = false;
            while (!exit)
            {
                int input;
                while (true)
                {
                    Console.WriteLine("Welcome " + admin.UserName + "!\n1. Add Task\n2. Add User\n3. Add Teacher\n4. Delete Task\n5. Remove User\n6. Remove Teacher\n" +
                        "7. Edit Task Name\n8. Edit Task Deadline\n9. Show All Tasks\n10. Show All Users\n\n0. Exit");
                    input = Int32.Parse(Console.ReadLine());
                    if (input >= 0 && input <= 10)
                        break;
                    else
                        Console.WriteLine("Bad Input");

                }
                Console.Clear();
                switch (input)
                {
                    case 0:
                        exit = true;
                        //  LoginMenu();
                        break;
                    case 1:
                        admin.addNewTask();
                        break;
                    case 2:
                        admin.addUser();
                        break;
                    case 3:
                        admin.addTeacher();
                        break;
                    case 4:
                        admin.deleteTask();
                        break;
                    case 5:
                        admin.removeUser();
                        break;
                    case 6:

                        break;
                    case 7:
                        admin.editTaskName();
                        break;
                    case 8:
                        admin.editTaskDeadline();
                        break;
                    case 9:
                        admin.printAllTasks();
                        break;
                    case 10:
                        admin.printAllUsers();
                        break;


                    default:
                        break;
                }
            }
        }

        private static void TeacherMenu()
        {

            bool exit = false;
            while (!exit)
            {
                int input;

                while (true)
                {
                    Console.WriteLine("Welcome " + teacher.UserName + "!\n1. Add Task\n2. Print list of users who finished task\n3. Exit");
                    input = Int32.Parse(Console.ReadLine());
                    if (input > 0 && input <= 3)
                        break;
                    else
                        Console.WriteLine("Bad Input");

                }
                Console.Clear();
                switch (input)
                {

                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        exit = true;
                        LoginMenu();
                        break;

                    default:
                        break;

                }
            }
        }

        private static void SimpleUserMenu()
        {
            bool exit = false;
            while (!exit)
            {
                int input;

                while (true)
                {
                    Console.WriteLine("Hello " + currentUser.UserName + "!\n1. Mark Tasks\n2. Delete marked Tasks\n3. Exit");
                    input = Int32.Parse(Console.ReadLine());
                    if (input > 0 && input <= 3)
                        break;
                    else
                        Console.WriteLine("Bad Input");

                }
                Console.Clear();
                switch (input)
                {

                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        exit = true;
                        LoginMenu();
                        break;

                    default:
                        break;

                }
            }
        }

    }
}
