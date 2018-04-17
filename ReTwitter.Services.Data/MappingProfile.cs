using AutoMapper;
using ReTwitter.Data.Models;
using ReTwitter.DTO.TwitterDto;
using FolloweeDto = ReTwitter.DTO.TwitterDto.FolloweeDto;
using TweetDto = ReTwitter.DTO.TwitterDto.TweetDto;

namespace ReTwitter.Services.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FolloweeDto, Followee>();
            CreateMap<UserMentionDto, Followee>();

            CreateMap<TweetDto, Tweet>()
                .ForMember(
                ivm => ivm.Followee,
                cfg => cfg.MapFrom(
                    issue => issue.Followee)
             );

            CreateMap<HashtagDto, Tag>()
                .ForMember(
                    ivm => ivm.Text,
                    cfg => cfg.MapFrom(
                        issue => issue.Hashtag)
                );

        }
    }
}
