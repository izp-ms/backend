using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class UserDetailService : IUserDetailService
{
    private readonly IUserDetailRepository _userDetailRepository;
    private readonly IMapper _mapper;

    public UserDetailService(IUserDetailRepository userDetailRepository, IMapper mapper)
    {
        _userDetailRepository = userDetailRepository;
        _mapper = mapper;
    }

    public async Task<UserDetailDto> GetUserDetailById(int userId)
    {
        UserDetail userDetail = await _userDetailRepository.Get(userId) ?? throw new Exception(userId.ToString());
        return _mapper.Map<UserDetailDto>(userDetail);
    }

    public async Task<UserDetailDto> UpdateUserDetail(UserDetailDto userDetailDto)
    {
        UserDetail mappedUserDetail = _mapper.Map<UserDetail>(userDetailDto);
        if (mappedUserDetail == null)
        {
            throw new Exception($"UserDetail with id {userDetailDto.Id} not found");
        }
        UserDetail userDetail = await _userDetailRepository.Update(mappedUserDetail) ?? throw new Exception("UserDetail not updated");
        return _mapper.Map<UserDetailDto>(userDetail);
    }
}
