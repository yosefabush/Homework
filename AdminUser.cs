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
            string  taskName = Console.ReadLine();
            //get task deadline from admin user
            Console.WriteLine("Enter Deadline to complete the task(dd/MM/yyyy): ");
            string  inputDeadline = Console.ReadLine();
            DateTime deadline = DateTime.ParseExact(inputDeadline, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //add the task to DataBase
            DataBase.Instance.addTask (ClassID, taskName, deadline);

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
                    break;
                Console.WriteLine("Enter new Name: ");
                string taskNameEdit = Console.ReadLine();
                long taskId = getTaskID(taskName);
                if (taskId >= 0)
                {
                    DataBase.Instance.updateTaskName(taskId, taskNameEdit);
                    Console.WriteLine("Task was successfuly updated");
                    break;
                }
                else
                    Console.WriteLine("No task was found with that name");
            }
        }

        public void editTaskDeadline()
        {
            while (true)
            {
                Console.WriteLine("Enter Task Name to edit('-1' to cancel): ");
                string taskName = Console.ReadLine();
                if (taskName.Equals("-1"))
                    break;
                Console.WriteLine("Enter new Deadline date(dd/MM/yyyy): ");
                string deadlineEdit = Console.ReadLine();
                DateTime newDeadline = DateTime.ParseExact(deadlineEdit, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                long taskId = getTaskID(taskName);
                if (taskId >= 0)
                {
                    DataBase.Instance.updateTaskDeadline (taskId, newDeadline);
                    Console.WriteLine("Task was successfuly updated");
                    break;
                }
                else
                    Console.WriteLine("No task was found with that name");
            }
        }

        /***********************Manage Users************************/
        public void addUser(string username, string password, string email)
        {
            List<SimpleUser> Users = DataBase.Instance.getAllUsers();
            foreach (SimpleUser su in Users)
                if (su.Email.Equals(email))
                {
                    Console.WriteLine("User with this Email already exists");
                    return;
                }
            DataBase.Instance.addUser(username,password,email);
        }

        public void removeUser(string email)
        {
            List<SimpleUser> Users = DataBase.Instance.getAllUsers();
            foreach (SimpleUser su in Users)
                if (su.Email.Equals(email))
                {
                    Console.WriteLine("User with this Email already exists");
                    return;
                }
            DataBase.Instance.removeUser(email);
        }

        public SimpleUser getUser(string email)
        {
            return DataBase.Instance.getUser(email);
        }

        public void addTeacher(string username, string password, string email)
        {      
            DataBase.Instance.createTeacher(username, password, email);
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


    }




}


