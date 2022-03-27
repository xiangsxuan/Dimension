using DimensionClient.Context;
using DimensionClient.Models;

namespace DimensionClient.Dao.LoginUser
{
    public static class LoginUserDAO
    {
        /// <summary>
        /// 已登录账户列表
        /// </summary>
        /// <returns></returns>
        public static List<LoginUserModel> GetLoginUsers()
        {
            using ClientContext context = new();
            return context.LoginUser.ToList();
        }

        /// <summary>
        /// 更新已登录账户列表
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static bool UpdateLoginUser(LoginUserModel loginUser)
        {
            using ClientContext context = new();
            if (context.LoginUser.Where(item => item.UserID == loginUser.UserID).FirstOrDefault() is LoginUserModel login)
            {
                login.NickName = loginUser.NickName;
                login.LoginName = loginUser.LoginName;
                login.HeadPortrait = loginUser.HeadPortrait;
                if (loginUser.Password != null)
                {
                    login.Password = loginUser.Password;
                }
            }
            else
            {
                context.LoginUser.Add(loginUser);
            }
            return context.SaveChanges() > 0;
        }

        /// <summary>
        /// 删除账户历史
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteLoginUser(int id)
        {
            using ClientContext context = new();
            if (context.LoginUser.Where(item => item.ID == id).FirstOrDefault() is LoginUserModel login)
            {
                context.LoginUser.Remove(login);
            }
            return context.SaveChanges() > 0;
        }
    }
}
