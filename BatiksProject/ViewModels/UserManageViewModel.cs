using System.Collections.Generic;
using BatiksProject.Dto;

namespace BatiksProject.ViewModels
{
    public class UserManageViewModel
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}
