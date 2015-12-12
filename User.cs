using System;

namespace Homework_Project
{
	public abstract class User
	{
		private string username;
		private string password;
		private string email;


		public User (string username,string password,string email)
		{
			if(username.Length>0)
				UserName = username;
			if(password.Length>=8)
				Password = password;
			if (email.Contains("@") && email.Contains("."))
				Email = email;
		

		}

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

		public override string ToString(){
			return "UserName: "+UserName+" Password: "+Password+" Email: "+Email;
		}

		public virtual void Print(){
			Console.Write ("       ***New-User***\nUserName: "+UserName+"\nPassword: "+Password+"\nEmail: "+Email);
		}

	}
}

