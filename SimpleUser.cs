using System;

namespace Homework_Project
{
	public class SimpleUser:User
	{
	

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

	}
}

