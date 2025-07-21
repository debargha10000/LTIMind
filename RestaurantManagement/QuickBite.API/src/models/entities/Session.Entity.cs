using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickBite.API.src.models.entities;

public class SessionEntity
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Refresh token is required to create a session")]
    public required string Token { get; set; }

    public DateTime ExpDate { get; set; } = DateTime.Now.AddDays(7);

    [ForeignKey("User")]
    public int UserId { get; set; }

    public UserEntity? User { get; set; }
}
