using System;

namespace Homework_Project
{
	public class TeacherUser:SimpleUser
	{
		public TeacherUser (string username ,string password, string email):base(username,password,email)
		{


		}

        public TeacherUser():base()
        {

        }

        public TeacherUser getTeacher(string email) {
            return DataBase.Instance.getTeacher(email);
        }

		public override void Print(){
			base.Print ();

		}
	}
}

