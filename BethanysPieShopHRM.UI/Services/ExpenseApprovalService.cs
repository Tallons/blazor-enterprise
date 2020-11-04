﻿using BethanysPieShopHRM.Shared;
using BethanysPieShopHRM.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.UI.Pages {
	public class ExpenseApprovalService : IExpenseApprovalService {

		private readonly IEmployeeDataService _employeeService;

		public ExpenseApprovalService(IEmployeeDataService employeeService) {
			_employeeService = employeeService;
		}

		public  async Task<ExpenseStatus> GetExpenseStatus(Expense expense) {

			var employee = await _employeeService.GetEmployeeDetails(expense.EmployeeId);

			if (employee.IsOPEX) {
				switch (expense.ExpenseType) {
					case ExpenseType.Conference:
						return ExpenseStatus.Denied;
					case ExpenseType.Transportation:
						return ExpenseStatus.Denied;
					case ExpenseType.Hotel:
						return ExpenseStatus.Denied;
				}

				if (expense.Status != ExpenseStatus.Denied) {
					expense.CoveredAmount = expense.Amount / 2;
				}
			}

			if (!employee.IsFTE) {
				if (expense.ExpenseType != ExpenseType.Training) {
					return ExpenseStatus.Denied;
				}
			}

			return ExpenseStatus.Pending;
		}
	}
}
