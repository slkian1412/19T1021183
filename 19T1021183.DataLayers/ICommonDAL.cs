using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021183.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lí dữ liệu chung( theo kiểu Generic)
    /// </summary>
    public interface ICommonDAL <T> where T : class
    {
        /// <summary>
        /// Tìm kiếm, lấy danh sách dữ liệu dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng hiển thị trên mỗi trang(0 nếu không phân trang) </param>
        /// <param name="searchValue">Giá trị tìm kiếm(chuỗi rỗng nếu lấy toàn bộ dữ liệu)</param>
        /// <returns></returns>
        IList<T> List(int page = 1, int pageSize = 0, string searchValue = "");
        /// <summary>
        /// Đếm số dòng thỏa điều kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Giá trị tìm kiếm(chuỗi rỗng nếu lấy toàn bộ dữ liệu)</param>
        /// <returns></returns>
        int Count(String searchValue);
        T Get(int id );
        /// <summary>
        /// Bổ sung dữ liệu vào CSDL
        /// </summary>
        /// <param name="data">Đối tượng chứa dữ liệu cần bổ sung</param>
        /// <returns></returns>
        int Add(T data);
        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(T data);
        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Delete(T data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool InUsed(int id);
        bool Delete(int id);
    }
  
}
