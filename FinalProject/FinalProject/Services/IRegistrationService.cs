using FinalProject.Models.Requests;

namespace FinalProject.Services
{
    public interface IRegistrationService
    {
        RegistrationResponse Registration(RegistrationRequest authenticationRequest);
    }
}
