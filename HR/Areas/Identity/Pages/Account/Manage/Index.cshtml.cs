using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HR.Data;
using HR.Models;
using HR.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HR.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;

        public IndexModel(
            UserManager<Employee> userManager,
            SignInManager<Employee> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.context = context;
        }
        //wait
 
        public string FirstName { get; set; }

        public string FatherName { get; set; }
  
        public string MiddleName { get; set; }

        public string Gender { get; set; }
  
        public string SocialStatus { get; set; }

        public string DepartmentName { get; set; }

        public IFormFile Photo { get; set; }
        //done

        //--- Address ---\\

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Pin Code")]
        public string PinCode { get; set; }

        //--- BankInfo ---\\

        [Required]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Required]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Required]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        //--- OfficialDetails ---\\

        [Required]
        [Display(Name = "Employee Type")]
        public string EmployeeTypeId { get; set; }

        [Required]
        [Display(Name = "Designation")]
        public string DesignationId { get; set; }

        [Required]
        [Display(Name = "Emlpoyee Grade")]
        public string EmlpoyeeGradeId { get; set; }

        [Required]
        [Display(Name = "EmlpoyeeGrade")]
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }
        [Required]
        [Display(Name = "PF Number")]
        public string PFNumber { get; set; }
        [Required]
        [Display(Name = "Shift")]
        public string ShiftId { get; set; }
        [Required]
        [Display(Name = "SuperVisor")]
        public string SuperEmlpoyeeId { get; set; }
        [Required]
        public int Sickleave { get; set; }
        [Required]
        public double TotalVacations { get; set; }
        [Required]
        public double TotoleHouresToleave { get; set; }
     

        //old
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(Employee user, OfficialDetails officialDetails,Address address)
        {
            var user1 = _userManager.GetUserId(HttpContext.User);
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //user info
            Username = userName;
            FatherName = user.FatherName;
            FirstName = user.FirstName;
            MiddleName = user.MiddleName;
            Gender = context.Gender.Where(u => u.Id == user.GenderId).Select(u => u.Type).FirstOrDefault();
            SocialStatus = context.SocialStatus.Where(u => u.Id == user.SocialStatusId).Select(u => u.Type).FirstOrDefault();
            DepartmentName = context.Department.Where(u => u.Id == user.DepartmentName).Select(u => u.Name).FirstOrDefault();
            //BankIno
            BankName = context.BankInfo.Where(u => u.EmployeeId == user.Id).Select(u => u.BankName).FirstOrDefault();
            AccountName = context.BankInfo.Where(u => u.EmployeeId == user.Id).Select(u => u.AccountName).FirstOrDefault();
            AccountNumber = context.BankInfo.Where(u => u.EmployeeId == user.Id).Select(u => u.AccountNumber).FirstOrDefault();
            BranchName = context.BankInfo.Where(u => u.EmployeeId == user.Id).Select(u => u.BranchName).FirstOrDefault();
            //Address
            Country = context.Adress.Where(u => u.EmployeeId == user1).Select(u => u.Country).FirstOrDefault();
            City = context.Adress.Where(u => u.EmployeeId == user1).Select(u => u.City).FirstOrDefault();
            State = context.Adress.Where(u => u.EmployeeId == user1).Select(u => u.State).FirstOrDefault();
            Street = context.Adress.Where(u => u.EmployeeId == user1).Select(u => u.Street).FirstOrDefault();
            PinCode = context.Adress.Where(u => u.EmployeeId == user1).Select(u => u.PinCode).FirstOrDefault();
            //OfficialDetails
            var emlpoyeeGradeId = context.OfficialDetails.Where(u => u.EmployeeId == user1).Select(u => u.EmlpoyeeGradeId).FirstOrDefault();
            EmlpoyeeGradeId = context.EmlpoyeeGrade.Where(u => u.Id == emlpoyeeGradeId).Select(u => u.Name).FirstOrDefault();
            var employeeTypeId = context.OfficialDetails.Where(u => u.EmployeeId == user1).Select(u => u.EmployeeTypeId).FirstOrDefault();
            EmployeeTypeId = context.EmployeeType.Where(u => u.Id == employeeTypeId).Select(u => u.Name).FirstOrDefault();
            DesignationId = context.Designation.Where(u => u.Id == officialDetails.DesignationId).Select(u => u.Name).FirstOrDefault();
            var shiftId = context.OfficialDetails.Where(u => u.EmployeeId == user1).Select(u => u.ShiftId).FirstOrDefault();
            ShiftId = context.Shift.Where(u => u.Id == shiftId).Select(u => u.Name).FirstOrDefault();
            JoinDate = context.OfficialDetails.Where(u => u.EmployeeId == user1).Select(u => u.JoinDate).FirstOrDefault();
            PFNumber = context.OfficialDetails.Where(u => u.EmployeeId == user1).Select(u => u.PFNumber).FirstOrDefault();
            var superEmlpoyeeId = _userManager.FindByIdAsync(context.OfficialDetails.Where(u => u.EmployeeId == user1).Select(u => u.SuperEmlpoyeeId).FirstOrDefault()).Result;
            SuperEmlpoyeeId = _userManager.GetUserNameAsync(superEmlpoyeeId).Result;
            Sickleave = context.OfficialDetails.Where(u => u.EmployeeId == user1).Select(u => u.Sickleave).FirstOrDefault();
            TotalVacations = context.OfficialDetails.Where(u => u.EmployeeId == user1).Select(u => u.TotalVacations).FirstOrDefault();
            TotoleHouresToleave = context.OfficialDetails.Where(u => u.EmployeeId == user1).Select(u => u.TotoleHouresToleave).FirstOrDefault();
            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync(OfficialDetails officialDetails, Address address)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user, officialDetails, address);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(OfficialDetails officialDetails, Address address)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user, officialDetails, address);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
       /* public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("UsersList");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }*/
    }
}
