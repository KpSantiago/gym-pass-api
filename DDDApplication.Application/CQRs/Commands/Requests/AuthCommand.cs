using System.ComponentModel.DataAnnotations;
using DDDApplication.Application.CQRs.Commands.Responses;
using MediatR;

namespace DDDApplication.Application.CQRs.Commands.Requests;

public class AuthCommand : IRequest<AuthResponse>
{
    [Required]
    [MinLength(8)]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}
