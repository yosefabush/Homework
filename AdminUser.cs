using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Homework_Project
{
	public class AdminUser:SimpleUser
	{
        private static long counter = 0;
		private long classId;

        public AdminUser() { }

		public AdminUser (string username ,string password, string email):base(username,password,email)
		{

            ClassID = counter++;
          //  DataBase.Instance.createAdmin(username,password,email);

		}
			
		public long ClassID{
			get{return this.classId;}
			set{this.classId = value;}
		}

		public override void Print(){
			base.Print ();

			Console.WriteLine ("Class ID: " + ClassID);
		}

        /***********************Manage Tasks************************/

		public void markDoneTask(long courseId){
			DataBase.Instance.markDoneTask (courseId);
		}

		public void printAllTasks(){
			DataBase.Instance.printAllTasks ();
		}

        public void addNewTask()
        {
            //get task name from admin user
            Console.WriteLine("Enter Task Name: ");
            string taskName = Console.ReadLine();
            //get task deadline from admin user
            string inputDeadline = "01/01/1970";
            DateTime deadline = DateTime.ParseExact(inputDeadline, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            while (true) {   //datetime format validation loop, ends when coreect input is entered
                Console.WriteLine("Enter Deadline to complete the task(dd/MM/yyyy): ");
                inputDeadline = Console.ReadLine();
                if (DateTime.TryParseExact(inputDeadline, "dd/MM/yyyy", CultureInfo.InvariantCulture,DateTimeStyles.None, out deadline))
                {
                    deadline = DateTime.ParseExact(inputDeadline, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    break;
                }
                Console.Clear();
                Console.WriteLine("Bad Date Format - Use dd/MM/yyyy Only!");
            }
            //add the task to DataBase
            DataBase.Instance.addTask (ClassID, taskName, deadline);
            Console.Clear();
            Console.WriteLine("Task was successfuly added!");

		}

        public void deleteTask()
        {
            while (true) { 
                Console.WriteLine("Enter Task Name to delete('-1' to cancel): ");
                string taskName = Console.ReadLine();
                if (taskName.Equals("-1"))
                    break;
                long taskId = getTaskID(taskName);
                if (taskId >= 0)
                {
                    DataBase.Instance.deleteTask(taskId);
                    Console.WriteLine("Task was successfuly deleted");
                    break;
                }
                else
                    Console.WriteLine("No task was found with that name");

             }
        }

        public long getTaskID(string taskName)
        {

            return DataBase.Instance.getTaskId(taskName);

        }

        public void editTaskName()
        {
            while (true)
            {
                Console.WriteLine("Enter Task Name to edit('-1' to cancel): ");
                string taskName = Console.ReadLine();
                if (taskName.Equals("-1"))
                {
                    Console.Clear();
                    break;
                }
                Console.WriteLine("Enter new Name: ");
                string taskNameEdit = Console.ReadLine();
                long taskId = getTaskID(taskName);
                if (taskId >= 0)
                {
                    DataBase.Instance.updateTaskName(taskId, taskNameEdit);
                    Console.Clear();
                    Console.WriteLine("Task was successfuly updated");
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("No task was found with that name");
                }
            }
        }

        public void editTaskDeadline()
        {
            while (true)
            {
                Console.WriteLine("Enter Task Name to edit('-1' to cancel): ");
                string taskName = Console.ReadLine();
                if (taskName.Equals("-1"))
                {
                    Console.Clear();
                    break;
                }
                string deadlineEdit = "01/01/1970";
                DateTime newDeadline = DateTime.ParseExact(deadlineEdit, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                while (true)    //datetime format validation loop, ends when coreect input is entered
                {
                    Console.WriteLine("Enter new Deadline date(dd/MM/yyyy): ");
                    deadlineEdit = Console.ReadLine();
                    if (DateTime.TryParseExact(deadlineEdit, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out newDeadline))
                    {
                        newDeadline = DateTime.ParseExact(deadlineEdit, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        break;
                    }
                    Console.Clear();
                    Console.WriteLine("Bad Date Format - Use dd/MM/yyyy Only!");
                }
                long taskId = getTaskID(taskName);
                if (taskId >= 0)
                {
                    DataBase.Instance.updateTaskDeadline(taskId, newDeadline);
                    Console.Clear();
                    Console.WriteLine("Task was successfuly updated");
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("No task was found with that name");
                }
            }
        }

        /***********************Manage Users************************/
        public void addUser()
        {
            string userEmail;
            InputValidation iv = new InputValidation();
            while (true)
            {
                Console.WriteLine("Enter User's Email");
                userEmail = Console.ReadLine();
                if (!iv.isValidEmail(userEmail))
                {
                    Console.WriteLine("Not a valid Email");
                    continue;
                }
                break;

            }
            List<SimpleUser> Users = getAllUsers();
            foreach (SimpleUser su in Users)
                if (su.Email.Equals(userEmail))
                {
                    DataBase.Instance.addUser(su);
                    break;
                }
            Console.Clear();
            Console.WriteLine("No user with that Email was found");
                
        }

        public void removeUser()
        {
            Console.WriteLine("Enter the Email of User you want to remove: ");
            string email = Console.ReadLine();
            List<SimpleUser> Users = getAllUsers();
            foreach (SimpleUser su in Users)
                if (su.Email.Equals(email))
                {
                    Console.Clear();
                    Console.WriteLine(su.UserName + " was successfuly removed");
                    DataBase.Instance.removeUser(email);          
                    return;
                }
            Console.Clear();
            Console.WriteLine("No User with that Email was found");
        }

        public SimpleUser getUser(string email)
        {
            return DataBase.Instance.getUser(email);
        }

        public List<SimpleUser> getAllUsers()
        {
            return DataBase.Instance.getAllUsers();
        }
        public void addTeacher(TeacherUser teacher)
        {
            DataBase.Instance.createTeacher(teacher);
        }
        public void addTeacher()
        {
            string userEmail;
            InputValidation iv = new InputValidation();
            while (true)
            {
                Console.WriteLine("Enter Teacher's Email");
                userEmail = Console.ReadLine();
                if (!iv.isValidEmail(userEmail))
                {
                    Console.Clear();
                    Console.WriteLine("Not a valid Email");
                    continue;
                }
                break;
            }
            TeacherUser teacher = DataBase.Instance.getTeacher();
            if (teacher.Email.Equals(userEmail))
                DataBase.Instance.createTeacher(teacher);
            else
            {
                Console.Clear();
                Console.WriteLine("No user with that Email was found");
            }
        }

        public void removeTeacher()
        {
            DataBase.Instance.removeTeacher();
        }

        public TeacherUser getTeacher()
        {
            return DataBase.Instance.getTeacher();
        }

        public AdminUser getAdmin(string username, string password)
        {
            if(DataBase.Instance.isAdmin(username, password))
                return DataBase.Instance.getAdmin();
            return new AdminUser("","","");
        }

        public void createAdmin()
        {
            DataBase.Instance.createAdmin(this);
        } 

        public void printAllUsers()
        {
            List<SimpleUser> Users = getAllUsers();
            TeacherUser teacher = getTeacher();
            Console.WriteLine("*********Admin:**********\n"+this);
            if(!teacher.Equals(null))
                Console.WriteLine("*********Teacher:**********\n"+teacher);
            Console.WriteLine("*********Users:**********\n");
            foreach(SimpleUser su in Users)
                Console.WriteLine(su);
        }

    }




}


