using System;
using System.Collections.Generic;

namespace Homework_Project
{
	public class SimpleUser:User,IMarkTask
	{
        private HashSet<Task> localTasks = new HashSet<Task>();

        public SimpleUser():base()
        {

        }

		public SimpleUser (string username ,string password, string email):base(username,password,email)
		{
		

		}
 
		public override string ToString(){
			return base.ToString();
		}

        public SimpleUser getUser(string email)
        {
            return DataBase.Instance.getUser(email);
        }

        public void getTasks()
        {
            localTasks = DataBase.Instance.getMyTasks(ClassID);
        }

        public void markTask(bool status)/////////-1///////////
        {
            while (true)
            {
                Console.WriteLine("Enter Task ID to mark('-1' to cancel): ");
                long taskID = Int32.Parse(Console.ReadLine());
                if (taskID == -1)
                {
                    Console.Clear();
                    break;
                }
                if (taskID >= 0)
                {
                    foreach (Task t in localTasks)
                    {
                        if (t.TaskID == taskID)
                        {
                            t.Status = status;
                            Console.Clear();
                            Console.WriteLine("Task was successfuly marked");
                            return;
                        }
                    }
                    Console.Clear();
                    Console.WriteLine("No task was found with that ID");
                }
            }
        }


    }
}

