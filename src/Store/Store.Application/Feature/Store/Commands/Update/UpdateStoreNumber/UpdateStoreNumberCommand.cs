using MediatR;
using Store.Domain.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Feature.Store.Commands.Update.UpdateStoreNumber
{
    public class UpdateStoreNumberCommand : IRequest<ResultDto>
    {
        public Guid ProductId { get; set; }
        public int Number { get; set; }
    }
}
