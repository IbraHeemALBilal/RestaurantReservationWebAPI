using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Entities
{
    public class CustomerWithPartySize
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public int PartySize { get; set; }
        public override string ToString()
        {
            return $"Customer ID: {CustomerId}, Name: {FirstName}, Email: {Email}, Party Size: {PartySize}";
        }
    }
}
