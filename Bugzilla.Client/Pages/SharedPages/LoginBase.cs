using Bugzilla.Shared;
using Bugzilla.Client.Service.IService;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using Blazored.LocalStorage;
using Bugzilla.Client.Pages.Manager;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Bugzilla.Client.Pages.SharedPages
{
    public class LoginBase : ComponentBase
    {

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }
        public LoginModel LoginData = new LoginModel();

        public string ErrorMessage { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public IUserService userService { get; set; }
        [Inject]
        public ILocalStorageService localstorage { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        //  public ClaimsPrincipal User = new ClaimsPrincipal();
        //protected override async void OnInitialized()
        //{

        //}
        protected async Task<string> SubmitForm()
        {
            try
            {
                if (ValidateInput()) // Client-side validation
                {
                    string tokenstr = await AuthService.Login(LoginData);
                    if (string.IsNullOrEmpty(tokenstr))
                    {
                        ErrorMessage = "Invalid email or password. Please try again.";
                        return null; // Login failed, return null
                    }

                    //  var LoggedInUserRole = await userService.GetClaimsFromToken(tokenstr);
                    var claims = await userService.GetClaimsFromToken(tokenstr);
                    await localstorage.SetItemAsync("Role", claims["Role"]);


                    //var claimsIdentity = new ClaimsIdentity(claims.Select(c => new Claim(c.Key, c.Value)), "token");
                    //var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);


                    if (claims.ContainsKey("Role") && claims["Role"] == "Manager")
                    {
                        navigationManager.NavigateTo("/Projects");
                    }
                    else if (claims.ContainsKey("Role") && claims["Role"] == "Developer")
                    {
                        string userId = claims["UserId"];
                        navigationManager.NavigateTo($"Projects/{userId}");
                    }
                    else if (claims.ContainsKey("Role") && claims["Role"] == "QA")
                    {


                        string userId = claims["UserId"];
                        navigationManager.NavigateTo($"Projects/{userId}");
                    }
                    else
                    {

                        navigationManager.NavigateTo("/unauthorized");
                    }

                    return tokenstr;
                }
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                ErrorMessage = "An error occurred while processing your request.";
                return null;
            }
        }
        private bool ValidateInput()
        {
            var validationContext = new ValidationContext(LoginData);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(LoginData, validationContext, validationResults, true);

            if (!isValid)
            {
                ErrorMessage = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
            }

            return isValid;
        }
    }
}



