using BethanysPieShopHRM.Shared;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.UI.Pages {

	public interface IExpenseApprovalService {
		Task<ExpenseStatus> GetExpenseStatus(Expense expense);
	}
}