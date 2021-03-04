using System;
using System.ComponentModel.DataAnnotations;

namespace BTITaxPayerService.Model
{
    public class ClientDto
    {
        [Key]
        //public int Id { get; set; }
        public string Identifier { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string AccountType { get; set; }
        public string AliasInvoicePK { get; set; }
        public string AliasInvoiceGB { get; set; }
        public string AliasDespatchAdvicePK { get; set; }
        public string AliasDespatchAdviceGB { get; set; }
        public DateTime? FirstCreationTimeInvoicePK { get; set; }
        public DateTime? FirstCreationTimeInvoiceGB { get; set; }
        public DateTime? AliasCreationTimeInvoicePK { get; set; }
        public DateTime? AliasDeletionTimeInvoicePK { get; set; }
        public DateTime? AliasCreationTimeInvoiceGB { get; set; }
        public DateTime? AliasDeletionTimeInvoiceGB { get; set; }
        public DateTime? AliasCreationTimeDespatchAdvicePK { get; set; }
        public DateTime? AliasCreationTimeDespatchAdviceGB { get; set; }
    }
}
