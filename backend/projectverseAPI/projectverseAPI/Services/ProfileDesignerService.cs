using AutoMapper;
using Microsoft.EntityFrameworkCore;
using projectverseAPI.Data;
using projectverseAPI.DTOs.Designer;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Services
{
    public class ProfileDesignerService : IProfileDesignerService
    {
        private readonly ProjectVerseContext _context;
        private readonly IMapper _mapper;

        public ProfileDesignerService(
            ProjectVerseContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProfileDesigner> Create(Guid userId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var newDesignerId = Guid.NewGuid();
                var newDesigner = new ProfileDesigner()
                {
                    Id = newDesignerId,
                    UserId = userId,
                    Theme = "Default",
                    Components = new List<ProfileComponent>
                    {
                        new ProfileComponent()
                        {
                            Id = Guid.NewGuid(),
                            ProfileDesignerId = newDesignerId,
                            Type = "Header",
                            Category = "Header",
                            ColumnStart = 1,
                            ColumnEnd = 13,
                            RowStart = 1,
                            RowEnd = 4,
                            Data = null
                        }
                    }
                };

                var createdEntity = await _context.ProfileDesigners.AddAsync(newDesigner);
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

        public async Task<ProfileDesigner> GetById(Guid userId)
        {
            var designer = await _context.ProfileDesigners
                .Where(d => d.UserId == userId)
                .Include(d => d.Components)
                .FirstOrDefaultAsync();

            if (designer is null)
                throw new ArgumentException("Profile designer doesn't exist.");

            return designer;
        }

        public async Task<ProfileDesigner> Update(UpdateProfileDesignerRequestDTO dto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var designer = await _context.ProfileDesigners
                    .Where(d => d.Id == dto.Id)
                    .Include(d => d.Components)
                    .FirstOrDefaultAsync();

                if (designer is null)
                    throw new ArgumentException("Profile designer doesn't exist.");

                designer.Theme = dto.Theme;

                await ProcessComponentsUpsertOrDelete(
                    designer.Id,
                    dto.Components,
                    designer.Components);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return designer;
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

        private Task ProcessComponentsUpsertOrDelete(
            Guid designerId,
            List<UpsertProfileComponentDTO> dtos,
            List<ProfileComponent> entities)
        {
            foreach (var dto in dtos)
            {
                ProfileComponent? component;
                //Create
                if(dto.isNew is not null && (bool)dto.isNew)
                {
                    component = _mapper.Map<ProfileComponent>(dto);
                    component.ProfileDesignerId = designerId;
                    entities.Add(component);
                    _context.Entry(component).State = EntityState.Added;
                }
                else
                {
                    component = entities.FirstOrDefault(d => d.Id == dto.Id);

                    if (component is not null)
                    {
                        //Update
                        component.Data = dto.Data;
                        component.Category = dto.ComponentType.Category;
                        component.Type = dto.ComponentType.Type;
                        component.ColumnStart = dto.ColStart;
                        component.ColumnEnd = dto.ColEnd;
                        component.RowStart = dto.RowStart;
                        component.RowEnd = dto.RowEnd;
                        _context.Entry(component).State = EntityState.Modified;
                    }
                }
            }

            entities
                .ExceptBy(
                    dtos.Select(x => x.Id),
                    x => x.Id)
                .ToList()
                .ForEach(i => _context.Entry(i).State = EntityState.Deleted);


            return Task.CompletedTask;
        }
    }
}
