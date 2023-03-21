


namespace Data.Interfaces
{
    public interface IApplicationUser
    {
        Task<RegisterModelDTO> SiginUp(RegisterModelDTO registerModelDTO);
        Task<LoginDTO> Login(LoginDTO loginDTO);
    }
}