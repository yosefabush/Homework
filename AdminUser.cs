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
			DataBase.Instance.printAllTasks (ClassID);
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
            if (DataBase.Instance.addTask(taskName, deadline, ClassID))
            {
                Console.Clear();
                Console.WriteLine("Task was successfuly added!");
            }
            else {
                Console.Clear();
                Console.WriteLine("Task already exists");
            }

		}

        public void deleteTask()
        {
            while (true) { 
                Console.WriteLine("Enter Task ID to delete('-1' to cancel): ");
                long taskID = Int32.Parse(Console.ReadLine());
                if (taskID==-1)
                    break;
                if (taskID >= 0)
                {
                    DataBase.Instance.deleteTask(taskID, ClassID);
                    Console.Clear();
                    Console.WriteLine("Task was successfuly deleted");
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("No task was found with that name");
                }

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
                Console.WriteLine("Enter Task ID to edit('-1' to cancel): ");
                long taskID = Int32.Parse(Console.ReadLine());
                if (taskID == -1)
                {
                    Console.Clear();
                    break;
                }
                Console.WriteLine("Enter new Name: ");
                string taskNameEdit = Console.ReadLine();
                if (taskID >= 0)
                {
                    DataBase.Instance.updateTaskName(taskID, taskNameEdit,ClassID);
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
                    DataBase.Instance.updateTaskDeadline(taskId, newDeadline, ClassID);
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
            HashSet<SimpleUser> Users = getAllUsers(-1);
            foreach (SimpleUser su in Users)
                if (su.Email.Equals(userEmail))
                {
                    if (DataBase.Instance.addUser(su, ClassId))
                    {
                        Console.Clear();
                        Console.WriteLine("User was added successfuly!");
                        return;
                    }
                    else {
                        Console.Clear();
                        Console.WriteLine("User already exists");
                        return;
                    }
                }
            Console.Clear();
            Console.WriteLine("No user with that Email was found");
                
        }

        public void removeUser()
        {
            Console.WriteLine("Enter the Email of User you want to remove: ");
            string email = Console.ReadLine();
            HashSet<SimpleUser> Users = getAllUsers(ClassID);
            foreach (SimpleUser su in Users)
                if (su.Email.Equals(email)&&su.ClassId==ClassID)
                {
                    Console.Clear();
                    Console.WriteLine(su.UserName + " was successfuly removed");
                    long classId = DataBase.Instance.getUserClassId(email);
                    DataBase.Instance.removeUser(email,classId);          
                    return;
                }
            Console.Clear();
            Console.WriteLine("No User with that Email was found");
        }

        public HashSet<SimpleUser> getAllUsers(long classId)
        {
            return DataBase.Instance.getAllUsers(classId);
        }

        public HashSet<TeacherUser> getAllTeachers(long classId)
        {
            return DataBase.Instance.getAllTeachers(classId);
        }

        public void addTeacher(TeacherUser teacher)
        {
            string teacherEmail;
            InputValidation iv = new InputValidation();
            while (true)
            {
                Console.WriteLine("Enter User's Email");
                teacherEmail = Console.ReadLine();
                if (!iv.isValidEmail(teacherEmail))
                {
                    Console.WriteLine("Not a valid Email");
                    continue;
                }
                break;

            }
            HashSet<TeacherUser> Teachers = getAllTeachers(-1);
            foreach (TeacherUser tu in Teachers)
                if (tu.Email.Equals(teacherEmail))
                {
                    if (DataBase.Instance.addUser(tu, ClassId))
                    {
                        Console.Clear();
                        Console.WriteLine("Teacher was added successfuly!");
                        return;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Teacher already exists");
                        return;
                    }
                }
            Console.Clear();
            Console.WriteLine("No Teacher with that Email was found");
        }

        public void addTeacher()
        {
            string teacherEmail;
            InputValidation iv = new InputValidation();
            while (true)
            {
                Console.WriteLine("Enter Teacher's Email");
                teacherEmail = Console.ReadLine();
                if (!iv.isValidEmail(teacherEmail))
                {
                    Console.Clear();
                    Console.WriteLine("Not a valid Email");
                    continue;
                }
                break;
            }
            HashSet<TeacherUser> Teachers = getAllTeachers(-1);
            foreach (TeacherUser tu in Teachers)
                if (tu.Email.Equals(teacherEmail))
                {
                    if (DataBase.Instance.addTeacher(tu, ClassId))
                    {
                        Console.Clear();
                        Console.WriteLine("Teacher was added successfuly!");
                        return;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Teacher already exists");
                        return;
                    }
                }
            Console.Clear();
            Console.WriteLine("No Teacher with that Email was found");
        }

        public void removeTeacher()
        {
            DataBase.Instance.removeTeacher(ClassID);
        }

        public TeacherUser getTeacher(long classId)
        {
            return DataBase.Instance.getTeacher(classId);
        }

        public AdminUser getAdmin(string username, string password)
        {
            if(DataBase.Instance.isAdmin(username, password)!="")
                return DataBase.Instance.getAdmin(DataBase.Instance.isAdmin(username, password));
            return new AdminUser();
        }

        public void addAdmin()
        {
            DataBase.Instance.addAdmin(this);
        } 

        public void printAllUsers()
        {
            HashSet<SimpleUser> Users = getAllUsers(ClassID);
            TeacherUser teacher = getTeacher(ClassID);
            Console.WriteLine("*********Admin:**********\n"+this);
            if(teacher.UserName!=(null))
                Console.WriteLine("*********Teacher:**********\n"+teacher);
            Console.WriteLine("*********Users:**********\n");
            foreach(SimpleUser su in Users)
                Console.WriteLine(su);
        }

    }




}


