using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySchool.Data;
using MySchool.Data.Models;
using MySchool.Web.EmailSending;

namespace MySchool.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<MySchoolUser> _signInManager;
        private readonly UserManager<MySchoolUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<MySchoolUser> userManager,
            SignInManager<MySchoolUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IConfiguration configuration,
            RoleManager<IdentityRole<string>> roleManager,
            MySchoolContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            Configuration = configuration;
            RoleManager = roleManager;
            Context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        public IConfiguration Configuration { get; }
        public MySchoolContext Context { get; }

        public RoleManager<IdentityRole<string>> RoleManager;

        public class InputModel
        {
            [Required(ErrorMessage = "Аз съм полето е задължително.")]
            [Display(Name = "Аз съм")]
            public string RegisterAs { get; set; }

            [Required(ErrorMessage = "Потребителското име е задължително.")]
            [Display(Name = "Потребителско име")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "{0}ът е задължителен.")]
            [EmailAddress(ErrorMessage = "Невалиден имейл адрес.")]
            [Display(Name = "Имейл")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Паролата е задължителна.")]
            [StringLength(100, ErrorMessage = "{0}та трябва дa бъде по малко то {2} и повече от {1} синвола.", MinimumLength = 3)]
            [DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Повторете паролата")]
            [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new MySchoolUser { UserName = Input.UserName, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (!await RoleManager.RoleExistsAsync("Student"))
                {
                    await RoleManager.CreateAsync(new IdentityRole("Student"));
                }
                if (!await RoleManager.RoleExistsAsync("Teacher"))
                {
                    await RoleManager.CreateAsync(new IdentityRole("Teacher"));
                }
                if (!await RoleManager.RoleExistsAsync("Parent"))
                {
                    await RoleManager.CreateAsync(new IdentityRole("Parent"));
                }
                Context.SaveChanges();

                if (Input.RegisterAs == "Student")
                {
                    await _userManager.AddToRoleAsync(user, "Student");
                }
                else if (Input.RegisterAs == "Teacher")
                {
                    await _userManager.AddToRoleAsync(user, "Teacher");
                }
                else if (Input.RegisterAs == "Parent")
                {
                    await _userManager.AddToRoleAsync(user, "Parent");
                }

                Context.SaveChanges();
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    EmailSender emailSender = new EmailSender(Configuration);
                    emailSender.Send(user.Email, "You successfully registered", callbackUrl);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    TempData["registered"] = "registered";

                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    if (error.Code == "DuplicateEmail")
                    {
                        ModelState.AddModelError(error.Code, "Този имейл вече съществува.");
                    }
                    else if (error.Code == "DuplicateUserName")
                    {
                        ModelState.AddModelError(error.Code, "Това патребителско име вече съществува.");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
