using Bugzilla.Shared;
using Bugzilla.Client.Service.IService;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace Bugzilla.Client.Pages.SharedPages
{
    public class RegisterBase : ComponentBase
    {
        public enum RoleType
        {
            Manager,
            Developer,
            QA
        }
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        public string ErrorMessage { get; set; }
        public RegisterModel registerData = new RegisterModel();

        protected async Task SubmitForm()
        {
            try
            {
                if (ValidateInput())
                {
                   
                    var IsUserExist = await UserService.IsUserExist(registerData.Email);
                    if (!IsUserExist)
                    {
                        var response = await AuthService.Register(registerData);
                        if (response)
                        {
                            navigationManager.NavigateTo("/Login");
                        }
                    }
                    else
                    {
                        ErrorMessage = "User already exists";
                    }
                  
                }
                else
                {
                    ErrorMessage = "An error occurred while processing your request.";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private bool ValidateInput()
        {
            var validationContext = new ValidationContext(registerData);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(registerData, validationContext, validationResults, true);

            if (!isValid)
            {
                ErrorMessage = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
            }

            return isValid;
        }
    }
}
