using System;
using System.Collections;
using System.Globalization;

namespace Homework_Project
{
    class MainClass
    {
        static ArrayList Users = new ArrayList(); 
        static AdminUser admin;
        static TeacherUser teacher;
        static SimpleUser currentUser;
        public static void Main(string[] args)
        {


            while (true)
            {
                string input;
                while (true)
                {
                    Console.WriteLine("Welcome to Share Homework!");
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
            DataBase.Instance.CreateUsers();
            if (DataBase.Instance.isAdmin(username, password))
            {
                admin = DataBase.Instance.getAdmin();
                userType = 1;
            }
            else if (DataBase.Instance.isTeacher(username, password))
            {
                userType = 2;
            }
            else if (DataBase.Instance.isSimpleUser(username, password))
            {
                userType = 3;
            }
            switch (userType)
            {
                case 0:
                    Console.WriteLine("Username is not in this class...");
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
            Console.Write("Choose Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.Write("Re-Enter Password: ");
            string passwordAgain = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Clear();

            switch (userType)
            {
              
                case 1:
                    AdminUser au = new AdminUser(username,password,email);
                    au.createAdmin();
                    //AdminMenu();
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

        private static void AdminMenu()
        {
            bool exit = false;
            while (!exit)
            {
                int input;
                
                while (true)
                {
                    Console.WriteLine("Welcome " + admin.UserName + "!\n1. Add Task\n2. Add User\n3. Add Teacher\n4. Delete Task\n5. Remove User\n6. Remove Teacher\n" +
                        "7. Edit Task Name\n8. Edit Task Deadline\n9. Show All Tasks\n10. Logout\n\n0. Exit");
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
                       // admin = null;
                        return;
                    case 1:

                        admin.addNewTask();
                        break;
                    case 2:
                        /*   Console.WriteLine ("Enter User's Email");
                             string UserEmail = Console.ReadLine ();
                             SimpleUser su = new SimpleUser("lider", "abcd1234", "a@b.com");
                             Users.Add(su);  */
                        break;
                    case 3:

                        break;
                    case 4:
                        admin.deleteTask();
                        break;
                    case 5:

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
                        exit = true;
                        LoginMenu();
                        break;

                    default:
                        break;
                }
            }
        }

        private static void TeacherMenu()
        {
            Console.WriteLine("Helo Irit!!");
            Console.WriteLine("Choose 1 to add Task\n2 for print list of users how finish task");
        }

        private static void SimpleUserMenu()
        {
            Console.WriteLine("hello " + currentUser.UserName + "\nChoose:\n 1 - to mark Tasks\n 2 - to delete mark tasks");
        }


       
        


      
    }
}
