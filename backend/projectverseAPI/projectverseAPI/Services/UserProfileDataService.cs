using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using projectverseAPI.Data;
using projectverseAPI.DTOs.UserProfileData;
using projectverseAPI.Interfaces;
using projectverseAPI.Interfaces.Marker;
using projectverseAPI.Models;
using System.Data;
using System.Reflection;

namespace projectverseAPI.Services
{
    public class UserProfileDataService : IUserProfileDataService
    {
        private readonly ProjectVerseContext _context;
        private readonly IMapper _mapper;

        public UserProfileDataService(
            ProjectVerseContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserProfileData> Create(User user)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var newProfileData = new UserProfileData
                {
                    Id = Guid.NewGuid(),
                    User = user,
                    UserId = Guid.Parse(user.Id)
                };

                var createdEntity = await _context.UserProfileData.AddAsync(newProfileData);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return createdEntity.Entity;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<UserProfileData> GetById(Guid userId)
        {
            var profileData = await _context.UserProfileData
                .AsNoTracking()
                .Where(p => p.UserId == userId)
                .Include(p => p.User)
                .Include(p => p.Certificates)
                .Include(p => p.Educations)
                .Include(p => p.Socials)
                .Include(p => p.KnownTechnologies)
                .Include(p => p.Interests)
                .AsSplitQuery()
                .FirstOrDefaultAsync();

            if (profileData is null)
                throw new ArgumentException("Profile doesn't exist.");

            return profileData;
        }

        public async Task<UserProfileData> Update(UpdateUserProfileDataRequestDTO dto)
        {
            using var transaction = _context.Database.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                var profileData = await _context.UserProfileData
                    .AsSplitQuery()
                    .Where(p => p.Id == dto.Id)
                    .Include(p => p.Interests)
                    .Include(p => p.Certificates)
                    .Include(p => p.Educations)
                    .Include(p => p.Socials)
                    .Include(p => p.KnownTechnologies)
                    .FirstOrDefaultAsync();

                if (profileData is null)
                    throw new ArgumentException("Profile doesn't exist.");

                profileData.AboutMe = dto.AboutMe;
                profileData.Achievements = dto.Achievements;
                profileData.PrimaryTechnology = dto.PrimaryTechnology;

                await Task.WhenAll(
                    ProcessUserProfileDataItems(dto.Interests, profileData.Interests!),
                    ProcessUserProfileDataItems(dto.Certificates, profileData.Certificates!),
                    ProcessUserProfileDataItems(dto.Educations, profileData.Educations!),
                    ProcessUserProfileDataItems(dto.KnownTechnologies, profileData.KnownTechnologies!),
                    ProcessUserProfileDataItems(dto.Socials, profileData.Socials!));

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return profileData;
            }
            catch (ArgumentException)
            {
                await transaction.RollbackAsync();
                throw;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

        }

        private Task ProcessUserProfileDataItems<TDto, TEntity>(
            IEnumerable<TDto> dtos, //ex. dto.Interests
            ICollection<TEntity> entities) //ex. dbProfileData.Interests
        where TDto : IIdentifiable
        where TEntity : class, IIdentifiable
        {
            foreach (var dto in dtos)
            {
               TEntity entity;

                if (dto.Id is null)
                {
                    dto.Id = Guid.NewGuid();
                    entity = _mapper.Map<TEntity>(dto);
                    entities.Add(entity);
                    _context.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    entity = entities.FirstOrDefault(e => e.Id == dto.Id);

                    if (entity != null)
                    {
                        UpdateEntityFromDto(dto, entity);
                        _context.Entry(entity).State = EntityState.Modified;
                    }
                }
            }
            return Task.CompletedTask;
        }

        private static void UpdateEntityFromDto<TDto, TEntity>(TDto dto, TEntity entity)
        {
            var dtoType = typeof(TDto);
            var entityType = typeof(TEntity);

            foreach (var item in dtoType.GetProperties())
            {
                if (!item.Name.Equals("Id"))
                {
                    PropertyInfo? propertyToAssignTo = entityType.GetProperty(item.Name);
                    object? updatedValue = item.GetValue(dto);
                    if (updatedValue is not null && propertyToAssignTo is not null)
                        propertyToAssignTo.SetValue(entity, updatedValue);
                }
            }
        }
    }
}
