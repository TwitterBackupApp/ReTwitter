using AutoMapper;
using ReTwitter.Data.Models;
using ReTwitter.DTO;
using ReTwitter.DTO.TwitterDto;

namespace ReTwitter.Services.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FolloweeFromApiDto, Followee>();
            CreateMap<UserMentionDto, Followee>();

            CreateMap<Followee, FolloweeDisplayListDto>()
                .ForMember(ivm => ivm.Description, cfg => cfg.MapFrom(
                    imp => imp.Description))
                .ForMember(ivm => ivm.FolloweeOriginallyCreatedOn, cfg => cfg.MapFrom(
                    imp => imp.FolloweeOriginallyCreatedOn))
                .ForMember(ivm => ivm.FolloweeId, cfg => cfg.MapFrom(
                    imp => imp.FolloweeId))
                .ForMember(ivm => ivm.Name, cfg => cfg.MapFrom(
                    imp => imp.Name))
                .ForMember(ivm => ivm.ScreenName, cfg => cfg.MapFrom(
                    imp => imp.ScreenName));
                

            CreateMap<TweetFromApiDto, Tweet>()
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

            CreateMap<Tweet, TweetDto>()
                .ForMember(
                    ivm => ivm.Text,
                    cfg => cfg.MapFrom(
                        issue => issue.Text)
                ).ForMember(
                    ivm => ivm.OriginalTweetCreatedOn,
                    cfg => cfg.MapFrom(
                        issue => issue.OriginalTweetCreatedOn)
                ).ForMember(
                    ivm => ivm.UsersMentioned,
                    cfg => cfg.MapFrom(
                        issue => issue.UsersMentioned.ToString())
                );

        }
    }
}
