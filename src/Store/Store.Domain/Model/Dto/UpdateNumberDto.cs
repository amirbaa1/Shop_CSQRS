﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Model.Dto
{
    public class UpdateNumberDto
    {
        public Guid ProductId { get; set; }
        public int Number { get; set; }
    }
}
