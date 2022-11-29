﻿using SchollOfDevs.Enuns;

namespace SchollOfDevs.Entities
{
    public class User: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public int Age { get; set; }
        public TypeUser TypeUser { get; set; }
    }
}