using Microsoft.AspNetCore.Mvc;
using PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Models;

namespace PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Controllers
{
    public class CollaborationController : Controller
    {
        CollaborationModel collaborationModel = new CollaborationModel();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public IActionResult Collabs()
        {
            List<MessageModel> messages = collaborationModel.GetMessages().Result;
            return View(messages);
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// Returns a message to the view
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public IActionResult AddCollaboration()
        {
            // Set TempData with the message
            TempData["Message"] = "This feature is under development and will be completed when out of the prototype stage";

            // Redirect to the original view
            return RedirectToAction("Collabs");
        }
        //======================================================= End of Method ===================================================


        /// <summary>
        /// 
        /// </summary>
        /// <param name="MessageContent"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMessage(string MessageContent)
        {
            MessageModel message = new MessageModel();
            message.Message = MessageContent;
            message.Sender= CoreModel.SignedInUser;
            message.Time = DateTime.Now;
            await collaborationModel.AddMessage(message);
            return RedirectToAction("Collabs");
        }
        //======================================================= End of Method ===================================================
    }
}
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> END OF FILE >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>