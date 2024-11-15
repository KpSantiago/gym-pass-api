using System.ComponentModel.DataAnnotations;
using GymPass.Application.CQRs.Commands.Responses;
using MediatR;

namespace GymPass.Application.CQRs.Commands.Requests;

// RegitserCommand vai ser o Command de criação de usuário e a sua request tem como resposta RegitserResponse que retornará Email, Name e Id
public class RegisterCommand : IRequest<RegisterResponse>
{
    [Required]
    [MinLength(3)]
    public string Name { get; set; } = default!;

    [Required]
    [MinLength(8)]
    [EmailAddress]
    public string Email { get; set; } = default!;
    
    [Required]
    [MinLength(6)]
    public string Password {get; set;} = default!;
}