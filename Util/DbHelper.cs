using DbBasicApp.Models;
using Microsoft.Data.Entity;

namespace DbBasicApp.Util
{
    /// <summary>
    /// 数据库帮助类
    /// </summary>
    public static class DbHelper
    {
        private static bool _dbChecked = false;
        
        /// <summary>
        /// 确保数据库已经创建
        /// </summary>
        /// <param name="context">数据库上下文对象</param>
        public static void EnsureDatabaseCreated(AppDbContext context)
        {
            if (!_dbChecked)
            {
                _dbChecked = true;
                context.Database.Migrate();
            }
        }
    }
}