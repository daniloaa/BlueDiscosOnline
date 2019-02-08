using System;
using System.Collections.Generic;
using System.Text;

namespace BlueDiscosOnline.Domain.Models
{
    public class VendaModel
    {
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
