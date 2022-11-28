using Library.Helpers;
using Library.Models;
using Library.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using QuickOrderAdmin.Models;
using QuickOrderAdmin.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickOrderAdmin.Controllers
{
    public class EmployeeController : Controller
    {
        readonly IEmployeeDataStore employeeDataStore;
        readonly IStoreDataStore storeDataStore;
        readonly IUserDataStore userDataStore;
        readonly IEmployeeWorkHourDataStore employeeWorkHourDataStore;

        static List<Employee> StoreEmployees;

        public EmployeeController(IEmployeeDataStore employeeData, IUserDataStore userData, IStoreDataStore storeData, IEmployeeWorkHourDataStore employeeWorkHourData)
        {


            employeeDataStore = employeeData;
            userDataStore = userData;
            storeDataStore = storeData;
            employeeWorkHourDataStore = employeeWorkHourData;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ShowEmployees()
        {
            var employeedata = await employeeDataStore.GetEmployeesOfStore(SelectedStore.CurrentStore.ID);

            StoreEmployees = new List<Employee>(employeedata);

            foreach (var item in employeedata)
            {
                item.EmployeeStore = await storeDataStore.GetItemAsync(item.StoreId.ToString());
                item.EmployeeUser = await userDataStore.GetItemAsync(item.UserId.ToString());
            }

            return View(employeedata);
        }


        public async Task<IActionResult> EditEmployee(Guid id)
        {

            var employee = StoreEmployees.Where(e => e.ID == id).FirstOrDefault();


            EditEmployeeViewModel editEmployeeViewModel = new EditEmployeeViewModel();

            editEmployeeViewModel.EmployeeInformation = employee;

            editEmployeeViewModel.EmployeeInformation.EmployeeUser = await userDataStore.GetItemAsync(editEmployeeViewModel.EmployeeInformation.UserId.ToString());
            return View(editEmployeeViewModel);
        }


        public async Task<IActionResult> EditEmployeeForm(EditEmployeeViewModel employeeViewModel)
        {

            var editEmployee = await employeeDataStore.GetItemAsync(employeeViewModel.EmployeeInformation.ID.ToString());

            var workhourshedule = new List<EmployeeWorkHour>();

            workhourshedule.Add(new EmployeeWorkHour()
            {
                EmpId = editEmployee.ID,
                Day = DayOfWeek.Monday.ToString(),
                ID = Guid.NewGuid(),
                WillWork=employeeViewModel.WillWorkMonday,
                CloseTime = employeeViewModel.MCloseTime,
                OpenTime = employeeViewModel.MOpenTime

            });
            workhourshedule.Add(new EmployeeWorkHour()
            {
                EmpId = editEmployee.ID,
                Day = DayOfWeek.Tuesday.ToString(),
                ID = Guid.NewGuid(),
                WillWork = employeeViewModel.WillWorkTuesday,
                CloseTime = employeeViewModel.TCloseTime,
                OpenTime = employeeViewModel.TOpenTime

            });
            workhourshedule.Add(new EmployeeWorkHour()
            {
                EmpId = editEmployee.ID,
                Day = DayOfWeek.Wednesday.ToString(),
                ID = Guid.NewGuid(),
                WillWork = employeeViewModel.WillWorkWednesday,
                CloseTime = employeeViewModel.WCloseTime,
                OpenTime = employeeViewModel.WOpenTime

            });
            workhourshedule.Add(new EmployeeWorkHour()
            {
                EmpId = editEmployee.ID,
                Day = DayOfWeek.Thursday.ToString(),
                ID = Guid.NewGuid(),
                WillWork = employeeViewModel.WillWorkThursday,
                CloseTime = employeeViewModel.ThCloseTime,
                OpenTime = employeeViewModel.ThOpenTime

            });
            workhourshedule.Add(new EmployeeWorkHour()
            {
                EmpId = editEmployee.ID,
                Day = DayOfWeek.Friday.ToString(),
                ID = Guid.NewGuid(),
                WillWork = employeeViewModel.WillWorkFriday,
                CloseTime = employeeViewModel.FCloseTime,
                OpenTime = employeeViewModel.FOpenTime

            });
            workhourshedule.Add(new EmployeeWorkHour()
            {
                EmpId = editEmployee.ID,
                Day = DayOfWeek.Saturday.ToString(),
                ID = Guid.NewGuid(),
                WillWork = employeeViewModel.WillWorkSaturday,
                CloseTime = employeeViewModel.SCloseTime,
                OpenTime = employeeViewModel.SOpenTime

            });
            workhourshedule.Add(new EmployeeWorkHour()
            {
                EmpId = editEmployee.ID,
                Day = DayOfWeek.Sunday.ToString(),
                ID = Guid.NewGuid(),
                WillWork = employeeViewModel.WillWorkSunday,
                CloseTime = employeeViewModel.SuCloseTime,
                OpenTime = employeeViewModel.SuOpenTime

            });

            EmployeeType emptype;

            Enum.TryParse(employeeViewModel.Position, out emptype);

            editEmployee.Type = emptype;
            editEmployee.EmployeeWorkHours = workhourshedule;


            var result = await employeeDataStore.UpdateItemAsync(editEmployee);

            if (result)
            {
                return RedirectToAction("ShowEmployees");
            }
            else
            {

                return RedirectToAction("EditEmployee", new { id = editEmployee.ID });
            }

        }

        public async Task<IActionResult> DetailEmployee(Guid id)
        {
            var employee = await employeeDataStore.GetItemAsync(id.ToString());

            var user = await userDataStore.GetItemAsync(employee.UserId.ToString());

            var workhours = await employeeWorkHourDataStore.GetEmployeeWorkHours(employee.ID.ToString());

            var detailEmployeeVm = new EmployeeDetailViewModel()
            {
                Address = user.Address,
                Gender = user.Gender,
                EmployeeWorkHours = workhours.ToList(),
                EmployeeId = employee.ID,
                Email = user.Email,
                Name = user.Name,
                Phone = user.Phone,
                EmpType = employee.Type
            };

            return View(detailEmployeeVm);
        }

        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var result = await employeeDataStore.DeleteItemAsync(id.ToString());

            return RedirectToAction("ShowEmployees");

        }
    }
}