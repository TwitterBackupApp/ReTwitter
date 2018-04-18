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
            CreateMap<FolloweeDto, Followee>().ReverseMap();
            CreateMap<UserMentionDto, Followee>().ReverseMap();

            CreateMap<UserFollowee, Followee>()
                .ForMember(
                    ivm => ivm.FolloweeId,
                    cfg => cfg.MapFrom(
                        issue => issue.FolloweeId))
                .ForMember(ivm => ivm.CreatedOn,
                    cfg => cfg.MapFrom(
                        issue => issue.CreatedOn))
                .ForMember(ivm => ivm.DeletedOn,
                    cfg => cfg.MapFrom(
                        issue => issue.DeletedOn))
                .ForMember(ivm => ivm.ModifiedOn,
                    cfg => cfg.MapFrom(
                        issue => issue.ModifiedOn))
                .ForMember(ivm => ivm.IsDeleted,
                    cfg => cfg.MapFrom(
                        issue => issue.IsDeleted));

            CreateMap<TweetDto, Tweet>()
                .ForMember(
                ivm => ivm.Followee,
                cfg => cfg.MapFrom(
                    issue => issue.Followee))
                .ForMember(ivm => ivm.UsersMentioned,
                    cfg => cfg.MapFrom(
                        imp => imp.Entities.UserMentions.Length));

            CreateMap<HashtagDto, Tag>()
                .ForMember(
                    ivm => ivm.Text,
                    cfg => cfg.MapFrom(
                        issue => issue.Hashtag)
                );

        }
    }
}
