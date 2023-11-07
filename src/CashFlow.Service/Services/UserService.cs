using CashFlow.Service.Dtos.Users;
using CashFlow.Service.Interfaces;
using CashFlow.Data.IRepositories;
using CashFlow.Service.Configurations;
using CashFlow.Domain.Entities;
using AutoMapper;
using CashFlow.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using CashFlow.Service.Extensions;

namespace CashFlow.Service.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public UserService(IMapper mapper, IRepository<User> userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<UserForResultDto> AddAsync(UserForCreationDto dto)
    {
        var users = await _userRepository.SelectAll()
            .Where(u => u.Email == dto.Email)
            .FirstOrDefaultAsync();

        if (users is not null)
            throw new CashFlowException(409, "User is already exist.");

        var mappedUser = _mapper.Map<User>(dto);
        mappedUser.CreatedAt = DateTime.UtcNow;

        var createdUser = await _userRepository.InsertAsync(mappedUser);
        return _mapper.Map<UserForResultDto>(mappedUser);
    }

    public async Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto)
    {
        var user = await _userRepository.SelectAll()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        if (user is null)
            throw new CashFlowException(404, "User not found");

        user.UpdatedAt = DateTime.UtcNow;
        var person = _mapper.Map(dto, user);

        await _userRepository.UpdateAsync(person);

        return _mapper.Map<UserForResultDto>(person);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var user = await _userRepository.SelectAll()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        if (user is null)
            throw new CashFlowException(404, "User is not found");

        await _userRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var user = _userRepository.SelectAll()
            .Include(u => u.userAssets)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<UserForResultDto>>(user);
    }

    public async Task<UserForResultDto> RetrieveByEmailAsync(string email)
    {
        var users = await _userRepository.SelectAll()
                .Where(u => u.Email == email)
                .Include(a => a.userAssets)
                .FirstOrDefaultAsync();
        if (users is null)
            throw new CashFlowException(404, "User is not found");

        return _mapper.Map<UserForResultDto>(users);
        throw new NotImplementedException();
    }

    public async Task<UserForResultDto> RetrieveByIdAsync(long id)
    {
        var users = await _userRepository.SelectAll()
                .Where(u => u.Id == id)
                .Include(a => a.userAssets)
                .FirstOrDefaultAsync();
        if (users is null)
            throw new CashFlowException(404, "User is not found");

        return _mapper.Map<UserForResultDto>(users);
    }
}
