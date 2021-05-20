using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PriyangaBankAdmin.ViewModels.Konto
{
    public class KontoNewTransactionViewModel
    {
        public int AccountId { get; set; }
        public List<SelectListItem> Operations { get; set; }

        [Required(ErrorMessage = "Välj transaktionstyp")]
        public int SelectedOperationId { get; set; }

        [Required(ErrorMessage = "Välj belopp")]
        public decimal Amount { get; set; }
        public decimal AvailableBalance { get; set; }
        public int TransferToAccountNumber { get; set; }

    }
}
