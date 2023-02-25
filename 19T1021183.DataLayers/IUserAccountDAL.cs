using _19T1021183.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021183.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lí tài liệu liên quan đến tài khoản
    /// </summary>
    public interface IUserAccountDAL
    {
        /// <summary>
        /// Kiểm tra thông tin tài khoản, nếu đăng nhập thành công, trả về thông tin tài khoản, nếu không thành công thì trả về null
        /// </summary>
        /// <param name="uerName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserAccount Authorize(string uerName, string password);
        /// <summary>
        /// Đổi mât khẩu của tài khoản
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}
