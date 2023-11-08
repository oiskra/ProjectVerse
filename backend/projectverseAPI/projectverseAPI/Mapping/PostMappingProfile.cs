using projectverseAPI.DTOs.Post;
using projectverseAPI.Models;

namespace projectverseAPI.Mapping
{
    public class PostMappingProfile : AutoMapper.Profile
    {

        public PostMappingProfile()
        {
            CreateMap<Post, PostResponseDTO>();
            CreateMap<Project, PostProjectDTO>();
            CreateMap<PostComment, PostCommentDTO>();
            CreateMap<User, PostCommentAuthorDTO>();
                
        }
    }
}
