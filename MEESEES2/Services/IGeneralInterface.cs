using MEESEES2.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MEESEES2.Commons
{
    public interface IGeneralInterface
    {
        //GET ALL
        Task<IEnumerable<User>> GetUsers();
        Task<IEnumerable<UserSetting>> GetUserSettings();
        Task<IEnumerable<UserData>> GetUserDatas();
        Task<IEnumerable<Category>> GetCategories();

        //GET WITH PARAM
        Task<IEnumerable<User>> GetUsersByPin(string pin);
        Task<IEnumerable<UserSetting>> GetUserSettingsByPin(string pin);
        Task<IEnumerable<UserData>> GetUserDataByPin(string pin);
        Task<IEnumerable<Category>> GetCategoriesByType(string type);

        //GET RECORD
        Task<User> GetUser(int id);
        Task<UserData> GetUserData(int id);
        Task<UserSetting> GetSetting(int id);
        Task<Category> GetCategory(int id);

        //ADD RECORD
        Task CreateUser(User user);
        Task CreateSetting(UserSetting setting);
        Task AddUserData(UserData data);
        Task CreateCategory(Category category);

        //DELETE RECORD
        Task DeleteUser(User user);
        Task DeleteUserData(UserData data);
        Task DeleteUserSetting(UserSetting setting);
        Task DeleteCategory(Category category);

        //UPDATE RECORD
        Task UpdateUser(User user);
        Task UpdateUserData(UserData data);
        Task UpdateUserSetting(UserSetting setting);
        Task UpdateCategory(Category category);

        //RESET RECORD
        Task ResetUserData(string pin);
    }
}
