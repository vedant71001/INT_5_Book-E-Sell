using BookStore.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model.Models
{
    public class UserModel
    {
        public UserModel() { }

        public UserModel(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            Roleid = user.Roleid;
            Password = user.Password;
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int Roleid { get; set; }
        public string Password { get; set; }
    }
}
