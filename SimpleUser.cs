using System;

namespace Homework_Project
{
	public class SimpleUser:User
	{
	

        public SimpleUser():base("username","password","email")
        {

        }

		public SimpleUser (string username ,string password, string email):base(username,password,email)
		{
		

		}
 
		public override string ToString(){
			return base.ToString();
		}

        public SimpleUser getCurrentUser()
        {
            return DataBase.Instance.getCurrentUser();
        }

	}
}

