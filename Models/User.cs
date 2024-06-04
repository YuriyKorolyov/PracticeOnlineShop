﻿using MyApp.Interfaces.BASE;

namespace MyApp.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string ShippingAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ViewHistory> ViewHistories { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public Role Role { get; set; }
    }

}
