using System;
using System.Collections;
using System.Collections.Generic;

namespace Homework_Project{
	public class DataBase
	{
		private static DataBase instance;

		private DataBase() {  }
		private static ArrayList Tasks = new ArrayList ();
        private static List<SimpleUser> Users = new List<SimpleUser>();
        private ArrayList tasksCopy;
        private TeacherUser teacher;
        private AdminUser admin;
        private SimpleUser currentUser;

		public static DataBase Instance
		{
			get 
			{
				if (instance == null)
				{
					instance = new DataBase();
				}
				return instance;
			}
		}
        public ArrayList TasksCopy
        {
            get {
                this.tasksCopy = Tasks;
                return this.tasksCopy;
            }
        }

        public void CreateUsers()
        {

            for (int i = 0; i < 5; i++)
            {
                SimpleUser su;
                
                switch (i)
                {
                    case 0:
                        admin = new AdminUser("theKing", "1234abcd", "king@A.com");
                        teacher = new TeacherUser("Moran", "usa12345", "d@x.com");
                        su = new SimpleUser("lider", "abcd1234", "a@b.com");
                        Users.Add(su);
                        break;
                    case 1:
                        su = new SimpleUser("Meitar", "abcd1234", "m@t.com");
                        Users.Add(su);
                        break;
                    case 2:
                        su = new SimpleUser("yosef", "abcd1243", "y@g.com");
                        Users.Add(su);
                        break;
                    case 3:
                        su = new SimpleUser("lior", "fedd4453", "l@b.com");
                        Users.Add(su);
                        break;
                    case 4:
                        su = new SimpleUser("mor", "cfrd3462", "m@t.com");
                        Users.Add(su);
                        break;
                }
            }
        }
        /*************************************Manage Tasks**********************************/

        public void addTask(long taskID, string taskName, DateTime deadline){
			Task t = new Task (taskName, deadline);
			Tasks.Add (t);
		}

		public void deleteTask(long taskID)
        {
			foreach (Task t in Tasks)
				if (t.TaskID.Equals (taskID))
					Tasks.Remove (t);
		}

		public void printTask(long taskID)
        {
			foreach (Task t in Tasks)
				if (t.TaskID.Equals (taskID)) {
					Console.WriteLine (t);
					break;
				}
		}

		public void printAllTasks(){
			foreach (Task t in Tasks)
				Console.WriteLine (t);
		}

		public void updateTaskName(long taskID, string nameEdit){
			foreach (Task t in Tasks)
				if (t.TaskID.Equals (taskID)) {
					t.TaskName = nameEdit;
					break;
				}
		}

		public void updateTaskDeadline(long taskID, DateTime deadlineEdit){
			foreach (Task t in Tasks)
				if (t.TaskID.Equals (taskID)) {
					t.Deadline = deadlineEdit;
					break;
				}
		}

		public void updateTask(long taskID, string nameEdit, DateTime deadlineEdit){
			foreach (Task t in Tasks)
				if (t.TaskID.Equals (taskID)) {
					t.TaskName = nameEdit;
					t.Deadline = deadlineEdit;
					break;
				}
		}

		public void markDoneTask(long taskID)
        {
			foreach (Task t in Tasks)
				if (t.TaskID.Equals (taskID)) {
					t.Status = true;
					break;
				}
		}

        public long getTaskId(string taskName)
        {
            foreach (Task t in Tasks)
                if (t.TaskName.Equals(taskName))
                    return t.TaskID;
            return -1;
        }

        /***********************************Manage Users************************************/
        public void createAdmin(AdminUser au)
        {
            if (admin.Equals(null))
                admin = au;
        }

        public void addUser(string username, string password, string email)
        {
            SimpleUser user = new SimpleUser(username, password, email);
            Users.Add(user);
                    
        }

        public void removeUser(string email)
        {
            foreach (SimpleUser su in Users)
                if (su.Email.Equals(email))
                {
                    Users.Remove(su);
                    break;
                }
        }


        public List<SimpleUser> getAllUsers()
        {
            return Users;
        } 

        public SimpleUser getUser(string email)
        {
            foreach(SimpleUser su in Users)
                if (su.Email.Equals(email))
                {
                    return su;
                }
            return new SimpleUser();

        }

        public void createTeacher(string username, string password, string email)
        {
            if (teacher.Equals(null))
            {
                teacher = new TeacherUser(username, password, email);
            }
        }

        public void removeTeacher()
        {
            teacher = null;
        }

        public TeacherUser getTeacher()
        {
            return teacher;
        }

        public AdminUser getAdmin()
        { 
                return admin;
        }

        public SimpleUser getCurrentUser()
        {
            return currentUser;
        }

        /***********************************Manage Logins*************************************/

        public bool isAdmin(string username, string password)
        {
            if(admin!=null)
                if (username.Equals(admin.UserName) && password.Equals(admin.Password))
                   return true;

            return false;
        }

        public bool isTeacher(string username, string password)
        {
            if(teacher!=null)
                if (username == teacher.UserName && password == teacher.Password)
                    return true;
            return false;

        }

        public bool isSimpleUser(string username, string password)
        {
            foreach (SimpleUser su in Users)
            {
                if (su!=null)
                    if (su.UserName.Equals(username) && su.Password.Equals(password))
                    {
                        currentUser = su;
                        return true;
                    }

            }
            return false;

        }

    }

}
