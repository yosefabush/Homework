using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Homework_Project{
	public class DataBase
	{
		private static DataBase instance;

		private DataBase() {  }
		private static HashSet<Task> Tasks = new HashSet<Task> ();
        private static HashSet<SimpleUser> Users = new HashSet<SimpleUser>();
        private HashSet<TeacherUser> teachers = new HashSet<TeacherUser>();
        private HashSet<AdminUser> admins = new HashSet<AdminUser>();
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
        


        public void CreateUsers() //temp - using this function only because database isn't live
        {

            for (int i = 0; i < 5; i++)
            {
                SimpleUser su;
                
                switch (i)
                {
                    case 0:
                        AdminUser admin = new AdminUser("theKing", "1234abcd", "king@A.com");
                        TeacherUser  teacher = new TeacherUser("Moran", "usa12345", "d@x.com");
                        teacher.ClassID = 1;
                        admins.Add(admin);
                        teachers.Add(teacher);
                        su = new SimpleUser("lider", "abcd1234", "a@b.com");
                        Users.Add(su);
                        break;
                    case 1:
                        su = new SimpleUser("Meitar", "abcd1234", "m@t.com");
                        su.ClassID = 1;
                        Users.Add(su);
                        break;
                    case 2:
                        su = new SimpleUser("yosef", "abcd1243", "y@g.com");
                        su.ClassID = 2;
                        Users.Add(su);
                        break;
                    case 3:
                        su = new SimpleUser("lior", "fedd4453", "l@b.com");
                        Users.Add(su);
                        break;
                    case 4:
                        su = new SimpleUser("mor", "cfrd3462", "m@o.com");
                        Users.Add(su);
                        break;
                }
            }
        }

        public void CreateTasks() //temp - using this function only because database isn't live
        {
            for (int i = 0; i < 5; i++)
            {
                Task task;
                string deadlineDate = "19/12/2015";
                DateTime deadline = DateTime.ParseExact(deadlineDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                switch (i)
                {
                    case 0:
                        
                        task = new Task("Java - Irit - Sort Person",deadline);
                        task.ClassID = 2;
                        Tasks.Add(task);
                        break;
                    case 1:
                        deadlineDate = "21/12/2015";
                        deadline = DateTime.ParseExact(deadlineDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        task = new Task("Java - Moran - Sort Cars", deadline);
                        Tasks.Add(task);
                        break;
                    case 2:
                        deadlineDate = "25/12/2015";
                        deadline = DateTime.ParseExact(deadlineDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        task = new Task("Assembler - Yosimitsu - check Quiz", deadline);
                        Tasks.Add(task);
                        break;
                    case 3:
                        deadlineDate = "16/12/2015";
                        deadline = DateTime.ParseExact(deadlineDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        task = new Task("HTML - Tom - RioOlympics website", deadline);
                        Tasks.Add(task);
                        break;
                    case 4:
                        deadlineDate = "01/01/2016";
                        deadline = DateTime.ParseExact(deadlineDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        task = new Task("OS - Shayke - Timing task", deadline);
                        Tasks.Add(task);
                        break;
                }
            }
        }
        /*************************************Manage Tasks**********************************/

        public bool addTask(string taskName, DateTime deadline, long classID){
			Task t = new Task (taskName, deadline);
            t.ClassID = classID;
            if (Tasks.Contains(t) && t.ClassID == classID)
                return false;
            else
            {
                Tasks.Add(t);
                return true;
            }
            
		}

		public void deleteTask(long taskID,long classID)
        {
            foreach (Task t in Tasks)
                if (t.TaskID == taskID && t.ClassID==classID)
                {
                    Tasks.Remove(t);
                    break;
                }
		}

		public void printTask(long taskID)
        {
			foreach (Task t in Tasks)
				if (t.TaskID.Equals (taskID)) {
					Console.WriteLine (t);
					break;
				}
		}

		public void printAllTasks(long classID){
			foreach (Task t in Tasks)
                    if(t.ClassID==classID && t.Status==false)
				        Console.WriteLine (t);
		}

        public void printAllTasksIncMarked(long classID)
        {
            foreach (Task t in Tasks)
                if (t.ClassID == classID)
                    Console.WriteLine(t);
        }

        public HashSet<Task> getMyTasks(long classId)
        {
            HashSet<Task> temp = new HashSet<Task>();
            foreach (Task t in Tasks)
                if (t.ClassID == classId)
                    temp.Add(t);
            return temp;
        }

		public void updateTaskName(long taskID, string nameEdit,long classID){
			foreach (Task t in Tasks)
				if (t.TaskID.Equals (taskID) && t.ClassID == classID) {
					t.TaskName = nameEdit;
					break;
				}
		}

		public void updateTaskDeadline(long taskID, DateTime deadlineEdit, long classID){
			foreach (Task t in Tasks)
				if (t.TaskID.Equals (taskID) && t.ClassID==classID) {
					t.Deadline = deadlineEdit;
					break;
				}
		}

		public void updateTask(long taskID, string nameEdit, DateTime deadlineEdit, long classID){
			foreach (Task t in Tasks)
				if (t.TaskID.Equals (taskID) && t.ClassID == classID) {
					t.TaskName = nameEdit;
					t.Deadline = deadlineEdit;
					break;
				}
		}

		public bool markTask(long taskID, long ClassId, bool status)
        {
			foreach (Task t in Tasks)
				if (t.TaskID==taskID && t.ClassID==ClassId) {
					t.Status = status;
					return true;
				}
            return false;
		}

        public long getTaskId(string taskName)
        {
            foreach (Task t in Tasks)
                if (t.TaskName.Equals(taskName))
                    return t.TaskID;
            return -1;
        }

        /***********************************Manage Users************************************/
        public void addAdmin(AdminUser admin)
        {
            if (!admin.Equals(null))
                admins.Add(admin);
        }

        public bool addUser(SimpleUser user, long classId)
        {
            user.ClassID = classId;
            if (Users.Contains(user) && user.ClassID==classId)
                return false;
            else
            {
                user.getTasks();
                Users.Add(user);
                return true;
            }      
        }

        public bool addTeacher(TeacherUser teacher, long classId)
        {
                
            teacher.ClassID = classId;
            if (teachers.Contains(teacher) && teacher.ClassID == classId)
                return false;
            else
            {
                teachers.Add(teacher);
                return true;
            }
                
            
        }

        public void removeUser(string email, long classId)
        {
            foreach (SimpleUser su in Users)
                if (su.Email.Equals(email) && su.ClassID==classId)
                {
                    Users.Remove(su);
                    break;
                }
        }

        public HashSet<SimpleUser> getAllUsers(long classId)
        {
            HashSet<SimpleUser> temp = new HashSet<SimpleUser>();
            HashSet<SimpleUser> tempAll = new HashSet<SimpleUser>();
            foreach (SimpleUser su in Users)
            {
                if (su.ClassID == classId)
                    temp.Add(su);
                if (classId == -1)
                    tempAll.Add(su);
            }
            if(classId!=-1)        
                return temp;
            return tempAll;
        } 

        public HashSet<TeacherUser> getAllTeachers(long classId)
        {
            HashSet<TeacherUser> temp = new HashSet<TeacherUser>();
            HashSet<TeacherUser> tempAll = new HashSet<TeacherUser>();
            foreach (TeacherUser tu in teachers)
            {
                if (tu.ClassID == classId)
                    temp.Add(tu);
                if (classId == -1)
                    tempAll.Add(tu);
            }
            if (classId != -1)
                return temp;
            return tempAll;

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

        public long getUserClassId(string email)
        {
            foreach (SimpleUser su in Users)
                if (su.Email.Equals(email))
                    return su.ClassID;
            return -1;
        }

        public void removeTeacher(long classId)
        {
            foreach (TeacherUser tu in teachers)
                if (tu.ClassID == classId)
                    teachers.Remove(tu);
        }

        public TeacherUser getTeacher(long classId)
        {
            foreach (TeacherUser tu in teachers)
                if (tu.ClassID.Equals(classId))
                    return tu;
            return new TeacherUser();
        }

        public TeacherUser getTeacher(string email)
        {
            foreach (TeacherUser tu in teachers)
                if (tu.Email.Equals(email))
                    return tu;
            return new TeacherUser();
        }

        public long getTeacherClassId(string email)
        {
            foreach (TeacherUser tu in teachers)
                if (tu.Email.Equals(email))
                    return tu.ClassID;
            return -1;
        }

        public AdminUser getAdmin(string email)
        {
            foreach (AdminUser au in admins)
                if (au.Email.Equals(email))
                    return au;
             return new AdminUser();   
        }

        public SimpleUser getCurrentUser()
        {
            return currentUser;
        }

        /***********************************Manage Logins*************************************/

        public string isAdmin(string username, string password)
        {
            foreach(AdminUser admin in admins)
                if(admin!=null)
                    if (username.Equals(admin.UserName) && password.Equals(admin.Password))
                        return admin.Email;

            return "";
        }

        public string isTeacher(string username, string password)
        {
            foreach(TeacherUser teacher in teachers)
                if (teacher!= null)
                    if (username.Equals(teacher.UserName) && password.Equals(teacher.Password))
                        return teacher.Email;
            return "";

        }

        public string isSimpleUser(string username, string password)
        {
            foreach (SimpleUser user in Users)
            {
                if (user!=null)
                    if (user.UserName.Equals(username) && user.Password.Equals(password))
                    {
                        currentUser = user;
                        return user.Email;
                    }

            }
            return "";

        }

    }

}
