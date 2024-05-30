using Microsoft.AspNetCore.Mvc;
using PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Models;

namespace PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Controllers
{
    public class SignInController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        SignInModel signIn = new SignInModel();

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
            if (signIn.GetUserDetails(user.Username).Result==null)
            {
                await signIn.AddEmployee(user.Username, user.PasswordHash, user.Name, user.Email);
                CoreModel.SignedInUser = user.Username;
                return RedirectToAction("Index", "Home");
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
            bool anyFieldBlank = string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email) ;
            if (signIn.GetUserDetails(user.Username) != null || anyFieldBlank == false)
            {
                await signIn.AddFarmer(user.Username, user.PasswordHash, user.Name, user.Email);
                return RedirectToAction("Index", "Home");
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
            if (signIn.Login(user.Username, user.PasswordHash) != null && isFieldBlank == false)
            {
                var userLogin = signIn.Login(user.Username, user.PasswordHash);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorLogin"] = "Sorry, the username or password is incorrect. Try again.";
                return View("Login");
            }

        }
    }
}
