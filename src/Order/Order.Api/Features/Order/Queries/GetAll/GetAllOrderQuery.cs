using MediatR;
using Order.Domain.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Api.Features.Order.Queries.GetAll
{
    public class GetAllOrderQuery : IRequest<List<OrderModelDto>>
    {
    }
}
