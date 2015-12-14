using System;
using System.Globalization;

namespace Homework_Project
{
	public class Task
	{
		private static long counter = 0;
		private long taskID;
		private string taskName;
		private DateTime deadline;
		private bool status=false;
		public Task(string taskName,DateTime deadline){
				TaskName=taskName;
				Deadline=deadline;
				taskID = counter++;
				
			}

		public long TaskID{
			get{return this.taskID;}
		}

		public string TaskName{
			get{return this.taskName;}
			set{this.taskName = value;}
		}

		public bool Status{
			get{return this.status;}
			set{this.status = value;}
		}

		public DateTime Deadline{
			get{return this.deadline;}
			set{this.deadline = value;}
		}

		public override string ToString(){
			return "Task Name: " + TaskName+ "\nDeadline: " + deadline.ToString("dd/MM/yyyy") +
                "\n      Status: "+ Status.ToString();
		}


	

	}
}

