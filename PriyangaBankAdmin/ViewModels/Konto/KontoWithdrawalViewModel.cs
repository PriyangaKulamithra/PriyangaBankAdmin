using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PriyangaBankAdmin.ViewModels.Konto
{
    public class KontoWithdrawalViewModel
    {
        public int AccountId { get; set; }
        public decimal AvailableBalance { get; set; }

        [Required(ErrorMessage = "Var god ange ett belopp att ta ut")]
        [DataType(DataType.Currency)]
        public decimal AmountToWithdraw { get; set; }
    }
}
