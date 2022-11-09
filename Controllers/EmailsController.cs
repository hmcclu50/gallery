using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.Pkcs;
using farris_art_gallery.Data;
using farris_art_gallery.Models;
using Microsoft.AspNetCore.Mvc;

namespace farris_art_gallery.Controllers;

public class EmailsController : Controller
{
    private readonly ApplicationDbContext _context;

    public EmailsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Send()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Send([Bind("Subject,Body")] Email email)
    {

        try
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            // message.From = new MailAddress(""); // need to determine proper e-mail
            foreach (Subscriber subscriber in _context.Subscriber.ToList())
            {
                MailAddress replyTo = new MailAddress(subscriber.EmailAddress);
                message.ReplyToList.Add(replyTo);
            }
            message.Subject = email.Subject;
            message.Body = email.Body;
            // smtp.Port = // to be determined
            // smtp.Host = // to be determined
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            // smtp.Credentials = new NetworkCredential("Username", "Password");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
        catch (Exception)
        {
        }

        return RedirectToAction("Index", new { controller = "Home", action = "Index" });
    }
}