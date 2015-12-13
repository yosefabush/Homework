using System;

namespace Homework_Project
{
	public class TeacherUser:SimpleUser
	{
		public TeacherUser (string username ,string password, string email):base(username,password,email)
		{


		}
        public TeacherUser()
        {

        }


        public TeacherUser getTeacher() {
            return DataBase.Instance.getTeacher();
        }


		public override void Print(){
			base.Print ();

		}
	}
}

