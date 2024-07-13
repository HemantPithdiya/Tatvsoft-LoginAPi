using Data_Logic_Layer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Logic_Layer
{
    public class DALAdminUser
    {
        private readonly AppDBContex _context;

        public DALAdminUser(AppDBContex context)
        {
            _context = context;
        }
        public string AddUser(User user)
        {
            var result = "";
            try
            {
                var userEmailExists = _context.Users.FirstOrDefault(x =>  x.EmailAddress == user.EmailAddress);
                if (userEmailExists == null)
                {
                    var newUser = new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        EmailAddress = user.EmailAddress,
                        Password = user.Password,
                        UserType = user.UserType,
                    };
                    _context.Users.Add(newUser);
                    _context.SaveChanges();
                    var maxEmployeeId = 0;
                    var lastUserDetail = _context.UserDetail.ToList().LastOrDefault();

                    if (lastUserDetail != null)
                    {
                        maxEmployeeId = Convert.ToInt32(lastUserDetail.EmployeeId);
                    }
                    int newEmployeeId = maxEmployeeId + 1;
                    var newUserDetail = new UserDetail
                    {
                        UserId = newUser.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        EmailAddress = user.EmailAddress,
                        UserType = user.UserType,
                        Name = user.FirstName,
                        Surname = user.LastName,
                        EmployeeId = newEmployeeId.ToString(),
                        Department = "IT",
                        Status = true
                    };
                    _context.UserDetail.Add(newUserDetail);
                    _context.SaveChanges();

                    result = "User Add Suceessfully.";
                }
                else
                {
                    result = "Email is already exists.";
                }
                }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        public List<UserDetail> GetUserList()
        {
            var userDetailList = from u in _context.Users
                                 join ud in _context.UserDetail on u.Id equals ud.UserId into userDetailGroup
                                 from userDetail in userDetailGroup.DefaultIfEmpty()
                                 where u.UserType == "user" && !userDetail.IsDeleted
                                 select new UserDetail
                                 {
                                     Id = u.Id,
                                     FirstName = u.FirstName,
                                     LastName = u.LastName,
                                     PhoneNumber = u.PhoneNumber,
                                     EmployeeId = userDetail.EmployeeId,
                                     Department = userDetail.Department,
                                     Status = userDetail.Status,
                                 };
            return userDetailList.ToList();
        }

        

    }
}
