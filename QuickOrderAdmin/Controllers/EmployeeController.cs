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
            var employeedata = await employeeDataStore.GetEmployeesOfStore(SelectedStore.CurrentStore.StoreId);

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

            var employee = StoreEmployees.Where(e => e.EmployeeId == id).FirstOrDefault();


            EditEmployeeViewModel editEmployeeViewModel = new EditEmployeeViewModel();

            editEmployeeViewModel.EmployeeInformation = employee;

            editEmployeeViewModel.EmployeeInformation.EmployeeUser = await userDataStore.GetItemAsync(editEmployeeViewModel.EmployeeInformation.UserId.ToString());
            return View(editEmployeeViewModel);
        }


        public async Task<IActionResult> EditEmployeeForm(EditEmployeeViewModel employeeViewModel)
        {

            var editEmployee = await employeeDataStore.GetItemAsync(employeeViewModel.EmployeeInformation.EmployeeId.ToString());

            var workhourshedule = new List<EmployeeWorkHour>();

            workhourshedule.Add(new EmployeeWorkHour()
            {
                EmpId = editEmployee.EmployeeId,
                Day = DayOfWeek.Monday.ToString(),
                WorkHourId = Guid.NewGuid(),
                WillWork=employeeViewModel.WillWorkMonday,
                CloseTime = employeeViewModel.MCloseTime,
                OpenTime = employeeViewModel.MOpenTime

            });
            workhourshedule.Add(new EmployeeWorkHour()
            {
                EmpId = editEmployee.EmployeeId,
                Day = DayOfWeek.Tuesday.ToString(),
                WorkHourId = Guid.NewGuid(),
                WillWork = employeeViewModel.WillWorkTuesday,
                CloseTime = employeeViewModel.TCloseTime,
                OpenTime = employeeViewModel.TOpenTime

            });
            workhourshedule.Add(new EmployeeWorkHour()
            {
                EmpId = editEmployee.EmployeeId,
                Day = DayOfWeek.Wednesday.ToString(),
                WorkHourId = Guid.NewGuid(),
                WillWork = employeeViewModel.WillWorkWednesday,
                CloseTime = employeeViewModel.WCloseTime,
                OpenTime = employeeViewModel.WOpenTime

            });
            workhourshedule.Add(new EmployeeWorkHour()
            {
                EmpId = editEmployee.EmployeeId,
                Day = DayOfWeek.Thursday.ToString(),
                WorkHourId = Guid.NewGuid(),
                WillWork = employeeViewModel.WillWorkThursday,
                CloseTime = employeeViewModel.ThCloseTime,
                OpenTime = employeeViewModel.ThOpenTime

            });
            workhourshedule.Add(new EmployeeWorkHour()
            {
                EmpId = editEmployee.EmployeeId,
                Day = DayOfWeek.Friday.ToString(),
                WorkHourId = Guid.NewGuid(),
                WillWork = employeeViewModel.WillWorkFriday,
                CloseTime = employeeViewModel.FCloseTime,
                OpenTime = employeeViewModel.FOpenTime

            });
            workhourshedule.Add(new EmployeeWorkHour()
            {
                EmpId = editEmployee.EmployeeId,
                Day = DayOfWeek.Saturday.ToString(),
                WorkHourId = Guid.NewGuid(),
                WillWork = employeeViewModel.WillWorkSaturday,
                CloseTime = employeeViewModel.SCloseTime,
                OpenTime = employeeViewModel.SOpenTime

            });
            workhourshedule.Add(new EmployeeWorkHour()
            {
                EmpId = editEmployee.EmployeeId,
                Day = DayOfWeek.Sunday.ToString(),
                WorkHourId = Guid.NewGuid(),
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

                return RedirectToAction("EditEmployee", new { id = editEmployee.EmployeeId });
            }

        }

        public async Task<IActionResult> DetailEmployee(Guid id)
        {
            var employee = await employeeDataStore.GetItemAsync(id.ToString());

            var user = await userDataStore.GetItemAsync(employee.UserId.ToString());

            var workhours = await employeeWorkHourDataStore.GetEmployeeWorkHours(employee.EmployeeId.ToString());

            var detailEmployeeVm = new EmployeeDetailViewModel()
            {
                Address = user.Address,
                Gender = user.Gender,
                EmployeeWorkHours = workhours.ToList(),
                EmployeeId = employee.EmployeeId,
                Email = user.Email,
                Name = user.Name,
                Phone = user.Phone,
                EmpType = employee.Type
            };

            return View(detailEmployeeVm);
        }

        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var result = employeeDataStore.DeleteItemAsync(id.ToString());

            return RedirectToAction("ShowEmployees");

        }
    }
}