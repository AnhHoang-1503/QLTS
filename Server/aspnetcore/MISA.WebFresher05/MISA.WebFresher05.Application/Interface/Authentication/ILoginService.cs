namespace MISA.WebFresher05.Application
{
    public interface ILoginService
    {
        /// <summary>
        /// Xử lý login
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        /// Created by: vtahoang (16/09/2023) 
        public Task<string> LoginAsync(LoginRequest request);
    }
}
