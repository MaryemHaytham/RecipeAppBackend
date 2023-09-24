using RecipeAppBLL.Services.IService;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Services
{
    public class UserService : IUserService

    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public User Authenticate(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);

            if (user == null || !VerifyPasswordHash(password, user.Password))
            {
                return null; // Authentication failed
            }

            return user; // Authentication successful
        }

        private bool VerifyPasswordHash(string password1, string password2)
        {
            throw new NotImplementedException();
        }

        public bool ResetPassword(string email, string newPassword)
        {
            var user = _userRepository.GetUserByEmail(email);

            if (user == null)
            {
                return false; // User not found
            }

            user.Password = HashPassword(newPassword);
            _userRepository.Update(user);
            return true;
        }
        public bool UpdateProfile(int userId, User updatedUser)
        {
            var existingUser = _userRepository.GetById(userId);

            if (existingUser == null)
            {
                return false; // User not found
            }

            // Update user properties as needed
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Email = updatedUser.Email;

            _userRepository.Update(existingUser);
            return true;
        }

        //Register 
        public bool IsEmailUnique(string email)
        {
            // Check if the email is unique (not already registered)
            return !_userRepository.GetAll().Any(u => u.Email == email);
        }

        public bool RegisterUser(User user)
        {
            // Validate user input
            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName) ||
                string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            {
                // Input is invalid
                return false;
            }

            // Check if the email is unique
            if (!IsEmailUnique(user.Email))
            {
                // Email is already registered
                return false;
            }

            // Hash the password before storing it in the database
            user.Password = HashPassword(user.Password);



            // Add the user to the database
            _userRepository.Add(user);

            return true;
        }

        // Hash the password using a secure hashing algorithm 
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public User GetUser(int userId)
        {
            return _userRepository.GetById(userId);
        }
    }
}
