using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Exceptions
{
    // use primary constructor
    public abstract class NotFoundException(string message) : Exception(message)
    {
    }

    public sealed class ProductNotFoundException(int id) : NotFoundException($"Product With Id : {id} Is Not Found") { }

    public sealed class BasketNotFoundException(string id) : NotFoundException($"Basket With Id : {id} Is Not Found") { }

}
