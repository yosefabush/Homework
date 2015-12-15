using System;

namespace Homework_Project
{
	public abstract class User: IPrintTasks
    {
		private string username;
		private string password;
		private string email;
        private long classId;

		public User (string username,string password,string email)
		{
			if(username.Length>0)
				UserName = username;
			if(password.Length>=8)
				Password = password;
			if (email.Contains("@") && email.Contains("."))
				Email = email;
		

		}

        public User() { }

		public string UserName{
			get{return this.username;}
			set{this.username = value;}
		}

		public string Password{
			get{return this.password;}
			set{this.password = value;}
		}

		public string Email{
			get{return this.email;}
			set{this.email = value;}
		}

        public long ClassID
        {
            get { return this.classId; }
            set { this.classId = value; }
        }

        public override string ToString(){
			return "UserName: "+UserName+" Password: "+Password+" Email: "+Email;
		}

		public virtual void Print(){
			Console.Write ("       ***New-User***\nUserName: "+UserName+"\nPassword: "+Password+"\nEmail: "+Email);
		}

        public override bool Equals(object obj)
        {
            if(Email.Equals(((User)obj).Email))
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return 1;
        }

        public void printAllTasks()
        {
            DataBase.Instance.printAllTasks(ClassID);
        }

        public void printAllTasksIncMarked()
        {
            DataBase.Instance.printAllTasksIncMarked(ClassID);
        }
    }
}

