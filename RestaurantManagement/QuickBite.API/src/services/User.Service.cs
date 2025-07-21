using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using QuickBite.API.src.enums;
using QuickBite.API.src.models.dto.session;
using QuickBite.API.src.models.dto.user;
using QuickBite.API.src.models.entities;
using QuickBite.API.src.repositories;
using QuickBite.API.src.utils;

namespace QuickBite.API.src.services;


// Interface

public interface IUserService
{
    public Task<ResponseEntity<UserEntity?>> CreateNewCustomer(UserCreateRequestBodyDTO data);
    public Task<ResponseEntity<UserEntity?>> CreateNewAdmin(UserCreateRequestBodyDTO data);
    public Task<ResponseEntity<UserEntity?>> GetOneUserById(int id);
    public Task<ResponseEntity<IEnumerable<UserEntity>>> GetAllUsers();
    public Task<ResponseEntity<IEnumerable<UserEntity>>> GetAllUsers(UserRole role);
    public Task<ResponseEntity<UserEntity?>> UpdateUserById(int id, UserUpdateRequestBodyDTO data);
    public Task<ResponseEntity<UserEntity?>> DeleteUserById(int id);
    public Task<ResponseEntity<SessionResponseDTO>> LoginUser(UserLoginRequestBodyDTO data);
    public Task<ResponseEntity<SessionResponseDTO>> RefreshUserSession(SessionRefreshRequestBodyDTO data);
    public Task<ResponseEntity<SessionResponseDTO>> LogoutUser(SessionRemoveRequestBodyDTO data);
    public string GenerateJWT(int id);
    public string GenerateRefreshToken();

}

// Implementing Class
public class UserService(IConfiguration configuration, IUserRepository userRepository, ISessionRepository sessionRepository) : IUserService
{
    private readonly IConfiguration _configuration = configuration;

    private readonly IUserRepository _userRepository = userRepository;

    private readonly ISessionRepository _sessionRepository = sessionRepository;

    public async Task<ResponseEntity<UserEntity?>> CreateNewCustomer(UserCreateRequestBodyDTO data)
    {
        await _userRepository.Create(new UserEntity(data) { Role = UserRole.Customer });
        await _userRepository.SaveChangesAsync();
        return new ResponseEntity<UserEntity?>(HttpStatusCode.Created, "Customer Created Successfully");
    }

    public async Task<ResponseEntity<UserEntity?>> CreateNewAdmin(UserCreateRequestBodyDTO data)
    {
        await _userRepository.Create(new UserEntity(data) { Role = UserRole.Admin });
        await _userRepository.SaveChangesAsync();
        return new ResponseEntity<UserEntity?>(HttpStatusCode.Created, "Admin Created Successfully");
    }

    public async Task<ResponseEntity<UserEntity?>> UpdateUserById(int id, UserUpdateRequestBodyDTO data)
    {
        UserEntity? user = await _userRepository.FindById(id);
        if (user == null)
        {
            return new ResponseEntity<UserEntity?>(HttpStatusCode.NotFound, "User Not Found");
        }
        user.Address = data.Address;
        user.ContactNumber = data.ContactNumber;
        user.Name = data.Name;
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
        return new ResponseEntity<UserEntity?>(HttpStatusCode.OK, "Customer Updated Successfully");
    }

    public async Task<ResponseEntity<UserEntity?>> DeleteUserById(int id)
    {
        UserEntity? user = await _userRepository.FindById(id);
        if (user == null)
        {
            return new ResponseEntity<UserEntity?>(HttpStatusCode.NotFound, "User Not Found");
        }
        _userRepository.Delete(user);
        await _userRepository.SaveChangesAsync();
        return new ResponseEntity<UserEntity?>(HttpStatusCode.OK, "Customer Deleted Successfully");
    }

    public async Task<ResponseEntity<UserEntity?>> GetOneUserById(int id)
    {
        UserEntity? user = await _userRepository.FindById(id);
        if (user == null)
        {
            return new ResponseEntity<UserEntity?>(HttpStatusCode.NotFound, "User Not Found");
        }
        return new ResponseEntity<UserEntity?>(HttpStatusCode.OK, "User Found Successfully", user);
    }

    public async Task<ResponseEntity<IEnumerable<UserEntity>>> GetAllUsers()
    {
        IEnumerable<UserEntity> users = await _userRepository.FindAll();
        return new ResponseEntity<IEnumerable<UserEntity>>(HttpStatusCode.OK, $"{users.Count()} User(s) Found Successfully", users);
    }

    public async Task<ResponseEntity<IEnumerable<UserEntity>>> GetAllUsers(UserRole role)
    {
        IEnumerable<UserEntity> users = await _userRepository.FindAllByRole(role);
        return new ResponseEntity<IEnumerable<UserEntity>>(HttpStatusCode.OK, $"{users.Count()} {Enum.GetName(role)}(s) Found Successfully", users);
    }

    public async Task<ResponseEntity<SessionResponseDTO>> LoginUser(UserLoginRequestBodyDTO data)
    {
        UserEntity? user = await _userRepository.FindByEmail(data.Email);
        if (user == null)
        {
            return new ResponseEntity<SessionResponseDTO>(HttpStatusCode.Unauthorized, "incorrect Credentials");
        }
        if (!user.Password.Equals(data.Password))
        {
            return new ResponseEntity<SessionResponseDTO>(HttpStatusCode.Unauthorized, "Incorrect Credentials");
        }
        var token = GenerateJWT(user.Id);
        string refreshToken = GenerateRefreshToken();
        await _sessionRepository.Create(new SessionEntity() { Token = refreshToken, UserId = user.Id });
        await _sessionRepository.SaveChangesAsync();

        return new ResponseEntity<SessionResponseDTO>(HttpStatusCode.OK, "User Logged In", new SessionResponseDTO(token, refreshToken));
    }

    public async Task<ResponseEntity<SessionResponseDTO>> RefreshUserSession(SessionRefreshRequestBodyDTO data)
    {
        SessionEntity? session = await _sessionRepository.FindByToken(data.RefreshToken);
        if (session == null)
        {
            return new ResponseEntity<SessionResponseDTO>(HttpStatusCode.Unauthorized, "Unauthorized Access Attempt");
        }
        if (DateTime.Compare(session.ExpDate, DateTime.Now) < 0)
        {
            return new ResponseEntity<SessionResponseDTO>(HttpStatusCode.Unauthorized, "Session Expired. Please log in again");
        }

        var accessToken = GenerateJWT(session.UserId);
        string refreshToken = GenerateRefreshToken();
        session.Token = refreshToken;
        session.ExpDate = DateTime.Now.AddDays(7);
        _sessionRepository.Update(session);

        return new ResponseEntity<SessionResponseDTO>(HttpStatusCode.OK, "User Session Refreshed", new SessionResponseDTO(accessToken, refreshToken));
    }

    public async Task<ResponseEntity<SessionResponseDTO>> LogoutUser(SessionRemoveRequestBodyDTO data)
    {
        SessionEntity? session = await _sessionRepository.FindByToken(data.RefreshToken);
        Console.WriteLine(data.RefreshToken);
        Console.WriteLine(session);

        if (session == null)
        {
            return new ResponseEntity<SessionResponseDTO>(HttpStatusCode.Unauthorized, "Unauthorized Access Attempt");
        }

        if (DateTime.Compare(session.ExpDate, DateTime.Now) < 0)
        {
            return new ResponseEntity<SessionResponseDTO>(HttpStatusCode.Unauthorized, "Session Expired. Please log in again");
        }

        // TODO: Blacklist the existing AccessToken
        _sessionRepository.Delete(session);

        await _sessionRepository.SaveChangesAsync();

        return new ResponseEntity<SessionResponseDTO>(HttpStatusCode.OK, "User Logged Out");
    }


    // Helper
    public string GenerateJWT(int id)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"] ?? throw new KeyNotFoundException("JWT Secret Key Not Found")));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        var token = new JwtSecurityToken(
            issuer: _configuration["Authentication:Issuer"] ?? throw new KeyNotFoundException("JWT Issuer Not Found"),
            audience: _configuration["Authentication:Audience"] ?? throw new KeyNotFoundException("JWT Audience Not Found"),
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        string token = Guid.NewGuid().ToString();
        return token;
    }
}
