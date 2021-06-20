using HR.Data;

using HR.Models;

using HR.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HR.Controllers
{
    [Authorize(Roles = "Admin,HR")]
    public class AdministrationController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<Employee> userManager;
        private readonly SignInManager<Employee> signInManager;
        private readonly IWebHostEnvironment environment;
        public AdministrationController(ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager, UserManager<Employee> userManager,
             IWebHostEnvironment environment, SignInManager<Employee> signInManager)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.environment = environment;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Authorize(Policy = "CreateRole")]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Authorize(Policy = "CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "EditRole")]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Authorize(Policy = "EditRole")]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                return NotFound();
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "EditUsersInRole")]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }
            var model = new List<UserRoleViewModel>();
            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "EditUsersInRole")]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    if (!await userManager.IsInRoleAsync(user, "El-Super"))
                    {
                        result = await userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    continue;
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "DeleteRole")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            else
            {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("ListRoles");
        }
        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "ManageUserClaims")]
        public async Task<IActionResult> ManageUserClaims(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return NotFound();
            }
            var existingUserClaims = await userManager.GetClaimsAsync(user);
            var model = new UserClaimsViewModel
            {
                UserId = UserId
            };
            foreach (var claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    CliamType = claim.Type
                };
                if (existingUserClaims.Any(c => c.Type == claim.Type))
                {
                    userClaim.IsSelected = true;
                }
                model.Claims.Add(userClaim);
            }
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "ManageUserClaims")]
        public async Task<IActionResult> ManageUserClaims(UserClaimsViewModel model, string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return NotFound();
            }
            var claims = await userManager.GetClaimsAsync(user);
            var result = await userManager.RemoveClaimsAsync(user, claims);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing claims");
                return View(model);
            }
            result = await userManager.AddClaimsAsync(user, model.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.CliamType, c.CliamType)));

            return RedirectToAction("EditUser", new { Id = model.UserId });
        }
        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        public IActionResult UsersList()
        {
            var users = userManager.Users;
            return View(users);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "AddUser")]
        public IActionResult AddUser()
        {
            ViewData["Gender"] = new SelectList(context.Gender, "Id", "Type");
            ViewData["SocialStatus"] = new SelectList(context.SocialStatus, "Id", "Type");
            ViewData["DepartmentName"] = new SelectList(context.Department, "Id", "Name");
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "AddUser")]
        public async Task<IActionResult> AddUser(AddUser model)
        {
            string uniqueFileName = null;
            if (ModelState.IsValid)
            {
                ViewData["Gender"] = new SelectList(context.Gender, "Id", "Type");
                ViewData["SocialStatus"] = new SelectList(context.SocialStatus, "Id", "Type");
                ViewData["DepartmentName"] = new SelectList(context.Department, "Id", "Name");
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(environment.WebRootPath, "Files/EmployeePhotos");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                var user = new Employee
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Bday = model.Bday,
                    GenderId = model.Gender,
                    FatherName = model.FatherName,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    SocialStatusId = model.SocialStatus,
                    PhotoPath = uniqueFileName,
                    DepartmentName = model.DepartmentName,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("AddUserDetails", new { id = user.Id });
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "AddUserDetails")]
        public async Task<IActionResult> AddUserDetails(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            var Checkuser = userManager.FindByIdAsync(Id).Result;
            string usedUaser = userManager.GetUserIdAsync(Checkuser).Result;
            var useduserInOfficialDetails = context.OfficialDetails.Where(u => u.EmployeeId == usedUaser).Select(u => u.EmployeeId).FirstOrDefault();
            var useduserInAddress = context.Adress.Where(u => u.EmployeeId == usedUaser).Select(u => u.EmployeeId).FirstOrDefault();
            var useduserInBankInfo = context.BankInfo.Where(u => u.EmployeeId == usedUaser).Select(u => u.EmployeeId).FirstOrDefault();
            if (useduserInAddress != usedUaser && useduserInOfficialDetails != usedUaser && useduserInBankInfo != usedUaser)
            {
                if (user == null)
                {
                    return NotFound();
                }
                ViewData["SuperEmlpoyeeId"] = new SelectList(context.Users, "Id", "UserName");
                ViewData["EmlpoyeeGradeId"] = new SelectList(context.EmlpoyeeGrade, "Id", "Name");
                ViewData["EmployeeTypeId"] = new SelectList(context.EmployeeType, "Id", "Name");
                ViewData["ShiftId"] = new SelectList(context.Shift, "Id", "Name");
                ViewData["DesignationId"] = new SelectList(context.Designation, "Id", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("EditUserDetails", new { id = user.Id });
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "AddUserDetails")]
        public async Task<IActionResult> AddUserDetails(AddUserDetails model, string Id, Address address, BankInfo bankInfo , OfficialDetails officialDetails, Documents documents)
        {
            var user = await userManager.FindByIdAsync(Id);
            var Checkuser = userManager.FindByIdAsync(Id).Result;
            string usedUaser = userManager.GetUserIdAsync(Checkuser).Result;
            var useduserInOfficialDetails = context.OfficialDetails.Where(u => u.EmployeeId == usedUaser).Select(u => u.EmployeeId).FirstOrDefault();
            var useduserInAddress = context.Adress.Where(u => u.EmployeeId == usedUaser).Select(u => u.EmployeeId).FirstOrDefault();
            var useduserInBankInfo = context.BankInfo.Where(u => u.EmployeeId == usedUaser).Select(u => u.EmployeeId).FirstOrDefault();
            if (useduserInAddress != usedUaser && useduserInOfficialDetails != usedUaser && useduserInBankInfo != usedUaser)
            {
                string uniqueFileName = null;
                if (model.TheDocument != null)
                {
                    string uploadsFolder = Path.Combine(environment.WebRootPath, "Files/EmployeeDocuments");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.TheDocument.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.TheDocument.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                var UserAdress = new Address
                {
                    Country = model.Country,
                    City = model.City,
                    Street = model.Street,
                    State = model.State,
                    PinCode = model.PinCode,
                    EmployeeId = user.Id
                };
                var UserBankInfo = new BankInfo
                {
                    AccountName = model.AccountName,
                    AccountNumber = model.AccountNumber,
                    BankName = model.BankName,
                    BranchName = model.BranchName,
                    EmployeeId = user.Id
                };
                var UserOfficialDetails = new OfficialDetails
                {
                    DesignationId = model.DesignationId,
                    ShiftId = model.ShiftId,
                    EmployeeTypeId = model.EmployeeTypeId,
                    EmlpoyeeGradeId = model.EmlpoyeeGradeId,
                    JoinDate = model.JoinDate,
                    PFNumber = model.PFNumber,
                    TotalVacations = model.TotalVacations,
                    SuperEmlpoyeeId = model.SuperEmlpoyeeId,
                    EmployeeId = user.Id,
                    TotoleHouresToleave = model.TotoleHouresToleave,
                    Sickleave = model.Sickleave
                };
                var userDocuments = new Documents
                {
                    Name = model.Name,
                    TheDocumentPath = uniqueFileName,
                    EmployeeId = user.Id
                };
                context.Add(UserAdress);
                context.Add(UserBankInfo);
                context.Add(UserOfficialDetails);
                context.Add(userDocuments);
                await context.SaveChangesAsync();
                ViewData["SuperEmlpoyeeId"] = new SelectList(context.Users, "Id", "UserName");
                ViewData["EmlpoyeeGradeId"] = new SelectList(context.EmlpoyeeGrade, "Id", "Name");
                ViewData["EmployeeTypeId"] = new SelectList(context.EmployeeType, "Id", "Name");
                ViewData["ShiftId"] = new SelectList(context.Shift, "Id", "Name");
                ViewData["DesignationId"] = new SelectList(context.Designation, "Id", "Name");
                return RedirectToAction("EditUser", new { id = user.Id });
            }
            else
            {
                return RedirectToAction("EditUserDetails", new { id = user.Id });
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "EditUserDetails")]
        public async Task<IActionResult> EditUserDetails(string Id)
        {

            var user = await userManager.FindByIdAsync(Id);
            var userId = await userManager.GetUserIdAsync(user);
            var user1 = await userManager.GetUserIdAsync(user);
            if (user == null)
            {
                return NotFound();
            }
            var emlpoyeeGradeId = context.OfficialDetails.Where(u => u.EmployeeId == userId).Select(u => u.EmlpoyeeGradeId).FirstOrDefault();
            var employeeTypeId = context.OfficialDetails.Where(u => u.EmployeeId == userId).Select(u => u.EmployeeTypeId).FirstOrDefault();
            var shiftId = context.OfficialDetails.Where(u => u.EmployeeId == userId).Select(u => u.ShiftId).FirstOrDefault();
            //Address
            var address = await context.Adress
            .Include(a => a.Employee)
            .FirstOrDefaultAsync(m => m.EmployeeId == Id);
            //BankInfo
            var bankInfo = await context.BankInfo
               .Include(a => a.Employee)
               .FirstOrDefaultAsync(m => m.EmployeeId == Id);
            //Documents
            var documents = await context.Documents
               .Include(a => a.Employee)
               .FirstOrDefaultAsync(m => m.EmployeeId == Id);
            //OfficialDetails
            var officialDetails = await context.OfficialDetails
            .Include(a => a.Employee)
            .FirstOrDefaultAsync(m => m.EmployeeId == Id);
            var superEmlpoyeeId = userManager.FindByIdAsync(officialDetails.SuperEmlpoyeeId).Result;
            var model = new EditUserDetailsViewModel
            {
                //user info
                FatherName = user.FatherName,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                Gender = context.Gender.Where(u => u.Id == user.GenderId).Select(u => u.Type).FirstOrDefault(),
                SocialStatus = context.SocialStatus.Where(u => u.Id == user.SocialStatusId).Select(u => u.Type).FirstOrDefault(),
                DepartmentName = context.Department.Where(u => u.Id == user.DepartmentName).Select(u => u.Name).FirstOrDefault(),
                Bday = user.Bday,
                Id = user.Id,
                //BankIno
                BankName = bankInfo.BankName,
                AccountName = bankInfo.AccountName,
                AccountNumber = bankInfo.AccountNumber,
                BranchName = bankInfo.BankName,
                //Address
                Country = address.Country,
                City = address.City,
                State = address.State,
                Street = address.Street,
                PinCode = address.PinCode,
                //Documents
                Name = documents.Name,
                //OfficialDetails
                EmlpoyeeGradeId = officialDetails.EmlpoyeeGradeId,
                EmployeeTypeId = officialDetails.EmployeeTypeId,
                DesignationId = officialDetails.DesignationId,
                ShiftId = officialDetails.ShiftId,
                JoinDate = officialDetails.JoinDate,
                PFNumber = officialDetails.PFNumber,
                SuperEmlpoyeeId = officialDetails.SuperEmlpoyeeId,
                TotalVacations = officialDetails.TotalVacations,
                TotoleHouresToleave = officialDetails.TotoleHouresToleave,
                Sickleave = officialDetails.Sickleave
            };
            ViewData["SuperEmlpoyeeId"] = new SelectList(context.Users, "Id", "UserName", officialDetails.SuperEmlpoyeeId);
            ViewData["EmlpoyeeGradeId"] = new SelectList(context.EmlpoyeeGrade, "Id", "Name");
            ViewData["EmployeeTypeId"] = new SelectList(context.EmployeeType, "Id", "Name");
            ViewData["ShiftId"] = new SelectList(context.Shift, "Id", "Name");
            ViewData["DesignationId"] = new SelectList(context.Designation, "Id", "Name");
            ViewData["Gender"] = new SelectList(context.Gender, "Type", "Type");
            ViewData["SocialStatus"] = new SelectList(context.SocialStatus, "Type", "Type");
            ViewData["DepartmentName"] = new SelectList(context.Department, "Name", "Name");
                return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "EditUserDetails")]
        public async Task<IActionResult> EditUserDetails(EditUserDetailsViewModel model, string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            string uniqueFileName = null;
            if (model.TheDocument != null)
            {
                string uploadsFolder = Path.Combine(environment.WebRootPath, "Files/EmployeeDocuments");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.TheDocument.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.TheDocument.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            //User 
            user.FatherName = model.FatherName;
            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.DepartmentName = context.Department.Where(u => u.Name == model.DepartmentName).Select(u => u.Id).FirstOrDefault();
            user.GenderId = context.Gender.Where(u => u.Type == model.Gender).Select(u => u.Id).FirstOrDefault();
            user.SocialStatusId = context.SocialStatus.Where(u => u.Type == model.SocialStatus).Select(u => u.Id).FirstOrDefault();
            user.Bday = model.Bday;

            //Address
            var address = await context.Adress
               .Include(a => a.Employee)
               .FirstOrDefaultAsync(m => m.EmployeeId == Id);
            address.City = model.City;
            address.Country = model.Country;
            address.PinCode = model.PinCode;
            address.Street = model.Street;
            address.State = model.State;
            //BankInfo
            var bankInfo = await context.BankInfo
               .Include(a => a.Employee)
               .FirstOrDefaultAsync(m => m.EmployeeId == Id);
            bankInfo.AccountName = model.AccountName;
            bankInfo.AccountNumber = model.AccountNumber;
            bankInfo.BankName = model.BankName;
            bankInfo.BranchName = model.BranchName;
            //Documents
            var documents = await context.Documents
               .Include(a => a.Employee)
               .FirstOrDefaultAsync(m => m.EmployeeId == Id);
            if(model.TheDocument != null)
            {
                documents.TheDocumentPath = uniqueFileName;
            }
            else
            {
                documents.TheDocumentPath = documents.TheDocumentPath;
            }
            documents.Name = model.Name;
     

            //OfficialDetails
            var officialDetails = await context.OfficialDetails
            .Include(a => a.Employee)
            .FirstOrDefaultAsync(m => m.EmployeeId == Id);
            officialDetails.JoinDate = model.JoinDate;
            officialDetails.PFNumber = model.PFNumber;
            officialDetails.TotalVacations = model.TotalVacations;
            officialDetails.SuperEmlpoyeeId = model.SuperEmlpoyeeId;
            officialDetails.ShiftId = model.ShiftId;
            officialDetails.EmployeeTypeId = model.EmployeeTypeId;
            officialDetails.EmlpoyeeGradeId =model.EmlpoyeeGradeId;
            officialDetails.DesignationId  = model.DesignationId;
            officialDetails.TotalVacations = model.TotalVacations;
            officialDetails.TotoleHouresToleave = model.TotoleHouresToleave;
            officialDetails.Sickleave = model.Sickleave;
            //update-database
            await userManager.UpdateAsync(user);
            context.Update(address);
            context.Update(bankInfo);
            context.Update(documents);
            context.Update(officialDetails);
            await context.SaveChangesAsync();
            ViewData["SuperEmlpoyeeId"] = new SelectList(context.Users, "Id", "UserName");
            ViewData["EmlpoyeeGradeId"] = new SelectList(context.EmlpoyeeGrade, "Id", "Name");
            ViewData["EmployeeTypeId"] = new SelectList(context.EmployeeType, "Id", "Name");
            ViewData["ShiftId"] = new SelectList(context.Shift, "Id", "Name");
            ViewData["DesignationId"] = new SelectList(context.Designation, "Id", "Name");
            ViewData["Gender"] = new SelectList(context.Gender, "Type", "Type");
            ViewData["SocialStatus"] = new SelectList(context.SocialStatus, "Type", "Type");
            ViewData["DepartmentName"] = new SelectList(context.Department, "Name", "Name");
            return RedirectToAction("EditUser", new { id = user.Id });
        }
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "DeleteUser")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var noti = context.Notification.Where(a => a.FromUser == user.Id);
                var officialDetails = context.OfficialDetails.Where(a => a.EmployeeId == user.Id);
                var leaveApplication = context.LeaveApplication.Where(a => a.EmployeeInfo == user.Id);
                var bankInfos = context.BankInfo.Where(a => a.EmployeeId == user.Id);
                var doc = context.Documents.Where(a => a.EmployeeId == user.Id);
                var addresses = context.Adress.Where(a => a.EmployeeId == user.Id);
                var result = await userManager.DeleteAsync(user);
                context.Remove(noti);
                context.Remove(officialDetails);
                context.Remove(leaveApplication);
                context.Remove(bankInfos);
                context.Remove(doc);
                context.Remove(addresses);
                await context.SaveChangesAsync();
                if (result.Succeeded)
                {
                    return RedirectToAction("UsersList");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("UsersList");
        }
        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "ViewEditUser")]
        public async Task<IActionResult> EditUser(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }
            var userclimes = await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);
            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Clims = userclimes.Select(c => c.Value).ToList(),
                Roles = userRoles.ToList()
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "EditUser")]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.Email = model.Email;
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("UsersList");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "ManageUserRoles")]
        public async Task<IActionResult> ManageUserRoles(string UserId)
        {
            ViewBag.UserId = UserId;
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return NotFound();
            }
            var model = new List<ManageUserRole>();
            foreach (var role in roleManager.Roles)
            {
                var ManageUserRole = new ManageUserRole
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    ManageUserRole.IsSelected = true;
                }
                else
                {
                    ManageUserRole.IsSelected = false;
                }
                model.Add(ManageUserRole);
            }
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        [Authorize(Policy = "ManageUserRoles")]
        public async Task<IActionResult> ManageUserRoles(List<ManageUserRole> model, string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return NotFound();
            }
            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            return RedirectToAction("EditUser", new { id = UserId });
        }
        public IActionResult DownloadAsExel()
        {
            var builder = new StringBuilder();
            builder.AppendLine("First Name,Middle Name,Father Name,Email, UserName, Id, Department, super Employee,Toltal Sick Leaves, Total Vacations,Totole Houres To leave");
            foreach (var user in userManager.Users)
            {
                var department = context.Department.Where(u => u.Id == user.DepartmentName).Select(u => u.Name).SingleOrDefault();
                var super = context.OfficialDetails.Where(u => u.EmployeeId == user.Id).Select(u => u.SuperEmlpoyeeId).SingleOrDefault();
                var superEmployee = context.Users.Where(u => u.Id == super).Select(u => u.Email).SingleOrDefault();
                var Sickleave = context.OfficialDetails.Where(u => u.EmployeeId == user.Id).Select(u => u.Sickleave).SingleOrDefault();
                var TotalVacations = context.OfficialDetails.Where(u => u.EmployeeId == user.Id).Select(u => u.TotalVacations).SingleOrDefault();
                var TotoleHouresToleave = context.OfficialDetails.Where(u => u.EmployeeId == user.Id).Select(u => u.TotoleHouresToleave).SingleOrDefault();
                builder.AppendLine($"{user.FirstName},{user.FatherName},{user.MiddleName},{user.Email} , {user.UserName} , {user.Id}, {department}, {superEmployee},{Sickleave},{TotalVacations},{TotoleHouresToleave}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "UsersInfo.csv");
        }
    }
}