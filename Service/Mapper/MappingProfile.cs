using AutoMapper;
using Entities.Models;
using Shared.Dto.Balance;
using Shared.Dto.Bet;
using Shared.Dto.User;
using Shared.Extensions;

namespace Service.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Balance, BalanceDto>();
        CreateMap<User, UserDto>();
        
        CreateMap<AddUserDto, User>()
            .ForMember(up => up.PasswordHash, m => m
                .MapFrom(u => u.Password.ComputeSha256Hash()));

        CreateMap<Bet, BetDto>().ReverseMap()
            .ForMember(bet => bet.BetAmount, dto => dto
                .MapFrom(u => u.Amount));

        CreateMap<Bet, BetDtoResponse>();
    }
}