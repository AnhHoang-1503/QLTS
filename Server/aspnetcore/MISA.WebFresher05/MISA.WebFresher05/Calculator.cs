using System.Xml.Linq;

namespace MISA.WebFresher05
{
    public class Calculator
    {
        /// <summary>
        /// Cộng 2 số nguyên
        /// </summary>
        /// <param name="x"> Số thứ nhất </param>
        /// <param name="y"> Số thứ hai </param>
        /// <returns> Tổng 2 số x + y </returns>
        /// Created by: vtahoang (13/07/2023)
        public long Add(int x, int y)
        {
            return x + (long)y;
        }

        /// <summary>
        /// Trừ 2 số nguyên
        /// </summary>
        /// <param name="x"> Số thứ nhất </param>
        /// <param name="y"> Số thứ hai </param>
        /// <returns> Hiệu 2 số x - y </returns>
        /// Created by: vtahoang (13/07/2023)
        public long Sub(int x, int y)
        {
            return x - (long)y;
        }

        /// <summary>
        /// Nhân 2 số nguyên
        /// </summary>
        /// <param name="x"> Số thứ nhất </param>
        /// <param name="y"> Số thứ hai </param>
        /// <returns> Tích 2 số x * y </returns>
        /// Created by: vtahoang (13/07/2023)
        public long Mul(int x, int y)
        {
            return x * (long)y;
        }

        /// <summary>
        /// Thương 2 số nguyên
        /// </summary>
        /// <param name="x"> Số thứ nhất </param>
        /// <param name="y"> Số thứ hai </param>
        /// <returns> Thương 2 số dạng thập phân </returns>
        /// <exception cref="Exception"> Thrown khi y = 0 </exception>
        /// Created by: vtahoang (13/07/2023)
        public double Div(int x, int y)
        {
            if (y == 0)
            {
                throw new Exception("Không chia được cho 0");
            }

            return x / (double)y;
        }

        public long Add(string input)
        {
            // Chuỗi rỗng thì trả về 0
            if (input == "") return 0;

            // Cắt chuỗi theo dấu ','
            string[] value = input.Split(',');
            // Kết quả trả về
            var result = 0;
            // Chuỗi số âm
            var negativeNumber = "";
            // Chuỗi có hợp lệ không
            var isValid = true;
            foreach (var item in value)
            {
                var itemValue = 0;
                isValid = isValid && int.TryParse(item, out itemValue);
                if (itemValue < 0)
                {
                    negativeNumber += itemValue + ", ";
                }
                else
                {
                    result += itemValue;
                }
            }
            if (!isValid)
            {
                throw new Exception("Chuỗi truyền vào không hợp lệ");
            }
            if (negativeNumber.Length > 0)
            {
                var exceptionMessage = "Không chấp nhận toán tử âm: " + negativeNumber.Substring(0, negativeNumber.Length - 2);
                throw new Exception(exceptionMessage);
            }
            return result;
        }
    }
}
