using AutoMapper;
using Microsoft.EntityFrameworkCore;
using projectverseAPI.Data;
using projectverseAPI.DTOs.Post;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Mapping
{
    public class PostMappingProfile : AutoMapper.Profile
    {

        public PostMappingProfile()
        {
            CreateMap<Post, PostResponseDTO>()
                .AfterMap<SetIsLikedByCurrentUserAction>();
            CreateMap<Project, PostProjectDTO>();
            CreateMap<PostComment, PostCommentDTO>();
            CreateMap<User, PostCommentAuthorDTO>();

            CreateMap<CreatePostCommentRequestDTO, PostComment>()
                .ForMember(
                    x => x.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(
                    x => x.PostedAt,
                    opt => opt.MapFrom(src => DateTime.Now));
        }
    }

    public class SetIsLikedByCurrentUserAction : IMappingAction<Post, PostResponseDTO>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ProjectVerseContext _context;

        public SetIsLikedByCurrentUserAction(IAuthenticationService authenticationService, ProjectVerseContext context)
        {
            _authenticationService = authenticationService;
            _context = context;
        }

        public async void Process(Post source, PostResponseDTO destination, ResolutionContext context)
        {
            var currentUser = await _authenticationService.GetCurrentUser();

            destination.IsLikedByCurrentUser = _context.Likes
                .Include(l => l.User)
                .Include(l => l.Post)
                .Any(l => l.User.Id == currentUser.Id && l.Post.Id == destination.Id);

        }
    }
}
