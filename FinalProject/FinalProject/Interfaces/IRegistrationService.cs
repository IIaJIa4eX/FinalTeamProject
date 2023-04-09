using FinalProject.Models.Requests;
namespace FinalProject.Interfaces;

public interface IRegistrationService
{
    RegistrationResponse Registration(RegistrationRequest authenticationRequest);
}
