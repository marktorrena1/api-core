using System;

namespace api_core_library.Intefaces;

public interface IUserRole
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public DateTime AssignedAt { get; set; }
}
