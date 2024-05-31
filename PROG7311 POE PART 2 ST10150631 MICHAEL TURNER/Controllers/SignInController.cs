using Microsoft.AspNetCore.Mvc;
using PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Models;
using System.Text.RegularExpressions;

namespace PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Controllers
{
    public class SignInController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        SignInModel signIn = new SignInModel();
        ValidationModel validation = new ValidationModel();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public IActionResult Register()
        {
            return View();
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public IActionResult SignInPrompt()
        {
            return View();
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        [HttpGet]
        public IActionResult LoginUser()
        {
            var user = new UserModel();
            return View(user);
        }

        //======================================================= End of Method ===================================================


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        [HttpGet]
        public IActionResult RegisterEmployee()
        {
            var user = new UserModel();
            return View(user);
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        [HttpGet]
        public IActionResult RegisterFarmer()
        {
            var user = new UserModel();
            return View(user);
        }
        //======================================================= End of Method ===================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignOut()
        {
            CoreModel.SignedInUser = string.Empty;
            CoreModel.UserRole = 0;
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// This method will check if the employee details are valid 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> RegisterEmployee(UserModel user)
        {
            bool anyFieldBlank = string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrEmpty(user.PasswordHash);
            if (signIn.GetUserDetails(user.Username) == null || anyFieldBlank == false && IsValidEmail(user.Email))
            {
                await signIn.AddEmployee(user.Username, user.PasswordHash, user.Name, user.Email);
                CoreModel.SignedInUser = user.Username;
                return RedirectToAction("Index", "Home");
            }
            else if (!IsValidEmail(user.Email))
            {
                TempData["ErrorUsernameTaken"] = "Sorry, that email is invalid. Try again.";
                return View("Register", user);
            }
            else
            {
                TempData["ErrorUsernameTaken"] = "Sorry, that username has already been taken. Try another.";
                return View("Register");
            }
            
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// This method will check if the farmer details are valid 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterFarmer(UserModel user)
        {
            bool anyFieldBlank = string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.PasswordHash);
            if (signIn.GetUserDetails(user.Username) == null || anyFieldBlank == false && IsValidEmail(user.Email))
            {
                await signIn.AddFarmer(user.Username, user.PasswordHash, user.Name, user.Email);
                return RedirectToAction("Index", "Home");
            }
            else if(!IsValidEmail(user.Email))
            {
                TempData["ErrorUsernameTaken"] = "Sorry, that email is invalid. Try again.";
                return View("Register",user);
            }
            else
            {
                TempData["ErrorUsernameTaken"] = "Sorry, that username has already been taken or required fields are blank. Try again.";
                return View("Register");
            }
        }
        //======================================================= End of Method ===================================================
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUser(UserModel user)
        {
            bool isFieldBlank = string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrEmpty(user.PasswordHash);
            if (signIn.GetUserDetails(user.Username).Result !=null && isFieldBlank == false)
            {
                var userLogin = signIn.Login(user.Username, user.PasswordHash);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorLogin"] = "Login Failed Ensure that all fields are correct";
                return View("Login");
            }

        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// Returns a boolean value indicating if email is valid
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public bool IsValidEmail(string email)
        {
            // Regular expression pattern for validating email addresses
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            // Create a Regex object with the pattern
            Regex regex = new Regex(pattern);

            // Use the Matches method to check if the email matches the pattern
            Match match = regex.Match(email);

            // Return true if the email matches the pattern, false otherwise
            return match.Success;
        }

        //======================================================= End of Method ===================================================

    }
}
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> END OF FILE >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>