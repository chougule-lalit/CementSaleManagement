using CementSaleManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class UserMasterDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }
    }

    public class UserMasterCreateUpdateDto
    {
        public int? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int RoleId { get; set; }

        public string Password { get; set; }
    }

    public class GetUserInput
    {
        public int MaxResultCount { get; set; } = 10;

        public int SkipCount { get; set; }

        public string Search { get; set; }
    }

    public class LoginInputDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class LoginOutputDto
    {
        public int Id { get; set; }

        public RoleEnum Role { get; set; }

        public bool IsSuccess { get; set; }
    }

    public class UserDropdownDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
