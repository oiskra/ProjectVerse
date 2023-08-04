﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using projectverseAPI.DTOs.Collaboration;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Mapping
{
    public class CollaborationMappingProfile : AutoMapper.Profile
    {
        public CollaborationMappingProfile()
        {
            //CreateCollab
            CreateMap<CreateCollaborationRequestDTO, Collaboration>()
                .ForMember(
                    x => x.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(
                    x => x.Technologies,
                    opt => opt.MapFrom(src => 
                        src.Technologies.Select(tech => new Technology { Id = Guid.NewGuid(), Name = tech })));


            CreateMap<CreateCollaborationPositionDTO, CollaborationPosition>()
                .ForMember(
                    x => x.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid()));

            

            //Update
            CreateMap<UpdateCollaborationRequestDTO, Collaboration>();
            CreateMap<UpdateCollaborationPositionDTO, CollaborationPosition>();

            //GetCollab
            CreateMap<Collaboration, CollaborationResponseDTO>();
            CreateMap<CollaborationPosition, CollaborationPositionDTO>();
            CreateMap<User, CollaborationAuthorDTO>();
        }
    }
}