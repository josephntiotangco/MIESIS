using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEESEES2.Models;
using SQLite;

namespace MEESEES2.Commons
{
    public class SQLiteGeneral : IGeneralInterface
    {

        private SQLiteAsyncConnection _connection;
        public SQLiteGeneral(ISQLiteDb db, string mode)
        {
            _connection = db.GetConnection();
            if (mode == "reset")
            {
                _connection.DropTableAsync<User>();
                _connection.DropTableAsync<UserSetting>();
                _connection.DropTableAsync<UserData>();
                _connection.DropTableAsync<Category>();
            }
            _connection.CreateTableAsync<User>();
            _connection.CreateTableAsync<UserSetting>();
            _connection.CreateTableAsync<UserData>();
            _connection.CreateTableAsync<Category>();
        }
        //GET ALL RECORD
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _connection.Table<User>().ToListAsync();
        }
        public async Task<IEnumerable<UserSetting>> GetUserSettings()
        {
            return await _connection.Table<UserSetting>().ToListAsync();
        }
        public async Task<IEnumerable<UserData>> GetUserDatas()
        {
            return await _connection.Table<UserData>().ToListAsync();
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _connection.Table<Category>().ToListAsync();
        }

        //GET RECORD BY PIN
        public async Task<IEnumerable<User>> GetUsersByPin(string pin)
        {
            return await _connection.Table<User>().Where(d => d.Pin == pin).ToListAsync();

        }
        public async Task<IEnumerable<UserSetting>> GetUserSettingsByPin(string pin)
        {
            return await _connection.Table<UserSetting>().Where(d => d.Pin == pin).ToListAsync();

        }
        public async Task<IEnumerable<UserData>> GetUserDataByPin(string pin)
        {
            return await _connection.Table<UserData>().Where(d => d.Pin == pin).ToListAsync();
        }
        public async Task<IEnumerable<Category>> GetCategoriesByType(string type)
        {
            return await _connection.Table<Category>().Where(c => c.Type == type).ToListAsync();
        }
        //CREATE RECORD
        public async Task CreateUser(User user)
        {
            await _connection.InsertAsync(user);
        }
        public async Task AddUserData(UserData data)
        {
            await _connection.InsertAsync(data);
        }
        public async Task CreateSetting(UserSetting setting)
        {
            await _connection.InsertAsync(setting);
        }
        public async Task CreateCategory(Category category)
        {
            await _connection.InsertAsync(category);
        }

        //UPDATE RECORD
        public async Task UpdateUser(User user)
        {
            await _connection.UpdateAsync(user);
        }

        public async Task UpdateUserData(UserData data)
        {
            await _connection.UpdateAsync(data);
        }

        public async Task UpdateUserSetting(UserSetting setting)
        {
            await _connection.UpdateAsync(setting);
        }
        public async Task UpdateCategory(Category category)
        {
            await _connection.UpdateAsync(category);
        }

        //DELETE RECORD
        public async Task DeleteUser(User user)
        {
            await _connection.DeleteAsync(user);
        }
        public async Task DeleteUserData(UserData data)
        {
            await _connection.DeleteAsync(data);
        }
        public async Task DeleteUserSetting(UserSetting setting)
        {
            await _connection.DeleteAsync(setting);
        }
        public async Task DeleteCategory(Category category)
        {
            await _connection.DeleteAsync(category);
        }

        //GET RECORD
        public async Task<User> GetUser(int id)
        {
            return await _connection.FindAsync<User>(id);
        }
        public async Task<UserData> GetUserData(int id)
        {
            return await _connection.FindAsync<UserData>(id);
        }
        public async Task<UserSetting> GetSetting(int id)
        {
            return await _connection.FindAsync<UserSetting>(id);
        }
        public async Task<Category> GetCategory(int id)
        {
            return await _connection.FindAsync<Category>(id);
        }

        //RESET RECORD
        public async Task ResetUserData(string pin)
        {
            var userdatas = await this.GetUserDataByPin(pin);
            if (userdatas.Count() != 0)
            {
                try
                {
                    foreach (UserData data in userdatas)
                    {
                        await this.DeleteUserData(data);
                    }

                    int count = userdatas.Count();
                    Globals.isSuccess = true;
                    Globals.lastError = $"Transaction Reset Successful! Count of User Data: {count}";
                }
                catch (Exception ex)
                {
                    Globals.isSuccess = false;
                    Globals.lastError = ex.Message;
                    return;
                }
            }
            else
            {
                Globals.isSuccess = false;
                Globals.lastError = "User has no data to delete";
                return;
            }

        }
    }
}
